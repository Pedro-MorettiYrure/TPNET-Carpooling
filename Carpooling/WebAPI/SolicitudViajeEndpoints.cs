using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; // Necesario
using Microsoft.AspNetCore.Authorization; // Necesario
using Domain.Model; // Necesario
using System;       // Necesario
using System.Collections.Generic; // Necesario
using System.Linq; // Necesario

namespace WebAPI
{
    public static class SolicitudViajeEndpoints
    {
        public static void MapSolicitudViajeEndpoints(this WebApplication app)
        {
            // GET: Solicitudes de un Viaje (Solo para el Conductor de ese viaje o Admin)
            app.MapGet("/solicitudes/viaje/{idViaje}", [Authorize] (int idViaje, ClaimsPrincipal user, [FromServices] SolicitudViajeService solService, [FromServices] ViajeServices viajeService) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int currentUserId)) return Results.Unauthorized();

                    var viaje = viajeService.Get(idViaje);
                    if (viaje == null) return Results.NotFound(new { error = "Viaje no encontrado." });

                    if (currentUserId != viaje.IdConductor && !user.IsInRole(TipoUsuario.Administrador.ToString()))
                    {
                        return Results.Forbid(); // No sos el conductor ni admin
                    }

                    var solicitudes = solService.GetSolicitudesByViaje(idViaje);
                    return Results.Ok(solicitudes);
                }
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); }
            })
            .WithName("GetSolicitudesPorViaje")
            .Produces<IEnumerable<SolicitudViajeDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

            // GET: Solicitudes de un Pasajero (Solo para ESE pasajero o Admin)
            app.MapGet("/solicitudes/pasajero/{idPasajero}", [Authorize] (int idPasajero, ClaimsPrincipal user, [FromServices] SolicitudViajeService service) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int currentUserId)) return Results.Unauthorized();

                    if (currentUserId != idPasajero && !user.IsInRole(TipoUsuario.Administrador.ToString()))
                    {
                        return Results.Forbid(); // No sos el pasajero en cuestión ni admin
                    }

                    var solicitudes = service.GetSolicitudesByPasajero(idPasajero);
                    return Results.Ok(solicitudes);
                }
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); }
            })
            .WithName("GetSolicitudesPorPasajero")
            .Produces<IEnumerable<SolicitudViajeDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

            // GET: Solicitud por ID (Solo para el Pasajero o Conductor involucrados, o Admin)
            app.MapGet("/solicitudes/{idSolicitud}", [Authorize] (int idSolicitud, ClaimsPrincipal user, [FromServices] SolicitudViajeService service) =>
            {
                try
                {
                    // Usamos GetSolicitudById, que ahora (después de nuestras correcciones) DEBERÍA incluir la info del viaje
                    var solicitud = service.GetSolicitudById(idSolicitud);
                    if (solicitud == null) return Results.NotFound();

                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int currentUserId)) return Results.Unauthorized();

                    // Validar si el usuario actual está involucrado O es admin
                    if (currentUserId != solicitud.IdPasajero && currentUserId != solicitud.IdConductor && !user.IsInRole(TipoUsuario.Administrador.ToString()))
                    {
                        return Results.Forbid();
                    }

                    return Results.Ok(solicitud);
                }
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); }
            })
            .WithName("GetSolicitudPorId")
            .Produces<SolicitudViajeDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .RequireAuthorization()
            .WithOpenApi();

            // POST: Crear Solicitud (Requiere login, política "EsPasajero")
            app.MapPost("/solicitudes", [Authorize(Policy = "EsPasajero")] ([FromBody] SolicitudViajeDTO dto, ClaimsPrincipal user, [FromServices] SolicitudViajeService service) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int idPasajeroAutenticado)) return Results.Unauthorized();

                    dto.IdPasajero = idPasajeroAutenticado; // Forzar el ID del pasajero al del token

                    var createdDto = service.CrearSolicitud(dto.IdViaje, dto.IdPasajero);
                    return Results.Created($"/solicitudes/{createdDto.IdSolicitud}", createdDto);
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { error = ex.Message }); } // 400
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } // 409
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("CrearSolicitudViaje")
            .Produces<SolicitudViajeDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization("EsPasajero") // Usar Política
            .WithOpenApi();


            // PUT: Aceptar Solicitud (Requiere login del Conductor del viaje)
            app.MapPut("/solicitudes/{idSolicitud}/aceptar", [Authorize] (int idSolicitud, ClaimsPrincipal user, [FromServices] SolicitudViajeService service) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int idConductorAutenticado)) return Results.Unauthorized();

                    // El servicio valida internamente si idConductorAutenticado es el conductor
                    service.AceptarSolicitud(idSolicitud, idConductorAutenticado); // Lanza excepción si falla

                    return Results.Ok("Solicitud aceptada.");
                }
                catch (ArgumentException ex) { return Results.NotFound(new { error = ex.Message }); } // 404 (Solicitud no existe)
                catch (UnauthorizedAccessException ex) { return Results.Forbid(); } // 403 (No es el conductor)
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } // 409 (Viaje lleno, estado incorrecto)
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("AceptarSolicitud")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

            // PUT: Rechazar Solicitud (Requiere login del Conductor del viaje)
            app.MapPut("/solicitudes/{idSolicitud}/rechazar", [Authorize] (int idSolicitud, ClaimsPrincipal user, [FromServices] SolicitudViajeService service) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int idConductorAutenticado)) return Results.Unauthorized();

                    service.RechazarSolicitud(idSolicitud, idConductorAutenticado); // Lanza excepción si falla
                    return Results.Ok("Solicitud rechazada.");
                }
                catch (ArgumentException ex) { return Results.NotFound(new { error = ex.Message }); } // 404
                catch (UnauthorizedAccessException ex) { return Results.Forbid(); } // 403
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } // 409
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("RechazarSolicitud")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

            // PUT: Cancelar Solicitud (Requiere login del Pasajero que la hizo)
            app.MapPut("/solicitudes/{idSolicitud}/cancelar", [Authorize] (int idSolicitud, ClaimsPrincipal user, [FromServices] SolicitudViajeService service) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int idPasajeroAutenticado)) return Results.Unauthorized();

                    service.CancelarSolicitud(idSolicitud, idPasajeroAutenticado); // Lanza excepción si falla
                    return Results.Ok("Solicitud cancelada.");
                }
                catch (ArgumentException ex) { return Results.NotFound(new { error = ex.Message }); } // 404
                catch (UnauthorizedAccessException ex) { return Results.Forbid(); } // 403 (No es el pasajero dueño)
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } // 409
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("CancelarSolicitud")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();
        }
    }
}
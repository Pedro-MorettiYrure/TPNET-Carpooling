using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Model; // Para TipoUsuario
using System.Collections.Generic; // Para IEnumerable
using System;     // Para Exception
using System.Linq; // Para .Any()

namespace WebAPI
{
    public static class ViajesEndpoints
    {
        public static void MapViajesEndpoints(this WebApplication app)
        {
            // GET todos los viajes (público)
            app.MapGet("/viajes", (ViajeServices viajeService) =>
            {
                try
                {
                    var dtos = viajeService.GetAll();
                    return Results.Ok(dtos);
                }
                catch (Exception ex) { return Results.Problem($"Error: {ex.Message}"); }
            })
            .WithName("GetAllViajesPublic")
            .Produces<List<ViajeDTO>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithOpenApi(); // Sin RequireAuthorization


            // GET todos los viajes DE UN conductor (Solo ESE conductor o Admin)
            app.MapGet("/viajes/conductor/{idConductor}", [Authorize] (int idConductor, ClaimsPrincipal user, [FromServices] ViajeServices viajeServices) =>
            {
                var currentUserIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(currentUserIdClaim, out int currentUserId) || (currentUserId != idConductor && !user.IsInRole(TipoUsuario.Administrador.ToString())))
                {
                    return Results.Forbid();
                }

                try
                {
                    var viajes = viajeServices.GetAllByConductor(idConductor);
                    return Results.Ok(viajes);
                }
                catch (Exception ex) { return Results.Problem($"Error: {ex.Message}"); }
            })
            .WithName("GetAllViajesByConductor")
            .Produces<List<ViajeDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();


            // GET viaje por Id (público)
            app.MapGet("/viajes/{idViaje}", (int idViaje, ViajeServices viajeService) =>
            {
                try
                {
                    ViajeDTO? dto = viajeService.Get(idViaje);
                    return dto == null ? Results.NotFound() : Results.Ok(dto);
                }
                catch (Exception ex) { return Results.Problem($"Error: {ex.Message}"); }
            })
            .WithName("GetViajeByIdPublic")
            .Produces<ViajeDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithOpenApi(); // Sin RequireAuthorization

            // GET buscar viajes (público)
            app.MapGet("/viajes/buscar", async ([FromQuery] string origen, [FromQuery] string destino, [FromServices] ViajeServices viajeService) =>
            {
                if (string.IsNullOrWhiteSpace(origen) || string.IsNullOrWhiteSpace(destino))
                {
                    return Results.BadRequest(new { error = "Debe especificar origen y destino." });
                }
                try
                {
                    var viajes = viajeService.BuscarViajesDisponibles(origen, destino);
                    return Results.Ok(viajes);
                }
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); }
            })
            .WithName("BuscarViajesDisponibles")
            .Produces<IEnumerable<ViajeDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithOpenApi(); // Sin RequireAuthorization

            // POST crear viaje (Solo Conductor)
            // *** CORREGIDO: Usamos Policy en el atributo ***
            app.MapPost("/viajes/", [Authorize(Policy = "EsConductor")] ([FromBody] ViajeDTO viajeDTO, ClaimsPrincipal user, [FromServices] ViajeServices viajeService) =>
            {
                var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(idUsuarioClaim, out int idConductorAutenticado))
                {
                    return Results.Unauthorized(); // Token inválido
                }

                viajeDTO.IdConductor = idConductorAutenticado;

                try
                {
                    var created = viajeService.Add(viajeDTO);
                    return Results.Created($"/viajes/{created.IdViaje}", created);
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { error = ex.Message }); } // 400
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("AddViaje")
            .Produces<ViajeDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            // *** CORREGIDO: Usamos la POLÍTICA "EsConductor" ***
            .RequireAuthorization("EsConductor")
            .WithOpenApi();

            // PUT actualizar viaje (Solo el conductor dueño)
            app.MapPut("/viajes/{idViaje}", [Authorize] (int idViaje, [FromBody] ViajeDTO viajeDTO, ClaimsPrincipal user, [FromServices] ViajeServices viajeService) =>
            {
                var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(idUsuarioClaim, out int idConductorAutenticado))
                {
                    return Results.Unauthorized();
                }

                var viajeExistente = viajeService.Get(idViaje);
                if (viajeExistente == null) return Results.NotFound(new { error = "Viaje no encontrado." });

                if (viajeExistente.IdConductor != idConductorAutenticado) return Results.Forbid();

                viajeDTO.IdViaje = idViaje;
                viajeDTO.IdConductor = idConductorAutenticado;

                try
                {
                    var updated = viajeService.Update(viajeDTO);
                    return updated ? Results.Ok(viajeDTO) : Results.NotFound(); // NotFound si Update falló
                }
                catch (ArgumentException argEx) { return Results.BadRequest(new { error = argEx.Message }); } // 400
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } // 409
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("UpdateViaje")
            .Produces<ViajeDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

            // DELETE cancelar viaje (Solo el conductor dueño)
            app.MapDelete("/viajes/{idViaje}", [Authorize] (int idViaje, ClaimsPrincipal user, [FromServices] ViajeServices viajeService) =>
            {
                var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(idUsuarioClaim, out int idConductorAutenticado))
                {
                    return Results.Unauthorized();
                }

                var viajeExistente = viajeService.Get(idViaje);
                if (viajeExistente == null) return Results.NotFound(new { error = "Viaje no encontrado." });
                if (viajeExistente.IdConductor != idConductorAutenticado) return Results.Forbid();

                try
                {
                    var deleted = viajeService.Delete(idViaje);
                    return deleted ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } // 409
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("DeleteViaje")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

            // PUT Iniciar Viaje (Solo el conductor dueño)
            app.MapPut("/viajes/{idViaje}/iniciar", [Authorize] (int idViaje, ClaimsPrincipal user, [FromServices] ViajeServices viajeService) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int idConductorAutenticado))
                    {
                        return Results.Unauthorized();
                    }

                    viajeService.IniciarViaje(idViaje, idConductorAutenticado); // Lanza excepciones si falla
                    return Results.Ok("Viaje iniciado.");
                }
                catch (KeyNotFoundException ex) { return Results.NotFound(new { error = ex.Message }); } // 404
                catch (UnauthorizedAccessException ex) { return Results.Forbid(); } // 403
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } // 409
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("IniciarViaje")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

            // PUT Finalizar Viaje (Solo el conductor dueño)
            app.MapPut("/viajes/{idViaje}/finalizar", [Authorize] (int idViaje, ClaimsPrincipal user, [FromServices] ViajeServices viajeService) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int idConductorAutenticado))
                    {
                        return Results.Unauthorized();
                    }

                    var pasajerosParaCalificar = viajeService.FinalizarViaje(idViaje, idConductorAutenticado);
                    return Results.Ok(new { mensaje = "Viaje finalizado.", pasajeros = pasajerosParaCalificar });
                }
                catch (KeyNotFoundException ex) { return Results.NotFound(new { error = ex.Message }); } // 404
                catch (UnauthorizedAccessException ex) { return Results.Forbid(); } // 403
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } // 409
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("FinalizarViaje")
            .Produces<object>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapGet("/viajes/{idViaje}/pasajeros-confirmados", [Authorize] (int idViaje, ClaimsPrincipal user, [FromServices] ViajeServices viajeService) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int idConductorAutenticado))
                    {
                        return Results.Unauthorized();
                    }

                    // El servicio valida internamente si es el conductor correcto y el estado del viaje
                    var pasajeros = viajeService.GetPasajerosConfirmados(idViaje, idConductorAutenticado);
                    return Results.Ok(pasajeros);
                }
                catch (KeyNotFoundException ex) { return Results.NotFound(new { error = ex.Message }); } // 404
                catch (UnauthorizedAccessException ex) { return Results.Forbid(); } // 403
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } // 409 (ej. viaje no iniciado/realizado)
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("GetPasajerosConfirmadosViaje")
            .Produces<IEnumerable<UsuarioDTO>>(StatusCodes.Status200OK) //
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization() // Requiere login (conductor o admin podrían acceder si ajustas la lógica del servicio)
            .WithOpenApi();


        }
    }
}
using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Domain.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System; 
using System.Collections.Generic; 

namespace WebAPI
{
    public static class CalificacionesEndpoints
    {
        // DTO de entrada para calificar
        public record CalificacionInputDTO(int Puntaje, string? Comentario);

        public static void MapCalificacionesEndpoints(this WebApplication app)
        {
            // --- Endpoint para Conductor calificar a Pasajero ---
            app.MapPost("/viajes/{idViaje}/calificar-pasajero/{idPasajeroCalificado}", [Authorize]
            (int idViaje, int idPasajeroCalificado, [FromBody] CalificacionInputDTO input, ClaimsPrincipal user, [FromServices] CalificacionService service) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int idConductorAutenticado))
                    {
                        return Results.Unauthorized();
                    }

                    // El servicio validará si idConductorAutenticado es el conductor del viaje
                    var calificacionDto = service.CalificarPasajero(idViaje, idConductorAutenticado, idPasajeroCalificado, input.Puntaje, input.Comentario);
                    return Results.Created($"/calificaciones/{calificacionDto.IdCalificacion}", calificacionDto);
                }
                catch (KeyNotFoundException ex) { return Results.NotFound(new { error = ex.Message }); }
                catch (UnauthorizedAccessException) { return Results.Forbid(); }
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } 
                catch (ArgumentOutOfRangeException ex) { return Results.BadRequest(new { error = ex.Message }); } 
                catch (ArgumentException ex) { return Results.BadRequest(new { error = ex.Message }); } 
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } 
            })
            .WithName("CalificarPasajero")
            .Produces<CalificacionDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

            // --- Endpoint para Pasajero calificar a Conductor ---
            app.MapPost("/viajes/{idViaje}/calificar-conductor", [Authorize]
            (int idViaje, [FromBody] CalificacionInputDTO input, ClaimsPrincipal user, [FromServices] CalificacionService service) =>
            {
                try
                {
                    var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!int.TryParse(idUsuarioClaim, out int idPasajeroAutenticado))
                    {
                        return Results.Unauthorized();
                    }

                    // El servicio validará si idPasajeroAutenticado participó en el viaje
                    var calificacionDto = service.CalificarConductor(idViaje, idPasajeroAutenticado, input.Puntaje, input.Comentario);
                    return Results.Created($"/calificaciones/{calificacionDto.IdCalificacion}", calificacionDto);
                }
                catch (KeyNotFoundException ex) { return Results.NotFound(new { error = ex.Message }); }
                catch (UnauthorizedAccessException) { return Results.Forbid(); }
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); }
                catch (ArgumentOutOfRangeException ex) { return Results.BadRequest(new { error = ex.Message }); }
                catch (ArgumentException ex) { return Results.BadRequest(new { error = ex.Message }); }
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } 
            })
            .WithName("CalificarConductor")
             .Produces<CalificacionDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();


            // --- Endpoint para OBTENER calificaciones RECIBIDAS ---
            app.MapGet("/usuarios/{idUsuario}/calificaciones-recibidas", [Authorize]
            (int idUsuario, [FromQuery] string? rol, ClaimsPrincipal user, [FromServices] CalificacionService service) =>
            {
                var currentUserIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(currentUserIdClaim, out int currentUserId) || (currentUserId != idUsuario && !user.IsInRole(TipoUsuario.Administrador.ToString())))
                {
                    return Results.Forbid(); // No es el usuario solicitado ni admin
                }

                try
                {
                    IEnumerable<CalificacionDTO> calificaciones;
                    if (string.Equals(rol, "conductor", StringComparison.OrdinalIgnoreCase))
                    {
                        calificaciones = service.GetCalificacionesRecibidasComoConductor(idUsuario);
                    }
                    else if (string.Equals(rol, "pasajero", StringComparison.OrdinalIgnoreCase))
                    {
                        calificaciones = service.GetCalificacionesRecibidasComoPasajero(idUsuario);
                    }
                    else
                    {
                        calificaciones = service.GetCalificacionesRecibidasComoConductor(idUsuario)
                                                .Concat(service.GetCalificacionesRecibidasComoPasajero(idUsuario));
                    }
                    return Results.Ok(calificaciones);
                }
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("GetCalificacionesRecibidas")
            .Produces<IEnumerable<CalificacionDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

            app.MapGet("/usuarios/{idUsuario}/calificaciones-dadas", [Authorize]
            (int idUsuario, ClaimsPrincipal user, [FromServices] CalificacionService service) =>
            {
                var currentUserIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(currentUserIdClaim, out int currentUserId) || (currentUserId != idUsuario && !user.IsInRole(TipoUsuario.Administrador.ToString())))
                {
                    return Results.Forbid();
                }

                try
                {
                    var calificaciones = service.GetCalificacionesDadas(idUsuario);
                    return Results.Ok(calificaciones);
                }
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } 
            })
            .WithName("GetCalificacionesDadas") 
            .Produces<IEnumerable<CalificacionDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();

        }
    }
}
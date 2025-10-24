using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI
{
    public static class SolicitudViajeEndpoints
    {
        
        public static void MapSolicitudViajeEndpoints(this WebApplication app)
        {
            
            app.MapPost("/solicitudes", ([FromBody] SolicitudViajeDTO dto, [FromServices] SolicitudViajeService service) =>
            {
                try
                {
                    var createdDto = service.CrearSolicitud(dto.IdViaje, dto.IdPasajero);
                    return Results.Created($"/solicitudes/{createdDto.IdSolicitud}", createdDto);
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { error = ex.Message }); }
                catch (InvalidOperationException ex) { return Results.BadRequest(new { error = ex.Message }); }
                catch (Exception) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
            })
            .WithName("CrearSolicitudViaje")
            .Produces<SolicitudViajeDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(); 


            app.MapGet("/solicitudes/viaje/{idViaje}", (int idViaje, [FromServices] SolicitudViajeService service) =>
            {
                try
                {
                    var solicitudes = service.GetSolicitudesByViaje(idViaje);
                    return Results.Ok(solicitudes);
                }
                catch (Exception) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
            })
            .WithName("GetSolicitudesPorViaje")
            .Produces<IEnumerable<SolicitudViajeDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

           
            app.MapGet("/solicitudes/pasajero/{idPasajero}", (int idPasajero, [FromServices] SolicitudViajeService service) =>
            {
                try
                {
                    var solicitudes = service.GetSolicitudesByPasajero(idPasajero);
                    return Results.Ok(solicitudes);
                }
                catch (Exception) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
            })
            .WithName("GetSolicitudesPorPasajero")
            .Produces<IEnumerable<SolicitudViajeDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            
            app.MapGet("/solicitudes/{idSolicitud}", (int idSolicitud, [FromServices] SolicitudViajeService service) =>
            {
                try
                {
                    var solicitud = service.GetSolicitudById(idSolicitud);
                    return solicitud == null ? Results.NotFound() : Results.Ok(solicitud);
                }
                catch (Exception) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
            })
            .WithName("GetSolicitudPorId")
            .Produces<SolicitudViajeDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

           
            app.MapPut("/solicitudes/{idSolicitud}/aceptar", (int idSolicitud, [FromServices] SolicitudViajeService service /* TODO: Inject Auth */) =>
            {
                try
                {
                    int idConductorAutenticado = 1; // esto es temporal hasta q tengamos el token
                    bool success = service.AceptarSolicitud(idSolicitud, idConductorAutenticado);
                    return success ? Results.Ok() : Results.BadRequest("No se pudo aceptar la solicitud.");
                }
                catch (ArgumentException ex) { return Results.NotFound(new { error = ex.Message }); }
                catch (InvalidOperationException ex) { return Results.BadRequest(new { error = ex.Message }); }
                catch (Exception) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
            })
            .WithName("AceptarSolicitud")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

          
            app.MapPut("/solicitudes/{idSolicitud}/rechazar", (int idSolicitud, [FromServices] SolicitudViajeService service /* TODO: Inject Auth */) =>
            {
                try
                {
                    int idConductorAutenticado = 1; // Temporal hasta q tengamos el token
                    bool success = service.RechazarSolicitud(idSolicitud, idConductorAutenticado);
                    return success ? Results.Ok() : Results.BadRequest("No se pudo rechazar la solicitud.");
                }
                catch (ArgumentException ex) { return Results.NotFound(new { error = ex.Message }); }
                catch (InvalidOperationException ex) { return Results.BadRequest(new { error = ex.Message }); }
                catch (Exception) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
            })
            .WithName("RechazarSolicitud")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

          
            app.MapPut("/solicitudes/{idSolicitud}/cancelar", (int idSolicitud, [FromServices] SolicitudViajeService service /* TODO: Inject Auth */) =>
            {
                try
                {
                    int idPasajeroAutenticado = 1; // Temporal por falta d token
                    bool success = service.CancelarSolicitud(idSolicitud, idPasajeroAutenticado);
                    return success ? Results.Ok() : Results.BadRequest("No se pudo cancelar la solicitud.");
                }
                catch (ArgumentException ex) { return Results.NotFound(new { error = ex.Message }); }
                catch (InvalidOperationException ex) { return Results.BadRequest(new { error = ex.Message }); }
                catch (Exception) { return Results.StatusCode(StatusCodes.Status500InternalServerError); }
            })
            .WithName("CancelarSolicitud")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
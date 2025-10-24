using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI
{
    public static class ViajesEndpoints
    {
        public static void MapViajesEndpoints(this WebApplication app)
        {
            //GET: todos los viajes
            app.MapGet("/viajes", (ViajeServices viajeService) =>
            {
                var dtos = viajeService.GetAll();
                return Results.Ok(dtos);
            })
            .WithName("GetAllViajes")
            .Produces<List<ViajeDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // GET: todos los viajes de un usuario
            app.MapGet("/viajes/conductor/{idConductor}", ([FromRoute] int idConductor, [FromServices] ViajeServices viajeServices) =>
            {

                var viajes = viajeServices.GetAllByConductor(idConductor);
                return Results.Ok(viajes);
            });

            //GET: viaje por Id
            app.MapGet("/viajes/{idViaje}", (int idViaje, ViajeServices viajeService) =>
            {
                ViajeDTO dto = viajeService.Get(idViaje);
                return dto == null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetViaje")
            .Produces<ViajeDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // POST: agregar un viaje
            app.MapPost("/viajes/", ([FromBody] ViajeDTO viajeDTO, [FromServices] ViajeServices viajeService) =>
            {
                try
                {
                    var created = viajeService.Add(viajeDTO);
                    // Devuelve 201 Created si el servicio fue exitoso
                    return Results.Created($"/viajes/{created.IdViaje}", created);
                }
                catch (ArgumentException ex)
                {
                    // Capturamos la excepción de validación y devolvemos 400 Bad Request
                    // El mensaje de 'ex.Message' es el que dice "La licencia del conductor esta vencida..."
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    // Manejo de errores inesperados (p.ej., problemas de la base de datos)
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("AddViaje") // Opcional, pero bueno para Swagger
            .Produces<ViajeDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(); // Opcional

            // PUT: actualizar un vehículo
            app.MapPut("/viajes/{idViaje}", ([FromRoute] int idViaje, [FromBody] ViajeDTO viajeDTO, [FromServices] ViajeServices viajeService) =>
            {
                viajeDTO.IdViaje = idViaje;

                try
                {
                    var updated = viajeService.Update(viajeDTO);
                    if (!updated) return Results.NotFound();
                }
                catch (ArgumentException argEx)
                {
                    return Results.BadRequest(new { error = argEx.Message });
                }
                catch (Exception ex)
                {
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
                }
                return Results.Ok(viajeDTO);
            });

            // DELETE: eliminar un vehículo
            app.MapDelete("/viajes/{idViaje}", ([FromRoute] int idViaje, [FromServices] ViajeServices viajeService) =>
            {
                var deleted = viajeService.Delete(idViaje);
                if (!deleted) return Results.NotFound();
                return Results.NoContent();
            });

        }
    }
}

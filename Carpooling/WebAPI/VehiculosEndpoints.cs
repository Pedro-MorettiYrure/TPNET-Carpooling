using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI
{
    public static class VehiculosEndpoints
    {
        public static void MapVehiculosEndpoints(this WebApplication app)
        {

            // GET: todos los vehículos de un usuario
            app.MapGet("/vehiculos/{idUsuario}", ([FromRoute] int idUsuario, [FromServices] VehiculoService vehiculoService) =>
            {
                var vehiculos = vehiculoService.GetByUsuario(idUsuario);
                return Results.Ok(vehiculos);
            });

            // GET: un vehículo por patente y usuario
            app.MapGet("/vehiculos/{patente}/{idUsuario}", ([FromRoute] string patente, [FromRoute] int idUsuario, [FromServices] VehiculoService vehiculoService) =>
            {
                var vehiculo = vehiculoService.GetByUsuario(idUsuario)
                                 .FirstOrDefault(v => v.Patente == patente);

                if (vehiculo == null) return Results.NotFound();
                return Results.Ok(vehiculo);
            });

            // POST: agregar un vehículo
            app.MapPost("/vehiculos/{idUsuario}", ([FromRoute] int idUsuario, [FromBody] VehiculoDTO vehiculoDto, [FromServices] VehiculoService vehiculoService) =>
            {
                vehiculoDto.IdUsuario = idUsuario; // aseguramos que el dto tenga el idUsuario correcto
                var created = vehiculoService.Add(vehiculoDto);
                return Results.Created($"/vehiculos/{created.Patente}/{idUsuario}", created);
            });

            // PUT: actualizar un vehículo
            app.MapPut("/vehiculos/{patente}/{idUsuario}", ([FromRoute] string patente, [FromRoute] int idUsuario, [FromBody] VehiculoDTO vehiculoDto, [FromServices] VehiculoService vehiculoService) =>
            {
                try
                {
                    vehiculoDto.Patente = patente;
                    vehiculoDto.IdUsuario = idUsuario;

                    var updated = vehiculoService.Update(vehiculoDto);
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
                return Results.Ok(vehiculoDto);
            });

            // DELETE: eliminar un vehículo
            app.MapDelete("/vehiculos/{patente}/{idUsuario}", ([FromRoute] string patente, [FromRoute] int idUsuario, [FromServices] VehiculoService vehiculoService) =>
            {
                var deleted = vehiculoService.Delete(patente, idUsuario);
                if (!deleted) return Results.NotFound();
                return Results.NoContent();
            });


        }
    }
}

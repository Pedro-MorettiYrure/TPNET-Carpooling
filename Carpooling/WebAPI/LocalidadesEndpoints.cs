using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI
{
    public static class LocalidadesEndpoints
    {
        public static void MapLocalidadesEndpoints(this WebApplication app)
        {
            app.MapGet("/localidades/{CodPostal}", (string CodPostal, LocalidadService localidadService) =>
            {
                LocalidadDTO? dto = localidadService.Get(CodPostal);
                return dto == null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetLocalidad")
            .Produces<LocalidadDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/localidades", (LocalidadService localidadService) =>
            {
                var dtos = localidadService.GetAll();
                return Results.Ok(dtos);
            })
            .WithName("GetAllLocalidades")
            .Produces<List<LocalidadDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/localidades", (LocalidadDTO dto, LocalidadService localidadService) =>
            {
                try
                {
                    LocalidadDTO localidadDTO = localidadService.Add(dto);
                    return Results.Created($"/localidades/{localidadDTO.CodPostal}", localidadDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddLocalidad")
            .Produces<LocalidadDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            // PUT: actualizar una localidad
            app.MapPut("/localidades/{codPostal}", ([FromRoute] string codPostal, [FromBody] LocalidadDTO dto, [FromServices] LocalidadService localidadService) =>
            {
                try
                {
                    dto.CodPostal = codPostal;
                    bool updated = localidadService.Update(dto);

                    return updated ? Results.Ok(dto) : Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateLocalidad")
            .Produces<LocalidadDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();



            // DELETE: eliminar una localidad
            app.MapDelete("/localidades/{codPostal}", (string codPostal, LocalidadService localidadService) =>
            {
                try
                {
                    bool deleted = localidadService.Delete(codPostal);

                    return deleted ? Results.NoContent() : Results.NotFound();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }                
            })
            .WithName("DeleteLocalidad")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }
    }
}

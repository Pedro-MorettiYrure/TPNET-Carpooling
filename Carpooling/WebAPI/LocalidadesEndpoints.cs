using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Domain.Model;
using System;
using System.Collections.Generic; 
using Microsoft.AspNetCore.Http; 

namespace WebAPI
{
    public static class LocalidadesEndpoints
    {
        public static void MapLocalidadesEndpoints(this WebApplication app)
        {
            app.MapGet("/localidades/{CodPostal}", (string CodPostal, LocalidadService localidadService) =>
            {
                try
                {
                    LocalidadDTO? dto = localidadService.Get(CodPostal);
                    return dto == null ? Results.NotFound() : Results.Ok(dto);
                }
                catch (Exception ex) { return Results.Problem($"Error: {ex.Message}"); }
            })
            .WithName("GetLocalidad")
            .Produces<LocalidadDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithOpenApi(); 

            app.MapGet("/localidades", (LocalidadService localidadService) =>
            {
                try
                {
                    var dtos = localidadService.GetAll();
                    return Results.Ok(dtos);
                }
                catch (Exception ex) { return Results.Problem($"Error: {ex.Message}"); }
            })
            .WithName("GetAllLocalidades")
            .Produces<List<LocalidadDTO>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithOpenApi(); 

            
            app.MapPost("/localidades", [Authorize(Policy = "EsAdmin")] (LocalidadDTO dto, LocalidadService localidadService) =>
            {
                try
                {
                    LocalidadDTO localidadDTO = localidadService.Add(dto);
                    return Results.Created($"/localidades/{localidadDTO.CodPostal}", localidadDTO);
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { error = ex.Message }); } // 400
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } // 500
            })
            .WithName("AddLocalidad")
            .Produces<LocalidadDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization("EsAdmin")
            .WithOpenApi();

            
            app.MapPut("/localidades/{codPostal}", [Authorize(Policy = "EsAdmin")] ([FromRoute] string codPostal, [FromBody] LocalidadDTO dto, [FromServices] LocalidadService localidadService) =>
            {
                try
                {
                    dto.CodPostal = codPostal;
                    bool updated = localidadService.Update(dto);
                    return updated ? Results.Ok(dto) : Results.NotFound();
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { error = ex.Message }); } 
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } 
            })
            .WithName("UpdateLocalidad")
            .Produces<LocalidadDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization("EsAdmin")
            .WithOpenApi();

            
            app.MapDelete("/localidades/{codPostal}", [Authorize(Policy = "EsAdmin")] (string codPostal, LocalidadService localidadService) =>
            {
                try
                {
                    bool deleted = localidadService.Delete(codPostal);
                    return deleted ? Results.NoContent() : Results.NotFound(); 
                }
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } 
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); }
            })
            .WithName("DeleteLocalidad")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization("EsAdmin")
            .WithOpenApi();
        }
    }
}
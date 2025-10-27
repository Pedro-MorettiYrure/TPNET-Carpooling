using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Domain.Model;
using System;      
using System.Linq; 
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
namespace WebAPI
{
    public static class VehiculosEndpoints
    {
        public static void MapVehiculosEndpoints(this WebApplication app)
        {

            app.MapGet("/vehiculos/{idUsuario}", [Authorize] (int idUsuario, ClaimsPrincipal user, [FromServices] VehiculoService vehiculoService) =>
            {
                var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(idUsuarioClaim, out int currentUserId) || (currentUserId != idUsuario && !user.IsInRole(TipoUsuario.Administrador.ToString())))
                {
                    return Results.Forbid(); 
                }

                try
                {
                    var vehiculos = vehiculoService.GetByUsuario(idUsuario);
                    return Results.Ok(vehiculos);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error inesperado al obtener vehículos: {ex.Message}");
                }
            })
            .WithName("GetAllVehiculos")
            .Produces<IEnumerable<VehiculoDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(); 

            app.MapGet("/vehiculos/{patente}/{idUsuario}", [Authorize] (string patente, int idUsuario, ClaimsPrincipal user, [FromServices] VehiculoService vehiculoService) =>
            {
                var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(idUsuarioClaim, out int currentUserId) || (currentUserId != idUsuario && !user.IsInRole(TipoUsuario.Administrador.ToString())))
                {
                    return Results.Forbid(); 
                }

                try
                {
                    var vehiculo = vehiculoService.GetByUsuario(idUsuario)
                                     .FirstOrDefault(v => v.Patente.Equals(patente, StringComparison.OrdinalIgnoreCase));

                    if (vehiculo == null) return Results.NotFound();
                    return Results.Ok(vehiculo);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error inesperado al obtener vehículo: {ex.Message}"); // 500
                }
            })
            .WithName("GetVehiculo")
            .Produces<VehiculoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

            
            app.MapPost("/vehiculos/{idUsuario}", [Authorize(Policy = "EsConductor")] (int idUsuario, [FromBody] VehiculoDTO vehiculoDto, ClaimsPrincipal user, [FromServices] VehiculoService vehiculoService) =>
            {
                var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(idUsuarioClaim, out int currentUserId) || currentUserId != idUsuario)
                {
                    return Results.Problem(detail: "No puedes agregar vehículos a otro usuario.", statusCode: StatusCodes.Status403Forbidden);
                }

                try
                {
                    vehiculoDto.IdUsuario = idUsuario;
                    var created = vehiculoService.Add(vehiculoDto);
                    return Results.Created($"/vehiculos/{created.Patente}/{idUsuario}", created);
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { error = ex.Message }); } 
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); } 
            })
            .WithName("AddVehiculo")
            .Produces<VehiculoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization("EsConductor")
            .WithOpenApi();

            
            app.MapPut("/vehiculos/{patente}/{idUsuario}", [Authorize(Policy = "EsConductor")] (string patente, int idUsuario, [FromBody] VehiculoDTO vehiculoDto, ClaimsPrincipal user, [FromServices] VehiculoService vehiculoService) =>
            {
                var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(idUsuarioClaim, out int currentUserId) || currentUserId != idUsuario)
                {
                    return Results.Problem(detail: "No puedes modificar vehículos de otro usuario.", statusCode: StatusCodes.Status403Forbidden);
                }

                vehiculoDto.Patente = patente;
                vehiculoDto.IdUsuario = idUsuario;

                try
                {
                    var updated = vehiculoService.Update(vehiculoDto);
                    if (!updated) return Results.NotFound("Vehículo no encontrado o no pertenece al usuario.");
                    return Results.Ok(vehiculoDto); 
                }
                catch (ArgumentException argEx) { return Results.BadRequest(new { error = argEx.Message }); } 
                catch (Exception ex) { return Results.Problem($"Error inesperado al actualizar vehículo: {ex.Message}"); }
            })
            .WithName("UpdateVehiculo")
            .Produces<VehiculoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization("EsConductor")
            .WithOpenApi();

         
            app.MapDelete("/vehiculos/{patente}/{idUsuario}", [Authorize(Policy = "EsConductor")] (string patente, int idUsuario, ClaimsPrincipal user, [FromServices] VehiculoService vehiculoService) =>
            {
                var idUsuarioClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(idUsuarioClaim, out int currentUserId) || currentUserId != idUsuario)
                {
                    return Results.Problem(detail: "No puedes eliminar vehículos de otro usuario.", statusCode: StatusCodes.Status403Forbidden);
                }

                try
                {
                    var deleted = vehiculoService.Delete(patente, idUsuario);
                    if (!deleted) return Results.NotFound("Vehículo no encontrado o no pertenece al usuario.");
                    return Results.NoContent(); // 204
                }
                catch (InvalidOperationException ex) { return Results.Conflict(new { error = ex.Message }); } // 409
                catch (Exception ex) { return Results.Problem($"Error inesperado al eliminar vehículo: {ex.Message}"); } // 500
            })
            .WithName("DeleteVehiculo")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization("EsConductor")
            .WithOpenApi();
        }
    }
}
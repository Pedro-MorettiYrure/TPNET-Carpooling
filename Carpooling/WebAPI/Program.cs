using Application.Services;
using Domain.Model;
using DTOs;
using Microsoft.AspNetCore.OpenApi; // Este using lo usas para el .WithOpenApi()
using Swashbuckle.AspNetCore; // Si bien el using está, el paquete se usa por debajo con AddSwaggerGen()
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers(); esta se usa si tuvieramos que crear controladores manualmente, pero en este caso usamos los de ASP.NET Core
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(o => { });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers(); esta se usa si tuvieramos que crear controladores manualmente, pero en este caso usamos los de ASP.NET Core por ahora 

app.MapGet("/localidades/{CodPostal}", (string CodPostal) =>
{
    LocalidadService localidadService = new LocalidadService();

    LocalidadDTO dto = localidadService.Get(CodPostal);

    if (dto == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(dto);
})
.WithName("GetLocalidad")
.Produces<LocalidadDTO>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapGet("/localidades", () =>
{
    LocalidadService localidadService = new LocalidadService();

    var dtos = localidadService.GetAll();

    return Results.Ok(dtos);

})
.WithName("GetAllLocalidades")
.Produces<List<LocalidadDTO>>(StatusCodes.Status200OK)
.WithOpenApi();

app.MapPost("/localidades", (LocalidadDTO dto) =>
{
    try
    {
        LocalidadService localidadService = new LocalidadService();

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

app.MapPut("/localidades", (LocalidadDTO dto) =>
{
    try
    {
        LocalidadService localidadService = new LocalidadService();

        var found = localidadService.Update(dto);

        if (!found)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("UpdateLocalidad")
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest)
.WithOpenApi();

app.MapDelete("/localidades/{codPostal}", (string codPostal) =>
{
    LocalidadService localidadService = new LocalidadService();

    var deleted = localidadService.Delete(codPostal);

    if (!deleted)
    {
        return Results.NotFound();
    }

    return Results.NoContent();

})
.WithName("DeleteLocalidad")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapGet("/vehiculos/{patente}", (string patente) =>
{
    VehiculoService vehiculoService = new VehiculoService();
    VehiculoDTO dto = vehiculoService.Get(patente);
    if (dto == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(dto);
})
.WithName("GetVehiculo")
.Produces<VehiculoDTO>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapGet("/vehiculos", () =>
{
    VehiculoService vehiculoService = new VehiculoService();
    var dtos = vehiculoService.GetAll();
    return Results.Ok(dtos);
})
.WithName("GetAllVehiculos")
.Produces<List<VehiculoDTO>>(StatusCodes.Status200OK)
.WithOpenApi();

app.MapPost("/vehiculos", (VehiculoDTO dto) =>
{
    try
    {
        VehiculoService vehiculoService = new VehiculoService();
        VehiculoDTO vehiculoDTO = vehiculoService.Add(dto);
        return Results.Created($"/vehiculos/{vehiculoDTO.Patente}", vehiculoDTO);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddVehiculo")
.Produces<VehiculoDTO>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithOpenApi();

app.MapPut("/vehiculos", (VehiculoDTO dto) =>
{
    try
    {
        VehiculoService vehiculoService = new VehiculoService();
        var found = vehiculoService.Update(dto);
        if (!found)
        {
            return Results.NotFound();
        }
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("UpdateVehiculo")
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest)
.WithOpenApi();

app.MapDelete("/vehiculos/{patente}", (string patente) =>
{
    VehiculoService vehiculoService = new VehiculoService();
    var deleted = vehiculoService.Delete(patente);
    if (!deleted)
    {
        return Results.NotFound();
    }
    return Results.NoContent();
})
.WithName("DeleteVehiculo")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();


app.Run();

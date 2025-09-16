using Application.Services;
using Data;
using Domain.Model;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(o => { });

// Configuración de DbContext con DI
builder.Services.AddDbContext<TPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de repositorios
builder.Services.AddScoped<LocalidadRepository>();
builder.Services.AddScoped<UsuarioRepository>();

// Inyección de servicios
builder.Services.AddScoped<LocalidadService>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

// ==================== Localidades ====================

app.MapGet("/localidades/{CodPostal}", (string CodPostal, LocalidadService localidadService) =>
{
    LocalidadDTO dto = localidadService.Get(CodPostal);
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

// Repetís lo mismo para PUT y DELETE usando LocalidadService inyectado
// ==================== Usuarios ====================

app.MapPost("/usuarios", (UsuarioDTO dto, UsuarioService usuarioService) =>
{
    try
    {
        UsuarioDTO usuarioDTO = usuarioService.Registrar(dto, dto.Contraseña);
        return Results.Created($"/usuarios/{usuarioDTO.IdUsuario}", usuarioDTO);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddUsuario")
.Produces<UsuarioDTO>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithOpenApi();

app.MapPost("/usuarios/login", (UsuarioDTO dto, UsuarioService usuarioService) =>
{
    bool ok = usuarioService.Login(dto.Email, dto.Contraseña);
    return ok ? Results.Ok(true) : Results.NotFound(false);
})
.WithName("LoginUsuario")
.Produces<bool>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapGet("/usuarios/{email}", (string email, UsuarioService usuarioService) =>
{
    UsuarioDTO? dto = usuarioService.GetByEmail(email);
    return dto == null ? Results.NotFound() : Results.Ok(dto);
})
.WithName("GetUsuarioByEmail")
.Produces<UsuarioDTO>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.Run();

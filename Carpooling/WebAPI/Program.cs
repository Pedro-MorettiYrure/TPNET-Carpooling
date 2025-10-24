using Application.Services;
using Data;
using Domain.Model;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using static DTOs.UsuarioDTO;

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
builder.Services.AddScoped<VehiculoRepository>();
builder.Services.AddScoped<ViajeRepository>(); 


// Inyección de servicios
builder.Services.AddScoped<LocalidadService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<ViajeServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

//COMANDO PARA MIGRAR LA BDD
// dotnet ef migrations add NombreMigracion 

//dotnet ef database update

//IServiceScope scope = app.Services.CreateScope();
//TPIContext context = scope.ServiceProvider.GetRequiredService<TPIContext>();
//context.Database.Migrate();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TPIContext>();
    //db.Database.Migrate();
}

app.UseHttpsRedirection();



using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TPIContext>();

    if (!context.Usuarios.Any(u => u.Email == "admin@gmail.com"))
    {
        var admin = Usuario.Crear("Admin", "Admin", "admin@gmail.com", "1234", null);
        admin.TipoUsuario = TipoUsuario.Administrador;
        context.Usuarios.Add(admin);
        context.SaveChanges();
    }
}
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
    bool deleted = localidadService.Delete(codPostal);

    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteLocalidad")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();


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

// web api.cs
app.MapPut("/usuarios/{idUsuario}/convertir-a-conductor", (int idUsuario, [FromBody] ConductorUpgradeDTO dto, UsuarioService usuarioService) =>
{
    try
    {
        bool ok = usuarioService.ConvertirAConductor(idUsuario, dto);
        if (!ok) return Results.NotFound();
    }
    catch (ArgumentException argEx)
    {
        return Results.BadRequest(new { error = argEx.Message });
    }
    catch (Exception ex)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }

    return Results.Ok();
})
.WithName("ConvertirAConductor")
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.MapPut("/usuarios/{idUsuario}", (int idUsuario, UsuarioDTO dto, UsuarioService usuarioService) =>
{
    try
    {
        bool ok = usuarioService.Actualizar(idUsuario, dto);

        if(!ok) return Results.NotFound();
    }
    catch (ArgumentException argEx)
    {
        return Results.BadRequest(new { error = argEx.Message });
    }
    catch (Exception ex)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
    return Results.Ok();
})
.WithName("ActualizarUsuario")
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();


// ==================== Vehiculos ====================

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


// ==================== Viajes ====================
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

// === Run === 
app.Run();

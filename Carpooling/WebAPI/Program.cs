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
using WebAPI;
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

app.MapLocalidadesEndpoints();


// ==================== Usuarios ====================

app.MapUsuariosEndpoints();

// ==================== Vehiculos ====================

app.MapVehiculosEndpoints();

// ==================== Viajes ====================

app.MapViajesEndpoints();

// === Run === 
app.Run();

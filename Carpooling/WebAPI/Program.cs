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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging; 
var builder = WebApplication.CreateBuilder(args);

// --- Servicios ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Carpooling WebAPI", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa 'Bearer' [espacio] y luego tu token JWT.\n\nEjemplo: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddHttpLogging(o => { });

// Configuración de DbContext con DI
builder.Services.AddDbContext<TPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de repositorios
builder.Services.AddScoped<LocalidadRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<VehiculoRepository>();
builder.Services.AddScoped<ViajeRepository>();
builder.Services.AddScoped<SolicitudViajeRepository>();
builder.Services.AddScoped<CalificacionRepository>(); 

// Inyección de servicios
builder.Services.AddScoped<LocalidadService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<ViajeServices>();
builder.Services.AddScoped<SolicitudViajeService>();
builder.Services.AddScoped<CalificacionService>(); 
builder.Services.AddScoped<ReportService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EsAdmin", policy =>
        policy.RequireRole(nameof(TipoUsuario.Administrador))); 

    options.AddPolicy("EsConductor", policy =>
        policy.RequireRole(nameof(TipoUsuario.PasajeroConductor))); 

    options.AddPolicy("EsPasajero", policy =>
        policy.RequireRole(nameof(TipoUsuario.Pasajero), nameof(TipoUsuario.PasajeroConductor))); 

    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TPIContext>();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization(); 

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TPIContext>();
    if (!context.Usuarios.Any(u => u.Email == "admin@gmail.com"))
    {
        var admin = Usuario.Crear("Admin", "Admin", "admin@gmail.com", "1234", null, TipoUsuario.Administrador);
        context.Usuarios.Add(admin);
        context.SaveChanges();
    }
}


app.MapLocalidadesEndpoints();
app.MapUsuariosEndpoints();
app.MapVehiculosEndpoints();
app.MapViajesEndpoints();
app.MapSolicitudViajeEndpoints();
app.MapCalificacionesEndpoints();
app.MapReportEndpoints();

app.Run();
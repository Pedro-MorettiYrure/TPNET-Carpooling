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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers(); esta se usa si tuvieramos que crear controladores manualmente, pero en este caso usamos los de ASP.NET Core por ahora 

app.Run();

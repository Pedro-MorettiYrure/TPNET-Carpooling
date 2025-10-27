using Carpooling.Blazor.Components;
using API.Auth.Blazor;
using API.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<ServicioSesionUsuario>(); 
var app = builder.Build();

// Configurar AuthServiceProvider para ApiClients
var authService = app.Services.GetRequiredService<ServicioSesionUsuario>();
AuthServiceProvider.Register(authService); 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

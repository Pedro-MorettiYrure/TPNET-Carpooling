using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Domain.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static DTOs.UsuarioDTO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System; 

namespace WebAPI
{
    public static class UsuariosEndpoints
    {
        public static void MapUsuariosEndpoints(this WebApplication app)
        {
            // Endpoint POST /usuarios para registrar
            app.MapPost("/usuarios", (UsuarioDTO dto, UsuarioService usuarioService) =>
            {
                try
                {
                    UsuarioDTO usuarioDTO = usuarioService.Registrar(dto, dto.Contraseña ?? string.Empty);
                    var usuarioCreadoDto = new UsuarioDTO
                    {
                        IdUsuario = usuarioDTO.IdUsuario,
                        Nombre = usuarioDTO.Nombre,
                        Apellido = usuarioDTO.Apellido,
                        Email = usuarioDTO.Email,
                        Telefono = usuarioDTO.Telefono,
                        TipoUsuario = usuarioDTO.TipoUsuario
                    };
                    return Results.Created($"/usuarios/{usuarioCreadoDto.Email}", usuarioCreadoDto);
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { error = ex.Message }); }
                catch (Exception ex) { return Results.Problem($"Ocurrió un error inesperado durante el registro: {ex.Message}"); }
            })
            .WithName("AddUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithOpenApi();


            // Endpoint Login para devolver Token
            app.MapPost("/usuarios/login", (UsuarioDTO loginDto, UsuarioService usuarioService, IConfiguration config, ILogger<Program> logger) =>
            {
                if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Contraseña))
                {
                    return Results.BadRequest(new { error = "Email y contraseña son requeridos." });
                }

                bool credencialesOk = usuarioService.Login(loginDto.Email, loginDto.Contraseña);
                if (!credencialesOk)
                {
                    return Results.Unauthorized();
                }

                UsuarioDTO? usuario = usuarioService.GetByEmail(loginDto.Email);
                if (usuario == null)
                {
                    logger.LogError("Error crítico: Usuario no encontrado después de login exitoso para {Email}", loginDto.Email);
                    return Results.Problem("Error interno: Usuario no encontrado después del login exitoso.");
                }

                try
                {
                    // Genera el Token JWT usando el helper
                    var jwtToken = GenerateJwtToken(usuario, config);
                    return Results.Ok(new { token = jwtToken });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error EXCEPCIÓN al generar el token JWT para {Email}", loginDto.Email);
                    return Results.Problem("Error al generar el token de autenticación.");
                }
            })
            .WithName("LoginUsuario")
            .Produces<object>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

            // Endpoint GET /usuarios/{email} (Sin cambios)
            app.MapGet("/usuarios/{email}", [Authorize] (string email, UsuarioService usuarioService, ClaimsPrincipal user) =>
            {
                var userEmailClaim = user.FindFirstValue(ClaimTypes.Email);
                if (!user.IsInRole(TipoUsuario.Administrador.ToString()) && userEmailClaim != email)
                {
                    return Results.Forbid();
                }

                UsuarioDTO? dto = usuarioService.GetByEmail(email);
                return dto == null ? Results.NotFound() : Results.Ok(dto);
            })
            .WithName("GetUsuarioByEmail")
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .RequireAuthorization()
            .WithOpenApi();

            //Endpoint para convertir a conductor
            app.MapPut("/usuarios/{idUsuario}/convertir-a-conductor", [Authorize(Policy = "EsPasajero")] (int idUsuario, [FromBody] ConductorUpgradeDTO dto, UsuarioService usuarioService, IConfiguration config, ILogger<Program> logger, ClaimsPrincipal user) =>
            {
                var currentUserIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(currentUserIdClaim, out int currentUserId) || (currentUserId != idUsuario && !user.IsInRole(TipoUsuario.Administrador.ToString())))
                {
                    return Results.Forbid();
                }

                // Solo un Pasajero puede convertirse
                if (!user.IsInRole(TipoUsuario.Pasajero.ToString()))
                {
                    return Results.Conflict(new { error = "Solo los usuarios con rol 'Pasajero' pueden convertirse a conductor." });
                }

                try
                {
                    bool ok = usuarioService.ConvertirAConductor(idUsuario, dto);
                    if (!ok) return Results.NotFound(new { error = "Usuario no encontrado o no es pasajero." });

                    // Si fue exitoso, obtener los datos actualizados del usuario
                    // Usamos el email del token actual para obtener el usuario actualizado
                    UsuarioDTO? usuarioActualizado = usuarioService.GetByEmail(user.FindFirstValue(ClaimTypes.Email)!);
                    if (usuarioActualizado == null)
                    {
                        return Results.Problem("Error: No se pudieron obtener los datos actualizados del usuario.");
                    }

                    // Generar un NUEVO token con el rol "PasajeroConductor"
                    var nuevoToken = GenerateJwtToken(usuarioActualizado, config);

                    // Devolver el nuevo token
                    return Results.Ok(new { token = nuevoToken, mensaje = "Usuario actualizado a Conductor." });
                }
                catch (ArgumentException argEx) { return Results.BadRequest(new { error = argEx.Message }); } 
                catch (InvalidOperationException opEx) { return Results.Conflict(new { error = opEx.Message }); } 
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error al convertir a conductor o generar token para {idUsuario}", idUsuario);
                    return Results.Problem($"Error inesperado: {ex.Message}"); 
                }
            })
             .WithName("ConvertirAConductor")
             .Produces<object>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status401Unauthorized)
             .Produces(StatusCodes.Status403Forbidden)
             .Produces(StatusCodes.Status404NotFound)
             .Produces(StatusCodes.Status409Conflict)
             .ProducesProblem(StatusCodes.Status500InternalServerError)
             .RequireAuthorization("EsPasajero") 
             .WithOpenApi();

            // Endpoint PUT /usuarios/{idUsuario} para actualizar
            app.MapPut("/usuarios/{idUsuario}", [Authorize] (int idUsuario, UsuarioDTO dto, UsuarioService usuarioService, ClaimsPrincipal user) =>
            {
                var currentUserIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(currentUserIdClaim, out int currentUserId) || (currentUserId != idUsuario && !user.IsInRole(TipoUsuario.Administrador.ToString())))
                {
                    return Results.Forbid();
                }

                dto.IdUsuario = idUsuario; 
                dto.Email = user.FindFirstValue(ClaimTypes.Email)!;

                try
                {
                    bool ok = usuarioService.Actualizar(idUsuario, dto);
                    if (!ok) return Results.NotFound(new { error = "Usuario no encontrado." });

                    
                    return Results.Ok("Usuario actualizado.");
                }
                catch (ArgumentException argEx) { return Results.BadRequest(new { error = argEx.Message }); }
                catch (Exception ex) { return Results.Problem($"Error inesperado: {ex.Message}"); }
            })
            .WithName("ActualizarUsuario")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .RequireAuthorization()
            .WithOpenApi();
        }


        // HELPER PRIVADO para generar el token 
        private static string GenerateJwtToken(UsuarioDTO usuario, IConfiguration config)
        {
            var secretKey = config["Jwt:SecretKey"];
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];

            // Validación robusta de la configuración
            if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
            {
                throw new InvalidOperationException("La configuración JWT (Jwt:SecretKey) es nula, vacía o demasiado corta (requiere min. 32 caracteres).");
            }
            if (string.IsNullOrEmpty(issuer))
            {
                throw new InvalidOperationException("La configuración JWT (Jwt:Issuer) es nula o vacía.");
            }
            if (string.IsNullOrEmpty(audience))
            {
                throw new InvalidOperationException("La configuración JWT (Jwt:Audience) es nula o vacía.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()), 
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario.ToString()), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8), // Duración
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

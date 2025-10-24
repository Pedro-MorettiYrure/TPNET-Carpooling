using Application.Services;
using DTOs;
using static DTOs.UsuarioDTO;

namespace WebAPI
{
    public static class UsuariosEndpoints
    {
        public static void MapUsuariosEndpoints(this WebApplication app)
        {
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
            .WithName("ActualizarUsuario")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();


        }
    }
}

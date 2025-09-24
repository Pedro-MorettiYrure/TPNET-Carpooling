using Data;
using Domain.Model;
using DTOs;
using static DTOs.UsuarioDTO;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repo;

        // Recibe el repositorio por constructor
        public UsuarioService(UsuarioRepository repo)
        {
            _repo = repo;
        }

        // Registrar usuario usando el método de fábrica
        public UsuarioDTO Registrar(UsuarioDTO dto, string contraseña)
        {
            var usuario = Usuario.Crear(dto.Nombre, dto.Apellido, dto.Email, contraseña);
            _repo.Add(usuario);

            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Contraseña = usuario.ContraseñaHash,
                TipoUsuario = usuario.TipoUsuario
            };
        }

        public bool Login(string email, string contraseña)
        {
            var usuario = _repo.GetByEmail(email);
            if (usuario == null) return false;

            return usuario.VerificarContraseña(contraseña);
        }

        public UsuarioDTO? GetByEmail(string email)
        {
            var usuario = _repo.GetByEmail(email);
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Contraseña = usuario.ContraseñaHash,
                TipoUsuario = usuario.TipoUsuario
            };
        }

        // Método para convertir al usuario a conductor
        public bool ConvertirAConductor(int idUsuario, ConductorUpgradeDTO dto)
        {
            var usuario = _repo.GetById(idUsuario);

            if (usuario == null)
            {
                return false;
            }
            if (usuario.TipoUsuario == TipoUsuario.Pasajero)
            {
                try
                {
                    usuario.ConvertirAConductor(dto.nroLicenciaConductor, dto.fechaVencimientoLicencia);
                    _repo.Update(usuario);
                    return true;
                }
                catch (ArgumentException)
                {
                    return false; // Retorna false si los datos del DTO no son válidos
                }
            }
            return false;
        }
    }
}

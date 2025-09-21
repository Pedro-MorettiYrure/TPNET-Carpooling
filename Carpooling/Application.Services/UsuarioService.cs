using Data;
using Domain.Model;
using DTOs;

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
        public bool ConvertirAConductor(int idUsuario)
        {
            var usuario = _repo.GetById(idUsuario);

            if (usuario != null && usuario.TipoUsuario == "Pasajero")
            {
                usuario.TipoUsuario = "Pasajero-Conductor";
                // Aquí podrías agregar lógica para guardar los datos adicionales del conductor
                _repo.Update(usuario); // Asume que tienes un método Update en el repositorio
                return true;
            }
            return false;
        }
    }
}

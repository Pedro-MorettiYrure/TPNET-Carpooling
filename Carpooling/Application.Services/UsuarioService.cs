using Data;
using Domain.Model;
using DTOs;
using static DTOs.UsuarioDTO;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repo;

        public UsuarioService(UsuarioRepository repo)
        {
            _repo = repo;
        }

        public UsuarioDTO Registrar(UsuarioDTO dto, string contraseña)
        {
            var existe = _repo.GetByEmail(dto.Email);

            if (existe == null)
            {

                var usuario = Usuario.Crear(dto.Nombre, dto.Apellido, dto.Email, contraseña, dto.Telefono);
                _repo.Add(usuario);

                return new UsuarioDTO
                {
                    IdUsuario = usuario.IdUsuario,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    Telefono = usuario.Telefono,
                    Contraseña = usuario.ContraseñaHash,
                    TipoUsuario = usuario.TipoUsuario
                };
            }
            else
            {
                return null;
            }
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
                TipoUsuario = usuario.TipoUsuario,
                Telefono = usuario.Telefono,
                nroLicenciaConductor = usuario.nroLicenciaConductor,
                fechaVencimientoLicencia = usuario.fechaVencimientoLicencia
            };
        }

        public bool Actualizar(int id, UsuarioDTO dto)
        {
            var usuario = _repo.GetById(id);
            if (usuario == null) return false;

            try
            {
                usuario.SetNombre(dto.Nombre);
                usuario.SetApellido(dto.Apellido);
                usuario.SetTelefono(dto.Telefono);

                if (usuario.TipoUsuario == TipoUsuario.PasajeroConductor)
                {
                    usuario.nroLicenciaConductor = dto.nroLicenciaConductor;
                    usuario.fechaVencimientoLicencia = dto.fechaVencimientoLicencia;
                }

                _repo.Update(usuario);
                return true;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }


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
                    throw; // Retorna false si los datos del DTO no son válidos
                }
            }
            else
            {
                throw new Exception(message: "El usuario no es Pasajero");
            }
        }
    }
}

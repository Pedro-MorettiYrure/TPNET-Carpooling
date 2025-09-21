using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }

        // El cliente manda la contraseña en texto plano.
        // El dominio (Usuario.cs) la va a hashear.
        public string? Contraseña { get; set; }
    }
}


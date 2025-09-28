using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string? Telefono { get; set; } 

        public string? nroLicenciaConductor { get; set; } // Solo para conductores

        public DateTime? fechaVencimientoLicencia { get; set; } // Solo para conductores

        // El cliente manda la contraseña en texto plano.
        // El dominio (Usuario.cs) la va a hashear.
        public string? Contraseña { get; set; }
        public class ConductorUpgradeDTO
        {
            public string nroLicenciaConductor { get; set; }
            public DateTime fechaVencimientoLicencia { get; set; }
        }
    }
}


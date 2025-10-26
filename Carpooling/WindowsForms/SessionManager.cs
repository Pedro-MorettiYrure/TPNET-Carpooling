using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms
{
    public static class SessionManager
    {
        public static UsuarioDTO UsuarioActual { get; private set; }
        // *** Propiedad para guardar el Token JWT ***
        public static string? JwtToken { get; private set; }

        public static void IniciarSesion(UsuarioDTO usuario, string token)
        {
            UsuarioActual = usuario;
            JwtToken = token; // Guardamos el token
        }

        public static void CerrarSesion()
        {
            UsuarioActual = null;
            JwtToken = null; // Borramos el token
        }

        public static bool EstaLogueado => UsuarioActual != null && !string.IsNullOrEmpty(JwtToken);
    }
}
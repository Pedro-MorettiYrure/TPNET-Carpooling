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

        public static void IniciarSesion(UsuarioDTO usuario)
        {
            UsuarioActual = usuario;
        }

        public static void CerrarSesion()
        {
            UsuarioActual = null;
        }

        public static bool EstaLogueado => UsuarioActual != null;
    }
}


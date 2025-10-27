using System;

namespace DTOs
{
    public class TopConductorDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double PromedioCalificacion { get; set; } 
        public int CantidadCalificaciones { get; set; }
    }
}
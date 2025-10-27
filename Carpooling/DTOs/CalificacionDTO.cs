using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CalificacionDTO
    {
        public int IdCalificacion { get; set; }
        public int IdViaje { get; set; }
        public int IdCalificador { get; set; }
        public string? NombreCalificador { get; set; } 
        public int IdCalificado { get; set; }
        public string? NombreCalificado { get; set; } 
        public string RolCalificado { get; set; } 
        public int Puntaje { get; set; }
        public string? Comentario { get; set; }
        public DateTime FechaHora { get; set; }
    }

}

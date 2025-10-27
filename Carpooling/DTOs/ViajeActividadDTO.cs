using System;
using System.Collections.Generic;

namespace DTOs
{
    public class ViajeActividadDTO
    {
        public int IdViaje { get; set; }
        public DateTime FechaHora { get; set; }
        public string OrigenNombre { get; set; } = string.Empty;
        public string DestinoNombre { get; set; } = string.Empty;
        public string ConductorNombreCompleto { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty; 
        public int PasajerosConfirmados { get; set; }
    }

    public class ReporteActividadViajesDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int TotalViajesPublicados { get; set; }
        public int TotalViajesRealizados { get; set; }
        public int TotalViajesCancelados { get; set; }
        public int TotalViajesPendientesEnCurso { get; set; }
        public List<ViajeActividadDTO> ViajesDetalle { get; set; } = new List<ViajeActividadDTO>();
    }
}
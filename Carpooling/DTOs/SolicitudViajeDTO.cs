using Domain.Model; // Necesario para EstadoViaje
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs
{
    public class SolicitudViajeDTO
    {
        public int IdSolicitud { get; set; }
        public DateTime SolicitudFecha { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EstadoSolicitud Estado { get; set; } // Estado de la SOLICITUD
        public int IdViaje { get; set; }
        public int IdPasajero { get; set; }

        // --- Datos del Pasajero (para vista del conductor) ---
        public string? NombrePasajero { get; set; }
        public string? ApellidoPasajero { get; set; }

        // --- NUEVO: Datos del Viaje (para vista del pasajero) ---
        public DateTime? FechaHoraViaje { get; set; }
        public EstadoViaje? EstadoDelViaje { get; set; } // Estado del VIAJE
        public string? NombreConductor { get; set; }
        public string? ApellidoConductor { get; set; }
        public int? IdConductor { get; set; }
        public string? OrigenViajeNombre { get; set; } // Nombre de la localidad Origen
        public string? DestinoViajeNombre { get; set; } // Nombre de la localidad Destino
        // --- FIN NUEVO ---
    }
}
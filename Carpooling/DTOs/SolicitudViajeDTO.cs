using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class SolicitudViajeDTO
    {
        public int IdSolicitud { get; set; }
        public DateTime SolicitudFecha { get; set; }
        public string Estado { get; set; }

        //claves foraneas
        public int IdViaje { get; set; }
        public int IdPasajero { get; set; }

        //por si queremos mostrar datos del pasajero o del viaje
        public string NombrePasajero { get; set; }
        public string ApellidoPasajero { get; set; }
        //public string OrigenViaje { get; set; }
        //public string DestinoViaje { get; set; }
        //public DateTime FechaHoraViaje { get; set; }


    }
}

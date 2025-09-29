using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace DTOs
{
    public class ViajeDTO
    {
        public int IdViaje { get; set; }
        public DateTime FechaHora { get; set; } // Fecha y hora juntos
        public int CantLugares { get; set; }
        public EstadoViaje Estado { get; set; }
        public string? Comentario { get; set; }
        public decimal Precio { get; set; } // prefiero decimal para dinero
        public string OrigenCodPostal { get; set; }
        public string DestinoCodPostal { get; set; }

        public int IdVehiculo { get; set; }


        // Opcional: datos completos de las localidades si hay q devolverlos
        public LocalidadDTO? Origen { get; set; }
        public LocalidadDTO? Destino { get; set; }

        // información sobre el conductor
        public int IdConductor { get; set; }


        
    }
}

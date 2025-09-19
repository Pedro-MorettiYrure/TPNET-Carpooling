using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class VehiculoDTO
    {
        public int IdVehiculo { get; set; }
        public string Patente { get; set; }
        public string Modelo { get; set; }
        public int CantLugares { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }

        // Para saber a qué usuario pertenece
        public int IdUsuario { get; set; }

    }
}

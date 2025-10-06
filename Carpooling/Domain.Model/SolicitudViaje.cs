using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Domain.Model;  //no se si es necesario

namespace Domain.Model
{
    public class SolicitudViaje
    {
        public int idSolicitud { get; set; }
        public DateTime SolicitudFecha { get; set; }
        public EstadoSolicitud Estado { get; set; }

        //claves foraneas

        public int IdViaje { get; set; }
        public int IdPasajero { get; set; }


        //prop de navegacion para el EF core
        public Viaje Viaje { get; set; }
        public Usuario Pasajero { get; set; }
    }
}



/*using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Model
{
    public class Localidad
    {
        public string codPostal { get; set; }
        public string nombreLoc { get; set; }

        public Localidad() { }

        public Localidad(string codigopostal, string nombre)
        {
            SetCodPostal(codigopostal);
            SetNombreLoc(nombre);
        }

        public void SetNombreLoc(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            nombreLoc = nombre;
        }

        public void SetCodPostal(string codigopostal)
        {
            if (string.IsNullOrWhiteSpace(codigopostal))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(codigopostal));
            codPostal = codigopostal;
        }
    }
}
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Viaje
    {
        public int IdViaje {  get; set; }

        public DateTime FechaHora { get; private set; }

        public int CantLugares { get; private set; }

        public EstadoViaje Estado { get; set; }

        public string? Comentario { get; private set; }

        public decimal Precio { get; private set; }

        public Localidad Origen { get; set; }

        public string OrigenCodPostal { get; set; }

        public Localidad Destino { get; set; }

        public string DestinoCodPostal { get; private set; }

        public Usuario Conductor { get; set; }

        public int IdConductor { get; set; }

        public Vehiculo Vehiculo { get; set; }
        public int IdVehiculo { get; set; }
        public virtual ICollection<SolicitudViaje> Solicitudes { get; set; } = new List<SolicitudViaje>();

        public Viaje() { }

        public static Viaje Crear(DateTime fecha, int cantLugares, decimal precio, string? comentario,
                           string origenCodPostal, string destinoCodPostal,
                           int idConductor, int capacidadVehiculo, int idVehiculo)
        {
            // Validación de la capacidad
            if (cantLugares > capacidadVehiculo)
                throw new ArgumentException("La cantidad de lugares no puede superar la capacidad del vehículo.");

            var v = new Viaje();
            v.SetFechaHora(fecha);
            v.SetCantLugares(cantLugares);
            v.SetPrecio(precio);   
            v.SetComentario(comentario);

            v.OrigenCodPostal = origenCodPostal;
            v.SetDestinoCodPostal(destinoCodPostal);
            v.IdConductor = idConductor;
            v.IdVehiculo = idVehiculo;
            v.Estado = EstadoViaje.Pendiente;

            return v;
        }


        public void SetFechaHora(DateTime fecha)
        {
            if (fecha < DateTime.Today)
                throw new ArgumentOutOfRangeException("No se pueden seleccionar fechas anteriores a hoy");
            FechaHora = fecha;
        }

        public void SetCantLugares(int cantLugares)
        {
            if (cantLugares <= 0)
                throw new ArgumentOutOfRangeException(nameof(cantLugares), "La cantidad de lugares debe ser mayor que cero.");
            CantLugares = cantLugares;
        }

        public void SetComentario(string? comentario)
        {
            if (comentario != null && comentario.Length > 500)
                throw new ArgumentException("El comentario no puede superar los 500 caracteres.", nameof(comentario));
            Comentario = comentario;
        }

        public void SetPrecio(decimal precio)
        {
            if (precio < 0)
                throw new ArgumentOutOfRangeException(nameof(precio), "El precio no puede ser negativo.");
            Precio = precio;
        }

        public void SetDestinoCodPostal(string destino)
        {
            if (string.IsNullOrWhiteSpace(destino))
                throw new ArgumentException("El código postal de destino no puede ser nulo o vacío.");
            if (Origen != null && destino == this.OrigenCodPostal)
                throw new ArgumentException("El destino no puede ser la misma localidad que el origen.", nameof(destino));
            DestinoCodPostal = destino;
        }
    }


}

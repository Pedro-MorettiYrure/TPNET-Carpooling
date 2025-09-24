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

        public DateTime Fecha { get; set; }

        public DateTime Hora { get; set; }

        public int CantLugares { get; set; }

        public EstadoViaje Estado { get; set; }

        public string? Comentario { get; set; }

        public float Precio { get; set; }

        public Localidad Origen { get; set; }

        public string OrigenCodPostal { get; set; }

        public Localidad Destino { get; set; }

        public string DestinoCodPostal { get; set; }

        public Viaje() { }

        public static Viaje Crear(DateTime fecha, DateTime hora, int cantLugares,
                                    string? comentario, float precio, Localidad origen, Localidad destino)
        {
            var v = new Viaje();
            v.SetFechaHora(fecha);
            v.SetHora(hora);
            v.SetCantLugares(cantLugares);
            v.SetComentario(comentario);
            v.SetOrigen(origen);
            v.SetDestino(destino);
            v.Estado = EstadoViaje.Pendiente;
            return v;
        }

        public void SetFechaHora(DateTime fecha)
        {
            if (fecha < DateTime.Today)
                throw new ArgumentOutOfRangeException("No se pueden seleccionar fechas anteriores a hoy");
            Fecha = fecha;
        }

        public void SetHora(DateTime hora)
        {
            Hora = hora;
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

        public void SetPrecio(float precio)
        {
            if (precio < 0)
                throw new ArgumentOutOfRangeException(nameof(precio), "El precio no puede ser negativo.");
            Precio = precio;
        }

        public void SetOrigen(Localidad origen)
        {
            if (origen == null)
                throw new ArgumentNullException(nameof(origen));
            Origen = origen;
        }

        public void SetDestino(Localidad destino)
        {
            if (destino == null)
                throw new ArgumentNullException(nameof(destino));
            if (Origen != null && destino.codPostal == Origen.codPostal)
                throw new ArgumentException("El destino no puede ser la misma localidad que el origen.", nameof(destino));
            Destino = destino;
        }
    }


}

using System;
using Domain.Model;  

namespace Domain.Model
{
    public class SolicitudViaje
    {
        public int IdSolicitud { get; set; }
        public DateTime SolicitudFecha { get; set; }
        public EstadoSolicitud Estado { get; set; }

        //claves foraneas
        public int IdViaje { get; private set; }
        public int IdPasajero { get; private set; }


        //props de navegacion para el EF core
        public Viaje Viaje { get; set; }
        public Usuario Pasajero { get; set; }

        public SolicitudViaje() { }

        public SolicitudViaje(int idViaje, int idPasajero)
        {
            SetIdViaje(idViaje);
            SetIdPasajero(idPasajero);
            this.SolicitudFecha = DateTime.Now; 
            this.Estado = EstadoSolicitud.Pendiente; 
        }

        public void SetIdViaje(int idViaje)
        {
            if (idViaje <= 0) 
                throw new ArgumentOutOfRangeException(nameof(idViaje));
            IdViaje = idViaje;
        }

        public void SetIdPasajero(int idPasajero)
        {
            if (idPasajero <= 0)
                throw new ArgumentOutOfRangeException(nameof(idPasajero));
            IdPasajero = idPasajero;
        }

        public void Cancelar()
        {
            if (Estado == EstadoSolicitud.Pendiente || Estado == EstadoSolicitud.Aprobada)
            {
                Estado = EstadoSolicitud.Cancelada; 
            }
        }

        public void Rechazar()
        {
            if (Estado == EstadoSolicitud.Pendiente)
            {
                Estado = EstadoSolicitud.Rechazada;
            }
        }
    } 
}


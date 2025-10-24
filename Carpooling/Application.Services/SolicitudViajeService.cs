using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class SolicitudViajeService
    {
        private readonly SolicitudViajeRepository _solicitudRepo;
        private readonly ViajeRepository _viajeRepo;
        private readonly UsuarioRepository _usuarioRepo;

        public SolicitudViajeService(SolicitudViajeRepository solicitudRepo, ViajeRepository viajeRepo, UsuarioRepository usuarioRepo)
        {
            _solicitudRepo = solicitudRepo;
            _viajeRepo = viajeRepo;
            _usuarioRepo = usuarioRepo;
        }

        public SolicitudViajeDTO CrearSolicitud(int idViaje, int idPasajero)
        {
            var viaje = _viajeRepo.Get(idViaje);
            if (viaje == null)
                throw new ArgumentException("El viaje especificado no existe.");

            var pasajero = _usuarioRepo.GetById(idPasajero);
            if (pasajero == null)
                throw new ArgumentException("El usuario pasajero no existe.");

            if (viaje.IdConductor == idPasajero)
                throw new InvalidOperationException("El conductor no puede solicitar unirse a su propio viaje.");

            if (viaje.Estado != EstadoViaje.Pendiente)
                throw new InvalidOperationException("Solo se pueden realizar solicitudes a viajes pendientes.");

            if (_solicitudRepo.ExisteSolicitud(idViaje, idPasajero))
                throw new InvalidOperationException("Ya existe una solicitud activa para este viaje y pasajero.");

            var solicitudesAprobadas = _solicitudRepo.GetAllByViaje(idViaje)
                                        .Count(s => s.Estado == EstadoSolicitud.Aprobada);
            if (solicitudesAprobadas >= viaje.CantLugares)
                throw new InvalidOperationException("No quedan lugares disponibles en este viaje.");

            var nuevaSolicitud = new SolicitudViaje(idViaje, idPasajero);
            _solicitudRepo.Add(nuevaSolicitud);
            return MapToDTO(nuevaSolicitud);
        }

        public IEnumerable<SolicitudViajeDTO> GetSolicitudesByViaje(int idViaje)
        {
            var solicitudes = _solicitudRepo.GetAllByViaje(idViaje);
            return solicitudes.Select(s => MapToDTO(s, includePasajeroInfo: true)).ToList();
        }

        public IEnumerable<SolicitudViajeDTO> GetSolicitudesByPasajero(int idPasajero)
        {
            var solicitudes = _solicitudRepo.GetAllByPasajero(idPasajero);
            return solicitudes.Select(s => MapToDTO(s, includePasajeroInfo: false)).ToList();
        }

        public SolicitudViajeDTO? GetSolicitudById(int idSolicitud)
        {
            var solicitud = _solicitudRepo.GetById(idSolicitud);
            return solicitud == null ? null : MapToDTO(solicitud, includePasajeroInfo: true);
        }


        public bool AceptarSolicitud(int idSolicitud, int idConductor)
        {
            var solicitud = _solicitudRepo.GetById(idSolicitud);
            if (solicitud == null)
                throw new ArgumentException("La solicitud no existe.");

            if (solicitud.Viaje.IdConductor != idConductor)
                throw new InvalidOperationException("Solo el conductor del viaje puede aceptar la solicitud.");

            var viaje = solicitud.Viaje;
            var solicitudesAceptadas = _solicitudRepo.GetAllByViaje(viaje.IdViaje)
                                        .Count(s => s.Estado == EstadoSolicitud.Aprobada);
            if (solicitudesAceptadas >= viaje.CantLugares)
                throw new InvalidOperationException("No quedan lugares disponibles para aceptar esta solicitud.");

            if (solicitud.Estado == EstadoSolicitud.Pendiente)
            {
                solicitud.Estado = EstadoSolicitud.Aprobada;
                _solicitudRepo.Update(solicitud);
                return true;
            }
            return false;
        }

        public bool RechazarSolicitud(int idSolicitud, int idConductor)
        {
            var solicitud = _solicitudRepo.GetById(idSolicitud);
            if (solicitud == null)
                throw new ArgumentException("La solicitud no existe.");

            if (solicitud.Viaje.IdConductor != idConductor)
                throw new InvalidOperationException("Solo el conductor del viaje puede rechazar la solicitud.");

            if (solicitud.Estado == EstadoSolicitud.Pendiente)
            {
                solicitud.Estado = EstadoSolicitud.Rechazada;
                _solicitudRepo.Update(solicitud);
                return true;
            }
            return false;
        }

        public bool CancelarSolicitud(int idSolicitud, int idPasajero) 
        {
            var solicitud = _solicitudRepo.GetById(idSolicitud);
            if (solicitud == null)
                throw new ArgumentException("La solicitud no existe.");

            if (solicitud.IdPasajero != idPasajero)
                throw new InvalidOperationException("Solo el pasajero que realizó la solicitud puede cancelarla.");

            if (solicitud.Estado == EstadoSolicitud.Pendiente || solicitud.Estado == EstadoSolicitud.Aprobada)
            {
                solicitud.Estado = EstadoSolicitud.Cancelada; 
                _solicitudRepo.Update(solicitud);
                return true;
            }
            return false; // ya estaba Rechazada o Cancelada
        }

        private SolicitudViajeDTO MapToDTO(SolicitudViaje solicitud, bool includePasajeroInfo = false)
        {
            var dto = new SolicitudViajeDTO
            {
                IdSolicitud = solicitud.IdSolicitud,
                SolicitudFecha = solicitud.SolicitudFecha,
                Estado = solicitud.Estado.ToString(),
                IdViaje = solicitud.IdViaje,
                IdPasajero = solicitud.IdPasajero
            };

            if (includePasajeroInfo && solicitud.Pasajero != null)
            {
                dto.NombrePasajero = solicitud.Pasajero.Nombre;
                dto.ApellidoPasajero = solicitud.Pasajero.Apellido;
            }

            return dto;
        }
    }
}
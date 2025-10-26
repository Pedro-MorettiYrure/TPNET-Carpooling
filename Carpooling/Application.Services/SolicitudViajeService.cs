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
            // El repo Get debe incluir Origen, Destino, Conductor
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

            // Usar el método del repo que chequea solicitudes ACTIVAS
            if (_solicitudRepo.ExisteSolicitudActiva(idViaje, idPasajero))
                throw new InvalidOperationException("Ya existe una solicitud pendiente o aprobada para este viaje.");

            var solicitudesAprobadas = _solicitudRepo.GetAllByViaje(idViaje)
                                           .Count(s => s.Estado == EstadoSolicitud.Aprobada);
            if (solicitudesAprobadas >= viaje.CantLugares)
                throw new InvalidOperationException("No quedan lugares disponibles en este viaje.");

            var nuevaSolicitud = new SolicitudViaje(idViaje, idPasajero);
            _solicitudRepo.Add(nuevaSolicitud);

            // Adjuntar objetos para el mapeo
            nuevaSolicitud.Pasajero = pasajero;
            nuevaSolicitud.Viaje = viaje;

            // Mapear DTO completo
            return MapToDTO(nuevaSolicitud, includePasajeroInfo: true, includeViajeInfo: true);
        }

        public IEnumerable<SolicitudViajeDTO> GetSolicitudesByViaje(int idViaje)
        {
            // El repo GetAllByViaje debe incluir Pasajero
            var solicitudes = _solicitudRepo.GetAllByViaje(idViaje);
            return solicitudes.Select(s => MapToDTO(s, includePasajeroInfo: true, includeViajeInfo: false)).ToList();
        }

        public IEnumerable<SolicitudViajeDTO> GetSolicitudesByPasajero(int idPasajero)
        {
            // El repo GetAllByPasajero debe incluir Viaje.Origen, Viaje.Destino, Viaje.Conductor
            var solicitudes = _solicitudRepo.GetAllByPasajero(idPasajero);
            return solicitudes.Select(s => MapToDTO(s, includePasajeroInfo: false, includeViajeInfo: true)).ToList();
        }

        public SolicitudViajeDTO? GetSolicitudById(int idSolicitud)
        {
            // El repo GetById debe incluir Pasajero y Viaje.Conductor (y Origen/Destino si se quiere)
            var solicitud = _solicitudRepo.GetById(idSolicitud);
            return solicitud == null ? null : MapToDTO(solicitud, includePasajeroInfo: true, includeViajeInfo: true);
        }


        public bool AceptarSolicitud(int idSolicitud, int idConductor)
        {
            var solicitud = _solicitudRepo.GetById(idSolicitud); // Repo debe incluir Viaje
            if (solicitud == null)
                throw new ArgumentException("La solicitud no existe.");
            if (solicitud.Viaje == null)
                throw new InvalidOperationException("Error interno: No se pudo cargar el viaje de la solicitud.");

            if (solicitud.Viaje.IdConductor != idConductor)
                throw new UnauthorizedAccessException("Solo el conductor del viaje puede aceptar la solicitud.");

            var viaje = solicitud.Viaje;
            var solicitudesAceptadas = _solicitudRepo.GetAllByViaje(viaje.IdViaje)
                                           .Count(s => s.Estado == EstadoSolicitud.Aprobada);

            // Contar esta solicitud si estuviera pendiente
            if (solicitud.Estado == EstadoSolicitud.Pendiente && (solicitudesAceptadas + 1) > viaje.CantLugares)
                throw new InvalidOperationException("No quedan lugares disponibles para aceptar esta solicitud.");
            // Si ya está aprobada, no sumar
            if (solicitud.Estado == EstadoSolicitud.Aprobada)
                throw new InvalidOperationException("Esta solicitud ya se encuentra Aprobada.");


            if (solicitud.Estado == EstadoSolicitud.Pendiente)
            {
                solicitud.Estado = EstadoSolicitud.Aprobada;
                _solicitudRepo.Update(solicitud);
                return true;
            }

            throw new InvalidOperationException($"La solicitud no está Pendiente (estado actual: {solicitud.Estado}).");
        }

        public bool RechazarSolicitud(int idSolicitud, int idConductor)
        {
            var solicitud = _solicitudRepo.GetById(idSolicitud); // Repo debe incluir Viaje
            if (solicitud == null)
                throw new ArgumentException("La solicitud no existe.");
            if (solicitud.Viaje == null)
                throw new InvalidOperationException("Error interno: No se pudo cargar el viaje de la solicitud.");

            if (solicitud.Viaje.IdConductor != idConductor)
                throw new UnauthorizedAccessException("Solo el conductor del viaje puede rechazar la solicitud.");

            if (solicitud.Estado == EstadoSolicitud.Pendiente)
            {
                solicitud.Estado = EstadoSolicitud.Rechazada;
                _solicitudRepo.Update(solicitud);
                return true;
            }

            throw new InvalidOperationException($"La solicitud no está Pendiente (estado actual: {solicitud.Estado}).");
        }

        public bool CancelarSolicitud(int idSolicitud, int idPasajero)
        {
            var solicitud = _solicitudRepo.GetById(idSolicitud); // Repo *podría* incluir Viaje
            if (solicitud == null)
                throw new ArgumentException("La solicitud no existe.");

            if (solicitud.IdPasajero != idPasajero)
                throw new UnauthorizedAccessException("Solo el pasajero que realizó la solicitud puede cancelarla.");

            // Validar que el viaje no haya iniciado (opcional pero recomendado)
            if (solicitud.Viaje != null && (solicitud.Viaje.Estado == EstadoViaje.EnCurso || solicitud.Viaje.Estado == EstadoViaje.Realizado))
            {
                throw new InvalidOperationException("No se puede cancelar una solicitud de un viaje que ya está en curso o finalizado.");
            }

            if (solicitud.Estado == EstadoSolicitud.Pendiente || solicitud.Estado == EstadoSolicitud.Aprobada)
            {
                solicitud.Estado = EstadoSolicitud.Cancelada;
                _solicitudRepo.Update(solicitud);
                return true;
            }

            throw new InvalidOperationException($"La solicitud ya se encuentra en estado '{solicitud.Estado}' y no puede ser cancelada.");
        }

        // *** MODIFICADO: MapToDTO para incluir info del viaje ***
        private SolicitudViajeDTO MapToDTO(SolicitudViaje solicitud, bool includePasajeroInfo = false, bool includeViajeInfo = false)
        {
            if (solicitud == null) throw new ArgumentNullException(nameof(solicitud));

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

            if (includeViajeInfo && solicitud.Viaje != null)
            {
                dto.FechaHoraViaje = solicitud.Viaje.FechaHora;
                dto.EstadoDelViaje = solicitud.Viaje.Estado;
                dto.IdConductor = solicitud.Viaje.IdConductor;

                if (solicitud.Viaje.Conductor != null)
                {
                    dto.NombreConductor = solicitud.Viaje.Conductor.Nombre;
                    dto.ApellidoConductor = solicitud.Viaje.Conductor.Apellido;
                }
                if (solicitud.Viaje.Origen != null)
                {
                    dto.OrigenViajeNombre = solicitud.Viaje.Origen.nombreLoc;
                }
                if (solicitud.Viaje.Destino != null)
                {
                    dto.DestinoViajeNombre = solicitud.Viaje.Destino.nombreLoc;
                }
            }

            return dto;
        }
    }
}
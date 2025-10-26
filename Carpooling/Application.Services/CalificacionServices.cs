using Data;
using Domain.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class CalificacionService
    {
        private readonly CalificacionRepository _califRepo;
        private readonly ViajeRepository _viajeRepo;
        private readonly SolicitudViajeRepository _solicitudRepo; // Para validar participación

        public CalificacionService(CalificacionRepository califRepo, ViajeRepository viajeRepo, SolicitudViajeRepository solicitudRepo)
        {
            _califRepo = califRepo;
            _viajeRepo = viajeRepo;
            _solicitudRepo = solicitudRepo;
        }

        // El Conductor califica a un Pasajero
        public CalificacionDTO CalificarPasajero(int idViaje, int idConductor, int idPasajeroCalificado, int puntaje, string? comentario)
        {
            var viaje = _viajeRepo.Get(idViaje);
            ValidarCalificacion(viaje, idConductor, idPasajeroCalificado, RolCalificado.Pasajero);

            // Validar que el pasajero estuviera aprobado en el viaje
            if (!_solicitudRepo.GetAllByViaje(idViaje).Any(s => s.IdPasajero == idPasajeroCalificado && s.Estado == EstadoSolicitud.Aprobada))
            {
                throw new InvalidOperationException("El pasajero indicado no participó (o no fue aprobado) en este viaje.");
            }

            var calificacion = Calificacion.Crear(idViaje, idConductor, idPasajeroCalificado, RolCalificado.Pasajero, puntaje, comentario);
            _califRepo.Add(calificacion);

            // Aquí podrías recalcular y actualizar el promedio de calificación del pasajero si lo guardas en Usuario

            return MapToDTO(calificacion);
        }

        // El Pasajero califica al Conductor
        public CalificacionDTO CalificarConductor(int idViaje, int idPasajeroCalificador, int puntaje, string? comentario)
        {
            var viaje = _viajeRepo.Get(idViaje);
            if (viaje == null) throw new KeyNotFoundException("Viaje no encontrado.");

            int idConductorCalificado = viaje.IdConductor;
            ValidarCalificacion(viaje, idPasajeroCalificador, idConductorCalificado, RolCalificado.Conductor);

            // Validar que el pasajero estuviera aprobado en el viaje
            if (!_solicitudRepo.GetAllByViaje(idViaje).Any(s => s.IdPasajero == idPasajeroCalificador && s.Estado == EstadoSolicitud.Aprobada))
            {
                throw new InvalidOperationException("No puedes calificar un viaje en el que no participaste (o no fuiste aprobado).");
            }


            var calificacion = Calificacion.Crear(idViaje, idPasajeroCalificador, idConductorCalificado, RolCalificado.Conductor, puntaje, comentario);
            _califRepo.Add(calificacion);

            // Aquí podrías recalcular y actualizar el promedio de calificación del conductor si lo guardas en Usuario

            return MapToDTO(calificacion);
        }

        // Método auxiliar de validación común
        private void ValidarCalificacion(Viaje? viaje, int idCalificador, int idCalificado, RolCalificado rol)
        {
            if (viaje == null) throw new KeyNotFoundException("Viaje no encontrado.");
            if (viaje.Estado != EstadoViaje.Realizado) throw new InvalidOperationException("Solo se pueden calificar viajes realizados.");

            // Verificar si ya existe una calificación para evitar duplicados
            if (_califRepo.YaCalifico(viaje.IdViaje, idCalificador, idCalificado, rol))
            {
                throw new InvalidOperationException("Ya has calificado a este usuario para este viaje en este rol.");
            }
        }

        // Métodos para obtener calificaciones (ejemplos)
        public IEnumerable<CalificacionDTO> GetCalificacionesRecibidasComoConductor(int idUsuario)
        {
            return _califRepo.GetCalificacionesRecibidas(idUsuario, RolCalificado.Conductor)
                             .Select(MapToDTO)
                             .ToList();
        }

        public IEnumerable<CalificacionDTO> GetCalificacionesRecibidasComoPasajero(int idUsuario)
        {
            return _califRepo.GetCalificacionesRecibidas(idUsuario, RolCalificado.Pasajero)
                            .Select(MapToDTO)
                            .ToList();
        }

        // Mapeo a DTO (puedes crear CalificacionDTO en tu proyecto DTOs)
        private CalificacionDTO MapToDTO(Calificacion c)
        {
            return new CalificacionDTO
            {
                IdCalificacion = c.IdCalificacion,
                IdViaje = c.IdViaje,
                IdCalificador = c.IdCalificador,
                NombreCalificador = c.Calificador?.Nombre + " " + c.Calificador?.Apellido, // Cargar si es necesario
                IdCalificado = c.IdCalificado,
                NombreCalificado = c.Calificado?.Nombre + " " + c.Calificado?.Apellido, // Cargar si es necesario
                RolCalificado = c.RolCalificado.ToString(),
                Puntaje = c.Puntaje,
                Comentario = c.Comentario,
                FechaHora = c.FechaHora
            };
        }
    }

    
}
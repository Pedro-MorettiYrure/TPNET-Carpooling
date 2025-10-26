using Domain.Model;
using Microsoft.EntityFrameworkCore; // Necesario para Include y ThenInclude
using System; // Para Exception
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class SolicitudViajeRepository
    {
        private readonly TPIContext _context;
        public SolicitudViajeRepository(TPIContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(SolicitudViaje solicitud)
        {
            _context.SolicitudesViaje.Add(solicitud);
            _context.SaveChanges();
        }

        public void Update(SolicitudViaje solicitud)
        {
            _context.SolicitudesViaje.Update(solicitud);
            _context.SaveChanges();
        }

        // *** MODIFICADO: Asegurar Include de Pasajero ***
        public SolicitudViaje? GetById(int idSolicitud)
        {
            return _context.SolicitudesViaje
                .Include(s => s.Pasajero) // Incluir Pasajero
                .Include(s => s.Viaje)    // Incluir Viaje
                    .ThenInclude(v => v.Conductor) // Incluir Conductor del Viaje
                 .Include(s => s.Viaje)
                     .ThenInclude(v => v.Origen) // Incluir Origen
                 .Include(s => s.Viaje)
                     .ThenInclude(v => v.Destino) // Incluir Destino
                .FirstOrDefault(s => s.IdSolicitud == idSolicitud);
        }

        // *** MODIFICADO: Asegurar Include de Pasajero ***
        public IEnumerable<SolicitudViaje> GetAllByViaje(int idViaje)
        {
            return _context.SolicitudesViaje
                .Include(s => s.Pasajero) // Incluir datos del pasajero
                .Where(s => s.IdViaje == idViaje)
                .OrderBy(s => s.SolicitudFecha)
                .ToList();
        }

        // *** MODIFICADO: Asegurar Include de Viaje y sus relaciones ***
        public IEnumerable<SolicitudViaje> GetAllByPasajero(int idPasajero)
        {
            return _context.SolicitudesViaje
                .Include(s => s.Viaje) // Incluir el Viaje
                    .ThenInclude(v => v.Origen) // Incluir Localidad Origen del Viaje
                .Include(s => s.Viaje)
                    .ThenInclude(v => v.Destino) // Incluir Localidad Destino del Viaje
                .Include(s => s.Viaje)
                    .ThenInclude(v => v.Conductor) // Incluir Usuario Conductor del Viaje
                .Where(s => s.IdPasajero == idPasajero)
                .OrderByDescending(s => s.SolicitudFecha)
                .ToList();
        }

        // Chequea si existe CUALQUIER solicitud (sin importar estado)
        public bool ExisteSolicitud(int idViaje, int idPasajero)
        {
            return _context.SolicitudesViaje.Any(s => s.IdViaje == idViaje && s.IdPasajero == idPasajero);
        }

        // *** MODIFICADO: Chequear si existe solicitud ACTIVA (Pendiente o Aprobada) ***
        // (Tu servicio ya lo usaba, pero este es el lugar correcto para la lógica)
        public bool ExisteSolicitudActiva(int idViaje, int idPasajero)
        {
            return _context.SolicitudesViaje.Any(s =>
               s.IdViaje == idViaje &&
               s.IdPasajero == idPasajero &&
               (s.Estado == EstadoSolicitud.Pendiente || s.Estado == EstadoSolicitud.Aprobada));
        }
    }
}
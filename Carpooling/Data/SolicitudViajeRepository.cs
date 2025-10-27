using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System; 
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

        public SolicitudViaje? GetById(int idSolicitud)
        {
            return _context.SolicitudesViaje
                .Include(s => s.Pasajero) 
                .Include(s => s.Viaje)    
                    .ThenInclude(v => v.Conductor) 
                 .Include(s => s.Viaje)
                     .ThenInclude(v => v.Origen)
                 .Include(s => s.Viaje)
                     .ThenInclude(v => v.Destino) 
                .FirstOrDefault(s => s.IdSolicitud == idSolicitud);
        }

        public IEnumerable<SolicitudViaje> GetAllByViaje(int idViaje)
        {
            return _context.SolicitudesViaje
                .Include(s => s.Pasajero)
                .Include(s => s.Viaje)    
                    .ThenInclude(v => v.Conductor)
                .Include(s => s.Viaje)
                     .ThenInclude(v => v.Origen) 
                 .Include(s => s.Viaje)
                     .ThenInclude(v => v.Destino)
                .Where(s => s.IdViaje == idViaje)
                .OrderBy(s => s.SolicitudFecha)
                .ToList();
        }

        public IEnumerable<SolicitudViaje> GetAllByPasajero(int idPasajero)
        {
            return _context.SolicitudesViaje
                .Include(s => s.Viaje) 
                    .ThenInclude(v => v.Origen) 
                .Include(s => s.Viaje)
                    .ThenInclude(v => v.Destino) 
                .Include(s => s.Viaje)
                    .ThenInclude(v => v.Conductor) 
                .Where(s => s.IdPasajero == idPasajero)
                .OrderByDescending(s => s.SolicitudFecha)
                .ToList();
        }

        // Chequea si existe CUALQUIER solicitud (sin importar estado)
        public bool ExisteSolicitud(int idViaje, int idPasajero)
        {
            return _context.SolicitudesViaje.Any(s => s.IdViaje == idViaje && s.IdPasajero == idPasajero);
        }

        //  Esto se usa p cuando se quiere solicitar un viaje, q valide si el pasajero no tiene ya una solicitud rechazada, pendiente o aprobada(si tiene una cancelada le permitimos volver a solicitar)
        public bool ExisteSolicitudActiva(int idViaje, int idPasajero)
        {
            return _context.SolicitudesViaje.Any(s =>
               s.IdViaje == idViaje &&
               s.IdPasajero == idPasajero &&
               (s.Estado == EstadoSolicitud.Pendiente || s.Estado == EstadoSolicitud.Aprobada  || s.Estado == EstadoSolicitud.Rechazada));
        }
    }
}
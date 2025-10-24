using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SolicitudViajeRepository
    {
        private readonly TPIContext _context;
        public SolicitudViajeRepository(TPIContext context)
        {
            _context = context;
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
                .FirstOrDefault(v => v.IdSolicitud == idSolicitud);
        }

        public IEnumerable<SolicitudViaje> GetAllByViaje(int idViaje)
        {
            return _context.SolicitudesViaje
                .Include(s => s.Pasajero)
                .Where(s => s.IdViaje == idViaje)
                .ToList();
        }

        public IEnumerable<SolicitudViaje> GetAllByPasajero(int idPasajero)
        {
            return _context.SolicitudesViaje
                .Include(s => s.Viaje) 
                    .ThenInclude(v => v.Origen) 
                .Include(s => s.Viaje)
                    .ThenInclude(v => v.Destino) 
                .Where(s => s.IdPasajero == idPasajero)
                .ToList();
        }
        public bool ExisteSolicitud(int idViaje, int idPasajero)
        {
            return _context.SolicitudesViaje.Any(s =>
                s.IdViaje == idViaje &&
                s.IdPasajero == idPasajero &&
                (s.Estado == EstadoSolicitud.Pendiente || s.Estado == EstadoSolicitud.Aprobada || s.Estado == EstadoSolicitud.Rechazada));
        }
    }
}


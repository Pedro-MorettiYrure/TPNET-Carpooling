using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class CalificacionRepository
    {
        private readonly TPIContext _context;

        public CalificacionRepository(TPIContext context)
        {
            _context = context;
        }

        public void Add(Calificacion calificacion)
        {
            _context.Calificaciones.Add(calificacion);
            _context.SaveChanges();
        }

        // Obtener calificaciones RECIBIDAS por un usuario en un rol específico
        public IEnumerable<Calificacion> GetCalificacionesRecibidas(int idCalificado, RolCalificado rol)
        {
            return _context.Calificaciones
                .Include(c => c.Calificador) // Incluir quién calificó
                .Include(c => c.Viaje)       // Incluir info del viaje
                .Where(c => c.IdCalificado == idCalificado && c.RolCalificado == rol)
                .OrderByDescending(c => c.FechaHora)
                .ToList();
        }

        // Obtener calificaciones DADAS por un usuario
        public IEnumerable<Calificacion> GetCalificacionesDadas(int idCalificador)
        {
            return _context.Calificaciones
               .Include(c => c.Calificado) // Incluir a quién calificó
               .Include(c => c.Viaje)      // Incluir info del viaje
               .Where(c => c.IdCalificador == idCalificador)
               .OrderByDescending(c => c.FechaHora)
               .ToList();
        }

        // Verificar si un usuario ya calificó a otro en un viaje específico
        public bool YaCalifico(int idViaje, int idCalificador, int idCalificado, RolCalificado rol)
        {
            return _context.Calificaciones.Any(c =>
                c.IdViaje == idViaje &&
                c.IdCalificador == idCalificador &&
                c.IdCalificado == idCalificado &&
                c.RolCalificado == rol);
        }

        // Obtener pasajeros confirmados (Aprobados) de un viaje para que el conductor califique
        public IEnumerable<Usuario> GetPasajerosConfirmados(int idViaje)
        {
            return _context.SolicitudesViaje
                           .Where(s => s.IdViaje == idViaje && s.Estado == EstadoSolicitud.Aprobada)
                           .Select(s => s.Pasajero)
                           .ToList();
        }
    }
}
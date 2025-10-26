using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Necesario para Include/ThenInclude
using Domain.Model;
using Data;

namespace Data
{
    public class ViajeRepository
    {
        private readonly TPIContext _context;

        public ViajeRepository(TPIContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Viaje viaje)
        {
            _context.Viajes.Add(viaje);
            _context.SaveChanges();
        }

        public bool Update(Viaje viaje)
        {
            //var viajeExistente = _context.Viajes.Find(viaje.IdViaje); // Find no usa Includes
            //if (viajeExistente != null)
            //{
            _context.Viajes.Update(viaje);
            return _context.SaveChanges() > 0; // Devuelve true si se afectaron filas
            //}
            //return false;
        }

        public Vehiculo? GetVehiculo(int idVehiculo)
        {
            return _context.Vehiculos.FirstOrDefault(v => v.IdVehiculo == idVehiculo);
        }

        // *** MODIFICADO: Get(id) ahora incluye Origen, Destino y Conductor ***
        public Viaje? Get(int id) // Devuelve nullable
        {
            return _context.Viajes
                .Include(v => v.Origen)
                .Include(v => v.Destino)
                .Include(v => v.Conductor)
                // .Include(v => v.Vehiculo) // Descomentar si es necesario
                // .Include(v => v.Solicitudes).ThenInclude(s => s.Pasajero) // Descomentar si es necesario
                .FirstOrDefault(v => v.IdViaje == id);
        }

        public IEnumerable<Viaje> GetAllByConductor(int idUsuario)
        {
            // *** MODIFICADO: Incluir Origen y Destino para el DTO ***
            return _context.Viajes
                .Include(v => v.Origen)
                .Include(v => v.Destino)
                .Where(v => v.IdConductor == idUsuario)
                .OrderByDescending(v => v.FechaHora)
                .ToList();
        }

        public IEnumerable<Viaje> GetAll()
        {
            return _context.Viajes
                .Include(v => v.Origen)
                .Include(v => v.Destino)
                .OrderBy(p => p.IdViaje)
                .ToList();
        }

        public IEnumerable<Viaje> GetViajesDisponiblesPorRuta(string origenCodPostal, string destinoCodPostal)
        {
            var ahora = DateTime.Now;

            return _context.Viajes
                .Include(v => v.Origen)  // Necesario para NombreOrigen
                .Include(v => v.Destino) // Necesario para NombreDestino
                .Include(v => v.Conductor) // Opcional, si la búsqueda muestra el nombre
                .Include(v => v.Vehiculo)  // Opcional, si la búsqueda muestra el auto
                .Where(v => v.OrigenCodPostal == origenCodPostal &&
                             v.DestinoCodPostal == destinoCodPostal &&
                             v.Estado == EstadoViaje.Pendiente &&
                             v.FechaHora > ahora)
                // La lógica de lugares disponibles debería estar en el servicio de Solicitud (al crear)
                // O podrías filtrarlo aquí si es un requisito estricto de búsqueda
                .OrderBy(v => v.FechaHora) // Más próximos primero
                .ToList();
        }
    }
}
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
            
            _context.Viajes.Update(viaje);
            return _context.SaveChanges() > 0; 
        }

        public Vehiculo? GetVehiculo(int idVehiculo)
        {
            return _context.Vehiculos.FirstOrDefault(v => v.IdVehiculo == idVehiculo);
        }

        public Viaje? Get(int id) 
        {
            return _context.Viajes
                .Include(v => v.Origen)
                .Include(v => v.Destino)
                .Include(v => v.Conductor)
                .Include(v => v.Vehiculo) 
                .FirstOrDefault(v => v.IdViaje == id);
        }

        public IEnumerable<Viaje> GetAllByConductor(int idUsuario)
        {
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
                .Include(v => v.Origen) 
                .Include(v => v.Destino) 
                .Include(v => v.Conductor) 
                .Include(v => v.Vehiculo)  
                .Where(v => v.OrigenCodPostal == origenCodPostal &&
                             v.DestinoCodPostal == destinoCodPostal &&
                             v.Estado == EstadoViaje.Pendiente &&
                             v.FechaHora > ahora)
                
                .OrderBy(v => v.FechaHora)
                .ToList();
        }
        public IEnumerable<Viaje> GetViajesByDateRange(DateTime fechaInicio, DateTime fechaFin)
        {
            // Aseguramos que incluya la info necesaria para el reporte
            return _context.Viajes
                .Include(v => v.Origen) 
                .Include(v => v.Destino) 
                .Include(v => v.Conductor) 
                .Where(v => v.FechaHora >= fechaInicio && v.FechaHora < fechaFin.AddDays(1)) 
                .OrderBy(v => v.FechaHora) 
                .ToList(); 
        }
        public Viaje? GetWithPasajerosConfirmados(int idViaje)
        {
            return _context.Viajes
                .Include(v => v.Conductor) 
                .Include(v => v.Solicitudes) 
                    .ThenInclude(s => s.Pasajero)
                .FirstOrDefault(v => v.IdViaje == idViaje);
        }


    }
}
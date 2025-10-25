using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Data;


namespace Data
{
    public class ViajeRepository
    {
        private readonly TPIContext _context;

        public ViajeRepository(TPIContext context)
        {
            _context = context;
        }

        public void Add(Viaje viaje)
        {
            _context.Viajes.Add(viaje);
            _context.SaveChanges();
        }

        public bool Update(Viaje viaje)
        {
            var viajeExistente = _context.Viajes.Find(viaje.IdViaje);

            if (viajeExistente != null)
            {
                _context.Viajes.Update(viaje);      //usar Set() para validar?
                _context.SaveChanges();
                return true;
            }
            return false;
        }


        //ya no usamos este metodo, hacemos baja logica en el servicio

        /*public void Delete(Viaje viaje)
        {
            _context.Viajes.Remove(viaje);
            _context.SaveChanges();
        }*/

        public Vehiculo? GetVehiculo(int idVehiculo)
        {
            return _context.Vehiculos.FirstOrDefault(v => v.IdVehiculo == idVehiculo);
        }


        public Viaje Get(int id)
        {
            return _context.Viajes.FirstOrDefault(v => v.IdViaje == id);
        }

        public IEnumerable<Viaje> GetAllByConductor(int idUsuario)
        {
            return _context.Viajes.Where(v => v.IdConductor == idUsuario).ToList();
        }

        public IEnumerable<Viaje> GetAll()
        {
            return _context.Viajes.OrderBy(p => p.IdViaje).ToList();
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
                                                  // Subconsulta para calcular lugares ocupados 
                //.Select(v => new {
                //    Viaje = v,
                //    LugaresOcupados = _context.SolicitudesViaje.Count(s => s.IdViaje == v.IdViaje && s.Estado == EstadoSolicitud.Aprobada)
                //})
                //.Where(x => x.LugaresOcupados < x.Viaje.CantLugares)
                //.Select(x => x.Viaje)
                .ToList(); 
        }
    }
}

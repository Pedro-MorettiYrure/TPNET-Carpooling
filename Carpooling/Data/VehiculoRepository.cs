using Domain.Model;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class VehiculoRepository
    {
        private readonly TPIContext _context;

        public VehiculoRepository(TPIContext context)
        {
            _context = context;
        }

        public void Add(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            _context.SaveChanges();
        }

        public void Update(Vehiculo vehiculo)
        {
            _context.Vehiculos.Update(vehiculo);
            _context.SaveChanges();
        }

        public void Delete(Vehiculo vehiculo)
        {
            _context.Vehiculos.Remove(vehiculo);
            _context.SaveChanges();
        }

        public Vehiculo? GetById(int idVehiculo)
        {
            return _context.Vehiculos.FirstOrDefault(v => v.IdVehiculo == idVehiculo);
        }

        public Vehiculo? Get(string patente)
        {
            return _context.Vehiculos.FirstOrDefault(v => v.Patente == patente);
        }

        public IEnumerable<Vehiculo> GetAllByUsuario(int idUsuario)
        {
            return _context.Vehiculos.Where(v => v.IdUsuario == idUsuario).ToList();
        }
    }
}

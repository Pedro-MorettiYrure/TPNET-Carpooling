using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Delete(Viaje viaje)
        {
            _context.Viajes.Remove(viaje);
            _context.SaveChanges();
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


    }
}

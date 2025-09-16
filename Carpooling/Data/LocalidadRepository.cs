using Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class LocalidadRepository
    {
        private readonly TPIContext _context;

        public LocalidadRepository(TPIContext context)
        {
            _context = context; // DI inyecta el contexto
        }

        public void Add(Localidad localidad)
        {
            _context.Localidades.Add(localidad);
            _context.SaveChanges();
        }

        public IEnumerable<Localidad> GetAll()
        {
            return _context.Localidades.ToList();
        }

        public Localidad? Get(string cod)
        {
            return _context.Localidades.FirstOrDefault(p => p.codPostal == cod);
        }

        public bool Update(Localidad localidad)
        {
            var existing = _context.Localidades.Find(localidad.codPostal);
            if (existing != null)
            {
                existing.SetNombreLoc(localidad.nombreLoc);
                existing.SetCodPostal(localidad.codPostal);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(string cod)
        {
            var localidad = _context.Localidades.Find(cod);
            if (localidad != null)
            {
                _context.Localidades.Remove(localidad);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

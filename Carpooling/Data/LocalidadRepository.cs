using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class LocalidadRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public void Add(Localidad localidad)
        {
            using var context = CreateContext();
            context.Localidades.Add(localidad);
            context.SaveChanges();
        }

        public IEnumerable<Localidad> GetAll()
        {
            using var context = CreateContext();
            return context.Localidades
                //.Include(p => p.Localidad)
                .ToList();
        }

        public Localidad? Get(string cod)
        {
            using var context = CreateContext();
            return context.Localidades
                //.Include(p => p.Localidad)
                .FirstOrDefault(p => p.codPostal == cod);
        }

        public bool Update(Localidad localidad)
        {
            using var context = CreateContext();
            var existing = context.Localidades.Find(localidad.codPostal);
            if (existing != null)
            {
                existing.SetNombreLoc(localidad.nombreLoc);
                //existing.SetApellido(persona.Apellido);
                existing.SetCodPostal(localidad.codPostal);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(string cod)
        {
            using var context = CreateContext();
            var persona = context.Localidades.Find(cod);
            if (persona != null)
            {
                context.Localidades.Remove(persona);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

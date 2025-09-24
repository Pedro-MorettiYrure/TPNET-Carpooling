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

        public void Update(Viaje viaje)
        {
            _context.Viajes.Update(viaje);
            _context.SaveChanges();
        }

        

    }
}

using Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class UsuarioRepository
    {
        private readonly TPIContext _context;

        public UsuarioRepository(TPIContext context)
        {
            _context = context; // DI inyecta el contexto configurado
        }

        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public Usuario? GetByEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public Usuario? GetById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        }
    }
}

using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        // Constructor que EF y DI usarán
        public TPIContext(DbContextOptions<TPIContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ContraseñaHash).IsRequired();
            });

            // Configuración de Localidad
            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.HasKey(e => e.codPostal);
                entity.Property(e => e.nombreLoc).IsRequired().HasMaxLength(100);

                entity.HasData(
                    new { codPostal = "2000", nombreLoc = "Rosario" },
                    new { codPostal = "2607", nombreLoc = "Villa Cañas" }
                );
            });
        }
    }
}

using Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Data
{
    public class TPIContext : DbContext
    {
        //public DbSet<Persona> Personas { get; set; }
        public DbSet<Localidad> Localidades { get; set; }

        internal TPIContext()
        {
            this.Database.EnsureCreated(); // crea la BD si no existe
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Persona
            /*modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.Localidad)
                      .WithMany()
                      .HasForeignKey(e => e.LocalidadId);
            });*/

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

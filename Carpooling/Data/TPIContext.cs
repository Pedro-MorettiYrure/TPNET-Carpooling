using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Viaje> Viajes { get; set; }

        // Constructor que EF y DI usarán
        public TPIContext(DbContextOptions<TPIContext> options) : base(options) 
        {
            //this.Database.EnsureCreated();
        }

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
                entity.Property(e => e.TipoUsuario).IsRequired();
                entity.Property(e => e.Telefono).HasMaxLength(200);     //required?
                entity.Property(e => e.nroLicenciaConductor).HasMaxLength(50);
                entity.Property(e => e.fechaVencimientoLicencia).HasColumnType("datetime2");
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

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculos");
                entity.HasKey(e => e.IdVehiculo); // PK
                entity.Property(e => e.Patente).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Color).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Marca).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CantLugares).IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Vehiculos)
                      .HasForeignKey(e => e.IdUsuario)
                      .IsRequired()
                      //.OnDelete(DeleteBehavior.Cascade)
                      ; 
            });

            modelBuilder.Entity<Viaje>(entity =>
            {
                entity.ToTable("Viajes");
                entity.HasKey(v => v.IdViaje);
                entity.Property(v => v.Fecha).IsRequired();
                entity.Property(v => v.Hora).IsRequired();
                entity.Property(v => v.CantLugares).IsRequired();
                entity.Property(v => v.Estado).IsRequired();
                entity.Property(v => v.Comentario);
                entity.Property(v => v.Precio);
                entity.HasOne(v => v.Origen)
                      .WithMany()
                      .HasForeignKey(v => v.OrigenCodPostal)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(v => v.Destino)
                      .WithMany()
                      .HasForeignKey(v => v.DestinoCodPostal)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.NoAction);


            });


        }
    }
}

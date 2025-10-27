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
        public DbSet<SolicitudViaje> SolicitudesViaje { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public TPIContext(DbContextOptions<TPIContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ---------------- Usuario ----------------
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ContraseñaHash).IsRequired();
                entity.Property(e => e.TipoUsuario).IsRequired();
                entity.Property(e => e.Telefono).HasMaxLength(200);
                entity.Property(e => e.nroLicenciaConductor).HasMaxLength(50);
                entity.Property(e => e.fechaVencimientoLicencia).HasColumnType("datetime2");
                entity.HasMany(u => u.CalificacionesRecibidas)
                      .WithOne(c => c.Calificado)
                      .HasForeignKey(c => c.IdCalificado)
                      .OnDelete(DeleteBehavior.Restrict); // Evita borrar usuario si tiene calificaciones

                entity.HasMany(u => u.CalificacionesDadas)
                      .WithOne(c => c.Calificador)
                      .HasForeignKey(c => c.IdCalificador)
                      .OnDelete(DeleteBehavior.Restrict); // Evita borrar usuario si dio calificaciones


            });

            // ---------------- Localidad ----------------
            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.ToTable("Localidades");
                entity.HasKey(e => e.codPostal);
                entity.Property(e => e.nombreLoc).IsRequired().HasMaxLength(100);

                entity.HasData(
                    new { codPostal = "2000", nombreLoc = "Rosario" },
                    new { codPostal = "2607", nombreLoc = "Villa Cañas" }
                );
            });

            // ---------------- Vehiculo ----------------
            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculos");
                entity.HasKey(e => e.IdVehiculo);
                entity.Property(e => e.Patente).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Color).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Marca).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CantLugares).IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Vehiculos)
                      .HasForeignKey(e => e.IdUsuario)
                      .IsRequired();
            });

            // ---------------- Viaje ----------------
            modelBuilder.Entity<Viaje>(entity =>
            {
                entity.ToTable("Viajes");
                entity.HasKey(v => v.IdViaje);
                entity.Property(v => v.FechaHora).IsRequired();
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

                entity.HasOne(e => e.Conductor)
                      .WithMany()
                      .HasForeignKey(e => e.IdConductor)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(v => v.Vehiculo)
                     .WithMany()
                     .HasForeignKey(v => v.IdVehiculo)
                     .OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(v => v.Solicitudes)         
                       .WithOne(s => s.Viaje)                
                       .HasForeignKey(s => s.IdViaje)        // Clave foránea en SolicitudViaje
                       .OnDelete(DeleteBehavior.Cascade);            
            });

            // ---------------- Solicitud Viaje ----------------
            modelBuilder.Entity<SolicitudViaje>(entity =>
            {
                entity.ToTable("SolicitudesViaje");
                entity.HasKey(e => e.IdSolicitud);
                entity.Property(e => e.SolicitudFecha).IsRequired();
                entity.Property(e => e.Estado).IsRequired();

                // Asegura que la relación con Viaje use IdViaje y la colección Solicitudes
                entity.HasOne(s => s.Viaje)
                      .WithMany(v => v.Solicitudes) // <-- Usa la propiedad de colección en Viaje
                      .HasForeignKey(s => s.IdViaje) // <-- Usa la clave foránea correcta
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade); // Cascade parece más lógico aquí

                entity.HasOne(s => s.Pasajero)
                      .WithMany() // Asume que Usuario no tiene colección específica
                      .HasForeignKey(s => s.IdPasajero)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.NoAction); // Mantén NoAction si es necesario
            });
            // ---------------- Calificaciones ----------------

            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.ToTable("Calificaciones");
                entity.HasKey(c => c.IdCalificacion);
                entity.Property(c => c.Puntaje).IsRequired();
                entity.Property(c => c.Comentario).HasMaxLength(500); // Límite para comentarios
                entity.Property(c => c.FechaHora).IsRequired();
                entity.Property(c => c.RolCalificado).IsRequired();

                // Relación con Viaje
                entity.HasOne(c => c.Viaje)
                      .WithMany() // Un viaje puede tener muchas calificaciones
                      .HasForeignKey(c => c.IdViaje)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict); // No borrar viaje si tiene calificaciones? O Cascade?

                // Las relaciones con Calificador y Calificado ya están definidas en Usuario
            });
        }

        



    }
}


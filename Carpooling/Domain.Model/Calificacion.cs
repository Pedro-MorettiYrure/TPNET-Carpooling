// Domain.Model/Calificacion.cs
using System;

namespace Domain.Model
{
    public class Calificacion
    {
        public int IdCalificacion { get; set; }
        public int IdViaje { get; set; }        // Viaje al que pertenece la calificación
        public int IdCalificador { get; set; }  // Quién califica
        public int IdCalificado { get; set; }   // A quién califican
        public RolCalificado RolCalificado { get; set; } // Rol del calificado en ESE viaje (Conductor o Pasajero)
        public int Puntaje { get; set; }        // Ej: 1 a 5
        public string? Comentario { get; set; }
        public DateTime FechaHora { get; set; }

        // Propiedades de navegación
        public virtual Viaje Viaje { get; set; } = null!;
        public virtual Usuario Calificador { get; set; } = null!;
        public virtual Usuario Calificado { get; set; } = null!;

        // Constructor privado para EF
        private Calificacion() { }

        // Método Fábrica para crear calificaciones
        public static Calificacion Crear(int idViaje, int idCalificador, int idCalificado, RolCalificado rolCalificado, int puntaje, string? comentario)
        {
            if (puntaje < 1 || puntaje > 5)
                throw new ArgumentOutOfRangeException(nameof(puntaje), "El puntaje debe estar entre 1 y 5.");
            if (idCalificador == idCalificado)
                throw new ArgumentException("Un usuario no puede calificarse a sí mismo.");

            return new Calificacion
            {
                IdViaje = idViaje,
                IdCalificador = idCalificador,
                IdCalificado = idCalificado,
                RolCalificado = rolCalificado,
                Puntaje = puntaje,
                Comentario = comentario?.Trim(), // Quita espacios extra
                FechaHora = DateTime.Now
            };
        }
    }

    // Enum para saber qué rol se está calificando
    public enum RolCalificado
    {
        Conductor = 0,
        Pasajero = 1
    }
}
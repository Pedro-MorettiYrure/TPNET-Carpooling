using System;

namespace Domain.Model
{
    public class Calificacion
    {
        public int IdCalificacion { get; set; }
        public int IdViaje { get; set; }       
        public int IdCalificador { get; set; }  
        public int IdCalificado { get; set; }   
        public RolCalificado RolCalificado { get; set; }
        public int Puntaje { get; set; }        
        public string? Comentario { get; set; }
        public DateTime FechaHora { get; set; }

        public virtual Viaje Viaje { get; set; } = null!;
        public virtual Usuario Calificador { get; set; } = null!;
        public virtual Usuario Calificado { get; set; } = null!;

        // Constructor privado para EF
        private Calificacion() { }

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
                Comentario = comentario?.Trim(), 
                FechaHora = DateTime.Now
            };
        }
    }

    public enum RolCalificado
    {
        Conductor = 0,
        Pasajero = 1
    }
}
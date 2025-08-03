using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Model
{
    public class Localidad
    {
        public string codPostal { get; set; }
        public string nombreLoc { get; set; }

        public Localidad(string codigopostal, string nombre)
        {
            SetCodPostal(codigopostal);
            SetNombreLoc(nombre);
        }

        public void SetNombreLoc(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            nombreLoc = nombre;
        }

        public void SetCodPostal(string codigopostal)
        {
            if (string.IsNullOrWhiteSpace(codigopostal))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(codigopostal));
            codPostal = codigopostal;
        }
    }
}

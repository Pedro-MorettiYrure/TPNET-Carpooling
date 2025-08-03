using Domain.Model;

namespace Data
{
    public class LocalidadInMemory
    {
        //No es ThreadSafe pero sirve para el proposito del ejemplo        
        public static List<Localidad> Localidades;

        static LocalidadInMemory()
        {
            Localidades = new List<Localidad>
            {
                new Localidad("2000","Rosario"),
                new Localidad("2607","Villa Canas"),
                new Localidad("3000","Cordoba"),
            };
        }
    }
}

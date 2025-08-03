using Domain.Model;

namespace Data
{
    public class VehiculoInMemory
    {
        public static List<Vehiculo> Vehiculos;

        static VehiculoInMemory()
        {
            Vehiculos = new List<Vehiculo>
            {
                new Vehiculo("AIA230","Etios", 5, "Blanco", "Toyota"),
                new Vehiculo("AB234PE","Ford", 4, "Rojo", "Ford"),
                new Vehiculo("LM925OR","Toyota", 5, "Dorado", "Toyota"),
            };
        }
    }
}

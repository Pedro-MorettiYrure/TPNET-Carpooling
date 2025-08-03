using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Vehiculo
    {
        public string patente { get; set; }
        public string modelo { get; set; }
        public int cantLugares { get; set; }
        public string color { get; set; }
        public string marca { get; set; }

        public Vehiculo(string patenteV, string modeloV, int cantidadLugares, string colorV, string marcaV)
        {
            SetPatente(patenteV);
            SetModelo(modeloV);
            SetCantLugares(cantidadLugares);
            SetColor(colorV);
            SetMarca(marcaV);
        }

        public void SetPatente(string patenteV)
        {
            if (string.IsNullOrWhiteSpace(patenteV))
                throw new ArgumentException("La patente no puede ser nulo o vacío.", nameof(patenteV));
            patente = patenteV;
        }
        public void SetModelo(string modeloV)
        {
            if (string.IsNullOrWhiteSpace(modeloV))
                throw new ArgumentException("El modelo no puede ser nulo o vacío.", nameof(modeloV));
            modelo = modeloV;
        }
        public void SetCantLugares(int cantidadLugares)
        {
            if (cantidadLugares <= 0)
                throw new ArgumentException("La cantidad de lugares debe ser mayor que cero.", nameof(cantidadLugares));
            cantLugares = cantidadLugares;
        }
        public void SetColor(string colorV)
        {
            if (string.IsNullOrWhiteSpace(colorV))
                throw new ArgumentException("El color no puede ser nulo o vacío.", nameof(colorV));
            color = colorV;
        }
        public void SetMarca(string marcaV)
        {
            if (string.IsNullOrWhiteSpace(marcaV))
                throw new ArgumentException("El color no puede ser nulo o vacío.", nameof(marcaV));
            marca = marcaV;
        }
    }
}



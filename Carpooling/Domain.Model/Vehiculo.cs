using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Vehiculo
    {
        public int IdVehiculo { get; set; }   // PK
        public string Patente { get; set; }
        public string Modelo { get; set; }
        public int CantLugares { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }

        // Relación con Usuario
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }   // Navigation property

        public Vehiculo() { }

        public Vehiculo(string patente, string modelo, int cantLugares, string color, string marca, int idUsuario)
        {
            SetPatente(patente);
            SetModelo(modelo);
            SetCantLugares(cantLugares);
            SetColor(color);
            SetMarca(marca);
            IdUsuario = idUsuario;
        }

        public void SetPatente(string patente) =>
            Patente = string.IsNullOrWhiteSpace(patente) ? throw new ArgumentException("La patente no puede ser vacía") : patente;

        public void SetModelo(string modelo) =>
            Modelo = string.IsNullOrWhiteSpace(modelo) ? throw new ArgumentException("El modelo no puede ser vacío") : modelo;

        public void SetCantLugares(int cantLugares) =>
            CantLugares = cantLugares <= 0 ? throw new ArgumentException("La cantidad de lugares debe ser mayor que cero") : cantLugares;

        public void SetColor(string color) =>
            Color = string.IsNullOrWhiteSpace(color) ? throw new ArgumentException("El color no puede ser vacío") : color;

        public void SetMarca(string marca) =>
            Marca = string.IsNullOrWhiteSpace(marca) ? throw new ArgumentException("La marca no puede ser vacía") : marca;
    }

}



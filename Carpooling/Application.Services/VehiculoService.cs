using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class VehiculoService
    {
        public VehiculoDTO Add(VehiculoDTO dto)
        {
            // Validar que la patente no esté duplicada
            if (VehiculoInMemory.Vehiculos.Any(v => v.patente.Equals(dto.Patente, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Ya existe un vehículo con la patente '{dto.Patente}'.");
            }
            Vehiculo vehiculo = new Vehiculo(dto.Patente, dto.Modelo, dto.CantLugares, dto.Color, dto.Marca);
            VehiculoInMemory.Vehiculos.Add(vehiculo);
            return dto;
        }
        public bool Delete(string patente)
        {
            Vehiculo? vehiculoToDelete = VehiculoInMemory.Vehiculos.Find(x => x.patente == patente);
            if (vehiculoToDelete != null)
            {
                VehiculoInMemory.Vehiculos.Remove(vehiculoToDelete);
                return true;
            }
            else
            {
                return false;
            }
        }
        public VehiculoDTO Get(string patente)
        {
            Vehiculo? vehiculo = VehiculoInMemory.Vehiculos.Find(x => x.patente == patente);
            if (vehiculo == null)
                return null;
            return new VehiculoDTO
            {
                Patente = vehiculo.patente,
                Modelo = vehiculo.modelo,
                CantLugares = vehiculo.cantLugares,
                Color = vehiculo.color,
                Marca = vehiculo.marca,
            };
        }
        public IEnumerable<VehiculoDTO> GetAll()
        {
            return VehiculoInMemory.Vehiculos.Select(vehiculo => new VehiculoDTO
            {
                Patente = vehiculo.patente,
                Modelo = vehiculo.modelo,
                CantLugares = vehiculo.cantLugares,
                Color = vehiculo.color,
                Marca = vehiculo.marca,
            }).ToList();
        }

        public bool Update(VehiculoDTO dto)
        {
            Vehiculo? vehiculoToUpdate = VehiculoInMemory.Vehiculos.Find(x => x.patente == dto.Patente);
            if (vehiculoToUpdate != null)
            {
                if (VehiculoInMemory.Vehiculos.Any(v => v.patente != dto.Patente && v.patente.Equals(dto.Patente, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new ArgumentException($"Ya existe un vehículo con la patente '{dto.Patente}'.");
                }
                vehiculoToUpdate.SetModelo(dto.Modelo);
                vehiculoToUpdate.SetCantLugares(dto.CantLugares);
                vehiculoToUpdate.SetColor(dto.Color);
                vehiculoToUpdate.SetMarca(dto.Marca);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

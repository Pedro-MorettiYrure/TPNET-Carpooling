using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class VehiculoService
    {
        private readonly VehiculoRepository _repo;

        public VehiculoService(VehiculoRepository repo)
        {
            _repo = repo;
        }

        public VehiculoDTO Add(VehiculoDTO dto)
        {
            // Primero verificamos si ya existe un vehículo con la misma patente
            if (_repo.Get(dto.Patente) != null)
                throw new ArgumentException($"Ya existe un vehículo con la patente '{dto.Patente}'.");

            
            Vehiculo vehiculo = new Vehiculo(
                dto.Patente,
                dto.Modelo,
                dto.CantLugares,
                dto.Color,
                dto.Marca,
                dto.IdUsuario  
            );
          
            _repo.Add(vehiculo);

            return dto;
        }


        public bool Delete(string patente, int idUsuario)
        {
            var vehiculo = _repo.Get(patente);
            if (vehiculo != null && vehiculo.IdUsuario == idUsuario)
            {
                _repo.Delete(vehiculo);
                return true;
            }
            return false;
        }

        public bool Update(VehiculoDTO dto)
        {
            var vehiculo = _repo.Get(dto.Patente);
            if (vehiculo != null && vehiculo.IdUsuario == dto.IdUsuario)
            {
                vehiculo.SetModelo(dto.Modelo);
                vehiculo.SetCantLugares(dto.CantLugares);
                vehiculo.SetColor(dto.Color);
                vehiculo.SetMarca(dto.Marca);

                _repo.Update(vehiculo);
                return true;
            }
            return false;
        }

        public IEnumerable<VehiculoDTO> GetByUsuario(int idUsuario)
        {
            return _repo.GetAllByUsuario(idUsuario).Select(v => new VehiculoDTO
            {
                Patente = v.Patente,
                Modelo = v.Modelo,
                CantLugares = v.CantLugares,
                Color = v.Color,
                Marca = v.Marca,
                IdUsuario = v.IdUsuario
            }).ToList();
        }
    }

}


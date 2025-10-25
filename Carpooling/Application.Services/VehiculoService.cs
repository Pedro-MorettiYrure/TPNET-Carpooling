using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class VehiculoService
    {
        private readonly VehiculoRepository _repo;
        private readonly TPIContext _context;


        public VehiculoService(VehiculoRepository repo, TPIContext context)
        {
            _repo = repo;
            _context = context;
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
                bool estaEnUso = _context.Viajes.Any(v => v.IdVehiculo == vehiculo.IdVehiculo);
                if (estaEnUso)
                {
                    throw new InvalidOperationException($"Existen viajes que referencian al vehiculo '{vehiculo.Patente}'");
                }
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
                IdVehiculo = v.IdVehiculo,
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


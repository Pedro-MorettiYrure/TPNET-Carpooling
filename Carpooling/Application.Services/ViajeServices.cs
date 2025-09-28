using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DTOs;
using Domain.Model;

namespace Application.Services
{
    public class ViajeServices
    {
        private readonly ViajeRepository _viajeRepo;
        private readonly VehiculoRepository _vehiculoRepo;

        public ViajeServices(ViajeRepository viajeRepo, VehiculoRepository vehiculoRepo)
        {
            _viajeRepo = viajeRepo;
            _vehiculoRepo = vehiculoRepo;
        }

        public ViajeDTO Add(ViajeDTO dto)
        {
            // Validar que el vehículo exista y pertenezca al conductor
            var vehiculo = _vehiculoRepo.GetById(dto.IdVehiculo);
            if (vehiculo == null)
            {
                throw new ArgumentException($"Vehículo con Id {dto.IdVehiculo} no encontrado para el conductor {dto.IdConductor}");
            }


            if (vehiculo == null || vehiculo.IdUsuario != dto.IdConductor)
                throw new ArgumentException("El vehículo no pertenece al conductor.");

            if (dto.CantLugares > vehiculo.CantLugares)
                throw new ArgumentException("La cantidad de lugares del viaje no puede superar los lugares disponibles del vehículo.");

            Viaje viaje = Viaje.Crear(
            dto.FechaHora,
            dto.CantLugares,
            dto.Precio,
            dto.Comentario,
            dto.OrigenCodPostal,
            dto.DestinoCodPostal,
            dto.IdConductor,
            vehiculo.CantLugares   
            );


            viaje.IdVehiculo = dto.IdVehiculo;

            _viajeRepo.Add(viaje);

            return dto;
        }

        public bool Delete(int idViaje)
        {
            var viaje = _viajeRepo.Get(idViaje);
            if (viaje != null)
            {
                _viajeRepo.Delete(viaje);
                return true;
            }
            return false;
        }

        public ViajeDTO? Get(int idViaje)
        {
            var l = _viajeRepo.Get(idViaje);
            if (l == null) return null;

            return new ViajeDTO
            {
                IdViaje = l.IdViaje,
                FechaHora = l.FechaHora,
                CantLugares = l.CantLugares,
                Precio = l.Precio,
                Comentario = l.Comentario,
                Estado = l.Estado,
                OrigenCodPostal = l.OrigenCodPostal,
                DestinoCodPostal = l.DestinoCodPostal,
                IdConductor = l.IdConductor
            };
        }

        public IEnumerable<ViajeDTO> GetAll()
        {
            return _viajeRepo.GetAll().Select(l => new ViajeDTO
            {
                IdViaje = l.IdViaje,
                FechaHora = l.FechaHora,
                CantLugares = l.CantLugares,
                Precio = l.Precio,
                Comentario = l.Comentario,
                Estado = l.Estado,
                OrigenCodPostal = l.OrigenCodPostal,
                DestinoCodPostal = l.DestinoCodPostal,
                IdConductor = l.IdConductor
            }).ToList();
        }

        public IEnumerable<ViajeDTO> GetAllByConductor(int idUsuario)
        {
            return _viajeRepo.GetAllByConductor(idUsuario).Select(l => new ViajeDTO
            {
                IdViaje = l.IdViaje,
                FechaHora = l.FechaHora,
                CantLugares = l.CantLugares,
                Precio = l.Precio,
                Comentario = l.Comentario,
                Estado = l.Estado,
                OrigenCodPostal = l.OrigenCodPostal,
                DestinoCodPostal = l.DestinoCodPostal,
                IdConductor = l.IdConductor
            }).ToList();
        }

        public bool Update(ViajeDTO dto)
        {
            var vehiculo = _vehiculoRepo.GetById(dto.IdVehiculo);

            if (vehiculo == null || vehiculo.IdUsuario != dto.IdConductor)
                throw new ArgumentException("El vehículo no pertenece al conductor.");

            if (dto.CantLugares > vehiculo.CantLugares)
                throw new ArgumentException("La cantidad de lugares del viaje no puede superar los lugares disponibles del vehículo.");

            Viaje viaje = Viaje.Crear(
            dto.FechaHora,
            dto.CantLugares,
            dto.Precio,
            dto.Comentario,
            dto.OrigenCodPostal,
            dto.DestinoCodPostal,
            dto.IdConductor,
            vehiculo.CantLugares   // 👈 acá va la capacidad real del vehículo
        );

            viaje.IdViaje = dto.IdViaje;

            return _viajeRepo.Update(viaje);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DTOs;
using Domain.Model;

namespace Application.Services
{
    public class ViajeServices
    {
        private readonly ViajeRepository _repo;

        public ViajeServices(ViajeRepository repo)
        {
            _repo = repo;
        }

        public ViajeDTO Add(ViajeDTO dto)
        {
            //verificar no existe un viaje en la misma fecha
            //if (_repo.Get())
            //    throw new ArgumentException($"Ya tienes un viaje programado para esa fecha y hora")

            Viaje viaje = Viaje.Crear(
                dto.FechaHora,
                dto.CantLugares,
                dto.Precio,
                dto.Comentario,
                dto.OrigenCodPostal,
                dto.DestinoCodPostal,
                dto.IdConductor
                );

            _repo.Add(viaje);

            return dto;
        }

        public bool Delete(int idViaje)
        {
            var viaje = _repo.Get(idViaje);
            if (viaje != null)
            {
                _repo.Delete(viaje);
                return true;
            }
            return false;
        }
        public ViajeDTO? Get(int idViaje)
        {
            var l = _repo.Get(idViaje);
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
            return _repo.GetAll().Select(l => new ViajeDTO
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
            return _repo.GetAllByConductor(idUsuario).Select(l => new ViajeDTO
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
            Viaje viaje = Viaje.Crear (dto.FechaHora, dto.CantLugares, dto.Precio, dto.Comentario,
                                        dto.OrigenCodPostal, dto.DestinoCodPostal, dto.IdConductor);
            viaje.IdViaje = dto.IdViaje;
            //_repo.Update (viaje);
            return _repo.Update(viaje);
            }
    }
}

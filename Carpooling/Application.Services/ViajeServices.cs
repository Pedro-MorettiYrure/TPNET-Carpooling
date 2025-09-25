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
    internal class ViajeServices
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

        public bool Delete(int idViaje, int idConductor)
        {
            var viaje = _repo.Get(idViaje);
            if (viaje != null && viaje.IdConductor == idConductor)
            {
                _repo.Delete(viaje);
                return true;
            }
            return false;
        }
    }
}

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
        private readonly UsuarioRepository _usuarioRepo;

        public ViajeServices(ViajeRepository viajeRepo, VehiculoRepository vehiculoRepo, UsuarioRepository usuarioRepo)
        {
            _viajeRepo = viajeRepo;
            _vehiculoRepo = vehiculoRepo;
            _usuarioRepo = usuarioRepo;
        }

        public ViajeDTO Add(ViajeDTO dto)
        {
            // validacion de licencia del conductor
            var conductor = _usuarioRepo.GetById(dto.IdConductor);

            // validamos que el conductor exista por seguridad 
            if (conductor == null || conductor.TipoUsuario != TipoUsuario.PasajeroConductor)
            {
                throw new ArgumentException("El ID de usuario proporcionado no corresponde a un conductor válido.");
            }

            // validamos la fecha de vencimiento de la licencia
            if (conductor.fechaVencimientoLicencia == null || conductor.fechaVencimientoLicencia.Value.Date < dto.FechaHora.Date)
            {
                throw new ArgumentException("La licencia del conductor está vencida. No se puede publicar el viaje.");
            }

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
            vehiculo.CantLugares, 
            vehiculo.IdVehiculo
            );


            //viaje.IdVehiculo = dto.IdVehiculo;

            _viajeRepo.Add(viaje);

            return dto;
        }

        public bool Delete(int idViaje)
        {
            var viaje = _viajeRepo.Get(idViaje);
            if (viaje != null)
            {
                //  BAJA LÓGICA (SOFT DELETE)
                // validar si ya está realizado
                if (viaje.Estado == EstadoViaje.Realizado)
                {
                    throw new InvalidOperationException("No se puede cancelar un viaje que ya fue realizado.");
                }

                // cambiamos el estado a Cancelado (Baja Lógica)
                viaje.Estado = EstadoViaje.Cancelado;

                return _viajeRepo.Update(viaje);

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
                IdConductor = l.IdConductor,
                IdVehiculo = l.IdVehiculo
            }).ToList();
        }


        public bool Update(ViajeDTO dto)
        {
            try
            {
                var viajeExistente = _viajeRepo.Get(dto.IdViaje);
                if (viajeExistente == null)
                {
                    return false;
                }

                if (viajeExistente.Estado != EstadoViaje.Pendiente)
                {
                    throw new InvalidOperationException("Solo se pueden editar viajes en estado Pendiente.");
                }

                var conductor = _usuarioRepo.GetById(dto.IdConductor);
                var vehiculo = _vehiculoRepo.GetById(dto.IdVehiculo);

                if (conductor == null || conductor.TipoUsuario != TipoUsuario.PasajeroConductor)
                {
                    throw new ArgumentException("El ID de usuario proporcionado no corresponde a un conductor válido.");
                }
                if (conductor.fechaVencimientoLicencia == null || conductor.fechaVencimientoLicencia.Value.Date < dto.FechaHora.Date)
                {
                    throw new ArgumentException("La licencia del conductor está vencida en la fecha del viaje.");
                }
                if (vehiculo == null || vehiculo.IdUsuario != dto.IdConductor)
                {
                    throw new ArgumentException("El vehículo no pertenece al conductor.");
                }
                if (dto.CantLugares > vehiculo.CantLugares)
                {
                    throw new ArgumentException("La cantidad de lugares del viaje no puede superar los lugares disponibles del vehículo.");
                }

            // actualizamos campos del viaje existente con los datos del DTO
            
                viajeExistente.SetFechaHora(dto.FechaHora);
                viajeExistente.SetCantLugares(dto.CantLugares);
                viajeExistente.SetPrecio(dto.Precio);
                viajeExistente.SetComentario(dto.Comentario);
                viajeExistente.OrigenCodPostal = dto.OrigenCodPostal;
                viajeExistente.DestinoCodPostal = dto.DestinoCodPostal;
                viajeExistente.IdVehiculo = dto.IdVehiculo;

                // guardamos los cambios
                return _viajeRepo.Update(viajeExistente);
            }
            catch (ArgumentException)
            {
                // hacemos q el controlador maneje el error
                throw;
            }
        }

        public IEnumerable<ViajeDTO> BuscarViajesDisponibles(string origenCodPostal, string destinoCodPostal)
        {
            var viajes = _viajeRepo.GetViajesDisponiblesPorRuta(origenCodPostal, destinoCodPostal);

            // Mapear la lista de Viaje a ViajeDTO directamente aquí
            return viajes.Select(v => new ViajeDTO
            {
                IdViaje = v.IdViaje,
                FechaHora = v.FechaHora,
                CantLugares = v.CantLugares,
                Precio = v.Precio,
                Comentario = v.Comentario,
                Estado = v.Estado, // O v.Estado.ToString() si el DTO lo necesita como string
                OrigenCodPostal = v.OrigenCodPostal,
                DestinoCodPostal = v.DestinoCodPostal,
                IdConductor = v.IdConductor,
                IdVehiculo = v.IdVehiculo,
                // Asegúrate de que Origen y Destino se carguen en el repo con .Include()
                NombreOrigen = v.Origen?.nombreLoc,
                NombreDestino = v.Destino?.nombreLoc
                // Agrega otros campos mapeados si es necesario
            }).ToList();
        }
    }
}

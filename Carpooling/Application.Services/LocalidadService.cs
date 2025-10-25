using Data;
using Domain.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class LocalidadService
    {
        private readonly LocalidadRepository _repo;
        private readonly TPIContext _context;
        // Recibe el repositorio por constructor
        public LocalidadService(LocalidadRepository repo, TPIContext context)
        {
            _repo = repo;
            _context = context;
        }

        public LocalidadDTO Add(LocalidadDTO dto)
        {
            // Validar que no exista en DB
            if (_repo.Get(dto.CodPostal) != null)
                throw new ArgumentException($"Ya existe una localidad con el Código Postal '{dto.CodPostal}'.");

            Localidad localidad = new Localidad(dto.CodPostal, dto.Nombre);
            _repo.Add(localidad);

            return dto;
        }

        public bool Delete(string cod)
        {
            bool estaEnUso = _context.Viajes
                                .Any(v => v.OrigenCodPostal == cod || v.DestinoCodPostal == cod);
            if (estaEnUso)
            {
                throw new InvalidOperationException($"Existen viajes que referencian a la Ciudad con Cod Postal '{cod}'");
            }
            return _repo.Delete(cod);
        }

        public LocalidadDTO? Get(string cod)
        {
            var loc = _repo.Get(cod);
            if (loc == null) return null;

            return new LocalidadDTO
            {
                CodPostal = loc.codPostal,
                Nombre = loc.nombreLoc
            };
        }

        public IEnumerable<LocalidadDTO> GetAll()
        {
            return _repo.GetAll().Select(l => new LocalidadDTO
            {
                CodPostal = l.codPostal,
                Nombre = l.nombreLoc
            }).ToList();
        }

        public bool Update(LocalidadDTO dto)
        {
            var loc = new Localidad(dto.CodPostal, dto.Nombre);
            return _repo.Update(loc);
        }
    }
}

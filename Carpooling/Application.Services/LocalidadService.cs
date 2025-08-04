using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class LocalidadService
    {
        public LocalidadDTO Add(LocalidadDTO dto)
        {
            // Validar que el cod postal no esté duplicado
            if (LocalidadInMemory.Localidades.Any(l => l.codPostal.Equals(dto.CodPostal, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Ya existe un cliente con el Email '{dto.CodPostal}'.");
            }


            Localidad localidad = new Localidad(dto.CodPostal, dto.Nombre);

            LocalidadInMemory.Localidades.Add(localidad);


            return dto;
        }

        public bool Delete(string cod)
        {
            Localidad? localidadToDelete = LocalidadInMemory.Localidades.Find(x => x.codPostal == cod);

            if (localidadToDelete != null)
            {
                LocalidadInMemory.Localidades.Remove(localidadToDelete);

                return true;
            }
            else
            {
                return false;
            }
        }

        public LocalidadDTO Get(string CodPostal)
        {
            Localidad? localidad = LocalidadInMemory.Localidades.Find(x => x.codPostal == CodPostal);

            if (localidad == null)
                return null;

            return new LocalidadDTO
            {
                CodPostal = localidad.codPostal,
                Nombre = localidad.nombreLoc,
            };
        }

        public IEnumerable<LocalidadDTO> GetAll()
        {
            return LocalidadInMemory.Localidades.Select(localidad => new LocalidadDTO
            {
                CodPostal = localidad.codPostal,
                Nombre = localidad.nombreLoc,
            }).ToList();
        }

        public bool Update(LocalidadDTO dto)
        {
            Localidad? localidadToUpdate = LocalidadInMemory.Localidades.Find(x => x.codPostal == dto.CodPostal);

            if (localidadToUpdate != null)
            {
                // Validar que el codigo postal no esté duplicado (excluyendo el cliente actual)
               


                localidadToUpdate.SetNombreLoc(dto.Nombre);
                

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

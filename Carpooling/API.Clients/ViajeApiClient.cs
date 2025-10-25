using Domain.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace API.Clients
{
    public class ViajeApiClient
    {
        private static readonly HttpClient client;

        static ViajeApiClient()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7139/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<IEnumerable<ViajeDTO>> GetByConductorAsync(int idUsuario)
        {
            HttpResponseMessage response = await client.GetAsync($"viajes/conductor/{idUsuario}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ViajeDTO>>();
        }

        public static async Task<ViajeDTO> GetAsync(string idViaje)
        {
            HttpResponseMessage response = await client.GetAsync($"viajes/{idViaje}");
            return await response.Content.ReadFromJsonAsync<ViajeDTO>();
        }

        public static async Task<IEnumerable<ViajeDTO>> BuscarViajesAsync(string origenCodPostal, string destinoCodPostal)
        {
            string requestUri = $"viajes/buscar?origen={origenCodPostal}&destino={destinoCodPostal}"; //Usamos requestUri xq esta llamada a la API necesita enviar datos (el origen y el destino) como query parameters en la URL

            HttpResponseMessage response = await client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al buscar viajes: {errorContent}");
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<ViajeDTO>>();
        }


        public static async Task AddAsync(ViajeDTO viaje)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"viajes", viaje);

            if (!response.IsSuccessStatusCode)
            {
                // 1. Intentar leer el cuerpo de la respuesta, donde está el mensaje de error del servicio.
                string errorContent = await response.Content.ReadAsStringAsync();

                // 2. Intentar lanzar una excepción más descriptiva.
                // Asumo que el cuerpo del error contiene el mensaje "La licencia..."
                // Si el error es 400 (Bad Request), usamos el mensaje del cuerpo.
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // Lanzamos una nueva excepción con el mensaje de validación del backend.
                    throw new Exception(errorContent);
                }
                else
                {
                    // Para otros errores (500, etc.), lanzamos la excepción genérica con el estado.
                    response.EnsureSuccessStatusCode();
                }
            }
            
        }
        

        public static async Task DeleteAsync(int idViaje)
        {
            HttpResponseMessage response = await client.DeleteAsync($"viajes/{idViaje}");
            response.EnsureSuccessStatusCode();
        }

        public static async Task UpdateAsync(ViajeDTO viaje)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"viajes/{viaje.IdViaje}", viaje);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new Exception(errorContent);
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }


    }
}

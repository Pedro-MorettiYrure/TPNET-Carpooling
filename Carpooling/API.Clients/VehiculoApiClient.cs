using DTOs;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace API.Clients
{
    public class VehiculoApiClient
    {
        private static readonly HttpClient client;

        static VehiculoApiClient()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7139/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<IEnumerable<VehiculoDTO>> GetByUsuarioAsync(int idUsuario)
        {
            HttpResponseMessage response = await client.GetAsync($"vehiculos/{idUsuario}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<VehiculoDTO>>();
        }

        public static async Task<VehiculoDTO> GetAsync(string patente, int idUsuario)
        {
            HttpResponseMessage response = await client.GetAsync($"vehiculos/{patente}/{idUsuario}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<VehiculoDTO>();
        }

        public static async Task AddAsync(VehiculoDTO vehiculo)
        {
            // Ahora POST pide /vehiculos/{idUsuario}
            HttpResponseMessage response = await client.PostAsJsonAsync($"vehiculos/{vehiculo.IdUsuario}", vehiculo);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteAsync(string patente, int idUsuario)
        {
            HttpResponseMessage response = await client.DeleteAsync($"vehiculos/{patente}/{idUsuario}");
            response.EnsureSuccessStatusCode();
        }

        public static async Task UpdateAsync(VehiculoDTO vehiculo)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"vehiculos/{vehiculo.Patente}/{vehiculo.IdUsuario}", vehiculo);
            response.EnsureSuccessStatusCode();
        }
    }
}

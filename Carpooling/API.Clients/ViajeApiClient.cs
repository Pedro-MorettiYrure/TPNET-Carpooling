using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using DTOs;

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

        public static async Task AddAsync(ViajeDTO viaje)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"viajes", viaje);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteAsync(int idViaje)
        {
            HttpResponseMessage response = await client.DeleteAsync($"viajes/{idViaje}");
            response.EnsureSuccessStatusCode();
        }

        public static async Task UpdateAsync(ViajeDTO viaje)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"viajes/{viaje.IdViaje}", viaje);
            response.EnsureSuccessStatusCode();
        }
    }
}

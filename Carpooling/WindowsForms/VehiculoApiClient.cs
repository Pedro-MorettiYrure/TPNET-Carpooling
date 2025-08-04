using DTOs;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WindowsForms
{
    public class VehiculoApiClient
    {
        private static HttpClient veh = new HttpClient();
        static VehiculoApiClient()
        {
            veh.BaseAddress = new Uri("http://localhost:5183/");
            veh.DefaultRequestHeaders.Accept.Clear();
            veh.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async Task<VehiculoDTO> GetAsync(string patente)
        {
            VehiculoDTO vehiculo = null;
            HttpResponseMessage response = await veh.GetAsync("vehiculos/" + patente);
            if (response.IsSuccessStatusCode)
            {
                vehiculo = await response.Content.ReadAsAsync<VehiculoDTO>();
            }
            return vehiculo;
        }
        public static async Task<IEnumerable<VehiculoDTO>> GetAllAsync()
        {
            IEnumerable<VehiculoDTO> vehiculos = null;
            HttpResponseMessage response = await veh.GetAsync("vehiculos");
            if (response.IsSuccessStatusCode)
            {
                vehiculos = await response.Content.ReadAsAsync<IEnumerable<VehiculoDTO>>();
            }
            return vehiculos;
        }
        public async static Task AddAsync(VehiculoDTO vehiculo)
        {
            HttpResponseMessage response = await veh.PostAsJsonAsync("vehiculos", vehiculo);
            response.EnsureSuccessStatusCode();
        }
        public static async Task DeleteAsync(string patente)
        {
            HttpResponseMessage response = await veh.DeleteAsync("vehiculos/" + patente);
            response.EnsureSuccessStatusCode();
        }
        public static async Task UpdateAsync(VehiculoDTO vehiculo)
        {
            HttpResponseMessage response = await veh.PutAsJsonAsync("vehiculos", vehiculo);
            response.EnsureSuccessStatusCode();
        }
    }
}
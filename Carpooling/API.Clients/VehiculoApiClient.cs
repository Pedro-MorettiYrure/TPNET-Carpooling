using DTOs;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
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
            try
            {
                HttpResponseMessage response = await veh.GetAsync("vehiculos/" + patente);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<VehiculoDTO>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener vehiculo con patente {patente}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener vehiculo con patente {patente}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener vehiculo con patente {patente}: {ex.Message}", ex);
            }
        }
        public static async Task<IEnumerable<VehiculoDTO>> GetAllAsync()
        {
            try
            {
                HttpResponseMessage response = await veh.GetAsync("vehiculos");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<VehiculoDTO>>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener lista de vehiculos. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener lista de vehiculos: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener lista de vehiculos: {ex.Message}", ex);
            }
        }

        public async static Task AddAsync(VehiculoDTO vehiculo)
        {
            try
            {
                HttpResponseMessage response = await veh.PostAsJsonAsync("vehiculos", vehiculo);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al agregar vehiculo. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al agregar vehiculo: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al agregar vehiculo: {ex.Message}", ex);

            }
        }
        public static async Task DeleteAsync(string patente)
        {
            try
            {
                HttpResponseMessage response = await veh.DeleteAsync("vehiculos/" + patente);
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al eliminar vehiculo con patente {patente}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al eliminar vehiculo con patente {patente}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al eliminar vehiculo con patente {patente}: {ex.Message}", ex);
            }
        }
        public static async Task UpdateAsync(VehiculoDTO vehiculo)
        {
            try
            {
                HttpResponseMessage response = await veh.PutAsJsonAsync("vehiculos", vehiculo);
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar vehiculo con patente {vehiculo.Patente}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al actualizar vehiculo con patente {vehiculo.Patente}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al actualizar vehiculo con patente {vehiculo.Patente}: {ex.Message}", ex);
            }
        }
    }
}

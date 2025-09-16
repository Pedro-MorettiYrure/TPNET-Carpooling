using DTOs;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;



namespace API.Clients
{
    public class LocalidadApiClient
    {
        private static HttpClient loc = new HttpClient();
        static LocalidadApiClient()
        {
            loc.BaseAddress = new Uri("https://localhost:7139/");
            loc.DefaultRequestHeaders.Accept.Clear();
            loc.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public static async Task<LocalidadDTO> GetAsync(string codPos)
        {
            try
            {
                HttpResponseMessage response = await loc.GetAsync("clientes/" + codPos);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LocalidadDTO>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener localidad con codigo postal {codPos}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener localidad con codigo postal {codPos}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener localidad con codigo postal {codPos}: {ex.Message}", ex);
            }
        }

        public static async Task<IEnumerable<LocalidadDTO>> GetAllAsync()
        {
            try
            {
                HttpResponseMessage response = await loc.GetAsync("localidades");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<LocalidadDTO>>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener lista de localidades. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener lista de localidades: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener lista de localidades: {ex.Message}", ex);
            }
        }

        public async static Task AddAsync(LocalidadDTO localidad)
        {
            try
            {
                HttpResponseMessage response = await loc.PostAsJsonAsync("localidades", localidad);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al crear localidad. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al crear localidad: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al crear localidad: {ex.Message}", ex);
            }
        }

        public static async Task DeleteAsync(string codPostal)
        {
            try
            {
                HttpResponseMessage response = await loc.DeleteAsync("localidades/" + codPostal);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al eliminar localidad con codigo postal {codPostal}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al eliminar localidad con codigo postal {codPostal}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al eliminar localidad con codigo postal {codPostal}: {ex.Message}", ex);
            }
        }

        public static async Task UpdateAsync(LocalidadDTO localidad)
        {
            try
            {
                HttpResponseMessage response = await loc.PutAsJsonAsync("localidades", localidad);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar localidad con codigo postal {localidad.CodPostal}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al actualizar localidad con codigo postal {localidad.CodPostal}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al actualizar localidad con codigo postal {localidad.CodPostal}: {ex.Message}", ex);
            }
        }
    }
}


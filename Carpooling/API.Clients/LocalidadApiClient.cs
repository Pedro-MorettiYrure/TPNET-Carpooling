using DTOs;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic; 
using System.Linq; 

namespace API.Clients
{
    public class LocalidadApiClient
    {
        private static readonly HttpClient _httpClient; 

        static LocalidadApiClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<LocalidadDTO?> GetAsync(string codPos) 
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"localidades/{codPos}");
                if (response.StatusCode == HttpStatusCode.NotFound) return null;
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener localidad {codPos}");
                return await response.Content.ReadFromJsonAsync<LocalidadDTO>();
            }
            catch (KeyNotFoundException) { return null; } 
            catch (HttpRequestException ex) { throw new Exception($"Error de conexión: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task<IEnumerable<LocalidadDTO>> GetAllAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("localidades");
                await ApiClientHelper.HandleResponseErrorsAsync(response, "obtener todas las localidades");
                return await response.Content.ReadFromJsonAsync<IEnumerable<LocalidadDTO>>() ?? Enumerable.Empty<LocalidadDTO>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de conexión: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task AddAsync(LocalidadDTO localidad, string token) 
        {
            try
            {
                JsonContent content = JsonContent.Create(localidad);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Post, "localidades", token, content);
                await ApiClientHelper.HandleResponseErrorsAsync(response, "agregar localidad");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de conexión: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task DeleteAsync(string codPostal, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Delete, $"localidades/{codPostal}", token);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"eliminar localidad {codPostal}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de conexión: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task UpdateAsync(LocalidadDTO localidad, string token) 
        {
            try
            {
                JsonContent content = JsonContent.Create(localidad);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Put, $"localidades/{localidad.CodPostal}", token, content);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"actualizar localidad {localidad.CodPostal}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de conexión: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }
    }
}
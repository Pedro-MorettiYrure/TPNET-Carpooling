using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class ViajeApiClient
    {
        private static readonly HttpClient _httpClient; 

        static ViajeApiClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<IEnumerable<ViajeDTO>> GetByConductorAsync(int idUsuario, string token)
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Get, $"viajes/conductor/{idUsuario}", token);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener viajes del conductor {idUsuario}");
                return await response.Content.ReadFromJsonAsync<IEnumerable<ViajeDTO>>() ?? Enumerable.Empty<ViajeDTO>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task<ViajeDTO?> GetAsync(string idViaje)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"viajes/{idViaje}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener viaje {idViaje}"); 
                return await response.Content.ReadFromJsonAsync<ViajeDTO>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task<IEnumerable<ViajeDTO>> BuscarViajesAsync(string origenCodPostal, string destinoCodPostal)
        {
            string requestUri = $"viajes/buscar?origen={Uri.EscapeDataString(origenCodPostal)}&destino={Uri.EscapeDataString(destinoCodPostal)}"; // Usar EscapeDataString
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                await ApiClientHelper.HandleResponseErrorsAsync(response, "buscar viajes");
                return await response.Content.ReadFromJsonAsync<IEnumerable<ViajeDTO>>() ?? Enumerable.Empty<ViajeDTO>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task AddAsync(ViajeDTO viaje, string token) 
        {
            try
            {
                JsonContent content = JsonContent.Create(viaje);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Post, "viajes", token, content);
                await ApiClientHelper.HandleResponseErrorsAsync(response, "agregar viaje");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task DeleteAsync(int idViaje, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Delete, $"viajes/{idViaje}", token);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"cancelar viaje {idViaje}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task UpdateAsync(ViajeDTO viaje, string token)
        {
            try
            {
                JsonContent content = JsonContent.Create(viaje);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Put, $"viajes/{viaje.IdViaje}", token, content);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"actualizar viaje {viaje.IdViaje}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task IniciarViajeAsync(int idViaje, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Put, $"viajes/{idViaje}/iniciar", token, null);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"iniciar viaje {idViaje}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task<FinalizarViajeResponse?> FinalizarViajeAsync(int idViaje, string token)
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Put, $"viajes/{idViaje}/finalizar", token, null);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"finalizar viaje {idViaje}");
                return await response.Content.ReadFromJsonAsync<FinalizarViajeResponse>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public class FinalizarViajeResponse
        {
            public string? mensaje { get; set; }
            public List<UsuarioDTO>? pasajeros { get; set; }
        }

        public static async Task<IEnumerable<UsuarioDTO>> GetPasajerosConfirmadosAsync(int idViaje, string token)
        {
            string requestUri = $"viajes/{idViaje}/pasajeros-confirmados";
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Get, requestUri, token); //
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener pasajeros confirmados del viaje {idViaje}"); //
                return await response.Content.ReadFromJsonAsync<IEnumerable<UsuarioDTO>>() ?? Enumerable.Empty<UsuarioDTO>(); //
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }
    }
}
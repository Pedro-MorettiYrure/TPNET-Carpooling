using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
//using WindowsForms; // NO USAR ESTO

namespace API.Clients
{
    public class ViajeApiClient
    {
        private static readonly HttpClient _httpClient; // Renombrado

        static ViajeApiClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GetByConductorAsync necesita token
        public static async Task<IEnumerable<ViajeDTO>> GetByConductorAsync(int idUsuario, string token) // <-- Recibe token
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

        // GetAsync público (asumido)
        public static async Task<ViajeDTO?> GetAsync(string idViaje)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"viajes/{idViaje}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener viaje {idViaje}"); // Aún puede haber otros errores
                return await response.Content.ReadFromJsonAsync<ViajeDTO>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        // BuscarViajesAsync público
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

        // AddAsync necesita token
        public static async Task AddAsync(ViajeDTO viaje, string token) // <-- Recibe token
        {
            try
            {
                JsonContent content = JsonContent.Create(viaje);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Post, "viajes", token, content);
                await ApiClientHelper.HandleResponseErrorsAsync(response, "agregar viaje");
                // Podríamos leer el ViajeDTO creado desde response.Content si la API lo devuelve y este método lo retornara
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        // DeleteAsync (Cancelar) necesita token
        public static async Task DeleteAsync(int idViaje, string token) // <-- Recibe token
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Delete, $"viajes/{idViaje}", token);
                // HandleResponseErrorsAsync ya maneja los errores, incluso si la respuesta esperada es 204
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"cancelar viaje {idViaje}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        // UpdateAsync necesita token
        public static async Task UpdateAsync(ViajeDTO viaje, string token) // <-- Recibe token
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

        // IniciarViajeAsync necesita token
        public static async Task IniciarViajeAsync(int idViaje, string token) // <-- Recibe token
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

        // FinalizarViajeAsync necesita token
        public static async Task<FinalizarViajeResponse?> FinalizarViajeAsync(int idViaje, string token) // <-- Recibe token
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

        // Clase auxiliar interna
        public class FinalizarViajeResponse
        {
            public string? mensaje { get; set; }
            public List<UsuarioDTO>? pasajeros { get; set; }
        }
        // *** FALTARÍA APLICAR ESTE PATRÓN A LOS MÉTODOS DE CALIFICACIÓN ***
        // Si tienes CalificacionApiClient, modifícalo de forma similar.
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
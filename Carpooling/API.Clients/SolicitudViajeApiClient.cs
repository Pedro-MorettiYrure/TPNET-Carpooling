using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers; 
using System.Net.Http.Json;
using System.Threading.Tasks;
using DTOs;
using System.Linq; 

namespace API.Clients
{
    public static class SolicitudViajeApiClient
    {
        private static readonly HttpClient _httpClient;

        static SolicitudViajeApiClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<SolicitudViajeDTO> CrearSolicitudAsync(SolicitudViajeDTO dto, string token)
        {
            try
            {
                JsonContent content = JsonContent.Create(dto);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Post, "solicitudes", token, content);
                await ApiClientHelper.HandleResponseErrorsAsync(response, "crear solicitud de viaje");
                return await response.Content.ReadFromJsonAsync<SolicitudViajeDTO>() ?? throw new Exception("Respuesta inválida al crear solicitud.");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task<IEnumerable<SolicitudViajeDTO>> GetSolicitudesPorViajeAsync(int idViaje, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Get, $"solicitudes/viaje/{idViaje}", token);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener solicitudes para el viaje {idViaje}");
                return await response.Content.ReadFromJsonAsync<IEnumerable<SolicitudViajeDTO>>() ?? Enumerable.Empty<SolicitudViajeDTO>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        
        public static async Task<IEnumerable<SolicitudViajeDTO>> GetSolicitudesPorPasajeroAsync(int idPasajero, string token)
        {
            try
            {
                // Idealmente, la API debería validar que el token corresponda a idPasajero
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Get, $"solicitudes/pasajero/{idPasajero}", token);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener solicitudes del pasajero {idPasajero}");
                return await response.Content.ReadFromJsonAsync<IEnumerable<SolicitudViajeDTO>>() ?? Enumerable.Empty<SolicitudViajeDTO>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task<SolicitudViajeDTO?> GetSolicitudPorIdAsync(int idSolicitud, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Get, $"solicitudes/{idSolicitud}", token);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener solicitud {idSolicitud}");
                return await response.Content.ReadFromJsonAsync<SolicitudViajeDTO>();
            }
            catch (KeyNotFoundException) { return null; } // Si el helper lanza KeyNotFound por 404
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        //  necesita token (del conductor)
        public static async Task AceptarSolicitudAsync(int idSolicitud, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Put, $"solicitudes/{idSolicitud}/aceptar", token, null);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"aceptar solicitud {idSolicitud}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        // necesita token (del conductor)
        public static async Task RechazarSolicitudAsync(int idSolicitud, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Put, $"solicitudes/{idSolicitud}/rechazar", token, null);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"rechazar solicitud {idSolicitud}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        // necesita token (del pasajero)
        public static async Task CancelarSolicitudPasajeroAsync(int idSolicitud, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Put, $"solicitudes/{idSolicitud}/cancelar", token, null);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"cancelar solicitud {idSolicitud}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }
    }
}
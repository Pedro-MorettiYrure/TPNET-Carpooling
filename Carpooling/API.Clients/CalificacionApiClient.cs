using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    // DTO auxiliar para ENVIAR una calificación.
    public class CalificacionInputDTO
    {
        public int Puntaje { get; set; }
        public string? Comentario { get; set; }
    }

    public static class CalificacionApiClient
    {
        private static readonly HttpClient _httpClient;

        static CalificacionApiClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") }; 
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Conductor califica a Pasajero
        public static async Task<CalificacionDTO> CalificarPasajeroAsync(int idViaje, int idPasajeroCalificado, CalificacionInputDTO input, string token)
        {
            try
            {
                JsonContent content = JsonContent.Create(input);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(
                    _httpClient,
                    HttpMethod.Post,
                    $"viajes/{idViaje}/calificar-pasajero/{idPasajeroCalificado}",
                    token,
                    content);

                await ApiClientHelper.HandleResponseErrorsAsync(response, "calificar al pasajero");
                return await response.Content.ReadFromJsonAsync<CalificacionDTO>() ?? throw new Exception("Respuesta inválida al calificar pasajero.");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        // Pasajero califica a Conductor
        public static async Task<CalificacionDTO> CalificarConductorAsync(int idViaje, CalificacionInputDTO input, string token)
        {
            try
            {
                JsonContent content = JsonContent.Create(input);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(
                    _httpClient,
                    HttpMethod.Post,
                    $"viajes/{idViaje}/calificar-conductor",
                    token,
                    content);

                await ApiClientHelper.HandleResponseErrorsAsync(response, "calificar al conductor");
                return await response.Content.ReadFromJsonAsync<CalificacionDTO>() ?? throw new Exception("Respuesta inválida al calificar conductor.");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        // Obtener calificaciones recibidas por un usuario
        public static async Task<IEnumerable<CalificacionDTO>> GetCalificacionesRecibidasAsync(int idUsuario, string? rol, string token)
        {
            try
            {
                string requestUri = $"usuarios/{idUsuario}/calificaciones-recibidas";
                if (!string.IsNullOrEmpty(rol))
                {
                    requestUri += $"?rol={Uri.EscapeDataString(rol)}";
                }

                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(
                    _httpClient,
                    HttpMethod.Get,
                    requestUri,
                    token);

                await ApiClientHelper.HandleResponseErrorsAsync(response, "obtener calificaciones recibidas");
                return await response.Content.ReadFromJsonAsync<IEnumerable<CalificacionDTO>>() ?? Enumerable.Empty<CalificacionDTO>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

    }
}
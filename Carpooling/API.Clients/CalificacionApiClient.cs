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
        public static async Task<IEnumerable<CalificacionDTO>> GetCalificacionesDadasAsync(int idUsuarioCalificador, string token)
        {
            string requestUri = $"usuarios/{idUsuarioCalificador}/calificaciones-dadas";

            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(
                    _httpClient,
                    HttpMethod.Get,
                    requestUri,
                    token);

                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener calificaciones dadas por el usuario {idUsuarioCalificador}");

                
                return await response.Content.ReadFromJsonAsync<IEnumerable<CalificacionDTO>>() ?? Enumerable.Empty<CalificacionDTO>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red al obtener calificaciones dadas: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout al obtener calificaciones dadas: {ex.Message}", ex); }
            catch (Exception ex) { throw; } 
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
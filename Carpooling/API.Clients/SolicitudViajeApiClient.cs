using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DTOs;

namespace API.Clients
{
    public static class SolicitudViajeApiClient
    {
        private static readonly HttpClient _httpClient;

        static SolicitudViajeApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7139/"); 
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<SolicitudViajeDTO> CrearSolicitudAsync(SolicitudViajeDTO dto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("solicitudes", dto);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al crear solicitud: {errorContent}");
            }
            return await response.Content.ReadFromJsonAsync<SolicitudViajeDTO>();
        }

        public static async Task<IEnumerable<SolicitudViajeDTO>> GetSolicitudesPorViajeAsync(int idViaje)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"solicitudes/viaje/{idViaje}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<SolicitudViajeDTO>>();
        }

        public static async Task<IEnumerable<SolicitudViajeDTO>> GetSolicitudesPorPasajeroAsync(int idPasajero)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"solicitudes/pasajero/{idPasajero}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<SolicitudViajeDTO>>();
        }

        public static async Task<SolicitudViajeDTO> GetSolicitudPorIdAsync(int idSolicitud)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"solicitudes/{idSolicitud}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SolicitudViajeDTO>();
        }

        public static async Task AceptarSolicitudAsync(int idSolicitud)
        {
            HttpResponseMessage response = await _httpClient.PutAsync($"solicitudes/{idSolicitud}/aceptar", null);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al aceptar solicitud: {errorContent}");
            }
        }

        public static async Task RechazarSolicitudAsync(int idSolicitud)
        {
            HttpResponseMessage response = await _httpClient.PutAsync($"solicitudes/{idSolicitud}/rechazar", null);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al rechazar solicitud: {errorContent}");
            }
        }

        public static async Task CancelarSolicitudPasajeroAsync(int idSolicitud)
        {
            HttpResponseMessage response = await _httpClient.PutAsync($"solicitudes/{idSolicitud}/cancelar", null); // O /cancelar-pasajero si cambiaste la ruta

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al cancelar solicitud: {errorContent}");
            }
        }
    }
}
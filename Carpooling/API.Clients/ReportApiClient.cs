using System;
using System.Net.Http;
using System.Net.Http.Headers; 
using System.Threading.Tasks;
using System.Collections.Generic; 
using DTOs; 

namespace API.Clients
{
    public static class ReportApiClient
    {
        private static readonly HttpClient _httpClient;

        static ReportApiClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") }; 
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<byte[]> GetTopConductoresPdfAsync(string token)
        {
            string requestUri = "api/reports/top-conductores";
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Get, requestUri, token);

                // MANEJO ESPECIAL PARA ARCHIVOS
                if (response.IsSuccessStatusCode && response.Content.Headers.ContentType?.MediaType == "application/pdf")
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    // Si no es PDF o no fue exitoso, intentar leer como error JSON
                    await ApiClientHelper.HandleResponseErrorsAsync(response, "obtener reporte top conductores");
                    throw new HttpRequestException($"Respuesta inesperada al obtener reporte PDF. Status: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; } 
        }

        public static async Task<byte[]> GetActividadViajesPdfAsync(DateTime fechaInicio, DateTime fechaFin, string token)
        {
            string fechaInicioStr = fechaInicio.ToString("yyyy-MM-dd");
            string fechaFinStr = fechaFin.ToString("yyyy-MM-dd");
            string requestUri = $"api/reports/actividad-viajes?fechaInicio={fechaInicioStr}&fechaFin={fechaFinStr}";

            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Get, requestUri, token);

                if (response.IsSuccessStatusCode && response.Content.Headers.ContentType?.MediaType == "application/pdf")
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    await ApiClientHelper.HandleResponseErrorsAsync(response, "obtener reporte actividad viajes");
                    throw new HttpRequestException($"Respuesta inesperada al obtener reporte PDF. Status: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }
    }
}
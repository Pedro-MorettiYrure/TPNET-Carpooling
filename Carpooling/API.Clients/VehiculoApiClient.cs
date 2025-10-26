using DTOs;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq; 

namespace API.Clients
{
    public class VehiculoApiClient
    {
        private static readonly HttpClient _httpClient; 

        static VehiculoApiClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<IEnumerable<VehiculoDTO>> GetByUsuarioAsync(int idUsuario, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Get, $"vehiculos/{idUsuario}", token);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener vehículos del usuario {idUsuario}");
                return await response.Content.ReadFromJsonAsync<IEnumerable<VehiculoDTO>>() ?? Enumerable.Empty<VehiculoDTO>();
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task<VehiculoDTO> GetAsync(string patente, int idUsuario, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Get, $"vehiculos/{patente}/{idUsuario}", token);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener vehículo {patente}");
                return await response.Content.ReadFromJsonAsync<VehiculoDTO>() ?? throw new Exception("Respuesta inválida al obtener vehículo.");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task AddAsync(VehiculoDTO vehiculo, string token) 
        {
            try
            {
                JsonContent content = JsonContent.Create(vehiculo);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Post, $"vehiculos/{vehiculo.IdUsuario}", token, content);
                await ApiClientHelper.HandleResponseErrorsAsync(response, "agregar vehículo");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task DeleteAsync(string patente, int idUsuario, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Delete, $"vehiculos/{patente}/{idUsuario}", token);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"eliminar vehículo {patente}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task UpdateAsync(VehiculoDTO vehiculo, string token) 
        {
            try
            {
                JsonContent content = JsonContent.Create(vehiculo);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Put, $"vehiculos/{vehiculo.Patente}/{vehiculo.IdUsuario}", token, content);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"actualizar vehículo {vehiculo.Patente}");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de red: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }
    }
}
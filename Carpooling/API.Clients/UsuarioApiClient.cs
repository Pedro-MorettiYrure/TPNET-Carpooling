using DTOs;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic; // Para Dictionary
using static DTOs.UsuarioDTO; // Para ConductorUpgradeDTO

namespace API.Clients
{
    public class UsuarioApiClient
    {
        private static readonly HttpClient _httpClient;

        static UsuarioApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7139/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //  devuelve token o null
        public static async Task<string?> LoginAsync(string email, string contraseña)
        {
            try
            {
                var loginDto = new UsuarioDTO { Email = email, Contraseña = contraseña };
                var response = await _httpClient.PostAsJsonAsync("usuarios/login", loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    return responseBody?.token;
                }
                // No lanzamos excepción aquí, devolvemos null si el login falla 
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en LoginAsync: {ex.Message}");
                return null;
            }
        }

        private class LoginResponse { public string? token { get; set; } }

        public static async Task<UsuarioDTO> RegistrarUsuarioAsync(UsuarioDTO usuario)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("usuarios", usuario);
                // Usamos el helper para manejar errores de forma consistente
                await ApiClientHelper.HandleResponseErrorsAsync(response, "registrar usuario");
                return await response.Content.ReadFromJsonAsync<UsuarioDTO>() ?? throw new Exception("Respuesta inesperada al registrar usuario.");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de conexión: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

       
        public static async Task<UsuarioDTO> GetByEmailAsync(string email, string token) 
        {
            try
            {
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Get, $"usuarios/{email}", token);
                await ApiClientHelper.HandleResponseErrorsAsync(response, $"obtener usuario {email}"); 
                return await response.Content.ReadFromJsonAsync<UsuarioDTO>() ?? throw new Exception("Respuesta inválida al obtener usuario.");
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de conexión al obtener usuario {email}: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout al obtener usuario {email}: {ex.Message}", ex); }
            catch (Exception ex) { throw; } 
        }

        public static async Task<bool> ActualizarUsuarioAsync(UsuarioDTO usuario, string token)
        {
            try
            {
                JsonContent content = JsonContent.Create(usuario);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Put, $"usuarios/{usuario.IdUsuario}", token, content);
                await ApiClientHelper.HandleResponseErrorsAsync(response, "actualizar usuario");
                return true; // Si no lanzó excepción, fue exitoso
            }
            catch (HttpRequestException ex) { throw new Exception($"Error de conexión al actualizar usuario: {ex.Message}", ex); }
            catch (TaskCanceledException ex) { throw new Exception($"Timeout al actualizar usuario: {ex.Message}", ex); }
            catch (Exception ex) { throw; }
        }

        public static async Task<string?> ConvertirAConductorAsync(int idUsuario, ConductorUpgradeDTO dto, string token)
        {
            try
            {
                JsonContent content = JsonContent.Create(dto);
                HttpResponseMessage response = await ApiClientHelper.SendAuthenticatedRequestAsync(_httpClient, HttpMethod.Put, $"usuarios/{idUsuario}/convertir-a-conductor", token, content);

                await ApiClientHelper.HandleResponseErrorsAsync(response, "convertir a conductor");

                var responseBody = await response.Content.ReadFromJsonAsync<LoginResponse>(); // Reusamos la clase auxiliar
                return responseBody?.token; // Devolvemos el nuevo token
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error en ConvertirAConductorAsync: {ex.Message}");
                return null; // Devolvemos null si hubo CUALQUIER error
            }
        }
    }
}
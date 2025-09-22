using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static DTOs.UsuarioDTO;

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
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<UsuarioDTO> RegistrarUsuarioAsync(UsuarioDTO usuario)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("usuarios", usuario);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al registrar usuario. Status: {response.StatusCode}, Detalle: {errorContent}");
                }

                return await response.Content.ReadFromJsonAsync<UsuarioDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al registrar usuario: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al registrar usuario: {ex.Message}", ex);
            }
        }

        public static async Task<bool> LoginAsync(string email, string contraseña)
        {
            try
            {
                var loginDto = new UsuarioDTO
                {
                    Email = email,
                    Contraseña = contraseña
                };

                var response = await _httpClient.PostAsJsonAsync("usuarios/login", loginDto);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }

        public static async Task<UsuarioDTO> GetByEmailAsync(string email)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"usuarios/{email}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UsuarioDTO>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener usuario {email}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener usuario {email}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener usuario {email}: {ex.Message}", ex);
            }
        }

        public static async Task<bool> ConvertirAConductorAsync(int idUsuario, ConductorUpgradeDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"usuarios/{idUsuario}/convertir-a-conductor", dto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}




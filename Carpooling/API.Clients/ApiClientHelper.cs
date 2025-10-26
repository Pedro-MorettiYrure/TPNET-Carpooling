using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json; 
using System.Text.Json; 
using System.Threading.Tasks;
using System.Collections.Generic; 

namespace API.Clients
{
    internal static class ApiClientHelper
    {
        // Método centralizado para enviar peticiones autenticadas
        internal static async Task<HttpResponseMessage> SendAuthenticatedRequestAsync(HttpClient client, HttpMethod method, string requestUri, string? token, HttpContent? content = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("El token JWT no fue proporcionado para la solicitud autenticada.");
            }

            using var request = new HttpRequestMessage(method, requestUri);

            // Agregar el token de autorización
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Agregar el cuerpo (contenido) si existe
            if (content != null)
            {
                request.Content = content;
            }

            return await client.SendAsync(request);
        }

        // Método centralizado para manejar errores de respuesta de la API
        internal static async Task HandleResponseErrorsAsync(HttpResponseMessage response, string operationDescription = "la operación")
        {
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                string errorMessage = errorContent; // Mensaje por defecto

                try
                {
                    using var jsonDoc = JsonDocument.Parse(errorContent);
                    if (jsonDoc.RootElement.TryGetProperty("error", out var errorProp) && errorProp.ValueKind == JsonValueKind.String)
                    {
                        errorMessage = errorProp.GetString() ?? errorMessage;
                    }
                    else if (jsonDoc.RootElement.TryGetProperty("title", out var titleProp) && titleProp.ValueKind == JsonValueKind.String)
                    {
                        errorMessage = titleProp.GetString() ?? errorMessage;
                        if (jsonDoc.RootElement.TryGetProperty("errors", out var errorsProp))
                        {
                            errorMessage += ": " + errorsProp.ToString(); // Agregar detalles de validación si existen
                        }
                    }
                }
                catch (JsonException)
                {
                }

                // Lanzar excepción específica según el código de estado
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized: // 401
                        throw new UnauthorizedAccessException($"No autorizado para {operationDescription}. Verifique el token.");
                    case System.Net.HttpStatusCode.Forbidden: // 403
                        throw new UnauthorizedAccessException($"Permiso denegado para {operationDescription}.");
                    case System.Net.HttpStatusCode.NotFound: // 404
                        throw new KeyNotFoundException($"Recurso no encontrado durante {operationDescription}. Detalle: {errorMessage}");
                    case System.Net.HttpStatusCode.Conflict: // 409
                        throw new InvalidOperationException($"Conflicto durante {operationDescription}. Detalle: {errorMessage}");
                    case System.Net.HttpStatusCode.BadRequest: // 400
                        throw new ArgumentException($"Solicitud incorrecta para {operationDescription}. Detalle: {errorMessage}");
                    default:
                        // Para otros errores (500, etc.)
                        throw new HttpRequestException($"Error en {operationDescription}. Status: {response.StatusCode}, Detalle: {errorMessage}", null, response.StatusCode);
                }
            }
        }
    }
}
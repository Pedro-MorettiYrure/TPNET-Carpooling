using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace API.Clients
{
    public interface IAuthService
    {
        //event Action<bool>? AuthenticationStateChanged;
        event Action? OnChange;

        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetTokenAsync();
        Task<UsuarioDTO?> GetUsuarioAsync();
        Task<bool> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task CheckTokenExpirationAsync();
        void NotifyStateChanged();

        //Task<bool> HasPermissionAsync(string permission);
    }
}

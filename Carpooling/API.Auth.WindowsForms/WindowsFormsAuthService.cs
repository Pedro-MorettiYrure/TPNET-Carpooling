using System; 
using System.IdentityModel.Tokens.Jwt;
using System.Linq; 
using System.Security.Claims;
using System.Threading.Tasks; 
using DTOs;
using API.Clients;

namespace API.Auth.WindowsForms 
{
    public class WindowsFormsAuthService : IAuthService
    {
        private static string? _currentToken;
        private static DateTime _tokenExpiration;
        private static UsuarioDTO? _currentUser;
        private int ExpirationTime = 30;

        public event Action<bool>? AuthenticationStateChanged;

        public event Action? OnChange;

        public void NotifyStateChanged()
        {
            bool isAuthenticated = !string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow < _tokenExpiration;
            AuthenticationStateChanged?.Invoke(isAuthenticated);
            OnChange?.Invoke();
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            return !string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow < _tokenExpiration;
        }

        public async Task<string?> GetTokenAsync()
        {
            var isAuth = await IsAuthenticatedAsync();
            return isAuth ? _currentToken : null;
        }

        public async Task<UsuarioDTO?> GetUsuarioAsync()
        {
            var isAuth = await IsAuthenticatedAsync();
            return isAuth ? _currentUser : null;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var token = await UsuarioApiClient.LoginAsync(email, password);

            if (token != null)
            {
                UsuarioDTO user = await UsuarioApiClient.GetByEmailAsync(email, token); 

                _currentToken = token;
                _tokenExpiration = DateTime.UtcNow.AddMinutes(ExpirationTime);
                _currentUser = user;

                NotifyStateChanged(); 
                return true;
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            _currentToken = null;
            _tokenExpiration = default;
            _currentUser = null;

            // AuthenticationStateChanged?.Invoke(false); 
            NotifyStateChanged();
        }

        public async Task CheckTokenExpirationAsync()
        {
            if (!string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow >= _tokenExpiration)
            {
                await LogoutAsync();
            }
        }

        /*
        public async Task<bool> HasPermissionAsync(string permission)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return false;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);

                // Buscar claims de "permission" 
                var permissionClaims = jsonToken.Claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value);

                return permissionClaims.Contains(permission);
            }
            catch
            {
                return false;
            }
        }
        */
    }
}


using DTOs;
using API.Clients;
using System.IdentityModel.Tokens.Jwt;

namespace API.Auth.Blazor
{
    public class ServicioSesionUsuario: IAuthService
    {
        public UsuarioDTO? UsuarioActual { get; private set; }

        private static SessionData? _currentSession;

        public int ExpirationTime = 30; //minutos

        public event Action? OnChange;

        private class SessionData
        {
            public string? Token { get; set; }
            //public UsuarioDTO? UsuarioActual { get; set; }
            public DateTime Expiration { get; set; }
        }

        public void IniciarSesion(UsuarioDTO usuario)   //SACAR
        {
            UsuarioActual = usuario;
            NotifyStateChanged();
        }
        public void CerrarSesion()  //SACAR
        {
            UsuarioActual = null;
            NotifyStateChanged();
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var token = await UsuarioApiClient.LoginAsync(email, password);

            if (token != null)
            {
                UsuarioDTO user = await UsuarioApiClient.GetByEmailAsync(email, token);

                UsuarioActual = user;
                _currentSession = new SessionData
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddMinutes(ExpirationTime)
                };

                NotifyStateChanged();
                return true;
            }

            return false;
        }
                
        public Task LogoutAsync()
        {
            _currentSession = null;
            UsuarioActual = null;
            NotifyStateChanged();
            return Task.CompletedTask;
        }

        public async Task CheckTokenExpirationAsync()
        {
            if (!await IsAuthenticatedAsync())
            {
                await LogoutAsync();
            }
        }

        public Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                if (_currentSession != null)
                {
                    return Task.FromResult(!string.IsNullOrEmpty(_currentSession.Token) && DateTime.UtcNow < _currentSession.Expiration);
                }
                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public Task<string?> GetTokenAsync()
        {
            try
            {
                return Task.FromResult(_currentSession?.Token);
            }
            catch
            {
                return Task.FromResult<string?>(null);
            }
        }

        public void SetToken(string newToken)
        {
            _currentSession.Token = newToken;
        }

        public Task<UsuarioDTO?> GetUsuarioAsync()
        {
            try
            {
                return Task.FromResult(UsuarioActual);
            }
            catch
            {
                return Task.FromResult<UsuarioDTO?>(null);
            }
        }

        public bool EstaLogueado => UsuarioActual != null;

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
        
        //public Task<bool> HasPermissionAsync(string permission)
        //{
        //    try
        //    {
        //        var token = _currentSession?.Token;
        //        if (string.IsNullOrEmpty(token))
        //            return Task.FromResult(false);

        //        var handler = new JwtSecurityTokenHandler();
        //        var jsonToken = handler.ReadJwtToken(token);

        //        var permissionClaims = jsonToken.Claims
        //            .Where(c => c.Type == "permission")
        //            .Select(c => c.Value);

        //        return Task.FromResult(permissionClaims.Contains(permission));
        //    }
        //    catch
        //    {
        //        return Task.FromResult(false);
        //    }
        //}

}

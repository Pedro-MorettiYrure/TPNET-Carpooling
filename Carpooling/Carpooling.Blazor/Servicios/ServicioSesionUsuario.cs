using DTOs;

//public class ServicioSesionUsuario
//{
//    public UsuarioDTO UsuarioActual { get; private set; }

//    public event Action OnChange;

//    public void IniciarSesion(UsuarioDTO usuario)
//    {
//        UsuarioActual = usuario;
//        NotifyStateChanged();
//    }

//    public void CerrarSesion()
//    {
//        UsuarioActual = null;
//        NotifyStateChanged();
//    }

//    public bool EstaLogueado => UsuarioActual != null;

//    private void NotifyStateChanged() => OnChange?.Invoke();
//}
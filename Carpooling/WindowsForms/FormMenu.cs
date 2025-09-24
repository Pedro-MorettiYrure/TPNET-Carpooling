using System;
using System.Windows.Forms;
using DTOs;
using Domain.Model;


namespace WindowsForms
{
    public partial class FormMenu : Form
    {
        private readonly UsuarioDTO _usuarioLogueado; // <-- usuario logueado

        public FormMenu(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;

            MostrarBtnsTipoUsuario();
        }

        private void MostrarBtnsTipoUsuario()
        {
            if (_usuarioLogueado.TipoUsuario == TipoUsuario.Pasajero)
            {
                btnConvertirAConductor.Visible = true;
                btnVehiculoLista.Visible = false; // El pasajero no puede ver vehículos
            }
            else if (_usuarioLogueado.TipoUsuario == TipoUsuario.PasajeroConductor || _usuarioLogueado.TipoUsuario == TipoUsuario.Administrador)
            {
                btnConvertirAConductor.Visible = false;
                btnVehiculoLista.Visible = true; // El conductor y admin sí pueden
            }
        }

        private void btnLocalidadLista_Click(object sender, EventArgs e)
        {
            LocalidadLista appLocalidad = new LocalidadLista();
            appLocalidad.ShowDialog();
        }

        private void btnVehiculoLista_Click(object sender, EventArgs e)
        {
            // Le pasamos el usuario logueado al constructor
            VehiculosLista appVehiculo = new VehiculosLista(_usuarioLogueado);
            appVehiculo.ShowDialog();
        }

        private void btnConvertirAConductor_Click(object sender, EventArgs e)
        {
            FormConductorUpgrade formUpgrade = new FormConductorUpgrade(_usuarioLogueado);
            formUpgrade.ShowDialog();
            MostrarBtnsTipoUsuario();
        }

        private void btnViajeLista_Click(object sender, EventArgs e)
        {

        }
    }
}

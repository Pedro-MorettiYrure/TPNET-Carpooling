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
            // Ocultamos todo por defecto
            btnConvertirAConductor.Visible = false;
            btnVehiculoLista.Visible = false;
            btnViajeLista.Visible = false;
            btnLocalidadLista.Visible = false;
            btnBuscarViaje.Visible = false;
            btnMisSolicitudes.Visible = false;
            btnReportes.Visible = false; 
            switch (_usuarioLogueado.TipoUsuario)
            {
                case TipoUsuario.Administrador:
                    btnLocalidadLista.Visible = true;
                    btnReportes.Visible = true;

                    break;

                case TipoUsuario.Pasajero:
                    btnConvertirAConductor.Visible = true;
                    btnViajeLista.Visible = true;
                    btnBuscarViaje.Visible = true;
                    btnMisSolicitudes.Visible = true;
                    break;

                case TipoUsuario.PasajeroConductor:
                    btnVehiculoLista.Visible = true;
                    btnViajeLista.Visible = true;
                    btnBuscarViaje.Visible = true;
                    break;
            }
        }
        private void btnReportes_Click(object sender, EventArgs e)
        {
            using (FormReportes formReportes = new FormReportes())
            {
                formReportes.ShowDialog();
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
            if (_usuarioLogueado.TipoUsuario == TipoUsuario.PasajeroConductor) 
            {
                DialogResult eleccion = MessageBox.Show(
                    "¿Desea ver sus viajes como conductor o como pasajero?",
                    "Seleccionar Vista de Mis Viajes",
                    MessageBoxButtons.YesNo, // Yes = Conductor, No = Pasajero
                    MessageBoxIcon.Question);

                if (eleccion == DialogResult.Yes) // como conductor
                {
                    ViajesLista formViajesConductor = new ViajesLista(_usuarioLogueado);
                    formViajesConductor.ShowDialog();
                }
                else if (eleccion == DialogResult.No) // como pasajero
                {
                    ViajesListaPasajero formViajesPasajero = new ViajesListaPasajero(_usuarioLogueado);
                    formViajesPasajero.ShowDialog();
                }
            }
            
            else if (_usuarioLogueado.TipoUsuario == TipoUsuario.Pasajero) 
            {
                ViajesListaPasajero formViajesPasajero = new ViajesListaPasajero(_usuarioLogueado);
                formViajesPasajero.ShowDialog();
            }
        }

        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            FormRegistrarse formEditar = new FormRegistrarse(_usuarioLogueado, true);
            if (formEditar.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnBuscarViaje_Click(object sender, EventArgs e)
        {
            FormBuscarViaje formBusqueda = new FormBuscarViaje(_usuarioLogueado);
            formBusqueda.ShowDialog();
        }

        private void btnMisSolicitudes_Click(object sender, EventArgs e)
        {
            SolicitudesListaPasajero formSolicitudesPasajero = new SolicitudesListaPasajero(_usuarioLogueado);
            formSolicitudesPasajero.ShowDialog();
        }

    }
}


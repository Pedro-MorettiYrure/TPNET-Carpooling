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

            switch (_usuarioLogueado.TipoUsuario)
            {
                case TipoUsuario.Administrador:
                    btnLocalidadLista.Visible = true;
                    break;

                case TipoUsuario.Pasajero:
                    btnConvertirAConductor.Visible = true;
                    break;

                case TipoUsuario.PasajeroConductor:
                    btnVehiculoLista.Visible = true;
                    btnViajeLista.Visible = true;
                    break;
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
            ViajesLista appLista = new ViajesLista(_usuarioLogueado);
            appLista.ShowDialog();
        }

        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            FormRegistrarse formEditar = new FormRegistrarse(_usuarioLogueado, true);
            if (formEditar.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}

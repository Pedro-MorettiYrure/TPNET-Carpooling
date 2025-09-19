using System;
using System.Windows.Forms;
using API.Clients;
using DTOs; // para UsuarioDTO

namespace WindowsForms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void lnkOlvidaPass_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Es Ud. un usuario muy descuidado, haga memoria",
                "Olvidé mi contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void lnkRegistrate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var formReg = new FormRegistrarse();
            formReg.ShowDialog();
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text;
                string contraseña = txtPass.Text;

                bool loginOk = await UsuarioApiClient.LoginAsync(email, contraseña);

                if (loginOk)
                {
                    // Obtener datos completos del usuario
                    UsuarioDTO usuarioLogueado = await UsuarioApiClient.GetByEmailAsync(email);

                    // Abrir el menú y pasar el usuario logueado
                    FormMenu menu = new FormMenu(usuarioLogueado);
                    this.Hide();
                    menu.ShowDialog();
                    this.Show(); // opcional si querés volver al login al cerrar el menú
                }
                else
                {
                    MessageBox.Show("Email o contraseña incorrectos.", "Error de login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión o inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

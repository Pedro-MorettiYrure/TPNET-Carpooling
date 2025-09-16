using System;
using System.Windows.Forms;
using API.Clients;

namespace WindowsForms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                bool loginOk = await UsuarioApiClient.LoginAsync(txtEmail.Text, txtPass.Text);

                if (loginOk)
                {
                    MessageBox.Show("Usted ha ingresado al sistema correctamente.",
                        "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el FormMenu
                    FormMenu formMenu = new FormMenu();
                    formMenu.ShowDialog();

                    // Cerrar el FormLogin
                    this.Hide(); // opcional: Hide() mantiene el form en memoria, Close() lo destruye
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña incorrectos", "Login",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar ingresar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}

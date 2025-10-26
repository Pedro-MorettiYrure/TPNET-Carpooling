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
            using (var formReg = new FormRegistrarse())
            {
                formReg.ShowDialog();
            }
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            btnIngresar.Enabled = false;
            try
            {
                string email = txtEmail.Text;
                string contraseña = txtPass.Text;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contraseña))
                {
                    MessageBox.Show("Debe ingresar email y contraseña.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string? token = await UsuarioApiClient.LoginAsync(email, contraseña);

                if (!string.IsNullOrEmpty(token))
                {
                    // Guardar token ANTES de la siguiente llamada
                    SessionManager.IniciarSesion(new UsuarioDTO { Email = email }, token); // DTO Temporal

                    try
                    {
                        // *** CORREGIDO: Pasar el token a GetByEmailAsync ***
                        UsuarioDTO usuarioLogueado = await UsuarioApiClient.GetByEmailAsync(email, token); // <- Pasar token aquí

                        // Actualizar sesión con datos completos
                        SessionManager.IniciarSesion(usuarioLogueado, token);

                        using (FormMenu menu = new FormMenu(usuarioLogueado))
                        {
                            this.Hide();
                            menu.ShowDialog();
                        }
                        this.Close();
                    }
                    catch (UnauthorizedAccessException authEx)
                    {
                        SessionManager.CerrarSesion();
                        MessageBox.Show($"Error de autorización al obtener datos: {authEx.Message}", "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception exUser)
                    {
                        SessionManager.CerrarSesion();
                        MessageBox.Show($"Login exitoso, pero error al obtener datos del usuario: {exUser.Message}", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
            finally
            {
                if (!this.IsDisposed) { btnIngresar.Enabled = true; }
            }
        }
    }
}
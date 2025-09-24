using DTOs;
using System;
using System.Windows.Forms;
using API.Clients;

namespace WindowsForms
{
    public partial class FormRegistrarse : Form
    {
        public FormRegistrarse()
        {
            InitializeComponent();
        }

        private async void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBoxNombre.Text) ||
                string.IsNullOrWhiteSpace(txtBoxApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtBoxContra.Text) ||
                string.IsNullOrWhiteSpace(txtBoxConfirmaCon.Text))
            {
                MessageBox.Show("Debe completar todos los campos.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtBoxContra.Text != txtBoxConfirmaCon.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var usuarioDto = new UsuarioDTO
                {
                    Nombre = txtBoxNombre.Text,
                    Apellido = txtBoxApellido.Text,
                    Email = txtEmail.Text,
                    Contraseña = txtBoxContra.Text
                };

                var usuarioGuardado = await UsuarioApiClient.RegistrarUsuarioAsync(usuarioDto);

                MessageBox.Show("Usuario registrado exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //this.Close(); // cierra el FormRegistrarse

                //// Abrir FormLogin
                //FormLogin formLogin = new FormLogin();
                //formLogin.ShowDialog();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}

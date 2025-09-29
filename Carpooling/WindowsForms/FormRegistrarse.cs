using DTOs;
using System;
using System.Windows.Forms;
using API.Clients;

namespace WindowsForms
{
    public partial class FormRegistrarse : Form
    {
        private readonly bool _esEdicion;
        private readonly UsuarioDTO _usuarioEditar;

        // Constructor vacío para registro nuevo
        public FormRegistrarse()
        {
            InitializeComponent();
            _esEdicion = false;
            _usuarioEditar = null;

            // Ocultar controles de licencia por defecto
            textBoxLicencia.Visible = false;
            dateTimePickerVencimiento.Visible = false;
            labelLicencia.Visible = false;
            labelVencimiento.Visible = false;
            labelConductor.Visible = false;
        }

        // Constructor para edición de usuario
        public FormRegistrarse(UsuarioDTO usuario, bool esEdicion = true)
        {
            InitializeComponent();
            _esEdicion = esEdicion;
            _usuarioEditar = usuario;

            if (_esEdicion && _usuarioEditar != null)
            {
                CargarDatosUsuario();
                PrepararModoEdicion();
            }
        }

        private async void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            if (_esEdicion && _usuarioEditar != null)
            {
                // Revisamos si hubo cambios
                bool huboCambios =
                    txtBoxNombre.Text != _usuarioEditar.Nombre ||
                    txtBoxApellido.Text != _usuarioEditar.Apellido ||
                    txtBoxTele.Text != (_usuarioEditar.Telefono ?? "");

                if (_usuarioEditar.TipoUsuario == Domain.Model.TipoUsuario.PasajeroConductor)
                {
                    huboCambios |= textBoxLicencia.Text != (_usuarioEditar.nroLicenciaConductor ?? "");
                    huboCambios |= dateTimePickerVencimiento.Value != _usuarioEditar.fechaVencimientoLicencia;
                }

                if (!huboCambios)
                {
                    MessageBox.Show("No editaste ningún dato.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    // Actualizamos los campos
                    _usuarioEditar.Nombre = txtBoxNombre.Text;
                    _usuarioEditar.Apellido = txtBoxApellido.Text;
                    _usuarioEditar.Telefono = txtBoxTele.Text;

                    if (_usuarioEditar.TipoUsuario == Domain.Model.TipoUsuario.PasajeroConductor)
                    {
                        _usuarioEditar.nroLicenciaConductor = textBoxLicencia.Text;
                        _usuarioEditar.fechaVencimientoLicencia = dateTimePickerVencimiento.Value;
                    }

                    bool actualizado = await UsuarioApiClient.ActualizarUsuarioAsync(_usuarioEditar);

                    if (actualizado)
                    {
                        MessageBox.Show("Datos actualizados correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el usuario.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtBoxNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtBoxApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtBoxContra.Text) ||
                    string.IsNullOrWhiteSpace(txtBoxConfirmaCon.Text) ||
                    string.IsNullOrWhiteSpace(txtBoxTele.Text))
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
                        Contraseña = txtBoxContra.Text,
                        Telefono = txtBoxTele.Text
                    };

                    var usuarioGuardado = await UsuarioApiClient.RegistrarUsuarioAsync(usuarioDto);

                    MessageBox.Show("Usuario registrado exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar usuario: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarDatosUsuario()
        {
            if (_usuarioEditar == null) return;

            txtBoxNombre.Text = _usuarioEditar.Nombre;
            txtBoxApellido.Text = _usuarioEditar.Apellido;
            txtEmail.Text = _usuarioEditar.Email;
            txtEmail.Enabled = false; // no se edita
            txtBoxTele.Text = _usuarioEditar.Telefono;

            if (_usuarioEditar.TipoUsuario == Domain.Model.TipoUsuario.PasajeroConductor)
            {
                textBoxLicencia.Text = _usuarioEditar.nroLicenciaConductor;
                dateTimePickerVencimiento.Value = _usuarioEditar.fechaVencimientoLicencia ?? DateTime.Now;
                dateTimePickerVencimiento.Visible = true;
                labelLicencia.Visible = true;
                labelVencimiento.Visible = true;
            }
            else
            {
                textBoxLicencia.Visible = false;
                dateTimePickerVencimiento.Visible = false;
                labelLicencia.Visible = false;
                labelVencimiento.Visible = false;
            }
        }

        private void PrepararModoEdicion()
        {
            this.Text = "Editar Usuario";

            txtBoxContra.Visible = false;
            txtBoxConfirmaCon.Visible = false;
            labelContra.Visible = false;
            labelConfirmarContra.Visible = false;

            txtEmail.Enabled = false;


        }

        private void label4_Click(object sender, EventArgs e)
        {
        }
    }
}

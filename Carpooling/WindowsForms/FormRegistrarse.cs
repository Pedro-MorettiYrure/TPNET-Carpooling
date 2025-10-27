using DTOs;
using System;
using System.Windows.Forms;
using API.Clients;
using Domain.Model; 

namespace WindowsForms
{
    public partial class FormRegistrarse : Form
    {
        private readonly bool _esEdicion;
        private readonly UsuarioDTO? _usuarioEditar; 

        public FormRegistrarse() : this(null, false) 
        {
        }

        public FormRegistrarse(UsuarioDTO? usuarioAEditar = null, bool esEdicion = false)
        {
            InitializeComponent();
            _esEdicion = esEdicion;
            _usuarioEditar = usuarioAEditar; 

            if (_esEdicion && _usuarioEditar != null)
            {
                PrepararModoEdicion(); 
            }
            else 
            {
                this.Text = "Registrar Nuevo Usuario";
                textBoxLicencia.Visible = false;
                dateTimePickerVencimiento.Visible = false;
                labelLicencia.Visible = false;
                labelVencimiento.Visible = false;
                labelConductor.Visible = false;
                // Contraseñas visibles
                labelContra.Visible = true;
                labelConfirmarContra.Visible = true;
                txtBoxContra.Visible = true;
                txtBoxConfirmaCon.Visible = true;
                txtEmail.Enabled = true; 
                btnCrearUsuario.Text = "Registrarse";
            }
        }


        private async void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            btnCrearUsuario.Enabled = false;

            if (_esEdicion && _usuarioEditar != null)
            {
                if (string.IsNullOrWhiteSpace(txtBoxNombre.Text) || string.IsNullOrWhiteSpace(txtBoxApellido.Text))
                {
                    MessageBox.Show("Nombre y Apellido son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnCrearUsuario.Enabled = true;
                    return;
                }
                if (_usuarioEditar.TipoUsuario == TipoUsuario.PasajeroConductor)
                {
                    if (string.IsNullOrWhiteSpace(textBoxLicencia.Text))
                    {
                        MessageBox.Show("El número de licencia es obligatorio para conductores.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnCrearUsuario.Enabled = true;
                        return;
                    }
                    if (dateTimePickerVencimiento.Value.Date <= DateTime.Today)
                    {
                        MessageBox.Show("La fecha de vencimiento de la licencia debe ser futura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnCrearUsuario.Enabled = true;
                        return;
                    }
                }


                bool huboCambios = txtBoxNombre.Text != _usuarioEditar.Nombre ||
                                   txtBoxApellido.Text != _usuarioEditar.Apellido ||
                                   txtBoxTele.Text != (_usuarioEditar.Telefono ?? "");

                if (_usuarioEditar.TipoUsuario == TipoUsuario.PasajeroConductor)
                {
                    huboCambios |= textBoxLicencia.Text != (_usuarioEditar.nroLicenciaConductor ?? "");
                    huboCambios |= dateTimePickerVencimiento.Value.Date != (_usuarioEditar.fechaVencimientoLicencia?.Date ?? DateTime.MinValue.Date);
                }

                if (!huboCambios)
                {
                    MessageBox.Show("No se detectaron cambios en los datos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCrearUsuario.Enabled = true; 
                    return;
                }

                try
                {
                    string? token = SessionManager.JwtToken;
                    if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("Sesión inválida.");

                    _usuarioEditar.Nombre = txtBoxNombre.Text;
                    _usuarioEditar.Apellido = txtBoxApellido.Text;
                    _usuarioEditar.Telefono = string.IsNullOrWhiteSpace(txtBoxTele.Text) ? null : txtBoxTele.Text; 

                    if (_usuarioEditar.TipoUsuario == TipoUsuario.PasajeroConductor)
                    {
                        _usuarioEditar.nroLicenciaConductor = textBoxLicencia.Text;
                        _usuarioEditar.fechaVencimientoLicencia = dateTimePickerVencimiento.Value;
                    }

                    bool actualizado = await UsuarioApiClient.ActualizarUsuarioAsync(_usuarioEditar, token);

                    if (actualizado)
                    {
                        MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (SessionManager.UsuarioActual?.IdUsuario == _usuarioEditar.IdUsuario)
                        {
                            SessionManager.IniciarSesion(_usuarioEditar, token); 
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el usuario (respuesta inesperada).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                catch (ArgumentException argEx) { MessageBox.Show($"Datos inválidos: {argEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } // Error 400
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { if (!this.IsDisposed) btnCrearUsuario.Enabled = true; } 
            }
            else
            {
                
                if (string.IsNullOrWhiteSpace(txtBoxNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtBoxApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtBoxContra.Text) ||
                    string.IsNullOrWhiteSpace(txtBoxConfirmaCon.Text)) 
                {
                    MessageBox.Show("Debe completar Nombre, Apellido, Email y Contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCrearUsuario.Enabled = true; 
                    return;
                }
                if (txtBoxContra.Text != txtBoxConfirmaCon.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCrearUsuario.Enabled = true; 
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
                        Telefono = string.IsNullOrWhiteSpace(txtBoxTele.Text) ? null : txtBoxTele.Text 
                    };

                    var usuarioGuardado = await UsuarioApiClient.RegistrarUsuarioAsync(usuarioDto);

                    MessageBox.Show("Usuario registrado exitosamente. Ahora puede iniciar sesión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (ArgumentException argEx) { MessageBox.Show($"Error de validación: {argEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } // Error 400 (ej: email ya existe)
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { if (!this.IsDisposed) btnCrearUsuario.Enabled = true; } 
            }
        }

        private void CargarDatosUsuario()
        {
            if (_usuarioEditar == null) return;

            txtBoxNombre.Text = _usuarioEditar.Nombre;
            txtBoxApellido.Text = _usuarioEditar.Apellido;
            txtEmail.Text = _usuarioEditar.Email;
            txtBoxTele.Text = _usuarioEditar.Telefono ?? "";

            if (_usuarioEditar.TipoUsuario == TipoUsuario.PasajeroConductor)
            {
                textBoxLicencia.Text = _usuarioEditar.nroLicenciaConductor ?? "";
                dateTimePickerVencimiento.Value = _usuarioEditar.fechaVencimientoLicencia ?? DateTime.Now.AddYears(1); // Valor por defecto si es null
                labelConductor.Visible = true;
                labelLicencia.Visible = true;
                labelVencimiento.Visible = true;
                textBoxLicencia.Visible = true;
                dateTimePickerVencimiento.Visible = true;
            }
            else
            {
                labelConductor.Visible = false;
                labelLicencia.Visible = false;
                labelVencimiento.Visible = false;
                textBoxLicencia.Visible = false;
                dateTimePickerVencimiento.Visible = false;
            }
        }

        private void PrepararModoEdicion()
        {
            this.Text = "Editar Mis Datos";
            btnCrearUsuario.Text = "Guardar Cambios";

            labelContra.Visible = false;
            labelConfirmarContra.Visible = false;
            txtBoxContra.Visible = false;
            txtBoxConfirmaCon.Visible = false;

            // Email no editable
            txtEmail.Enabled = false;

            CargarDatosUsuario();
        }

        private void label4_Click(object sender, EventArgs e) { }

    } 
}
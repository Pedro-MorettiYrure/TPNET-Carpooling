using API.Clients;
using DTOs;
using System;
using System.Windows.Forms;
using static DTOs.UsuarioDTO;
using Domain.Model;

namespace WindowsForms
{
    public partial class FormConductorUpgrade : Form
    {
        private readonly UsuarioDTO _usuarioLogueado;

        public FormConductorUpgrade(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado ?? throw new ArgumentNullException(nameof(usuarioLogueado));

            if (_usuarioLogueado.TipoUsuario != TipoUsuario.Pasajero)
            {
                MessageBox.Show("Ya tienes permisos de conductor o eres Administrador.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNroLicencia.Enabled = false;
                dtpFechaVencimiento.Enabled = false;
                btnConfirmar.Enabled = false;
                this.Load += (s, e) => this.Close(); // Cerrar automáticamente al cargar
            }
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNroLicencia.Text))
            {
                MessageBox.Show("Debe ingresar el número de licencia.", "Dato requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNroLicencia.Focus();
                return;
            }
            if (dtpFechaVencimiento.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("La fecha de vencimiento debe ser futura.", "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaVencimiento.Focus();
                return;
            }

            btnConfirmar.Enabled = false;

            try
            {
                string? tokenActual = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(tokenActual)) throw new UnauthorizedAccessException("Sesión inválida.");

                var dto = new ConductorUpgradeDTO
                {
                    nroLicenciaConductor = txtNroLicencia.Text,
                    fechaVencimientoLicencia = dtpFechaVencimiento.Value
                };

                string? nuevoToken = await UsuarioApiClient.ConvertirAConductorAsync(_usuarioLogueado.IdUsuario, dto, tokenActual);

                if (!string.IsNullOrEmpty(nuevoToken)) 
                {
                    MessageBox.Show("¡Felicidades! Ahora eres un conductor.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _usuarioLogueado.TipoUsuario = TipoUsuario.PasajeroConductor;
                    _usuarioLogueado.nroLicenciaConductor = dto.nroLicenciaConductor;
                    _usuarioLogueado.fechaVencimientoLicencia = dto.fechaVencimientoLicencia;

                    // ACTUALIZAR LA SESIÓN CON EL NUEVO TOKEN
                    SessionManager.IniciarSesion(_usuarioLogueado, nuevoToken);

                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo completar la actualización. Verifique los datos o si ya es conductor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (ArgumentException argEx) { MessageBox.Show($"Datos inválidos: {argEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (InvalidOperationException opEx) { MessageBox.Show($"Operación inválida: {opEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!this.IsDisposed) { btnConfirmar.Enabled = true; }
            }
        }
    }
}
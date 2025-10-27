using DTOs;
using API.Clients;
using System;
using System.Windows.Forms;
using System.Linq; 
using System.Globalization; 

namespace WindowsForms
{
    public partial class LocalidadDetalle : Form
    {
        public LocalidadDTO Localidad { get; set; } = new LocalidadDTO();
        public FormMode Mode { get; set; } = FormMode.Add;

        public enum FormMode { Add, Update }

        private FormMode mode;

        public LocalidadDetalle()
        {
            InitializeComponent();
            this.Load += LocalidadDetalle_Load;
        }

        private void LocalidadDetalle_Load(object sender, EventArgs e)
        {
            SetFormMode(Mode);
            if (Mode == FormMode.Update)
            {
                SetLocalidad();
            }
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!ValidateLocalidad()) return;

            btnConfirmar.Enabled = false;

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("Sesión inválida. Se requieren permisos de Administrador.");

                Localidad.Nombre = txtNombre.Text;
                Localidad.CodPostal = txtCodPostal.Text;

                if (this.Mode == FormMode.Update)
                {
                    await LocalidadApiClient.UpdateAsync(Localidad, token);
                    MessageBox.Show("Localidad actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await LocalidadApiClient.AddAsync(Localidad, token); 
                    MessageBox.Show("Localidad agregada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (ArgumentException argEx) { MessageBox.Show($"Datos inválidos: {argEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (Exception ex) { MessageBox.Show($"Error al guardar la localidad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { if (!this.IsDisposed) btnConfirmar.Enabled = true; }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SetLocalidad()
        {
            if (Localidad == null) return;
            this.txtCodPostal.Text = this.Localidad.CodPostal;
            this.txtNombre.Text = this.Localidad.Nombre;
        }

        private void SetFormMode(FormMode value)
        {
            mode = value; 
            Mode = value; 

            txtCodPostal.Enabled = (Mode == FormMode.Add);
            this.Text = (Mode == FormMode.Add) ? "Nueva Localidad" : "Editar Localidad";

            labelCodPostal.Visible = true;
            labelNombre.Visible = true;
            label1.Visible = true;
        }

        private bool ValidateLocalidad()
        {
            bool isValid = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(this.txtNombre.Text))
            {
                isValid = false;
                errorProvider.SetError(txtNombre, "El Nombre de la localidad es Requerido.");
            }
            if (string.IsNullOrWhiteSpace(this.txtCodPostal.Text))
            {
                isValid = false;
                errorProvider.SetError(txtCodPostal, "El Código Postal es Requerido.");
            }
            else if (Mode == FormMode.Add && (txtCodPostal.Text.Length != 4 || !txtCodPostal.Text.All(char.IsDigit)))
            {
                isValid = false;
                errorProvider.SetError(txtCodPostal, "El Código Postal debe tener 4 dígitos numéricos.");
            }

            return isValid;
        }
    }
}
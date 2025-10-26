using DTOs;
using API.Clients;
using System;
using System.Windows.Forms;
using System.Globalization;

namespace WindowsForms
{
    public partial class VehiculoDetalle : Form
    {
        public VehiculoDTO Vehiculo { get; set; } = new VehiculoDTO();
        public FormMode Mode { get; set; } = FormMode.Add;

        public enum FormMode { Add, Update }

        public VehiculoDetalle()
        {
            InitializeComponent();
            
            this.Load += VehiculoDetalle_Load;
        }

        private void VehiculoDetalle_Load(object sender, EventArgs e)
        {
            SetFormMode(Mode);
            if (Mode == FormMode.Update)
            {
                SetVehiculo();
            }
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!ValidateVehiculo())
            {
                return;
            }

            btnConfirmar.Enabled = false;

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("Sesión inválida.");

                Vehiculo.Patente = txtPatente.Text.ToUpper(); 
                Vehiculo.Color = txtColor.Text;
                Vehiculo.Marca = txtMarca.Text;
                Vehiculo.Modelo = txtModelo.Text;
                Vehiculo.CantLugares = int.Parse(txtCantLugares.Text, CultureInfo.InvariantCulture);

                if (Mode == FormMode.Update)
                {
                    await VehiculoApiClient.UpdateAsync(Vehiculo, token);
                    MessageBox.Show("Vehículo actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else 
                {
                    await VehiculoApiClient.AddAsync(Vehiculo, token);
                    MessageBox.Show("Vehículo agregado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (ArgumentException argEx) { MessageBox.Show($"Datos inválidos: {argEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } // Ej: Patente duplicada (400)
            catch (InvalidOperationException opEx) { MessageBox.Show($"Error: {opEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } // Ej: Conflicto (409)
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el vehículo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!this.IsDisposed) btnConfirmar.Enabled = true;
            }
        }

        private void SetVehiculo()
        {
            if (Vehiculo == null) return;
            txtPatente.Text = Vehiculo.Patente;
            txtModelo.Text = Vehiculo.Modelo;
            txtMarca.Text = Vehiculo.Marca;
            txtColor.Text = Vehiculo.Color;
            txtCantLugares.Text = Vehiculo.CantLugares.ToString();
        }

        private void SetFormMode(FormMode value)
        {
            Mode = value;
            txtPatente.Enabled = (Mode == FormMode.Add);

            if (Mode == FormMode.Add) this.Text = "Nuevo Vehículo";
            if (Mode == FormMode.Update) this.Text = "Editar Vehículo";

            labelMarca.Visible = true;
            labelModelo.Visible = true;
            labelCantLugares.Visible = true;
            labelColor.Visible = true;
            labelPatente.Visible = true;
        }

        private bool ValidateVehiculo()
        {
            bool isValid = true;
            errorProvider1.Clear(); 

            if (string.IsNullOrWhiteSpace(txtPatente.Text))
            {
                isValid = false;
                errorProvider1.SetError(txtPatente, "La patente es requerida.");
            }
            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                isValid = false;
                errorProvider1.SetError(txtModelo, "El modelo es requerido.");
            }
            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                isValid = false;
                errorProvider1.SetError(txtMarca, "La marca es requerida.");
            }
            if (string.IsNullOrWhiteSpace(txtColor.Text))
            {
                isValid = false;
                errorProvider1.SetError(txtColor, "El color es requerido.");
            }
            if (!int.TryParse(txtCantLugares.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int cant) || cant <= 0)
            {
                isValid = false;
                errorProvider1.SetError(txtCantLugares, "La cantidad de lugares debe ser un número entero positivo.");
            }

            return isValid;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close();
        }
    }
}
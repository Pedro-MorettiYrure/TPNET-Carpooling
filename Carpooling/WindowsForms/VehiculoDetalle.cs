using DTOs;
using API.Clients;
using System;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class VehiculoDetalle : Form
    {
        public VehiculoDetalle()
        {
            InitializeComponent();
            Mode = FormMode.Add;
        }

        public enum FormMode
        {
            Add,
            Update
        }

        private VehiculoDTO vehiculo;
        private FormMode mode;

        public VehiculoDTO Vehiculo
        {
            get { return vehiculo; }
            set
            {
                vehiculo = value;
                SetVehiculo();
            }
        }

        public FormMode Mode
        {
            get { return mode; }
            set { SetFormMode(value); }
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidateVehiculo())
            {
                try
                {
                    vehiculo.Patente = txtPatente.Text;
                    vehiculo.Color = txtColor.Text;
                    vehiculo.Marca = txtMarca.Text;
                    vehiculo.Modelo = txtModelo.Text;
                    vehiculo.CantLugares = int.Parse(txtCantLugares.Text);

                    if (Mode == FormMode.Update)
                    {
                        await VehiculoApiClient.UpdateAsync(vehiculo);
                    }
                    else
                    {
                        await VehiculoApiClient.AddAsync(vehiculo);
                    }

                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetVehiculo()
        {
            txtPatente.Text = vehiculo.Patente;
            txtModelo.Text = vehiculo.Modelo;
            txtMarca.Text = vehiculo.Marca;
            txtColor.Text = vehiculo.Color;
            txtCantLugares.Text = vehiculo.CantLugares.ToString();
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            txtPatente.Enabled = mode == FormMode.Add;

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
                errorProvider1.SetError(txtPatente, "La patente es requerida");
            }
            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                isValid = false;
                errorProvider1.SetError(txtModelo, "El modelo es requerido");
            }
            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                isValid = false;
                errorProvider1.SetError(txtMarca, "La marca es requerida");
            }
            if (string.IsNullOrWhiteSpace(txtColor.Text))
            {
                isValid = false;
                errorProvider1.SetError(txtColor, "El color es requerido");
            }
            if (!int.TryParse(txtCantLugares.Text, out int cant) || cant <= 0)
            {
                isValid = false;
                errorProvider1.SetError(txtCantLugares, "La cantidad de lugares debe ser un número positivo");
            }

            return isValid;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

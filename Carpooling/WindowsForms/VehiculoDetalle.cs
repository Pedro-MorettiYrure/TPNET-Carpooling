using DTOs;
using API.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                this.SetVehiculo();
            }
        }

        public FormMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                SetFormMode(value);
            }
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (this.ValidateVehiculo())
            {
                try
                {
                    this.Vehiculo.Patente = txtPatente.Text;
                    this.Vehiculo.Color = txtColor.Text;
                    this.Vehiculo.Marca = txtMarca.Text;
                    this.Vehiculo.Modelo = txtModelo.Text;
                    //this.Vehiculo.CantLugares = txtCantLugares.Text;
                    //El Detalle se esta llevando la responsabilidad de llamar al servicio
                    //pero tal vez deberia ser solo una vista y que esta responsabilidad quede
                    //en la Lista o tal vez en un Presenter o Controler
                    this.Vehiculo.CantLugares = int.Parse(txtCantLugares.Text);

                    if (this.Mode == FormMode.Update)
                    {
                        await API.Clients.VehiculoApiClient.UpdateAsync(this.Vehiculo);
                    }
                    else
                    {
                        await API.Clients.VehiculoApiClient.AddAsync(this.Vehiculo);
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
            this.txtPatente.Text = this.Vehiculo.Patente;
            this.txtModelo.Text = this.Vehiculo.Modelo;
            this.txtMarca.Text = this.Vehiculo.Marca;
            this.txtColor.Text = this.Vehiculo.Color;
            this.txtCantLugares.Text= this.Vehiculo.CantLugares.ToString();
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (Mode == FormMode.Add)
            {
                labelMarca.Visible = true;
                labelModelo.Visible = true;
                labelCantLugares.Visible = true;
                labelColor.Visible = true;
                labelPatente.Visible = true;
            }

            if (Mode == FormMode.Update)
            {
                labelMarca.Visible = true;
                labelModelo.Visible = true;
                labelCantLugares.Visible = true;
                labelColor.Visible = true;
                labelPatente.Visible = true;
                txtPatente.Enabled = false;
            }
        }

        private bool ValidateVehiculo()
        {
            bool isValid = true;

            errorProvider1.SetError(txtPatente, string.Empty);
            errorProvider1.SetError(txtModelo, string.Empty);
            errorProvider1.SetError(txtMarca, string.Empty);
            errorProvider1.SetError(txtColor, string.Empty);
            errorProvider1.SetError(txtCantLugares, string.Empty);


            if (this.txtPatente.Text == string.Empty)
            {
                isValid = false;
                errorProvider1.SetError(txtPatente, "La patente es requerida");
            }
            if (this.txtModelo.Text == string.Empty)
            {
                isValid = false;
                errorProvider1.SetError(txtModelo, "El Modelo es Requerido");
            }
            if (this.txtMarca.Text == string.Empty)
            {
                isValid = false;
                errorProvider1.SetError(txtMarca, "La marca es Requerida");
            }
            if (this.txtColor.Text == string.Empty)
            {
                isValid = false;
                errorProvider1.SetError(txtColor, "El Color es Requerido");
            }
            if (this.txtCantLugares.Text == string.Empty)
            {
                isValid = false;
                errorProvider1.SetError(txtCantLugares, "La cantidad de lugares es Requerida");
            }
            else
            {
                if (!int.TryParse(this.txtCantLugares.Text, out int cantLugares) || cantLugares <= 0)
                {
                    isValid = false;
                    errorProvider1.SetError(txtCantLugares, "La cantidad de lugares debe ser un número positivo");
                }
            }


            return isValid;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

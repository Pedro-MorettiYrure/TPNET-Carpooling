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
    public partial class LocalidadDetalle : Form
    {
        public LocalidadDetalle()
        {
            InitializeComponent();
            Mode = FormMode.Add;
        }

        public enum FormMode
        {
            Add,
            Update
        }

        private LocalidadDTO localidad;
        private FormMode mode;

        public LocalidadDTO Localidad
        {
            get { return localidad; }
            set
            {
                localidad = value;
                this.SetLocalidad();
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
            if (this.ValidateLocalidad())
            {
                try
                {
                    this.Localidad.Nombre = txtNombre.Text;
                    this.Localidad.CodPostal = txtCodPostal.Text;

                    //El Detalle se esta llevando la responsabilidad de llamar al servicio
                    //pero tal vez deberia ser solo una vista y que esta responsabilidad quede
                    //en la Lista o tal vez en un Presenter o Controler

                    if (this.Mode == FormMode.Update)
                    {
                        await LocalidadApiClient.UpdateAsync(this.Localidad);
                    }
                    else
                    {
                        await LocalidadApiClient.AddAsync(this.Localidad);
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetLocalidad()
        {
            this.txtCodPostal.Text = this.Localidad.CodPostal;
            this.txtNombre.Text = this.Localidad.Nombre;
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (Mode == FormMode.Add)
            {
                labelCodPostal.Visible = false;
                labelNombre.Visible = false;
            }

            if (Mode == FormMode.Update)
            {
                labelCodPostal.Visible = true;
                labelNombre.Visible = true;
            }
        }

        private bool ValidateLocalidad()
        {
            bool isValid = true;

            errorProvider.SetError(txtNombre, string.Empty);
            errorProvider.SetError(txtCodPostal, string.Empty);


            if (this.txtNombre.Text == string.Empty)
            {
                isValid = false;
                errorProvider.SetError(txtNombre, "El Nombre de la localidad es Requerido");
            }
            if (this.txtCodPostal.Text == string.Empty)
            {
                isValid = false;
                errorProvider.SetError(txtCodPostal, "El Codigo Postal es Requerido");
            }


            return isValid;
        }
    }
}

using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsForms.LocalidadDetalle;


namespace WindowsForms
{
    public partial class LocalidadLista : Form
    {
        public LocalidadLista()
        {
            InitializeComponent();
        }


        private void LocalidadLista_Load(object sender, EventArgs e)
        {
            this.GetAllAndLoad();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            LocalidadDetalle formCrear = new LocalidadDetalle();
            LocalidadDTO localidadNueva = new LocalidadDTO();

            formCrear.Mode = FormMode.Add;
            formCrear.Localidad = localidadNueva;

            formCrear.ShowDialog();

            this.GetAllAndLoad();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
        private async void tsbEditar_Click(object sender, EventArgs e)
        {
            try
            {
                LocalidadDetalle formEditar = new LocalidadDetalle();
                string codPostal = this.SelectedItem().CodPostal;

                LocalidadDTO localidad = await LocalidadApiClient.GetAsync(codPostal);

                formEditar.Mode = FormMode.Update;
                formEditar.Localidad = localidad;

                formEditar.ShowDialog();

                this.GetAllAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar localidad para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void tsbEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string codPostal = this.SelectedItem().CodPostal;

                var result = MessageBox.Show($"¿Está seguro que desea eliminar la localidad con codigo postal {codPostal}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await LocalidadApiClient.DeleteAsync(codPostal);
                    this.GetAllAndLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar localidad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */


        private async void GetAllAndLoad()
        {
            try
            {
                this.dgvLocalidad.DataSource = null;
                this.dgvLocalidad.DataSource = await LocalidadApiClient.GetAllAsync();

                if (this.dgvLocalidad.Rows.Count > 0)
                {
                    this.dgvLocalidad.Rows[0].Selected = true;
                    this.btnEliminar.Enabled = true;
                    this.btnEditar.Enabled = true;
                }
                else
                {
                    this.btnEditar.Enabled = false;
                    this.btnEliminar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de localidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.btnEliminar.Enabled = false;
                this.btnEditar.Enabled = false;
            }
        }

        private LocalidadDTO SelectedItem()
        {
            LocalidadDTO localidad;

            localidad = (LocalidadDTO)dgvLocalidad.SelectedRows[0].DataBoundItem;

            return localidad;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                string codPostal = this.SelectedItem().CodPostal;

                var result = MessageBox.Show($"¿Está seguro que desea eliminar la localidad con codigo postal {codPostal}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await LocalidadApiClient.DeleteAsync(codPostal);
                    this.GetAllAndLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar localidad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                LocalidadDetalle formEditar = new LocalidadDetalle();
                string codPostal = this.SelectedItem().CodPostal;

                LocalidadDTO localidad = await LocalidadApiClient.GetAsync(codPostal);

                formEditar.Mode = FormMode.Update;
                formEditar.Localidad = localidad;

                formEditar.ShowDialog();

                this.GetAllAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar localidad para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tscLocalidades_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }
    }
}

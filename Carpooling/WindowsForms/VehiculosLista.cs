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
using static WindowsForms.VehiculoDetalle;

namespace WindowsForms
{
    public partial class VehiculosLista : Form
    {
        public VehiculosLista()
        {
            InitializeComponent();
        }

        private void VehiculosLista_Load(object sender, EventArgs e)
        {
            this.GetAllAndLoad();
        }

        private async void tsbEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string patente = this.SelectedItem().Patente;

                var result = MessageBox.Show($"¿Está seguro que desea eliminar el vehiculo con patente {patente}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await VehiculoApiClient.DeleteAsync(patente);
                    this.GetAllAndLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar vehiculo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void tsbEditar_Click(object sender, EventArgs e)
        {
            try
            {
                VehiculoDetalle formEditar = new VehiculoDetalle();
                string patente = this.SelectedItem().Patente;

                VehiculoDTO vehiculo = await VehiculoApiClient.GetAsync(patente);

                formEditar.Mode = FormMode.Update;
                formEditar.Vehiculo = vehiculo;

                formEditar.ShowDialog();

                this.GetAllAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar localidad para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            VehiculoDetalle formCrear = new VehiculoDetalle();
            VehiculoDTO vehiculoNuevo = new VehiculoDTO();

            formCrear.Mode = FormMode.Add;
            formCrear.Vehiculo = vehiculoNuevo;

            formCrear.ShowDialog();

            this.GetAllAndLoad();

            formCrear.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void GetAllAndLoad()
        {
            try
            {
                this.dgvVehiculos.DataSource = null;
                this.dgvVehiculos.DataSource = await VehiculoApiClient.GetAllAsync();

                if (this.dgvVehiculos.Rows.Count > 0)
                {
                    this.dgvVehiculos.Rows[0].Selected = true;
                    this.tsbEliminar.Enabled = true;
                    this.tsbEditar.Enabled = true;
                }
                else
                {
                    this.tsbEditar.Enabled = false;
                    this.tsbEliminar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de vehiculos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tsbEliminar.Enabled = false;
                this.tsbEditar.Enabled = false;
            }

        }

        private VehiculoDTO SelectedItem()
        {
            VehiculoDTO vehiculo;

            vehiculo = (VehiculoDTO)dgvVehiculos.SelectedRows[0].DataBoundItem;

            return vehiculo;
        }

    }
}

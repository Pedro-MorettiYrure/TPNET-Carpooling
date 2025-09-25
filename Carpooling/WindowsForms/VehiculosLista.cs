using API.Clients;
using DTOs;
using System;
using System.Linq;
using System.Windows.Forms;
using static WindowsForms.VehiculoDetalle;

namespace WindowsForms
{
    public partial class VehiculosLista : Form
    {
        private readonly UsuarioDTO _usuario;

        public VehiculosLista(UsuarioDTO usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }

        private void VehiculosLista_Load(object sender, EventArgs e)
        {
            this.GetAllAndLoad();
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
                var vehiculos = await VehiculoApiClient.GetByUsuarioAsync(_usuario.IdUsuario);
                this.dgvVehiculos.DataSource = vehiculos.ToList();

                // Ocultás la columna IdUsuario
                if (dgvVehiculos.Columns["IdUsuario"] != null)
                    dgvVehiculos.Columns["IdUsuario"].Visible = false;
                if (dgvVehiculos.Columns["IdVehiculo"] != null)
                    dgvVehiculos.Columns["IdVehiculo"].Visible = false;

                bool tieneFilas = this.dgvVehiculos.Rows.Count > 0;
                this.btnEditar.Enabled = tieneFilas;
                this.btnEliminar.Enabled = tieneFilas;

                if (tieneFilas)
                    this.dgvVehiculos.Rows[0].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de vehículos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.btnEliminar.Enabled = false;
                this.btnEditar.Enabled = false;
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            VehiculoDetalle formCrear = new VehiculoDetalle();
            VehiculoDTO vehiculoNuevo = new VehiculoDTO
            {
                IdUsuario = _usuario.IdUsuario // asignamos al usuario logueado
            };

            formCrear.Mode = FormMode.Add;
            formCrear.Vehiculo = vehiculoNuevo;

            formCrear.ShowDialog();

            this.GetAllAndLoad();
        }

        private VehiculoDTO SelectedItem()
        {
            return (VehiculoDTO)dgvVehiculos.SelectedRows[0].DataBoundItem;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string patente = this.SelectedItem().Patente;

                var result = MessageBox.Show($"¿Está seguro que desea eliminar el vehículo con patente {patente}?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await VehiculoApiClient.DeleteAsync(patente, _usuario.IdUsuario);
                    this.GetAllAndLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar vehículo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                VehiculoDetalle formEditar = new VehiculoDetalle();
                var vehiculo = await VehiculoApiClient.GetAsync(SelectedItem().Patente, _usuario.IdUsuario);

                formEditar.Mode = FormMode.Update;
                formEditar.Vehiculo = vehiculo;

                formEditar.ShowDialog();

                this.GetAllAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vehículo para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

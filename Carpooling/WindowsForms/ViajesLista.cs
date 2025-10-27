using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;
using static WindowsForms.ViajeDetalle;

namespace WindowsForms
{
    public partial class ViajesLista : Form
    {
        private readonly UsuarioDTO _usuario;
        public ViajesLista(UsuarioDTO usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }
        private void ViajesLista_Load(object sender, EventArgs e)
        {
            this.GetAllAndLoad();
        }
        private async Task GetAllAndLoad()
        {
            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("Sesión inválida. Por favor, inicie sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close(); // O redirigir a login
                    return;
                }

                this.dgvViajesLista.DataSource = null;
                // Pasar el token al método del ApiClient
                var viajes = await ViajeApiClient.GetByConductorAsync(_usuario.IdUsuario, token);
                this.dgvViajesLista.DataSource = viajes.ToList();

                if (dgvViajesLista.Columns["IdConductor"] != null) dgvViajesLista.Columns["IdConductor"].Visible = false;
                if (dgvViajesLista.Columns["IdVehiculo"] != null) dgvViajesLista.Columns["IdVehiculo"].Visible = false;


                bool tieneFilas = this.dgvViajesLista.Rows.Count > 0;
                this.btnEliminar.Enabled = tieneFilas;
                this.btnEditar.Enabled = tieneFilas;
                if (tieneFilas) dgvViajesLista.Rows[0].Selected = true;
            }
            catch (UnauthorizedAccessException authEx)
            {
                MessageBox.Show($"Error de autorización: {authEx.Message}. Verifique su sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SessionManager.CerrarSesion();
                this.Close(); // O redirigir a login
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de viajes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.btnEliminar.Enabled = false;
                this.btnEditar.Enabled = false;
            }
        }

        private void btnSalirViajesLista_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            using (ViajeDetalle formCrear = new ViajeDetalle(_usuario))
            {
                // formCrear.Mode ya se establece en el constructor
                if (formCrear.ShowDialog() == DialogResult.OK)
                {
                    this.GetAllAndLoad(); // Recargar si se creó OK
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvViajesLista.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un viaje para cancelar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvViajesLista.SelectedRows[0];
            var viajeDTO = selectedRow.DataBoundItem as ViajeDTO;

            if (viajeDTO == null) return;

            DialogResult result = MessageBox.Show(
                $"¿Está seguro de que desea cancelar el viaje con ID: {viajeDTO.IdViaje}?",
                "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Obtener token
                    string? token = SessionManager.JwtToken;
                    if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Sesión inválida.");

                    await ViajeApiClient.DeleteAsync(viajeDTO.IdViaje, token);

                    MessageBox.Show("Viaje cancelado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await GetAllAndLoad();
                }
                catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                catch (InvalidOperationException sessionEx) { MessageBox.Show($"Error: {sessionEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cancelar el viaje.\nDetalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvViajesLista.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un viaje para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvViajesLista.SelectedRows[0];
            var viajeSeleccionado = selectedRow.DataBoundItem as ViajeDTO;

            if (viajeSeleccionado == null) return;

            try
            {
                // El formulario ViajeDetalle obtendrá el token antes de llamar a UpdateAsync
                using (var formDetalle = new ViajeDetalle(_usuario, viajeSeleccionado))
                {
                    if (formDetalle.ShowDialog() == DialogResult.OK)
                    {
                        await GetAllAndLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al preparar la edición del viaje: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnVerSolicitudes_Click(object sender, EventArgs e)
        {
            if (dgvViajesLista.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un viaje de la lista para ver sus solicitudes.",
                                "Selección Requerida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvViajesLista.SelectedRows[0];
            var viajeSeleccionado = selectedRow.DataBoundItem as ViajeDTO;

            if (viajeSeleccionado == null)
            {
                MessageBox.Show("Error al obtener los datos del viaje seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SolicitudesLista formSolicitudes = new SolicitudesLista(viajeSeleccionado.IdViaje, _usuario))
            {
                formSolicitudes.ShowDialog(); 
            }

            await GetAllAndLoad(); //actualiza lista de viajes
        }

        private void dgvViajesLista_SelectionChanged(object sender, EventArgs e)
        {
            btnVerSolicitudes.Enabled = (dgvViajesLista.SelectedRows.Count > 0);
        }
    }
}
    


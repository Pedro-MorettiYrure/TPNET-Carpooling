using API.Clients;
using DTOs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class VehiculosLista : Form
    {
        private readonly UsuarioDTO _usuario;

        public VehiculosLista(UsuarioDTO usuario)
        {
            InitializeComponent();
            _usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
        }

        private void VehiculosLista_Load(object sender, EventArgs e)
        {
            Task.Run(() => GetAllAndLoad());
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task GetAllAndLoad()
        {
            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token))
                {
                    this.Invoke((MethodInvoker)delegate {
                        MessageBox.Show("Sesión inválida. Por favor, inicie sesión.", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    });
                    return;
                }

                var vehiculos = await VehiculoApiClient.GetByUsuarioAsync(_usuario.IdUsuario, token);
                var listaVehiculos = vehiculos.ToList();

                this.Invoke((MethodInvoker)delegate {
                    dgvVehiculos.DataSource = null; 
                    dgvVehiculos.DataSource = listaVehiculos;

                    if (dgvVehiculos.Columns.Contains("IdUsuario")) dgvVehiculos.Columns["IdUsuario"].Visible = false;
                    if (dgvVehiculos.Columns.Contains("IdVehiculo")) dgvVehiculos.Columns["IdVehiculo"].Visible = false;

                    bool tieneFilas = listaVehiculos.Any();
                    btnEditar.Enabled = tieneFilas;
                    btnEliminar.Enabled = tieneFilas;

                    if (tieneFilas) dgvVehiculos.Rows[0].Selected = true;
                });
            }
            catch (UnauthorizedAccessException authEx) 
            {
                this.Invoke((MethodInvoker)delegate {
                    MessageBox.Show($"Error de autorización: {authEx.Message}. Verifique su sesión.", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SessionManager.CerrarSesion();
                    this.Close();
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate {
                    MessageBox.Show($"Error al cargar la lista de vehículos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnEliminar.Enabled = false;
                    btnEditar.Enabled = false;
                });
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            using (VehiculoDetalle formCrear = new VehiculoDetalle()) 
            {
                VehiculoDTO vehiculoNuevo = new VehiculoDTO { IdUsuario = _usuario.IdUsuario };
                formCrear.Vehiculo = vehiculoNuevo; 

                if (formCrear.ShowDialog() == DialogResult.OK)
                {
                    Task.Run(() => GetAllAndLoad()); 
                }
            }
        }

        private VehiculoDTO? SelectedItem()
        {
            if (dgvVehiculos.SelectedRows.Count > 0 && dgvVehiculos.SelectedRows[0].DataBoundItem is VehiculoDTO vehiculo)
            {
                return vehiculo;
            }
            return null;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var vehiculoSeleccionado = SelectedItem();
            if (vehiculoSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un vehículo para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string patente = vehiculoSeleccionado.Patente;
            var result = MessageBox.Show($"¿Está seguro que desea eliminar el vehículo con patente {patente}?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                   
                    string? token = SessionManager.JwtToken;
                    if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Sesión inválida.");

                    await VehiculoApiClient.DeleteAsync(patente, _usuario.IdUsuario, token);

                    MessageBox.Show("Vehículo eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await GetAllAndLoad();
                }
                catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                catch (InvalidOperationException sessionEx) { MessageBox.Show($"Error: {sessionEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                catch (Exception ex) 
                {
                    MessageBox.Show($"Error al eliminar vehículo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            var vehiculoSeleccionado = SelectedItem();
            if (vehiculoSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un vehículo para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Sesión inválida.");

                
                var vehiculoParaEditar = await VehiculoApiClient.GetAsync(vehiculoSeleccionado.Patente, _usuario.IdUsuario, token);

                if (vehiculoParaEditar == null)
                { 
                    MessageBox.Show("El vehículo seleccionado ya no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await GetAllAndLoad(); 
                    return;
                }


                using (VehiculoDetalle formEditar = new VehiculoDetalle())
                {
                    formEditar.Mode = VehiculoDetalle.FormMode.Update; 
                    formEditar.Vehiculo = vehiculoParaEditar; 

                    if (formEditar.ShowDialog() == DialogResult.OK)
                    {
                        await GetAllAndLoad(); 
                    }
                }
            }
            catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (InvalidOperationException sessionEx) { MessageBox.Show($"Error: {sessionEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (KeyNotFoundException)
            { 
                MessageBox.Show("El vehículo seleccionado no fue encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await GetAllAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vehículo para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
} 
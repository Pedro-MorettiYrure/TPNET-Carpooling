using API.Clients;
using DTOs;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class LocalidadLista : Form
    {
        public LocalidadLista()
        {
            InitializeComponent();
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            // Configurar DGV
            dgvLocalidad.AutoGenerateColumns = false;
            dgvLocalidad.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocalidad.MultiSelect = false;
            dgvLocalidad.ReadOnly = true;
            dgvLocalidad.AllowUserToAddRows = false;
            dgvLocalidad.AllowUserToDeleteRows = false;

            dgvLocalidad.Columns.Clear();
            dgvLocalidad.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código Postal", DataPropertyName = "CodPostal", Width = 100 });
            dgvLocalidad.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", DataPropertyName = "Nombre", Width = 250, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }


        private void LocalidadLista_Load(object sender, EventArgs e)
        {
            _ = GetAllAndLoadAsync();
        }

        private async Task GetAllAndLoadAsync()
        {
            try
            {
                this.Invoke((MethodInvoker)delegate {
                    dgvLocalidad.DataSource = null;
                    btnEditar.Enabled = false; 
                    btnEliminar.Enabled = false;
                });

                var localidades = (await LocalidadApiClient.GetAllAsync()).ToList();

                this.Invoke((MethodInvoker)delegate {
                    dgvLocalidad.DataSource = localidades;
                    bool tieneFilas = localidades.Any();
                    btnEditar.Enabled = tieneFilas;
                    btnEliminar.Enabled = tieneFilas;
                    if (tieneFilas) dgvLocalidad.Rows[0].Selected = true;
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate {
                    MessageBox.Show($"Error al cargar la lista de localidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                });
            }
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            using (LocalidadDetalle formCrear = new LocalidadDetalle())
            {
                formCrear.Mode = LocalidadDetalle.FormMode.Add;
                formCrear.Localidad = new LocalidadDTO(); 

                if (formCrear.ShowDialog() == DialogResult.OK)
                {
                    await GetAllAndLoadAsync(); 
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private LocalidadDTO? SelectedItem()
        {
            if (dgvLocalidad.SelectedRows.Count > 0 && dgvLocalidad.SelectedRows[0].DataBoundItem is LocalidadDTO localidad)
            {
                return localidad;
            }
            MessageBox.Show("Por favor, seleccione una localidad de la lista.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            var localidadSeleccionada = SelectedItem();
            if (localidadSeleccionada == null) return; 

            string codPostal = localidadSeleccionada.CodPostal;
            var result = MessageBox.Show($"¿Está seguro que desea eliminar la localidad '{localidadSeleccionada.Nombre}' (CP: {codPostal})?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                btnEliminar.Enabled = false; 
                btnEditar.Enabled = false;
                try
                {
                    string? token = SessionManager.JwtToken;
                    if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("Sesión inválida. Se requieren permisos de Administrador.");

                    await LocalidadApiClient.DeleteAsync(codPostal, token);

                    MessageBox.Show("Localidad eliminada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await GetAllAndLoadAsync(); 
                }
                catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                catch (InvalidOperationException opEx) { MessageBox.Show($"No se puede eliminar: {opEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } // Ej: Conflicto 409
                catch (Exception ex) { MessageBox.Show($"Error al eliminar localidad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally
                {
                    if (!this.IsDisposed && dgvLocalidad.Rows.Count == 0)
                    {
                        btnEliminar.Enabled = false;
                        btnEditar.Enabled = false;
                    }
                }
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            var localidadSeleccionada = SelectedItem();
            if (localidadSeleccionada == null) return;

            btnEditar.Enabled = false; 
            btnEliminar.Enabled = false;

            try
            {
                var localidadParaEditar = await LocalidadApiClient.GetAsync(localidadSeleccionada.CodPostal);

                if (localidadParaEditar == null)
                {
                    MessageBox.Show("La localidad seleccionada ya no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await GetAllAndLoadAsync();
                    return;
                }

                using (LocalidadDetalle formEditar = new LocalidadDetalle())
                {
                    formEditar.Mode = LocalidadDetalle.FormMode.Update;
                    formEditar.Localidad = localidadParaEditar; 

                    if (formEditar.ShowDialog() == DialogResult.OK)
                    {
                        await GetAllAndLoadAsync(); 
                    }
                    else
                    {
                        btnEditar.Enabled = true;
                        btnEliminar.Enabled = true;
                    }
                }
            }
            catch (KeyNotFoundException) { MessageBox.Show("La localidad seleccionada no fue encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); await GetAllAndLoadAsync(); }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar localidad para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnEditar.Enabled = true; 
                btnEliminar.Enabled = true;
            }
            
        }

        private void tscLocalidades_TopToolStripPanel_Click(object sender, EventArgs e) { }

    }
} 
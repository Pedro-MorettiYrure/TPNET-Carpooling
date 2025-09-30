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
                this.dgvViajesLista.DataSource = null;
                var viajes = await ViajeApiClient.GetByConductorAsync(_usuario.IdUsuario);
                this.dgvViajesLista.DataSource = viajes.ToList();

                //if (dgvViajesLista.Columns["IdViaje"]) != null)
                //{
                //        dgvViajesLista.Columns["IdViaje"].Visible = false;
                //}
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
            ViajeDetalle formCrear = new ViajeDetalle(_usuario);
            formCrear.Mode = FormMode.Add;
            formCrear.ShowDialog();

            this.GetAllAndLoad();
            //ViajeDTO viajeNuevo = new ViajeDTO
            //{
            //    IdConductor = _usuario.IdUsuario
            //}
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
                "Confirmar Cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // en el API, esta llamada DELETE realiza la baja lógica cambiando el estado.
                    await ViajeApiClient.DeleteAsync(viajeDTO.IdViaje);

                    MessageBox.Show("Viaje cancelado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await GetAllAndLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error al cancelar el viaje.\nDetalle: {ex.Message}",
                        "Error de Cancelación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
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
            var viajeDTO = selectedRow.DataBoundItem as ViajeDTO;

            if (viajeDTO == null) return;

            // abrimos el formulario de detalle en modo EDICIÓN
            // Usamos el constructor que creamos en ViajeDetalle.cs
            using (var formDetalle = new ViajeDetalle(_usuario, viajeDTO)) // 
            {
                if (formDetalle.ShowDialog() == DialogResult.OK)
                {
                    await GetAllAndLoad();
                }
            }
        }
    }
}

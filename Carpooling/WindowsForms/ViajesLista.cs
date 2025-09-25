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
        private async void GetAllAndLoad()
        {
            try
            {
                this.dgvViajesLista.DataSource = null;
                var viajes = await ViajeApiClient.GetByUsuarioAsync(_usuario.IdUsuario);
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
            //ViajeDTO viajeNuevo = new ViajeDTO
            //{
            //    IdConductor = _usuario.IdUsuario
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients; 
using DTOs;      
using Domain.Model;

namespace WindowsForms
{
    public partial class SolicitudesListaPasajero : Form
    {
        private readonly UsuarioDTO _pasajeroLogueado;
        private List<SolicitudViajeDTO> _misSolicitudes; 

        public SolicitudesListaPasajero(UsuarioDTO pasajeroLogueado)
        {
            InitializeComponent(); 
            _pasajeroLogueado = pasajeroLogueado;
            _misSolicitudes = new List<SolicitudViajeDTO>();
        }

        private async void FormMisSolicitudes_Load(object sender, EventArgs e)
        {
            await CargarMisSolicitudesAsync();
            ActualizarEstadoBotones(); 
        }

        private async Task CargarMisSolicitudesAsync()
        {
            string? token = SessionManager.JwtToken; 
            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Error de sesión.", "Error");
                btnCancelarSolicitud.Enabled = false;
                return;
            }

            try
            {
                dgvMisSolicitudes.DataSource = null;

                var todasMisSolicitudes = await SolicitudViajeApiClient.GetSolicitudesPorPasajeroAsync(_pasajeroLogueado.IdUsuario, token);
                _misSolicitudes = todasMisSolicitudes?.ToList() ?? new List<SolicitudViajeDTO>();

                dgvMisSolicitudes.DataSource = _misSolicitudes;

                if (dgvMisSolicitudes.DataSource != null && _misSolicitudes.Any())
                {
                    // ocultar columnas q no queremos q se muestren
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.IdSolicitud)].Visible = false;
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.IdViaje)].Visible = false;
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.IdPasajero)].Visible = false;
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.NombrePasajero)].Visible = false; // Ya sabe que es él
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.ApellidoPasajero)].Visible = false;
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.IdConductor)].Visible = false;

                    // columnas q queremos mostrar
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.SolicitudFecha)].HeaderText = "Fecha Solicitud";
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.FechaHoraViaje)].HeaderText = "Fecha Viaje";
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.OrigenViajeNombre)].HeaderText = "Origen";
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.DestinoViajeNombre)].HeaderText = "Destino";
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.NombreConductor)].HeaderText = "Conductor";
                    dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.Estado)].HeaderText = "Estado Solicitud"; // Pendiente, Aprobada, Rechazada, Cancelada
                    if (dgvMisSolicitudes.Columns.Contains(nameof(SolicitudViajeDTO.EstadoDelViaje)))
                    {
                        dgvMisSolicitudes.Columns[nameof(SolicitudViajeDTO.EstadoDelViaje)].HeaderText = "Estado Viaje"; // Para saber si ya se realizó
                    }


                    dgvMisSolicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else { MessageBox.Show("Aún no has enviado ninguna solicitud de viaje.", "Mis Solicitudes", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tus solicitudes: {ex.Message}", "Error");
                btnCancelarSolicitud.Enabled = false;
            }
        }

        private void dgvMisSolicitudes_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            bool haySeleccion = dgvMisSolicitudes.SelectedRows.Count > 0;
            bool esCancelable = false;

            if (haySeleccion)
            {
                var selectedRow = dgvMisSolicitudes.SelectedRows[0];
                var solicitud = selectedRow.DataBoundItem as SolicitudViajeDTO;

                esCancelable = (solicitud != null &&
                                (solicitud.Estado == EstadoSolicitud.Pendiente ||
                                 solicitud.Estado == EstadoSolicitud.Aprobada  ) &&
                                solicitud.FechaHoraViaje.HasValue &&
                                solicitud.FechaHoraViaje.Value > DateTime.Now);
            }
            btnCancelarSolicitud.Enabled = esCancelable;
        }

        private async void btnCancelarSolicitud_Click(object sender, EventArgs e)
        {
            if (dgvMisSolicitudes.SelectedRows.Count == 0) return;
            var selectedRow = dgvMisSolicitudes.SelectedRows[0];
            var solicitud = selectedRow.DataBoundItem as SolicitudViajeDTO;

            if (solicitud == null ||
                !(solicitud.Estado == EstadoSolicitud.Pendiente || solicitud.Estado == EstadoSolicitud.Aprobada) ||
                !(solicitud.FechaHoraViaje > DateTime.Now))
            {
                return; //esta validacion la hacemos por seguridad xq ya validamos en el boton
            }

            DialogResult confirm = MessageBox.Show("¿Está seguro de que desea cancelar esta solicitud?",
                                               "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) { MessageBox.Show("Error de sesión."); return; }

                await SolicitudViajeApiClient.CancelarSolicitudPasajeroAsync(solicitud.IdSolicitud, token);

                MessageBox.Show("Solicitud cancelada con éxito.", "Cancelación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarMisSolicitudesAsync(); // actuakizamos la lista
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cancelar la solicitud:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
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
    public partial class ViajesListaPasajero : Form
    {
        private readonly UsuarioDTO _pasajeroLogueado;
        private List<SolicitudViajeDTO> _misViajesConfirmados;

        public ViajesListaPasajero(UsuarioDTO pasajeroLogueado)
        {
            InitializeComponent(); 
            _pasajeroLogueado = pasajeroLogueado;
            _misViajesConfirmados = new List<SolicitudViajeDTO>();
            ViajesListaPasajero_Load(this, EventArgs.Empty); 
        }

        private async void ViajesListaPasajero_Load(object sender, EventArgs e)
        {
            await CargarMisViajesAsync();
            ActualizarEstadoBotones();
        }

        private async Task CargarMisViajesAsync()
        {
            string? token = SessionManager.JwtToken; 
            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Error de sesión. No se encontró el token.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnVerDetalles.Enabled = false;
                btnCancelarSolicitud.Enabled = false;
                return;
            }

            try
            {
                dgvMisViajes.DataSource = null;

                // Obtener TODAS las solicitudes del pasajero
                var todasMisSolicitudes = await SolicitudViajeApiClient.GetSolicitudesPorPasajeroAsync(_pasajeroLogueado.IdUsuario, token);

                // Filtrar solo las APROBADAS para mostrar (incluye pasadas y futuras)
                _misViajesConfirmados = todasMisSolicitudes?.Where(s => s.Estado == EstadoSolicitud.Aprobada).ToList()
                                        ?? new List<SolicitudViajeDTO>();

                dgvMisViajes.DataSource = _misViajesConfirmados;

                if (dgvMisViajes.DataSource != null && _misViajesConfirmados.Any())
                {
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.IdSolicitud)].Visible = false;
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.IdViaje)].Visible = false;
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.IdPasajero)].Visible = false;
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.SolicitudFecha)].Visible = false;
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.NombrePasajero)].Visible = false;
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.ApellidoPasajero)].Visible = false;
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.Estado)].Visible = false; 
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.IdConductor)].Visible = false;

                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.FechaHoraViaje)].HeaderText = "Fecha y Hora";
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.OrigenViajeNombre)].HeaderText = "Origen";
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.DestinoViajeNombre)].HeaderText = "Destino";
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.NombreConductor)].HeaderText = "Conductor"; 
                    dgvMisViajes.Columns[nameof(SolicitudViajeDTO.ApellidoConductor)].HeaderText = "Apellido Cond.";
                    if (dgvMisViajes.Columns.Contains(nameof(SolicitudViajeDTO.EstadoDelViaje)))
                    {
                        dgvMisViajes.Columns[nameof(SolicitudViajeDTO.EstadoDelViaje)].HeaderText = "Estado del Viaje";
                        dgvMisViajes.Columns[nameof(SolicitudViajeDTO.EstadoDelViaje)].Visible = true;
                    }

                    dgvMisViajes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar mis viajes confirmados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnVerDetalles.Enabled = false;
                btnCancelarSolicitud.Enabled = false;
            }
        }

       
        private void dgvMisViajes_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            bool haySeleccion = dgvMisViajes.SelectedRows.Count > 0;
            bool esCancelable = false; 

            if (haySeleccion)
            {
                var selectedRow = dgvMisViajes.SelectedRows[0];
                var solicitud = selectedRow.DataBoundItem as SolicitudViajeDTO;

                // Cancelar se habilita si la solicitud está Aprobada Y el viaje es futuro
                esCancelable = (solicitud != null &&
                                solicitud.Estado == EstadoSolicitud.Aprobada && 
                                solicitud.FechaHoraViaje.HasValue &&
                                solicitud.FechaHoraViaje.Value > DateTime.Now);
            }

            btnVerDetalles.Enabled = haySeleccion;
            btnCancelarSolicitud.Enabled = esCancelable;
            // btnCalificarConductor.Enabled = ... (Lógica que hicimos antes, si tienes el botón)
        }

        
        private void btnVerDetalles_Click(object sender, EventArgs e)
        {
            if (dgvMisViajes.SelectedRows.Count == 0) return;
            var selectedRow = dgvMisViajes.SelectedRows[0];
            var solicitud = selectedRow.DataBoundItem as SolicitudViajeDTO;
            if (solicitud == null) return;

            string detalles = $"Viaje ID: {solicitud.IdViaje}\n" +
                              $"Fecha: {solicitud.FechaHoraViaje:dd/MM/yyyy HH:mm}\n" +
                              $"Origen: {solicitud.OrigenViajeNombre}\n" +
                              $"Destino: {solicitud.DestinoViajeNombre}\n" +
                              $"Estado Solicitud: {solicitud.Estado}\n" + 
                              $"Estado del Viaje: {solicitud.EstadoDelViaje?.ToString() ?? "N/A"}\n" + // Muestra estado del viaje
                              $"Conductor: {solicitud.NombreConductor} {solicitud.ApellidoConductor}\n"; 

            MessageBox.Show(detalles, "Detalles del Viaje Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       
        private async void btnCancelarSolicitud_Click(object sender, EventArgs e)
        {
            if (dgvMisViajes.SelectedRows.Count == 0) return;
            var selectedRow = dgvMisViajes.SelectedRows[0];
            var solicitud = selectedRow.DataBoundItem as SolicitudViajeDTO;
            if (solicitud == null || solicitud.Estado != EstadoSolicitud.Aprobada || !(solicitud.FechaHoraViaje > DateTime.Now)) return;
            //validamos q la solicitud este aprobada porq aca estamos mostrando los viajes ya confirmados, tambien se puede cancelar una solicitud pendiente pero eso lo manejamos en el form de las solicitudes de usuario
            DialogResult confirm = MessageBox.Show("¿Está seguro de cancelar su lugar en este viaje?",
                                               "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) { MessageBox.Show("Error de sesión."); return; }

                await SolicitudViajeApiClient.CancelarSolicitudPasajeroAsync(solicitud.IdSolicitud, token);

                MessageBox.Show("Su participación en este viaje ha sido cancelada.", "Cancelación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarMisViajesAsync(); 
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cancelar la solicitud:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCalificarConductor_Click(object sender, EventArgs e) 
        {
            
            if (dgvMisViajes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un viaje para poder calificar al conductor.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvMisViajes.SelectedRows[0];
            var solicitud = selectedRow.DataBoundItem as SolicitudViajeDTO;

            if (solicitud == null || !solicitud.EstadoDelViaje.HasValue || solicitud.EstadoDelViaje.Value != EstadoViaje.Realizado)
            {
                MessageBox.Show("Solo puedes calificar viajes que ya se han realizado.", "Acción no Válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idViaje = solicitud.IdViaje;
            int idConductorACalificar = solicitud.IdConductor ?? 0;
            int idPasajeroQueCalifica = _pasajeroLogueado.IdUsuario; 

            if (idConductorACalificar == 0)
            {
                MessageBox.Show("No se pudo obtener la información del conductor para calificar.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (FormCalificar formCalificar = new FormCalificar(
                    idPasajeroQueCalifica,      
                    idConductorACalificar,   
                    idViaje,
                    RolCalificado.Conductor
                ))
                {
                    if (formCalificar.ShowDialog() == DialogResult.OK)
                    {
                        CalificacionInputDTO calificacionInput = formCalificar.CalificacionIngresada;

                        string? token = SessionManager.JwtToken;
                        if (string.IsNullOrEmpty(token))
                        {
                            MessageBox.Show("Error de sesión. No se encontró el token.", "Error");
                            return;
                        }

                        await CalificacionApiClient.CalificarConductorAsync(idViaje, calificacionInput, token);

                        MessageBox.Show("Calificación enviada con éxito.", "Calificación Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ActualizarEstadoBotones();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar la calificación:\n{ex.Message}", "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
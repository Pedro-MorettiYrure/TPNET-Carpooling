using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients; 
using DTOs;      
using Domain.Model;

namespace WindowsForms
{
    public partial class SolicitudesLista : Form
    {
        private readonly int _idViaje;
        private readonly UsuarioDTO _conductorLogueado;
        // Opcional: private ViajeDTO _viajeActual; // Si necesitas datos del viaje

    
        public SolicitudesLista(int idViaje, UsuarioDTO conductorLogueado)
        {
            InitializeComponent(); 
            _idViaje = idViaje;
            _conductorLogueado = conductorLogueado;
        }

        
        private async void FormGestionarSolicitudes_Load(object sender, EventArgs e)
        {
            await CargarSolicitudesAsync();
            ActualizarEstadoBotones(); // Estado inicial de botones
        }

       
        private async Task CargarSolicitudesAsync()
        {
           
            string? token = SessionManager.JwtToken; 
            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Error de sesión. No se encontró el token.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAceptarSolicitud.Enabled = false;
                btnRechazarSolicitud.Enabled = false;
                return;
            }

            try
            {
                dgvSolicitudes.DataSource = null;
                lblInfoViaje.Text = "Cargando..."; // Mensaje temporal
                lblInfoViaje.Visible = true;

                var solicitudes = await SolicitudViajeApiClient.GetSolicitudesPorViajeAsync(_idViaje, token);

                var listaSolicitudes = solicitudes?.ToList();

                dgvSolicitudes.DataSource = listaSolicitudes;

                if (listaSolicitudes != null && listaSolicitudes.Any())
                {
                    // Tomamos los datos del viaje de la primera solicitud
                    var primeraSolicitud = listaSolicitudes.First();
                    lblInfoViaje.Text = $"Solicitudes para: {primeraSolicitud.OrigenViajeNombre} a {primeraSolicitud.DestinoViajeNombre} - {primeraSolicitud.FechaHoraViaje:dd/MM/yyyy HH:mm}";
                }
                else
                {
                    lblInfoViaje.Text = "No hay solicitudes para este viaje.";
                }

                if (dgvSolicitudes.DataSource != null)
                {
                    // Ocultar columnas no deseadas
                    dgvSolicitudes.Columns[nameof(SolicitudViajeDTO.IdSolicitud)].Visible = false;
                    dgvSolicitudes.Columns[nameof(SolicitudViajeDTO.IdViaje)].Visible = false;
                    dgvSolicitudes.Columns[nameof(SolicitudViajeDTO.IdPasajero)].Visible = false;

                    // Renombrar encabezados
                    dgvSolicitudes.Columns[nameof(SolicitudViajeDTO.NombrePasajero)].HeaderText = "Nombre";
                    dgvSolicitudes.Columns[nameof(SolicitudViajeDTO.ApellidoPasajero)].HeaderText = "Apellido";
                    dgvSolicitudes.Columns[nameof(SolicitudViajeDTO.SolicitudFecha)].HeaderText = "Fecha Solicitud";
                    dgvSolicitudes.Columns[nameof(SolicitudViajeDTO.Estado)].HeaderText = "Estado";

                    dgvSolicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar solicitudes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAceptarSolicitud.Enabled = false;
                btnRechazarSolicitud.Enabled = false;
            }
        }

        private void dgvSolicitudes_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            bool haySeleccion = dgvSolicitudes.SelectedRows.Count > 0;
            bool esPendiente = false;

            if (haySeleccion)
            {
                var selectedRow = dgvSolicitudes.SelectedRows[0];
                var solicitud = selectedRow.DataBoundItem as SolicitudViajeDTO;
                esPendiente = (solicitud != null && solicitud.Estado == EstadoSolicitud.Pendiente.ToString());
            }

            btnAceptarSolicitud.Enabled = haySeleccion && esPendiente;
            btnRechazarSolicitud.Enabled = haySeleccion && esPendiente;
        }

        
        private async void btnAceptarSolicitud_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudes.SelectedRows.Count == 0) return;
            var selectedRow = dgvSolicitudes.SelectedRows[0];
            var solicitud = selectedRow.DataBoundItem as SolicitudViajeDTO;
            if (solicitud == null || solicitud.Estado != EstadoSolicitud.Pendiente.ToString()) return;

            try
            {
                string? token = SessionManager.JwtToken; 
                if (string.IsNullOrEmpty(token)) {  return; }

                await SolicitudViajeApiClient.AceptarSolicitudAsync(solicitud.IdSolicitud, token); // Pasar token
                MessageBox.Show($"Solicitud de {solicitud.NombrePasajero} aceptada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await CargarSolicitudesAsync(); // Refrescar
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aceptar la solicitud:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private async void btnRechazarSolicitud_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudes.SelectedRows.Count == 0) return;
            var selectedRow = dgvSolicitudes.SelectedRows[0];
            var solicitud = selectedRow.DataBoundItem as SolicitudViajeDTO;
            if (solicitud == null || solicitud.Estado != EstadoSolicitud.Pendiente.ToString()) return;

            DialogResult confirm = MessageBox.Show($"¿Seguro que desea rechazar la solicitud de {solicitud.NombrePasajero}?", "Confirmar Rechazo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            try
            {
                string? token = SessionManager.JwtToken; // Obtener token
                if (string.IsNullOrEmpty(token)) { /* Mostrar error y salir */ return; }

                await SolicitudViajeApiClient.RechazarSolicitudAsync(solicitud.IdSolicitud, token); // Pasar token
                MessageBox.Show($"Solicitud de {solicitud.NombrePasajero} rechazada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await CargarSolicitudesAsync(); // Refrescar
                ActualizarEstadoBotones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al rechazar la solicitud:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Botón Volver ---
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
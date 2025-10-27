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


        public SolicitudesLista(int idViaje, UsuarioDTO conductorLogueado)
        {
            InitializeComponent();
            _idViaje = idViaje;
            _conductorLogueado = conductorLogueado ?? throw new ArgumentNullException(nameof(conductorLogueado)); // Asegura que no sea null
            FormGestionarSolicitudes_Load(this, EventArgs.Empty); 
        }


        private async void FormGestionarSolicitudes_Load(object sender, EventArgs e)
        {
            dgvSolicitudes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSolicitudes.MultiSelect = false;
            dgvSolicitudes.ReadOnly = true;
            dgvSolicitudes.AllowUserToAddRows = false;
            dgvSolicitudes.AllowUserToDeleteRows = false;
            dgvSolicitudes.RowHeadersVisible = false; 

            await CargarSolicitudesAsync();
            ActualizarEstadoBotones(); 
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
                dgvSolicitudes.Columns.Clear(); // Limpiar columnas existentes
                dgvSolicitudes.AutoGenerateColumns = false; // Asegurarse de que no genere columnas automáticamente

                dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Nombre",
                    DataPropertyName = nameof(SolicitudViajeDTO.NombrePasajero),
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });
                dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Apellido",
                    DataPropertyName = nameof(SolicitudViajeDTO.ApellidoPasajero),
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });
                dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Fecha Solicitud",
                    DataPropertyName = nameof(SolicitudViajeDTO.SolicitudFecha),
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, // Formato de fecha/hora
                    Width = 120 
                });
                dgvSolicitudes.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Estado",
                    DataPropertyName = nameof(SolicitudViajeDTO.Estado),
                    Width = 100 
                });

                lblInfoViaje.Text = "Cargando...";
                lblInfoViaje.Visible = true;

                // 3. Obtener los datos
                var solicitudes = await SolicitudViajeApiClient.GetSolicitudesPorViajeAsync(_idViaje, token);
                var listaSolicitudes = solicitudes?.OrderBy(s => s.SolicitudFecha).ToList() ?? new List<SolicitudViajeDTO>(); 

                dgvSolicitudes.DataSource = listaSolicitudes;

                // 5. Configurar texto informativo y otros ajustes de UI
                if (listaSolicitudes.Any())
                {
                    
                }
                else
                {
                    lblInfoViaje.Text = "No hay solicitudes para este viaje.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar solicitudes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAceptarSolicitud.Enabled = false;
                btnRechazarSolicitud.Enabled = false;
                lblInfoViaje.Text = "Error al cargar."; 
            }
            finally
            {
                if (!this.IsDisposed)
                {
                    ActualizarEstadoBotones();
                }
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
                try
                {
                    var selectedRow = dgvSolicitudes.SelectedRows[0];
                    if (selectedRow.DataBoundItem is SolicitudViajeDTO solicitud)
                    {
                        // Comparamos usando el enum para más seguridad
                        esPendiente = solicitud.Estado.ToString() == EstadoSolicitud.Pendiente.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar botones: {ex.Message}");
                    esPendiente = false; 
                }
            }

            btnAceptarSolicitud.Enabled = haySeleccion && esPendiente;
            btnRechazarSolicitud.Enabled = haySeleccion && esPendiente;
        }


        private async void btnAceptarSolicitud_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudes.SelectedRows.Count == 0) return;
            var selectedRow = dgvSolicitudes.SelectedRows[0];
            if (!(selectedRow.DataBoundItem is SolicitudViajeDTO solicitud)) return;
            if (solicitud.Estado != EstadoSolicitud.Pendiente) return;


            btnAceptarSolicitud.Enabled = false; 
            btnRechazarSolicitud.Enabled = false;

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("Error de sesión. Por favor, inicie sesión de nuevo.", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                await SolicitudViajeApiClient.AceptarSolicitudAsync(solicitud.IdSolicitud, token);
                MessageBox.Show($"Solicitud de {solicitud.NombrePasajero} {solicitud.ApellidoPasajero} aceptada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await CargarSolicitudesAsync(); 
                
            }
            catch (InvalidOperationException opEx) 
            {
                MessageBox.Show($"No se pudo aceptar: {opEx.Message}", "Operación Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                await CargarSolicitudesAsync(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aceptar la solicitud:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Volver a habilitar botones sólo si falló por error genérico y sigue habiendo selección
                if (!this.IsDisposed) ActualizarEstadoBotones();
            }
            // No necesitamos finally para re-habilitar botones si CargarSolicitudesAsync ya lo hace
        }


        private async void btnRechazarSolicitud_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudes.SelectedRows.Count == 0) return;
            var selectedRow = dgvSolicitudes.SelectedRows[0];
            if (!(selectedRow.DataBoundItem is SolicitudViajeDTO solicitud)) return;
            if (solicitud.Estado.ToString() != EstadoSolicitud.Pendiente.ToString()) return;


            DialogResult confirm = MessageBox.Show($"¿Seguro que desea rechazar la solicitud de {solicitud.NombrePasajero} {solicitud.ApellidoPasajero}?", "Confirmar Rechazo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            btnAceptarSolicitud.Enabled = false;
            btnRechazarSolicitud.Enabled = false;

            try
            {
                string? token = SessionManager.JwtToken; // Obtener token
                if (string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("Error de sesión. Por favor, inicie sesión de nuevo.", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await SolicitudViajeApiClient.RechazarSolicitudAsync(solicitud.IdSolicitud, token); // Pasar token
                MessageBox.Show($"Solicitud de {solicitud.NombrePasajero} {solicitud.ApellidoPasajero} rechazada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await CargarSolicitudesAsync(); // Refrescar
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al rechazar la solicitud:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!this.IsDisposed) ActualizarEstadoBotones();
            }
        }

        // --- Botón Volver ---
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
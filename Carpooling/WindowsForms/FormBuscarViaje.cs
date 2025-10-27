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
    public partial class FormBuscarViaje : Form
    {
        private readonly UsuarioDTO _usuarioLogueado;

        public FormBuscarViaje(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado ?? throw new ArgumentNullException(nameof(usuarioLogueado));
            this.Load += FormBuscarViaje_Load;
           
        }

        private async void FormBuscarViaje_Load(object sender, EventArgs e)
        {
           
            ConfigureDataGridView();
            await LoadLocalidadesAsync(); // Espera a que carguen las localidades
            btnSolicitarViaje.Enabled = false; // Botón solicitar deshabilitado inicialmente
        }

        private void ConfigureDataGridView()
        {
            dgvBuscarViaje.AutoGenerateColumns = false;
            dgvBuscarViaje.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBuscarViaje.MultiSelect = false;
            dgvBuscarViaje.ReadOnly = true;
            dgvBuscarViaje.AllowUserToAddRows = false;
            dgvBuscarViaje.AllowUserToDeleteRows = false;

            dgvBuscarViaje.Columns.Clear();
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha/Hora", DataPropertyName = nameof(ViajeDTO.FechaHora), DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, Width = 120 });
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Origen", DataPropertyName = nameof(ViajeDTO.NombreOrigen), Width = 100 });
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Destino", DataPropertyName = nameof(ViajeDTO.NombreDestino), Width = 100 });
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Lugares", DataPropertyName = nameof(ViajeDTO.CantLugares), Width = 60 });
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = nameof(ViajeDTO.Precio), DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, Width = 70 });
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Comentario", DataPropertyName = nameof(ViajeDTO.Comentario), AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            // Conectar eventos
            dgvBuscarViaje.SelectionChanged += dgvResultadosBusqueda_SelectionChanged;
            btnSolicitarViaje.Click += btnSolicitarViaje_Click;
            btnCancelar.Click += btnCancelar_Click;
            btnBuscar.Click += btnBuscar_Click;
        }

        private async Task LoadLocalidadesAsync()
        {
            try
            {
                var localidades = (await LocalidadApiClient.GetAllAsync()).ToList();
                var origenDataSource = new List<LocalidadDTO>(localidades);
                var destinoDataSource = new List<LocalidadDTO>(localidades);

                comboBoxOrigen.DataSource = origenDataSource;
                comboBoxOrigen.DisplayMember = "Nombre"; // Asegúrate que sea "Nombre" o "nombreLoc"
                comboBoxOrigen.ValueMember = "CodPostal";
                comboBoxOrigen.SelectedIndex = -1;

                comboBoxDestino.DataSource = destinoDataSource;
                comboBoxDestino.DisplayMember = "Nombre"; // Asegúrate que sea "Nombre" o "nombreLoc"
                comboBoxDestino.ValueMember = "CodPostal";
                comboBoxDestino.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar localidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxOrigen.Enabled = false;
                comboBoxDestino.Enabled = false;
                btnBuscar.Enabled = false;
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (comboBoxOrigen.SelectedValue == null || comboBoxDestino.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar origen y destino.", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string origenCodPostal = comboBoxOrigen.SelectedValue.ToString() ?? "";
            string destinoCodPostal = comboBoxDestino.SelectedValue.ToString() ?? "";

            if (origenCodPostal == destinoCodPostal)
            {
                MessageBox.Show("Origen y destino no pueden ser iguales.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnBuscar.Enabled = false;
            dgvBuscarViaje.DataSource = null;
            btnSolicitarViaje.Enabled = false;

            try
            {
                var viajesEncontrados = await ViajeApiClient.BuscarViajesAsync(origenCodPostal, destinoCodPostal);
                var listaViajes = viajesEncontrados?.ToList() ?? new List<ViajeDTO>();

                dgvBuscarViaje.DataSource = listaViajes;

                if (!listaViajes.Any())
                {
                    MessageBox.Show("No se encontraron viajes disponibles para esta ruta.", "Búsqueda Vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar viajes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!this.IsDisposed) btnBuscar.Enabled = true;
            }
        }

        private void dgvResultadosBusqueda_SelectionChanged(object sender, EventArgs e)
        {
            bool habilitar = false;
            if (dgvBuscarViaje.SelectedRows.Count > 0 && dgvBuscarViaje.SelectedRows[0].DataBoundItem is ViajeDTO viajeSeleccionado)
            {
                // Habilita solicitar solo si el viaje está Pendiente
                habilitar = viajeSeleccionado.Estado == EstadoViaje.Pendiente;
            }
            btnSolicitarViaje.Enabled = habilitar;
        }

        private async void btnSolicitarViaje_Click(object sender, EventArgs e)
        {
            if (dgvBuscarViaje.SelectedRows.Count == 0) return;
            var selectedRow = dgvBuscarViaje.SelectedRows[0];
            if (!(selectedRow.DataBoundItem is ViajeDTO viajeSeleccionado)) return;

            if (viajeSeleccionado.Estado != EstadoViaje.Pendiente)
            {
                MessageBox.Show("Este viaje ya no está disponible.", "Viaje no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSolicitarViaje.Enabled = false;
                return;
            }

            btnSolicitarViaje.Enabled = false; // Deshabilitar mientras procesa

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("Sesión inválida.");

                var solicitudDto = new SolicitudViajeDTO
                {
                    IdViaje = viajeSeleccionado.IdViaje,
                    IdPasajero = _usuarioLogueado.IdUsuario
                };

                var solicitudCreada = await SolicitudViajeApiClient.CrearSolicitudAsync(solicitudDto, token);

                MessageBox.Show($"Solicitud enviada (ID {solicitudCreada.IdSolicitud}) para el viaje. Estado: Pendiente.",
                                "Solicitud Enviada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Opcional: Podrías cerrar este formulario o deshabilitar solicitar para este viaje
            }
            catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (InvalidOperationException opEx) { MessageBox.Show($"No se pudo enviar la solicitud: {opEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar la solicitud: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Volver a habilitar si falló por error genérico y hay selección
                if (!this.IsDisposed && dgvBuscarViaje.SelectedRows.Count > 0) btnSolicitarViaje.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Linq;
using System.Windows.Forms;
using API.Clients; 
using DTOs;      
using System.Threading.Tasks; 
using System.Collections.Generic; 

namespace WindowsForms
{
    public partial class FormBuscarViaje : Form
    {
        private readonly UsuarioDTO _usuarioLogueado;

        public FormBuscarViaje(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado ?? throw new ArgumentNullException(nameof(usuarioLogueado));
        }


        private async void FormBuscarViaje_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();

            await LoadLocalidadesAsync();
            btnSolicitarViaje.Enabled = false;
        }

        private void ConfigureDataGridView()
        {
            dgvBuscarViaje.AutoGenerateColumns = false; 
            dgvBuscarViaje.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleccionar fila completa
            dgvBuscarViaje.MultiSelect = false; 
            dgvBuscarViaje.ReadOnly = true; 
            dgvBuscarViaje.AllowUserToAddRows = false; 
            dgvBuscarViaje.AllowUserToDeleteRows = false; 

            dgvBuscarViaje.Columns.Clear(); 
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha/Hora", DataPropertyName = "FechaHora", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, Width = 120 });
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Origen", DataPropertyName = "NombreOrigen", Width = 100 }); // Usar NombreOrigen
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Destino", DataPropertyName = "NombreDestino", Width = 100 }); // Usar NombreDestino
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Lugares", DataPropertyName = "CantLugares", Width = 60 });
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = "Precio", DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, Width = 70 }); // Formato Moneda
            dgvBuscarViaje.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Comentario", DataPropertyName = "Comentario", Width = 150, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill }); // Rellenar espacio

            
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
                comboBoxOrigen.DisplayMember = "Nombre";
                comboBoxOrigen.ValueMember = "CodPostal";
                comboBoxOrigen.SelectedIndex = -1; 

                comboBoxDestino.DataSource = destinoDataSource;
                comboBoxDestino.DisplayMember = "Nombre";
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
                MessageBox.Show("Debe seleccionar una localidad de origen y una de destino.", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string origenCodPostal = comboBoxOrigen.SelectedValue.ToString() ?? ""; 
            string destinoCodPostal = comboBoxDestino.SelectedValue.ToString() ?? "";

            if (origenCodPostal == destinoCodPostal)
            {
                MessageBox.Show("El origen y el destino no pueden ser iguales.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Deshabilitar controles mientras busca
            btnBuscar.Enabled = false;
            dgvBuscarViaje.DataSource = null; 
            btnSolicitarViaje.Enabled = false; 

            try
            {
                var viajesEncontrados = await ViajeApiClient.BuscarViajesAsync(origenCodPostal, destinoCodPostal);
                var listaViajes = viajesEncontrados.ToList(); 

                if (listaViajes.Any())
                {
                    dgvBuscarViaje.DataSource = listaViajes; 
                }
                else
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
            // Habilita el botón si hay al menos una fila seleccionada Y esa fila corresponde a un viaje Pendiente
            bool habilitar = false;
            if (dgvBuscarViaje.SelectedRows.Count > 0 && dgvBuscarViaje.SelectedRows[0].DataBoundItem is ViajeDTO viajeSeleccionado)
            {
                habilitar = viajeSeleccionado.Estado == Domain.Model.EstadoViaje.Pendiente; 
            }
            btnSolicitarViaje.Enabled = habilitar;
        }

        private async void btnSolicitarViaje_Click(object sender, EventArgs e)
        {
            if (dgvBuscarViaje.SelectedRows.Count == 0) return;

            var selectedRow = dgvBuscarViaje.SelectedRows[0];
            if (!(selectedRow.DataBoundItem is ViajeDTO viajeSeleccionado)) return;

            // Doble chequeo por si cambió el estado mientras estaba en pantalla
            if (viajeSeleccionado.Estado != Domain.Model.EstadoViaje.Pendiente)
            {
                MessageBox.Show("Este viaje ya no está disponible para solicitar.", "Viaje no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSolicitarViaje.Enabled = false;
                return;
            }

            btnSolicitarViaje.Enabled = false;

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("Sesión inválida.");

                var solicitudDto = new SolicitudViajeDTO
                {
                    IdViaje = viajeSeleccionado.IdViaje,
                    IdPasajero = _usuarioLogueado.IdUsuario
                    // La API asignará la fecha y el estado Pendiente
                };

                var solicitudCreada = await SolicitudViajeApiClient.CrearSolicitudAsync(solicitudDto, token);

                MessageBox.Show($"Solicitud enviada para el viaje ID {solicitudCreada.IdViaje}. Su solicitud (ID {solicitudCreada.IdSolicitud}) está Pendiente.",
                                "Solicitud Enviada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (InvalidOperationException opEx) { MessageBox.Show($"No se pudo enviar la solicitud: {opEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } // Ej: Ya solicitó, no hay lugares
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar la solicitud: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!this.IsDisposed && dgvBuscarViaje.SelectedRows.Count > 0) btnSolicitarViaje.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
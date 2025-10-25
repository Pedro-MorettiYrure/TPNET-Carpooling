using System;
using System.Linq;
using System.Windows.Forms;
using API.Clients; 
using DTOs;       

namespace WindowsForms
{
    public partial class FormBuscarViaje : Form
    {
        private readonly UsuarioDTO _usuarioLogueado;

        public FormBuscarViaje(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;
        }

        
        private async void FormBuscarViaje_Load(object sender, EventArgs e)
        {
            await LoadLocalidadesAsync();
            // Inicialmente deshabilitar el botón de solicitar hasta que se seleccione un viaje
            btnSolicitarViaje.Enabled = false;
        }

        private async Task LoadLocalidadesAsync()
        {
            try
            {
                var localidades = await LocalidadApiClient.GetAllAsync();
                var listaLocalidades = localidades.ToList(); 

               
                comboBoxOrigen.DataSource = listaLocalidades.ToList(); // Crear copia para evitar compartir instancia
                comboBoxOrigen.DisplayMember = "Nombre";
                comboBoxOrigen.ValueMember = "CodPostal";
                comboBoxOrigen.SelectedIndex = -1; // Ninguno seleccionado

                
                comboBoxDestino.DataSource = listaLocalidades.ToList(); // Crear copia
                comboBoxDestino.DisplayMember = "Nombre";
                comboBoxDestino.ValueMember = "CodPostal";
                comboBoxDestino.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar localidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Lógica del Botón Buscar ---
        private async void btnBuscar_Click(object sender, EventArgs e)
        {
           
            if (comboBoxOrigen.SelectedValue == null || comboBoxDestino.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una localidad de origen y una de destino.", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string origenCodPostal = comboBoxOrigen.SelectedValue.ToString();
            string destinoCodPostal = comboBoxDestino.SelectedValue.ToString();

            if (origenCodPostal == destinoCodPostal)
            {
                MessageBox.Show("El origen y el destino no pueden ser iguales.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Limpiar resultados 
                dgvBuscarViaje.DataSource = null;
                btnSolicitarViaje.Enabled = false; 

                // Llamar a un NUEVO método del API Client para buscar por origen/destino
                var viajesEncontrados = await ViajeApiClient.BuscarViajesAsync(origenCodPostal, destinoCodPostal);

                if (viajesEncontrados != null && viajesEncontrados.Any())
                {
                    dgvBuscarViaje.DataSource = viajesEncontrados.ToList();
                }
                else
                {
                    MessageBox.Show("No se encontraron viajes disponibles.", "Búsqueda Vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar viajes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvResultadosBusqueda_SelectionChanged(object sender, EventArgs e)
        {
            // habilita el boton si hay al menos una fila seleccionada
            btnSolicitarViaje.Enabled = (dgvBuscarViaje.SelectedRows.Count > 0);
        }

        private async void btnSolicitarViaje_Click(object sender, EventArgs e)
        {
            if (dgvBuscarViaje.SelectedRows.Count == 0) return;

            var selectedRow = dgvBuscarViaje.SelectedRows[0];
            var viajeSeleccionado = selectedRow.DataBoundItem as ViajeDTO;

            if (viajeSeleccionado == null) return;

            try
            {
                
                var solicitudDto = new SolicitudViajeDTO
                {
                    IdViaje = viajeSeleccionado.IdViaje,
                    IdPasajero = _usuarioLogueado.IdUsuario
                };

              
                var solicitudCreada = await SolicitudViajeApiClient.CrearSolicitudAsync(solicitudDto);

                MessageBox.Show($"Solicitud enviada para el viaje ID {solicitudCreada.IdViaje}. Estado: Pendiente.", "Solicitud Enviada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar la solicitud: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

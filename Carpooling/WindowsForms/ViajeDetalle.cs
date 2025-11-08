using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Windows.Forms;
using API.Clients; 
using DTOs;
using Domain.Model;

namespace WindowsForms
{
    public partial class ViajeDetalle : Form
    {
        private readonly UsuarioDTO _usuarioLogueado;
        public FormMode Mode { get; private set; }
        private ViajeDTO? _viajeAEditar; 

        public ViajeDetalle(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado ?? throw new ArgumentNullException(nameof(usuarioLogueado));
            Mode = FormMode.Add;
            dtpFecha.Value = DateTime.Today;
            dtpHora.Value = DateTime.Now.Date.AddHours(DateTime.Now.Hour + 1);
            _ = LoadLocalidadesAsync();
            _ = LoadVehiculosAsync();
        }

        public ViajeDetalle(UsuarioDTO usuarioLogueado, ViajeDTO viajeAEditar)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado ?? throw new ArgumentNullException(nameof(usuarioLogueado));
            _viajeAEditar = viajeAEditar ?? throw new ArgumentNullException(nameof(viajeAEditar));
            Mode = FormMode.Update;
            _ = LoadLocalidadesAsync(); 
            _ = LoadVehiculosAsync();
            CargarDatosSimplesAEditar(); 
        }

        public enum FormMode
        {
            Add,
            Update
        }

        private void CargarDatosSimplesAEditar()
        {
            if (_viajeAEditar != null)
            {
                
                dtpFecha.Value = _viajeAEditar.FechaHora.Date;
                dtpHora.Value = _viajeAEditar.FechaHora;
                tbCantLugares.Text = _viajeAEditar.CantLugares.ToString();
                tbPrecio.Text = _viajeAEditar.Precio.ToString(System.Globalization.CultureInfo.InvariantCulture);
                tbComentario.Text = _viajeAEditar.Comentario;
            }
        }

        private async Task LoadLocalidadesAsync()
        {
            try
            {
                var localidades = (await LocalidadApiClient.GetAllAsync()).ToList(); 

                cbOrigen.DataSource = new List<LocalidadDTO>(localidades);
                cbOrigen.DisplayMember = "Nombre";
                cbOrigen.ValueMember = "CodPostal";


                cbDestino.DataSource = new List<LocalidadDTO>(localidades);
                cbDestino.DisplayMember = "Nombre";
                cbDestino.ValueMember = "CodPostal";


                if (Mode == FormMode.Update && _viajeAEditar != null)
                {
                    cbOrigen.SelectedValue = _viajeAEditar.OrigenCodPostal ?? ""; 
                    cbDestino.SelectedValue = _viajeAEditar.DestinoCodPostal ?? "";
                }
                else
                {
                    cbOrigen.SelectedIndex = -1; 
                    cbDestino.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar localidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbOrigen.Enabled = false;
                cbDestino.Enabled = false;
            }
        }

        private async Task LoadVehiculosAsync()
        {
            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Sesión inválida.");

                var vehiculos = (await VehiculoApiClient.GetByUsuarioAsync(_usuarioLogueado.IdUsuario, token)).ToList();

                cbVehiculos.DataSource = vehiculos;
                cbVehiculos.DisplayMember = "Patente"; 
                cbVehiculos.ValueMember = "IdVehiculo"; 

                if (Mode == FormMode.Update && _viajeAEditar != null)
                {
                    cbVehiculos.SelectedValue = _viajeAEditar.IdVehiculo;
                }
                else
                {
                    cbVehiculos.SelectedIndex = -1;
                }

                // Habilitar/deshabilitar controles según si hay vehículos
                cbVehiculos.Enabled = vehiculos.Any();
                btnConfirmar.Enabled = vehiculos.Any();
                if (!vehiculos.Any())
                {
                    errorProviderViajeDetalle.SetError(cbVehiculos, "Debe registrar un vehículo antes de crear un viaje.");
                }

            }
            catch (UnauthorizedAccessException authEx)
            {
                MessageBox.Show($"Error de autorización al cargar vehículos: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close(); // O redirigir al login
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vehículos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbVehiculos.Enabled = false; 
                btnConfirmar.Enabled = false;
            }
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            errorProviderViajeDetalle.Clear(); 
            if (!ValidateViaje()) // Validar ANTES de intentar enviar
            {
                return; 
            }

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("Error de sesión. Por favor, inicie sesión nuevamente.", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime fecha = dtpFecha.Value.Date;
                TimeSpan hora = dtpHora.Value.TimeOfDay;
                DateTime fechaHora = fecha.Add(hora); 

                if (cbOrigen.SelectedValue == null || cbDestino.SelectedValue == null || cbVehiculos.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar origen, destino y vehículo.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string origenCodPostal = cbOrigen.SelectedValue.ToString() ?? "";
                string destinoCodPostal = cbDestino.SelectedValue.ToString() ?? "";
                int idVehiculo = (int)cbVehiculos.SelectedValue;

                if (!int.TryParse(tbCantLugares.Text, out int cantLugares) || !decimal.TryParse(tbPrecio.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal precio))
                {
                    MessageBox.Show("Cantidad de lugares y precio deben ser números válidos.", "Datos Inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string comentario = tbComentario.Text;

                if (Mode == FormMode.Update && _viajeAEditar != null)
                {
                    try
                    {
                        var solicitudes = await SolicitudViajeApiClient.GetSolicitudesPorViajeAsync(_viajeAEditar.IdViaje, token);
                        var solicitudesAprobadas = solicitudes.Count(s => s.Estado == EstadoSolicitud.Aprobada); 

                        if (cantLugares < solicitudesAprobadas)
                        {
                            MessageBox.Show($"No puede reducir el número de lugares a {cantLugares}, porque ya tiene {solicitudesAprobadas} pasajeros confirmados. Contacte a los pasajeros.", "Error de Validación de Lugares",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            errorProviderViajeDetalle.SetError(tbCantLugares, $"No puede reducir a {cantLugares} lugares, ya tiene {solicitudesAprobadas} pasajeros confirmados. Contacte a los pasajeros.");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al validar lugares disponibles: {ex.Message}", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; 
                    }
                }
                var dto = new ViajeDTO
                {
                    FechaHora = fechaHora,
                    OrigenCodPostal = origenCodPostal,
                    DestinoCodPostal = destinoCodPostal,
                    CantLugares = cantLugares,
                    Precio = precio,
                    Comentario = comentario,
                    IdConductor = _usuarioLogueado.IdUsuario, 
                    IdVehiculo = idVehiculo,
                };

                if (Mode == FormMode.Update && _viajeAEditar != null)
                {
                    dto.IdViaje = _viajeAEditar.IdViaje; 
                    await ViajeApiClient.UpdateAsync(dto, token);
                    MessageBox.Show("Viaje actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else 
                {
                    await ViajeApiClient.AddAsync(dto, token);
                    MessageBox.Show("Viaje publicado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (UnauthorizedAccessException authEx) 
            {
                MessageBox.Show($"Error de autorización: {authEx.Message}. Verifique su sesión.", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException sessionEx) 
            {
                MessageBox.Show($"Error: {sessionEx.Message}. Por favor, inicie sesión.", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException argEx) 
            {
                MessageBox.Show($"Error de validación: {argEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"No se pudo confirmar el viaje.\nDetalle: {ex.Message}", "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateViaje()
        {
            bool isValid = true;
            errorProviderViajeDetalle.Clear(); 

            DateTime fecha = dtpFecha.Value.Date;
            TimeSpan hora = dtpHora.Value.TimeOfDay;
            DateTime fechaHora = fecha.Add(hora);

            if (fechaHora <= DateTime.Now) //  para no permitir la hora actual exacta
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(dtpFecha, "La fecha y hora deben ser futuras.");
                errorProviderViajeDetalle.SetError(dtpHora, "La fecha y hora deben ser futuras.");
            }

            if (cbOrigen.SelectedValue == null || string.IsNullOrWhiteSpace(cbOrigen.SelectedValue.ToString()))
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(cbOrigen, "Debe seleccionar una localidad de origen.");
            }

            if (cbDestino.SelectedValue == null || string.IsNullOrWhiteSpace(cbDestino.SelectedValue.ToString()))
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(cbDestino, "Debe seleccionar una localidad de destino.");
            }

            if (cbOrigen.SelectedValue != null && cbDestino.SelectedValue != null &&
                cbOrigen.SelectedValue.ToString() == cbDestino.SelectedValue.ToString())
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(cbDestino, "El destino no puede ser igual al origen.");
                errorProviderViajeDetalle.SetError(cbOrigen, "El origen no puede ser igual al destino.");
            }

            if (cbVehiculos.SelectedValue == null || !(cbVehiculos.SelectedValue is int) || (int)cbVehiculos.SelectedValue <= 0)
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(cbVehiculos, "Debe seleccionar un vehículo válido.");
            }

            if (!int.TryParse(tbCantLugares.Text, out int cantLugares) || cantLugares <= 0)
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(tbCantLugares, "Ingrese un número de lugares válido (mayor a 0).");
            }
            else if (cbVehiculos.SelectedItem is VehiculoDTO vehiculoSeleccionado && cantLugares > vehiculoSeleccionado.CantLugares)
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(tbCantLugares, $"No puede ofrecer más lugares que la capacidad del vehículo ({vehiculoSeleccionado.CantLugares}).");
            }


            if (!decimal.TryParse(tbPrecio.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal precio) || precio < 0)
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(tbPrecio, "Ingrese un precio válido (número no negativo). Use '.' como separador decimal.");
            }

            

            return isValid;
        }


    } 
}
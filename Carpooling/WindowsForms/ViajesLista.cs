using API.Clients;
using Domain.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("Sesión inválida. Por favor, inicie sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close(); // O redirigir a login
                    return;
                }

                this.dgvViajesLista.DataSource = null;
                var viajes = await ViajeApiClient.GetByConductorAsync(_usuario.IdUsuario, token);
                this.dgvViajesLista.DataSource = viajes.ToList();

                if (dgvViajesLista.Columns["IdConductor"] != null) dgvViajesLista.Columns["IdConductor"].Visible = false;
                if (dgvViajesLista.Columns["IdVehiculo"] != null) dgvViajesLista.Columns["IdVehiculo"].Visible = false;


                bool tieneFilas = this.dgvViajesLista.Rows.Count > 0;
                this.btnEliminar.Enabled = tieneFilas;
                this.btnEditar.Enabled = tieneFilas;
                if (tieneFilas) dgvViajesLista.Rows[0].Selected = true;
            }
            catch (UnauthorizedAccessException authEx)
            {
                MessageBox.Show($"Error de autorización: {authEx.Message}. Verifique su sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SessionManager.CerrarSesion();
                this.Close(); // O redirigir a login
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
            using (ViajeDetalle formCrear = new ViajeDetalle(_usuario))
            {
                if (formCrear.ShowDialog() == DialogResult.OK)
                {
                    this.GetAllAndLoad(); 
                }
            }
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
                "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string? token = SessionManager.JwtToken;
                    if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Sesión inválida.");

                    await ViajeApiClient.DeleteAsync(viajeDTO.IdViaje, token);

                    MessageBox.Show("Viaje cancelado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await GetAllAndLoad();
                }
                catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                catch (InvalidOperationException sessionEx) { MessageBox.Show($"Error: {sessionEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cancelar el viaje.\nDetalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var viajeSeleccionado = selectedRow.DataBoundItem as ViajeDTO;

            if (viajeSeleccionado == null) return;

            try
            {
                using (var formDetalle = new ViajeDetalle(_usuario, viajeSeleccionado))
                {
                    if (formDetalle.ShowDialog() == DialogResult.OK)
                    {
                        await GetAllAndLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al preparar la edición del viaje: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnVerSolicitudes_Click(object sender, EventArgs e)
        {
            if (dgvViajesLista.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un viaje de la lista para ver sus solicitudes.",
                                "Selección Requerida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvViajesLista.SelectedRows[0];
            var viajeSeleccionado = selectedRow.DataBoundItem as ViajeDTO;

            if (viajeSeleccionado == null)
            {
                MessageBox.Show("Error al obtener los datos del viaje seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SolicitudesLista formSolicitudes = new SolicitudesLista(viajeSeleccionado.IdViaje, _usuario))
            {
                formSolicitudes.ShowDialog(); 
            }

            await GetAllAndLoad(); 
        }

        private void dgvViajesLista_SelectionChanged(object sender, EventArgs e)
        {
            btnIniciarViaje.Enabled = false; 
            btnFinalizarViaje.Enabled = false; 
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false; 
            btnVerSolicitudes.Enabled = false;
            btnCalificarPasajeros.Enabled = false;

            if (dgvViajesLista.SelectedRows.Count > 0)
            {
                if (dgvViajesLista.SelectedRows[0].DataBoundItem is ViajeDTO viajeSeleccionado)
                {
                    btnVerSolicitudes.Enabled = true;

                    switch (viajeSeleccionado.Estado)
                    {
                        case EstadoViaje.Pendiente:
                            btnEditar.Enabled = true;
                            btnEliminar.Enabled = true;
                            btnIniciarViaje.Enabled = true;
                            break;

                        case EstadoViaje.EnCurso:
                            // habilitar Finalizar si está En Curso
                            btnFinalizarViaje.Enabled = true;
                            break;

                        case EstadoViaje.Realizado:
                            // Habilitar Calificar Pasajeros si está Realizado
                            btnCalificarPasajeros.Enabled = true;
                            break;

                        case EstadoViaje.Cancelado:
                            // No se habilitan acciones principales si está Cancelado
                            break;
                    }
                }
            }
        }
        private async void btnIniciarViaje_Click(object sender, EventArgs e)
        {
            if (dgvViajesLista.SelectedRows.Count == 0 || !(dgvViajesLista.SelectedRows[0].DataBoundItem is ViajeDTO viajeSeleccionado)) return;

            if (viajeSeleccionado.Estado != EstadoViaje.Pendiente || viajeSeleccionado.FechaHora.Date != DateTime.Today.Date)
            {
                MessageBox.Show("Este viaje no se puede iniciar ahora porque no está pendiente o no es hoy).", "Acción no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetBotonesAccionEnabled(false);

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Sesión inválida.");

                await ViajeApiClient.IniciarViajeAsync(viajeSeleccionado.IdViaje, token); 
                MessageBox.Show("Viaje iniciado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await GetAllAndLoad(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar el viaje: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!this.IsDisposed) SetBotonesAccionEnabled(true); 
            }
        }

        private async void btnFinalizarViaje_Click(object sender, EventArgs e)
        {
            if (dgvViajesLista.SelectedRows.Count == 0 || !(dgvViajesLista.SelectedRows[0].DataBoundItem is ViajeDTO viajeSeleccionado)) return;

            if (viajeSeleccionado.Estado != EstadoViaje.EnCurso)
            {
                MessageBox.Show("Este viaje no se puede finalizar porque aún no ha comenzado.", "Acción no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetBotonesAccionEnabled(false);

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Sesión inválida.");

                var response = await ViajeApiClient.FinalizarViajeAsync(viajeSeleccionado.IdViaje, token); // Llamada API

                if (response != null) 
                {
                    MessageBox.Show(response.mensaje ?? "Viaje finalizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await GetAllAndLoad(); 
                }
                else
                {
                    throw new Exception("La respuesta de la API al finalizar el viaje fue inesperada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al finalizar el viaje: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!this.IsDisposed) SetBotonesAccionEnabled(true);
            }
        }

        private async void btnCalificarPasajeros_Click(object sender, EventArgs e)
        {
            if (dgvViajesLista.SelectedRows.Count == 0 || !(dgvViajesLista.SelectedRows[0].DataBoundItem is ViajeDTO viajeSeleccionado))
            {
                MessageBox.Show("Seleccione un viaje realizado para calificar.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (viajeSeleccionado.Estado != EstadoViaje.Realizado)
            {
                MessageBox.Show("Solo se pueden calificar pasajeros de viajes realizados.", "Acción no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetBotonesAccionEnabled(false); 

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Sesión inválida.");

                var todasLasSolicitudes = await SolicitudViajeApiClient.GetSolicitudesPorViajeAsync(viajeSeleccionado.IdViaje, token);

                var solicitudesAprobadas = todasLasSolicitudes
                    .Where(s => s.Estado == EstadoSolicitud.Aprobada) 
                    .ToList();

                if (!solicitudesAprobadas.Any()) 
                {
                    MessageBox.Show("No hay pasajeros aprobados (o pendientes de calificar) en este viaje.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    return;
                }

                bool algunaCalificacionEnviada = false;
                var calificacionesYaHechas = (await CalificacionApiClient.GetCalificacionesDadasAsync(_usuario.IdUsuario, token)).ToList();

                foreach (var solicitud in solicitudesAprobadas)
                {
                    bool yaCalificado = calificacionesYaHechas.Any(c =>
                        c.IdViaje == viajeSeleccionado.IdViaje &&
                        c.IdCalificado == solicitud.IdPasajero && 
                        c.RolCalificado == RolCalificado.Pasajero.ToString()
                    );

                    if (yaCalificado)
                    {
                        Console.WriteLine($"Pasajero {solicitud.NombrePasajero} ya calificado, omitiendo.");
                        continue;
                    }

                    DialogResult preguntarCalificar = MessageBox.Show($"¿Desea calificar al pasajero {solicitud.NombrePasajero} {solicitud.ApellidoPasajero}?", 
                                                                "Calificar Pasajero", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (preguntarCalificar == DialogResult.Cancel) break;
                    if (preguntarCalificar == DialogResult.No) continue;

                    using (FormCalificar formCalificar = new FormCalificar(
                        _usuario.IdUsuario,
                        solicitud.IdPasajero,
                        viajeSeleccionado.IdViaje,
                        RolCalificado.Pasajero
                    ))
                    {
                        if (formCalificar.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                CalificacionInputDTO calificacionInput = formCalificar.CalificacionIngresada;

                                await CalificacionApiClient.CalificarPasajeroAsync(viajeSeleccionado.IdViaje, solicitud.IdPasajero, calificacionInput, token);

                                algunaCalificacionEnviada = true;
                                calificacionesYaHechas.Add(new CalificacionDTO { IdViaje = viajeSeleccionado.IdViaje, IdCalificado = solicitud.IdPasajero, RolCalificado = RolCalificado.Pasajero.ToString() });
                                MessageBox.Show($"Calificación para {solicitud.NombrePasajero} enviada.", "Éxito");
                            }
                            catch (InvalidOperationException opEx) when (opEx.Message.Contains("Ya has calificado"))
                            {
                                MessageBox.Show($"Ya habías calificado a {solicitud.NombrePasajero} (detectado por API).", "Información");
                                if (!calificacionesYaHechas.Any(c => c.IdViaje == viajeSeleccionado.IdViaje && c.IdCalificado == solicitud.IdPasajero))
                                {
                                    calificacionesYaHechas.Add(new CalificacionDTO { IdViaje = viajeSeleccionado.IdViaje, IdCalificado = solicitud.IdPasajero, RolCalificado = RolCalificado.Pasajero.ToString() });
                                }
                            }
                            catch (Exception exCalif)
                            {
                                MessageBox.Show($"Error al calificar a {solicitud.NombrePasajero}: {exCalif.Message}", "Error");
                            }
                        }
                        else
                        {
                            DialogResult seguir = MessageBox.Show("No se envió calificación. ¿Continuar calificando a otros pasajeros?",
                                                                "Continuar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (seguir == DialogResult.No) break;
                        }
                    }
                }

                //verificar si quedan pendientes
                bool quedanPendientes = false;
                var solicitudesAprobadasActualizadas = (await SolicitudViajeApiClient.GetSolicitudesPorViajeAsync(viajeSeleccionado.IdViaje, token))
                                                        .Where(s => s.Estado == EstadoSolicitud.Aprobada)
                                                        .ToList();
                var calificacionesActualizadas = (await CalificacionApiClient.GetCalificacionesDadasAsync(_usuario.IdUsuario, token)).ToList();

                foreach (var s in solicitudesAprobadasActualizadas)
                {
                    if (!calificacionesActualizadas.Any(c => c.IdViaje == viajeSeleccionado.IdViaje && c.IdCalificado == s.IdPasajero && c.RolCalificado == RolCalificado.Pasajero.ToString()))
                    {
                        quedanPendientes = true;
                        break;
                    }
                }

                if (!quedanPendientes)
                {
                    MessageBox.Show("Todos los pasajeros de este viaje han sido calificados.", "Completado");
                }
                else if (algunaCalificacionEnviada)
                {
                    MessageBox.Show("Proceso de calificación finalizado por ahora.", "Calificación Parcial");
                }
                else
                {
                    MessageBox.Show("No se enviaron nuevas calificaciones.", "Calificación");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el proceso de calificación: {ex.Message}", "Error General");
            }
            finally
            {
                SetBotonesAccionEnabled(true);

                if (!this.IsDisposed && dgvViajesLista.SelectedRows.Count > 0)
                {
                    dgvViajesLista_SelectionChanged(dgvViajesLista, EventArgs.Empty);
                }
                else if (!this.IsDisposed)
                {
                    SetBotonesAccionEnabled(false);
                    btnVerSolicitudes.Enabled = false;
                    btnCalificarPasajeros.Enabled = false; 
                }
            }
        }

        private void SetBotonesAccionEnabled(bool enabled)
        {
            
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate { SetBotonesAccionEnabled(enabled); });
            }
            else
            {
                btnIniciarViaje.Enabled = enabled;
                btnFinalizarViaje.Enabled = enabled;
                btnEditar.Enabled = enabled;
                btnEliminar.Enabled = enabled; // Cancelar
                btnVerSolicitudes.Enabled = enabled;
                btnCalificarPasajeros.Enabled = enabled;
            }
        }

    }
}
    


using System;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class ViajesListaPasajero : Form
    {
        private readonly UsuarioDTO _usuario;

        public ViajesListaPasajero(UsuarioDTO usuario)
        {
            InitializeComponent();
            _usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID Solicitud", DataPropertyName = "IdSolicitud", Width = 80 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha Solicitud", DataPropertyName = "SolicitudFecha", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, Width = 120 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado Solicitud", DataPropertyName = "Estado", Width = 100 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID Viaje", DataPropertyName = "IdViaje", Width = 60 });
            
            DataGridViewButtonColumn btnCancelCol = new DataGridViewButtonColumn();
            btnCancelCol.Name = "CancelarCol";
            btnCancelCol.HeaderText = "Acción";
            btnCancelCol.Text = "Cancelar Solicitud";
            btnCancelCol.UseColumnTextForButtonValue = true;
            btnCancelCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(btnCancelCol);

            dataGridView1.CellFormatting += DataGridView1_CellFormatting; 
            dataGridView1.CellContentClick += DataGridView1_CellContentClick; 

            this.Load += ViajesListaPasajero_Load;

        }

        private async void ViajesListaPasajero_Load(object sender, EventArgs e)
        {
            await GetAllSolicitudesAndLoad();
        }

        private async Task GetAllSolicitudesAndLoad()
        {
            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("Sesión inválida.");

                // Llamar al API para obtener las SOLICITUDES del pasajero
                var solicitudes = await SolicitudViajeApiClient.GetSolicitudesPorPasajeroAsync(_usuario.IdUsuario, token);
                var listaSolicitudes = solicitudes.ToList();

                this.Invoke((MethodInvoker)delegate {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = listaSolicitudes;
                });
            }
            catch (UnauthorizedAccessException authEx)
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SessionManager.CerrarSesion();
                    this.Close();
                });
            }
            catch (Exception ex)
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    MessageBox.Show($"Error al cargar tus solicitudes de viaje: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["CancelarCol"]?.Index && e.RowIndex >= 0)
            {
                var solicitud = dataGridView1.Rows[e.RowIndex].DataBoundItem as SolicitudViajeDTO;
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;

                if (solicitud != null && cell != null)
                {
                    // Habilitar solo si está Pendiente o Aprobada
                    bool habilitado = solicitud.Estado == Domain.Model.EstadoSolicitud.Pendiente.ToString() ||
                                       solicitud.Estado == Domain.Model.EstadoSolicitud.Aprobada.ToString();

                   
                    if (!habilitado)
                    {
                        
                        cell.FlatStyle = FlatStyle.Flat;
                        cell.Style.ForeColor = Color.Gray;
                        cell.Style.BackColor = Color.LightGray; 
                    }
                    else
                    {
                        cell.FlatStyle = FlatStyle.Standard;
                        cell.Style.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
                        cell.Style.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                    }
                }
            }
        }

        private async void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["CancelarCol"]?.Index)
            {
                var solicitud = dataGridView1.Rows[e.RowIndex].DataBoundItem as SolicitudViajeDTO;
                if (solicitud == null) return;

                // Verificar si la solicitud es cancelable
                if (solicitud.Estado != Domain.Model.EstadoSolicitud.Pendiente.ToString() &&
                    solicitud.Estado != Domain.Model.EstadoSolicitud.Aprobada.ToString())
                {
                    MessageBox.Show("Esta solicitud ya no puede ser cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                DialogResult confirm = MessageBox.Show($"¿Está seguro de cancelar su solicitud (ID: {solicitud.IdSolicitud}) para el viaje ID: {solicitud.IdViaje}?",
                                                     "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        string? token = SessionManager.JwtToken;
                        if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("Sesión inválida.");

                        await SolicitudViajeApiClient.CancelarSolicitudPasajeroAsync(solicitud.IdSolicitud, token);

                        MessageBox.Show("Solicitud cancelada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await GetAllSolicitudesAndLoad(); // Recargar la grilla
                    }
                    catch (UnauthorizedAccessException authEx) { MessageBox.Show($"Error de autorización: {authEx.Message}", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    catch (InvalidOperationException opEx) { MessageBox.Show($"Error: {opEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } // Ej: ya estaba cancelada/rechazada
                    catch (Exception ex) { MessageBox.Show($"Error al cancelar la solicitud: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

    }
} 
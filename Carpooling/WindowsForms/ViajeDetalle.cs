using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class ViajeDetalle : Form
    {
        private readonly UsuarioDTO _usuarioLogueado;
        public FormMode Mode;
        private ViajeDTO _viajeAEditar;

        public ViajeDetalle(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;
            this.Mode = FormMode.Add;
            LoadLocalidades();
            LoadVehiculos();
        }
        public ViajeDetalle(UsuarioDTO usuarioLogueado, ViajeDTO viajeAEditar)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;
            this.Mode = FormMode.Update; 
            _viajeAEditar = viajeAEditar;  

            CargarDatosSimplesAEditar();

            LoadLocalidades();
            LoadVehiculos(); 

        }
        private void CargarDatosSimplesAEditar()
        {
            if (_viajeAEditar != null)
            {
                // Solo cargamos campos que no dependen de los ComboBox
                dtpFechaHora.Value = _viajeAEditar.FechaHora;
                tbCantLugares.Text = _viajeAEditar.CantLugares.ToString();
                tbPrecio.Text = _viajeAEditar.Precio.ToString();
                tbComentario.Text = _viajeAEditar.Comentario;
            }
        }
        public enum FormMode
        {
            Add,
            Update
        }

        private async void LoadLocalidades()
        {
            try
            {
                var localidades = await LocalidadApiClient.GetAllAsync();
                cbOrigen.DataSource = localidades.ToList();
                cbOrigen.DisplayMember = "Nombre";
                cbOrigen.SelectedIndex = -1;
                cbOrigen.ValueMember = "CodPostal";

                cbDestino.DataSource = localidades.ToList();
                cbDestino.DisplayMember = "Nombre";
                cbDestino.SelectedIndex = -1;
                cbDestino.ValueMember = "CodPostal";

                // seleccionar el valor si estamos editando
                if (Mode == FormMode.Update && _viajeAEditar != null)
                {
                    cbOrigen.SelectedValue = _viajeAEditar.OrigenCodPostal;
                    cbDestino.SelectedValue = _viajeAEditar.DestinoCodPostal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar localidades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadVehiculos()
        {
            try
            {
                var vehiculos = await VehiculoApiClient.GetByUsuarioAsync(_usuarioLogueado.IdUsuario);

                cbVehiculos.DataSource = vehiculos.ToList();
                cbVehiculos.DisplayMember = "Patente";       // lo que se va a mostrar
                cbVehiculos.ValueMember = "IdVehiculo";      // lo que se guarda como valor
                cbVehiculos.SelectedIndex = -1;

                //Seleccionar el valor si estamos editando
                if (Mode == FormMode.Update && _viajeAEditar != null)
                {
                    cbVehiculos.SelectedValue = _viajeAEditar.IdVehiculo;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vehículos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {

                var fechaHora = dtpFechaHora.Value;
                var origen = cbOrigen.SelectedValue; //index o item?
                var destino = cbDestino.SelectedValue;
                var cantLugares = int.Parse(tbCantLugares.Text);
                var precio = decimal.Parse(tbPrecio.Text);
                var comentario = tbComentario.Text;
                var idVehiculo = (int)cbVehiculos.SelectedValue; // el IdVehiculo del combo


                var dto = new ViajeDTO      //validar!
                {
                    FechaHora = fechaHora,
                    OrigenCodPostal = origen.ToString(),
                    DestinoCodPostal = destino.ToString(), //id o object?
                    CantLugares = cantLugares,
                    Precio = precio,
                    Comentario = comentario,
                    IdConductor = _usuarioLogueado.IdUsuario,
                    IdVehiculo = idVehiculo
                };

                if (Mode == FormMode.Update)
                {
                    dto.IdViaje = _viajeAEditar.IdViaje;
                    await ViajeApiClient.UpdateAsync(dto);
                    MessageBox.Show("Viaje actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await ViajeApiClient.AddAsync(dto);
                    MessageBox.Show("Viaje publicado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo confirmar el viaje.\nDetalle: {ex.Message}", "Error de Publicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void cbVehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

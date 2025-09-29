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

        public ViajeDetalle(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            //ver lo de formmode
            _usuarioLogueado = usuarioLogueado;
            LoadLocalidades();
            LoadVehiculos();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vehículos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidateViaje())
            {

                try
                {

                    DateTime fecha = dtpFecha.Value.Date;
                    TimeSpan hora = dtpHora.Value.TimeOfDay;
                    DateTime fechaHora = fecha + new TimeSpan(hora.Hours, hora.Minutes, 0);
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
                    MessageBox.Show($"IdVehiculo seleccionado: {dto.IdVehiculo}, IdConductor: {dto.IdConductor}");

                    if (Mode == FormMode.Update)
                    {
                        await ViajeApiClient.UpdateAsync(dto);
                        MessageBox.Show("Viaje actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        await ViajeApiClient.AddAsync(dto);
                        MessageBox.Show("Viaje publicado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo confirmar el viaje.\nDetalle: {ex.Message}", "Error de Publicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private bool ValidateViaje()
        {
            bool isValid = true;
            errorProviderViajeDetalle.Clear();

            DateTime fecha = dtpFecha.Value.Date;
            TimeSpan hora = dtpHora.Value.TimeOfDay;
            DateTime fechaHora = fecha + new TimeSpan(hora.Hours, hora.Minutes, 0);

            if (fechaHora < DateTime.Now)
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(dtpFecha, "La fecha y hora no pueden ser anteriores a ahora");
                errorProviderViajeDetalle.SetError(dtpHora, "La fecha y hora no pueden ser anteriores a ahora");
            }

            var origenVal = cbOrigen.SelectedValue;
            var destinoVal = cbDestino.SelectedValue;

            if (origenVal == null || string.IsNullOrWhiteSpace(origenVal.ToString()))
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(cbOrigen, "Debe seleccionar una localidad de origen");
            }

            if (destinoVal == null || string.IsNullOrWhiteSpace(destinoVal.ToString()))
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(cbDestino, "Debe seleccionar una localidad de destino");
            }

            if (origenVal != null && destinoVal != null && origenVal.ToString() == destinoVal.ToString())
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(cbDestino, "El destino no puede ser la misma localidad que el origen");
                errorProviderViajeDetalle.SetError(cbOrigen, "El origen no puede ser la misma localidad que el destino");
            }

            if (!int.TryParse(tbCantLugares.Text, out int cantLugares) || cantLugares <= 0)
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(tbCantLugares, "La cantidad de lugares debe ser un número entero mayor que 0");
            }

            if (!decimal.TryParse(tbPrecio.Text, out decimal precio) || precio < 0m)
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(tbPrecio, "El precio debe ser un número no negativo");
            }

            var comentario = tbComentario.Text;
            if (!string.IsNullOrEmpty(comentario) && comentario.Length > 500)
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(tbComentario, "El comentario no puede superar los 500 caracteres");
            }

            if (cbVehiculos.SelectedValue == null || !int.TryParse(cbVehiculos.SelectedValue.ToString(), out int idVehiculo))
            {
                isValid = false;
                errorProviderViajeDetalle.SetError(cbVehiculos, "Debe seleccionar un vehículo válido");
            }

            return isValid;
        }

        private void cbVehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

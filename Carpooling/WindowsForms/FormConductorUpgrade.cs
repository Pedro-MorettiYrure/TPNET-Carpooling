using API.Clients;
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
using static DTOs.UsuarioDTO;

namespace WindowsForms
{
    public partial class FormConductorUpgrade : Form
    {

        private readonly UsuarioDTO _usuarioLogueado; // <-- usuario logueado

        public FormConductorUpgrade(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            // 1. Recolectar los datos del formulario
            var nroLicencia = txtNroLicencia.Text;
            var fechaVencimiento = dtpFechaVencimiento.Value;

            // 2. Crear una instancia del DTO que la API espera
            var dto = new ConductorUpgradeDTO
            {
                nroLicenciaConductor = nroLicencia,
                fechaVencimientoLicencia = fechaVencimiento
            };

            try
            {
                // 3. Llamar al cliente de la API para enviar la solicitud
                bool exito = await UsuarioApiClient.ConvertirAConductorAsync(_usuarioLogueado.IdUsuario, dto);

                // 4. Manejar la respuesta
                if (exito)
                {
                    MessageBox.Show("¡Felicidades! Ahora eres un conductor. Se ha guardado tu información.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Opcional: actualizar el DTO local para que la aplicación refleje el cambio de rol
                    _usuarioLogueado.TipoUsuario = "Pasajero-Conductor";
                    this.DialogResult = DialogResult.OK; // Para cerrar el formulario
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo convertir a conductor. Revisa los datos e intenta de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

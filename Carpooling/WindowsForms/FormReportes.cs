using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients; 

namespace WindowsForms
{
    public partial class FormReportes : Form
    {
        public FormReportes()
        {
            InitializeComponent();
        }

        private void FormReportes_Load(object sender, EventArgs e)
        {
            dtpFechaFin.Value = DateTime.Today;
            dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
        }

        private async void btnGenerarTopConductores_Click(object sender, EventArgs e)
        {
            await GenerarYGuardarReporte(async (token) =>
            {
                byte[] pdfBytes = await ReportApiClient.GetTopConductoresPdfAsync(token);
                return ("reporte_top_conductores.pdf", pdfBytes); 
            }, btnGenerarTopConductores); 
        }

        private async void btnGenerarActividad_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = dtpFechaInicio.Value.Date; 
            DateTime fechaFin = dtpFechaFin.Value.Date;   

            if (fechaInicio > fechaFin)
            {
                MessageBox.Show("La fecha de inicio no puede ser posterior a la fecha de fin.", "Fechas Inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await GenerarYGuardarReporte(async (token) =>
            {
                byte[] pdfBytes = await ReportApiClient.GetActividadViajesPdfAsync(fechaInicio, fechaFin, token);
                string nombreArchivo = $"reporte_actividad_{fechaInicio:yyyyMMdd}_{fechaFin:yyyyMMdd}.pdf";
                return (nombreArchivo, pdfBytes);
            }, btnGenerarActividad); 
        }

        private async Task GenerarYGuardarReporte(Func<string, Task<(string suggestedName, byte[] pdfBytes)>> apiCall, Button botonPresionado)
        {
            botonPresionado.Enabled = false; 
            
            statusLabel.Text = "Generando reporte...";

            try
            {
                string? token = SessionManager.JwtToken;
                if (string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("Error de sesión. Por favor, inicie sesión nuevamente.", "Error Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var (nombreSugerido, pdfBytes) = await apiCall(token);

                if (pdfBytes != null && pdfBytes.Length > 0)
                {
                    await GuardarPdf(pdfBytes, nombreSugerido);
                }
                else
                {
                    MessageBox.Show("La API no devolvió datos para generar el reporte.", "Respuesta Vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el reporte:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error al generar reporte.";
            }
            finally
            {
                if (!this.IsDisposed) 
                {
                    botonPresionado.Enabled = true; 
                    statusLabel.Text = "Listo.";
                }
            }
        }

        private async Task GuardarPdf(byte[] pdfBytes, string suggestedFileName)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf"; 
                saveFileDialog.FileName = suggestedFileName;     
                saveFileDialog.Title = "Guardar Reporte PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await File.WriteAllBytesAsync(saveFileDialog.FileName, pdfBytes);
                        MessageBox.Show($"Reporte guardado exitosamente en:\n{saveFileDialog.FileName}", "Guardado Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar el archivo PDF:\n{ex.Message}", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
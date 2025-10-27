using System;
using System.Windows.Forms;
using API.Clients;     
using Domain.Model; 

namespace WindowsForms
{
    public partial class FormCalificar : Form
    {
        public CalificacionInputDTO CalificacionIngresada { get; private set; }

        public FormCalificar(int idCalificador, int idCalificado, int idViaje, RolCalificado rolDelCalificado)
        {
            InitializeComponent();

            // Ajusta la UI según a quién se califica
            if (rolDelCalificado == RolCalificado.Conductor)
            {
                this.Text = "Calificar Conductor";
                lblInstruccion.Text = "Calificá tu experiencia con el conductor:";
            }
            else
            {
                this.Text = "Calificar Pasajero";
                lblInstruccion.Text = "Calificá tu experiencia con el pasajero:";
            }

            // inicializamos el dto por defecto
            CalificacionIngresada = new CalificacionInputDTO { Puntaje = 5 }; // valor inicial por defecto
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            int valoracion = (int)numValoracion.Value;
            string? comentario = txtComentarioCalificacion.Text;

            //validamos long del comentario
            if (comentario != null && comentario.Length > 500)
            {
                MessageBox.Show("El comentario no puede superar los 500 caracteres.", "Comentario Largo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            CalificacionIngresada = new CalificacionInputDTO
            {
                Puntaje = valoracion,
                Comentario = string.IsNullOrWhiteSpace(comentario) ? null : comentario.Trim() // Enviar null si está vacío
            };

            this.DialogResult = DialogResult.OK; 
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close();
        }
    }
}
namespace WindowsForms
{
    partial class ViajesListaPasajero
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvMisViajes = new DataGridView();
            btnVerDetalles = new Button();
            btnCancelarSolicitud = new Button();
            btnVolver = new Button();
            btnCalificarConductor = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMisViajes).BeginInit();
            SuspendLayout();
            // 
            // dgvMisViajes
            // 
            dgvMisViajes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMisViajes.Location = new Point(5, 12);
            dgvMisViajes.Name = "dgvMisViajes";
            dgvMisViajes.Size = new Size(568, 225);
            dgvMisViajes.TabIndex = 0;
            // 
            // btnVerDetalles
            // 
            btnVerDetalles.Location = new Point(379, 255);
            btnVerDetalles.Name = "btnVerDetalles";
            btnVerDetalles.Size = new Size(90, 30);
            btnVerDetalles.TabIndex = 1;
            btnVerDetalles.Text = "Ver detalles";
            btnVerDetalles.UseVisualStyleBackColor = true;
            btnVerDetalles.Click += btnVerDetalles_Click;
            // 
            // btnCancelarSolicitud
            // 
            btnCancelarSolicitud.Location = new Point(224, 255);
            btnCancelarSolicitud.Name = "btnCancelarSolicitud";
            btnCancelarSolicitud.Size = new Size(149, 30);
            btnCancelarSolicitud.TabIndex = 2;
            btnCancelarSolicitud.Text = "Cancelar lugar en el viaje";
            btnCancelarSolicitud.UseVisualStyleBackColor = true;
            btnCancelarSolicitud.Click += btnCancelarSolicitud_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(12, 255);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(75, 23);
            btnVolver.TabIndex = 3;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnCalificarConductor
            // 
            btnCalificarConductor.Location = new Point(475, 246);
            btnCalificarConductor.Name = "btnCalificarConductor";
            btnCalificarConductor.Size = new Size(98, 43);
            btnCalificarConductor.TabIndex = 4;
            btnCalificarConductor.Text = "Calificar al conductor";
            btnCalificarConductor.UseVisualStyleBackColor = true;
            btnCalificarConductor.Click += btnCalificarConductor_Click;
            // 
            // ViajesListaPasajero
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 298);
            Controls.Add(btnCalificarConductor);
            Controls.Add(btnVolver);
            Controls.Add(btnCancelarSolicitud);
            Controls.Add(btnVerDetalles);
            Controls.Add(dgvMisViajes);
            Name = "ViajesListaPasajero";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvMisViajes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvMisViajes;
        private Button btnVerDetalles;
        private Button btnCancelarSolicitud;
        private Button btnVolver;
        private Button btnCalificarConductor;
    }
}
namespace WindowsForms
{
    partial class SolicitudesLista
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
            btnAceptarSolicitud = new Button();
            dgvSolicitudes = new DataGridView();
            btnRechazarSolicitud = new Button();
            btnVolver = new Button();
            lblInfoViaje = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvSolicitudes).BeginInit();
            SuspendLayout();
            // 
            // btnAceptarSolicitud
            // 
            btnAceptarSolicitud.Location = new Point(550, 420);
            btnAceptarSolicitud.Name = "btnAceptarSolicitud";
            btnAceptarSolicitud.Size = new Size(89, 35);
            btnAceptarSolicitud.TabIndex = 0;
            btnAceptarSolicitud.Text = "Aceptar";
            btnAceptarSolicitud.UseVisualStyleBackColor = true;
            btnAceptarSolicitud.Click += btnAceptarSolicitud_Click;
            // 
            // dgvSolicitudes
            // 
            dgvSolicitudes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSolicitudes.Location = new Point(14, 27);
            dgvSolicitudes.Name = "dgvSolicitudes";
            dgvSolicitudes.Size = new Size(658, 375);
            dgvSolicitudes.TabIndex = 1;
            // 
            // btnRechazarSolicitud
            // 
            btnRechazarSolicitud.Location = new Point(469, 426);
            btnRechazarSolicitud.Name = "btnRechazarSolicitud";
            btnRechazarSolicitud.Size = new Size(75, 23);
            btnRechazarSolicitud.TabIndex = 2;
            btnRechazarSolicitud.Text = "Rechazar";
            btnRechazarSolicitud.UseVisualStyleBackColor = true;
            btnRechazarSolicitud.Click += btnRechazarSolicitud_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(14, 426);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(75, 23);
            btnVolver.TabIndex = 3;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // lblInfoViaje
            // 
            lblInfoViaje.AutoSize = true;
            lblInfoViaje.Location = new Point(31, 27);
            lblInfoViaje.Name = "lblInfoViaje";
            lblInfoViaje.Size = new Size(0, 15);
            lblInfoViaje.TabIndex = 4;
            // 
            // SolicitudesLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(lblInfoViaje);
            Controls.Add(btnVolver);
            Controls.Add(btnRechazarSolicitud);
            Controls.Add(dgvSolicitudes);
            Controls.Add(btnAceptarSolicitud);
            Name = "SolicitudesLista";
            Text = "SolicitudesLista";
            ((System.ComponentModel.ISupportInitialize)dgvSolicitudes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAceptarSolicitud;
        private DataGridView dgvSolicitudes;
        private Button btnRechazarSolicitud;
        private Button btnVolver;
        private Label lblInfoViaje;
    }
}
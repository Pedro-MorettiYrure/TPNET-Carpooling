namespace WindowsForms
{
    partial class SolicitudesListaPasajero
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
            dgvMisSolicitudes = new DataGridView();
            btnCancelarSolicitud = new Button();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMisSolicitudes).BeginInit();
            SuspendLayout();
            // 
            // dgvMisSolicitudes
            // 
            dgvMisSolicitudes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMisSolicitudes.Location = new Point(12, 12);
            dgvMisSolicitudes.Name = "dgvMisSolicitudes";
            dgvMisSolicitudes.Size = new Size(656, 342);
            dgvMisSolicitudes.TabIndex = 0;
            dgvMisSolicitudes.SelectionChanged += dgvMisSolicitudes_SelectionChanged;
            // 
            // btnCancelarSolicitud
            // 
            btnCancelarSolicitud.Enabled = false;
            btnCancelarSolicitud.Location = new Point(537, 362);
            btnCancelarSolicitud.Name = "btnCancelarSolicitud";
            btnCancelarSolicitud.Size = new Size(131, 45);
            btnCancelarSolicitud.TabIndex = 1;
            btnCancelarSolicitud.Text = "Cancelar solicitud";
            btnCancelarSolicitud.UseVisualStyleBackColor = true;
            btnCancelarSolicitud.Click += btnCancelarSolicitud_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(46, 373);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(75, 23);
            btnVolver.TabIndex = 2;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // SolicitudesListaPasajero
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(btnVolver);
            Controls.Add(btnCancelarSolicitud);
            Controls.Add(dgvMisSolicitudes);
            Name = "SolicitudesListaPasajero";
            Text = "SolicitudesListaPasajero";
            ((System.ComponentModel.ISupportInitialize)dgvMisSolicitudes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvMisSolicitudes;
        private Button btnCancelarSolicitud;
        private Button btnVolver;
    }
}
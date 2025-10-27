namespace WindowsForms
{
    partial class FormReportes
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
            lblTopConductores = new Label();
            btnGenerarTopConductores = new Button();
            lblReporteActividad = new Label();
            label1 = new Label();
            label2 = new Label();
            dtpFechaInicio = new DateTimePicker();
            dtpFechaFin = new DateTimePicker();
            btnGenerarActividad = new Button();
            statusLabel = new StatusStrip();
            SuspendLayout();
            // 
            // lblTopConductores
            // 
            lblTopConductores.AutoSize = true;
            lblTopConductores.Location = new Point(77, 43);
            lblTopConductores.Name = "lblTopConductores";
            lblTopConductores.Size = new Size(210, 15);
            lblTopConductores.TabIndex = 0;
            lblTopConductores.Text = "Reporte conductores mejor calificados";
            // 
            // btnGenerarTopConductores
            // 
            btnGenerarTopConductores.Location = new Point(82, 71);
            btnGenerarTopConductores.Name = "btnGenerarTopConductores";
            btnGenerarTopConductores.Size = new Size(118, 38);
            btnGenerarTopConductores.TabIndex = 1;
            btnGenerarTopConductores.Text = "Generar";
            btnGenerarTopConductores.UseVisualStyleBackColor = true;
            btnGenerarTopConductores.Click += btnGenerarTopConductores_Click;
            // 
            // lblReporteActividad
            // 
            lblReporteActividad.AutoSize = true;
            lblReporteActividad.Location = new Point(83, 145);
            lblReporteActividad.Name = "lblReporteActividad";
            lblReporteActividad.Size = new Size(266, 15);
            lblReporteActividad.TabIndex = 2;
            lblReporteActividad.Text = "Reporte de actividad de viajes en rango de fechas";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(118, 183);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 3;
            label1.Text = "Fecha Inicio:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(389, 183);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 4;
            label2.Text = "Fecha Fin:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(197, 177);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(148, 23);
            dtpFechaInicio.TabIndex = 5;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(455, 177);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(200, 23);
            dtpFechaFin.TabIndex = 6;
            // 
            // btnGenerarActividad
            // 
            btnGenerarActividad.Location = new Point(118, 220);
            btnGenerarActividad.Name = "btnGenerarActividad";
            btnGenerarActividad.Size = new Size(108, 42);
            btnGenerarActividad.TabIndex = 7;
            btnGenerarActividad.Text = "Generar";
            btnGenerarActividad.UseVisualStyleBackColor = true;
            btnGenerarActividad.Click += btnGenerarActividad_Click;
            // 
            // statusLabel
            // 
            statusLabel.Location = new Point(0, 428);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(800, 22);
            statusLabel.TabIndex = 8;
            statusLabel.Text = "statusStrip1";
            // 
            // FormReportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(statusLabel);
            Controls.Add(btnGenerarActividad);
            Controls.Add(dtpFechaFin);
            Controls.Add(dtpFechaInicio);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblReporteActividad);
            Controls.Add(btnGenerarTopConductores);
            Controls.Add(lblTopConductores);
            Name = "FormReportes";
            Text = "FormReportes";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTopConductores;
        private Button btnGenerarTopConductores;
        private Label lblReporteActividad;
        private Label label1;
        private Label label2;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        private Button btnGenerarActividad;
        private StatusStrip statusLabel;
    }
}
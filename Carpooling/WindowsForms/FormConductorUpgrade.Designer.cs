namespace WindowsForms
{
    partial class FormConductorUpgrade
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
            labelLicencia = new Label();
            txtNroLicencia = new TextBox();
            dtpFechaVencimiento = new DateTimePicker();
            labelVencimiento = new Label();
            btnConfirmar = new Button();
            labelCompleteDatos = new Label();
            SuspendLayout();
            // 
            // labelLicencia
            // 
            labelLicencia.AutoSize = true;
            labelLicencia.Location = new Point(192, 132);
            labelLicencia.Name = "labelLicencia";
            labelLicencia.Size = new Size(113, 15);
            labelLicencia.TabIndex = 0;
            labelLicencia.Text = "Numero de licencia:";
            // 
            // txtNroLicencia
            // 
            txtNroLicencia.Location = new Point(355, 132);
            txtNroLicencia.Name = "txtNroLicencia";
            txtNroLicencia.Size = new Size(200, 23);
            txtNroLicencia.TabIndex = 1;
            // 
            // dtpFechaVencimiento
            // 
            dtpFechaVencimiento.Location = new Point(355, 188);
            dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            dtpFechaVencimiento.Size = new Size(200, 23);
            dtpFechaVencimiento.TabIndex = 2;
            // 
            // labelVencimiento
            // 
            labelVencimiento.AutoSize = true;
            labelVencimiento.Location = new Point(158, 194);
            labelVencimiento.Name = "labelVencimiento";
            labelVencimiento.Size = new Size(169, 15);
            labelVencimiento.TabIndex = 3;
            labelVencimiento.Text = "Fecha de vencimiento licencia:";
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(355, 260);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 4;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // labelCompleteDatos
            // 
            labelCompleteDatos.AutoSize = true;
            labelCompleteDatos.Location = new Point(264, 59);
            labelCompleteDatos.Name = "labelCompleteDatos";
            labelCompleteDatos.Size = new Size(214, 15);
            labelCompleteDatos.TabIndex = 5;
            labelCompleteDatos.Text = "Complete los datos para ser conductor:";
            // 
            // FormConductorUpgrade
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelCompleteDatos);
            Controls.Add(btnConfirmar);
            Controls.Add(labelVencimiento);
            Controls.Add(dtpFechaVencimiento);
            Controls.Add(txtNroLicencia);
            Controls.Add(labelLicencia);
            Name = "FormConductorUpgrade";
            Text = "FormConductorUpgrade";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelLicencia;
        private TextBox txtNroLicencia;
        private DateTimePicker dtpFechaVencimiento;
        private Label labelVencimiento;
        private Button btnConfirmar;
        private Label labelCompleteDatos;
    }
}
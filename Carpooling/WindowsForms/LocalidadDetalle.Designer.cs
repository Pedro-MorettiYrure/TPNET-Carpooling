namespace WindowsForms
{
    partial class LocalidadDetalle
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            labelCodPostal = new Label();
            labelNombre = new Label();
            txtCodPostal = new TextBox();
            txtNombre = new TextBox();
            errorProvider = new ErrorProvider(components);
            btnConfirmar = new Button();
            btnSalir = new Button();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(258, 40);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "Localidad";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelCodPostal
            // 
            labelCodPostal.AutoSize = true;
            labelCodPostal.Location = new Point(155, 87);
            labelCodPostal.Name = "labelCodPostal";
            labelCodPostal.Size = new Size(84, 15);
            labelCodPostal.TabIndex = 1;
            labelCodPostal.Text = "Codigo Postal:";
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.Location = new Point(156, 140);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(54, 15);
            labelNombre.TabIndex = 2;
            labelNombre.Text = "Nombre:";
            // 
            // txtCodPostal
            // 
            txtCodPostal.Location = new Point(250, 84);
            txtCodPostal.Name = "txtCodPostal";
            txtCodPostal.Size = new Size(100, 23);
            txtCodPostal.TabIndex = 3;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(250, 137);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(100, 23);
            txtNombre.TabIndex = 4;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(311, 198);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 5;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(186, 198);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 6;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // LocalidadDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 270);
            Controls.Add(btnSalir);
            Controls.Add(btnConfirmar);
            Controls.Add(txtNombre);
            Controls.Add(txtCodPostal);
            Controls.Add(labelNombre);
            Controls.Add(labelCodPostal);
            Controls.Add(label1);
            Name = "LocalidadDetalle";
            Text = "LocalidadDetalle";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label labelCodPostal;
        private Label labelNombre;
        private TextBox txtCodPostal;
        private TextBox txtNombre;
        private ErrorProvider errorProvider;
        private Button btnSalir;
        private Button btnConfirmar;
    }
}
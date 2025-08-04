namespace WindowsForms
{
    partial class VehiculoDetalle
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
            labelPatente = new Label();
            labelModelo = new Label();
            labelColor = new Label();
            txtPatente = new TextBox();
            txtModelo = new TextBox();
            txtColor = new TextBox();
            labelCantLugares = new Label();
            txtCantLugares = new TextBox();
            btnSalir = new Button();
            btnConfirmar = new Button();
            txtMarca = new TextBox();
            labelMarca = new Label();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(271, 37);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 0;
            label1.Text = "Vehiculo:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelPatente
            // 
            labelPatente.AutoSize = true;
            labelPatente.Location = new Point(176, 80);
            labelPatente.Name = "labelPatente";
            labelPatente.Size = new Size(50, 15);
            labelPatente.TabIndex = 1;
            labelPatente.Text = "Patente:";
            // 
            // labelModelo
            // 
            labelModelo.AutoSize = true;
            labelModelo.Location = new Point(176, 116);
            labelModelo.Name = "labelModelo";
            labelModelo.Size = new Size(51, 15);
            labelModelo.TabIndex = 2;
            labelModelo.Text = "Modelo:";
            // 
            // labelColor
            // 
            labelColor.AutoSize = true;
            labelColor.Location = new Point(176, 145);
            labelColor.Name = "labelColor";
            labelColor.Size = new Size(39, 15);
            labelColor.TabIndex = 3;
            labelColor.Text = "Color:";
            // 
            // txtPatente
            // 
            txtPatente.Location = new Point(297, 77);
            txtPatente.Name = "txtPatente";
            txtPatente.Size = new Size(100, 23);
            txtPatente.TabIndex = 4;
            // 
            // txtModelo
            // 
            txtModelo.Location = new Point(297, 113);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(100, 23);
            txtModelo.TabIndex = 5;
            // 
            // txtColor
            // 
            txtColor.Location = new Point(297, 145);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(100, 23);
            txtColor.TabIndex = 6;
            // 
            // labelCantLugares
            // 
            labelCantLugares.AutoSize = true;
            labelCantLugares.Location = new Point(176, 211);
            labelCantLugares.Name = "labelCantLugares";
            labelCantLugares.Size = new Size(115, 15);
            labelCantLugares.TabIndex = 7;
            labelCantLugares.Text = "Cantidad de lugares:";
            // 
            // txtCantLugares
            // 
            txtCantLugares.Location = new Point(297, 203);
            txtCantLugares.Name = "txtCantLugares";
            txtCantLugares.Size = new Size(51, 23);
            txtCantLugares.TabIndex = 8;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(212, 250);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 9;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(322, 250);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 10;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(297, 174);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(100, 23);
            txtMarca.TabIndex = 12;
            // 
            // labelMarca
            // 
            labelMarca.AutoSize = true;
            labelMarca.Location = new Point(176, 177);
            labelMarca.Name = "labelMarca";
            labelMarca.Size = new Size(43, 15);
            labelMarca.TabIndex = 11;
            labelMarca.Text = "Marca:";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // VehiculoDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(607, 308);
            Controls.Add(txtMarca);
            Controls.Add(labelMarca);
            Controls.Add(btnConfirmar);
            Controls.Add(btnSalir);
            Controls.Add(txtCantLugares);
            Controls.Add(labelCantLugares);
            Controls.Add(txtColor);
            Controls.Add(txtModelo);
            Controls.Add(txtPatente);
            Controls.Add(labelColor);
            Controls.Add(labelModelo);
            Controls.Add(labelPatente);
            Controls.Add(label1);
            Name = "VehiculoDetalle";
            Text = "VehiculoDetalle";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label labelPatente;
        private Label labelModelo;
        private Label labelColor;
        private TextBox txtPatente;
        private TextBox txtModelo;
        private TextBox txtColor;
        private Label labelCantLugares;
        private TextBox txtCantLugares;
        private Button btnSalir;
        private Button btnConfirmar;
        private TextBox txtMarca;
        private Label labelMarca;
        private ErrorProvider errorProvider1;
    }
}
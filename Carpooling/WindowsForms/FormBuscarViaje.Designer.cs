namespace WindowsForms
{
    partial class FormBuscarViaje
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
            btnSolicitarViaje = new Button();
            btnCancelar = new Button();
            label1 = new Label();
            comboBoxDestino = new ComboBox();
            comboBoxOrigen = new ComboBox();
            dgvBuscarViaje = new DataGridView();
            btnBuscar = new Button();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvBuscarViaje).BeginInit();
            SuspendLayout();
            // 
            // btnSolicitarViaje
            // 
            btnSolicitarViaje.Location = new Point(364, 221);
            btnSolicitarViaje.Name = "btnSolicitarViaje";
            btnSolicitarViaje.Size = new Size(111, 39);
            btnSolicitarViaje.TabIndex = 0;
            btnSolicitarViaje.Text = "Solicitar viaje";
            btnSolicitarViaje.UseVisualStyleBackColor = true;
            btnSolicitarViaje.Click += btnSolicitarViaje_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(283, 237);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 9);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 2;
            // 
            // comboBoxDestino
            // 
            comboBoxDestino.FormattingEnabled = true;
            comboBoxDestino.Location = new Point(176, 23);
            comboBoxDestino.Name = "comboBoxDestino";
            comboBoxDestino.Size = new Size(140, 23);
            comboBoxDestino.TabIndex = 5;
            // 
            // comboBoxOrigen
            // 
            comboBoxOrigen.FormattingEnabled = true;
            comboBoxOrigen.Location = new Point(21, 23);
            comboBoxOrigen.Name = "comboBoxOrigen";
            comboBoxOrigen.Size = new Size(139, 23);
            comboBoxOrigen.TabIndex = 6;
            // 
            // dgvBuscarViaje
            // 
            dgvBuscarViaje.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBuscarViaje.Location = new Point(12, 52);
            dgvBuscarViaje.Name = "dgvBuscarViaje";
            dgvBuscarViaje.Size = new Size(463, 150);
            dgvBuscarViaje.TabIndex = 7;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(341, 23);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 8;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 5);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 9;
            label2.Text = "Origen";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(176, 5);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 10;
            label3.Text = "Destino";
            // 
            // FormBuscarViaje
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(503, 281);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnBuscar);
            Controls.Add(dgvBuscarViaje);
            Controls.Add(comboBoxOrigen);
            Controls.Add(comboBoxDestino);
            Controls.Add(label1);
            Controls.Add(btnCancelar);
            Controls.Add(btnSolicitarViaje);
            Name = "FormBuscarViaje";
            Text = "FormBuscarViaje";
            Click += FormBuscarViaje_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBuscarViaje).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSolicitarViaje;
        private Button btnCancelar;
        private Label label1;
        private ComboBox comboBoxDestino;
        private ComboBox comboBoxOrigen;
        private DataGridView dgvBuscarViaje;
        private Button btnBuscar;
        private Label label2;
        private Label label3;
    }
}
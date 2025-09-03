namespace WindowsForms
{
    partial class VehiculosLista
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
            tscVehiculos = new ToolStripContainer();
            tlVehiculos = new TableLayoutPanel();
            dgvVehiculos = new DataGridView();
            btnSalir = new Button();
            btnCrear = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            tscVehiculos.ContentPanel.SuspendLayout();
            tscVehiculos.SuspendLayout();
            tlVehiculos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehiculos).BeginInit();
            SuspendLayout();
            // 
            // tscVehiculos
            // 
            // 
            // tscVehiculos.ContentPanel
            // 
            tscVehiculos.ContentPanel.Controls.Add(tlVehiculos);
            tscVehiculos.ContentPanel.Size = new Size(800, 425);
            tscVehiculos.Dock = DockStyle.Fill;
            tscVehiculos.Location = new Point(0, 0);
            tscVehiculos.Name = "tscVehiculos";
            tscVehiculos.Size = new Size(800, 450);
            tscVehiculos.TabIndex = 0;
            tscVehiculos.Text = "toolStripContainer1";
            // 
            // tscVehiculos.TopToolStripPanel
            // 
            // 
            // tlVehiculos
            // 
            tlVehiculos.ColumnCount = 4;
            tlVehiculos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlVehiculos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlVehiculos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlVehiculos.ColumnStyles.Add(new ColumnStyle());
            tlVehiculos.Controls.Add(dgvVehiculos, 0, 0);
            tlVehiculos.Controls.Add(btnSalir, 3, 1);
            tlVehiculos.Controls.Add(btnCrear, 2, 1);
            tlVehiculos.Controls.Add(btnEliminar, 1, 1);
            tlVehiculos.Controls.Add(btnEditar, 0, 1);
            tlVehiculos.Dock = DockStyle.Fill;
            tlVehiculos.Location = new Point(0, 0);
            tlVehiculos.Name = "tlVehiculos";
            tlVehiculos.RowCount = 2;
            tlVehiculos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlVehiculos.RowStyles.Add(new RowStyle());
            tlVehiculos.Size = new Size(800, 425);
            tlVehiculos.TabIndex = 0;
            // 
            // dgvVehiculos
            // 
            dgvVehiculos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlVehiculos.SetColumnSpan(dgvVehiculos, 4);
            dgvVehiculos.Dock = DockStyle.Fill;
            dgvVehiculos.Location = new Point(3, 3);
            dgvVehiculos.Name = "dgvVehiculos";
            dgvVehiculos.Size = new Size(794, 390);
            dgvVehiculos.TabIndex = 0;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(735, 399);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(60, 23);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(491, 399);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(120, 23);
            btnCrear.TabIndex = 1;
            btnCrear.Text = "Nuevo Vehiculo";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(247, 399);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(120, 23);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eliminar Vehiculo\r\n";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(3, 399);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(120, 23);
            btnEditar.TabIndex = 4;
            btnEditar.Text = "Editar Vehiculo";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // VehiculosLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tscVehiculos);
            Name = "VehiculosLista";
            Text = "VehiculosLista";
            Load += VehiculosLista_Load;
            tscVehiculos.ContentPanel.ResumeLayout(false);
            tscVehiculos.ResumeLayout(false);
            tscVehiculos.PerformLayout();
            tlVehiculos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVehiculos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer tscVehiculos;
        private TableLayoutPanel tlVehiculos;
        private DataGridView dgvVehiculos;
        private Button btnCrear;
        private Button btnSalir;
        private Button btnEliminar;
        private Button btnEditar;
    }
}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VehiculosLista));
            tscVehiculos = new ToolStripContainer();
            tlVehiculos = new TableLayoutPanel();
            dgvVehiculos = new DataGridView();
            btnCrear = new Button();
            btnSalir = new Button();
            tsVehiculos = new ToolStrip();
            tsbEditar = new ToolStripButton();
            tsbEliminar = new ToolStripButton();
            tscVehiculos.ContentPanel.SuspendLayout();
            tscVehiculos.TopToolStripPanel.SuspendLayout();
            tscVehiculos.SuspendLayout();
            tlVehiculos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehiculos).BeginInit();
            tsVehiculos.SuspendLayout();
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
            tscVehiculos.TopToolStripPanel.Controls.Add(tsVehiculos);
            // 
            // tlVehiculos
            // 
            tlVehiculos.ColumnCount = 2;
            tlVehiculos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlVehiculos.ColumnStyles.Add(new ColumnStyle());
            tlVehiculos.Controls.Add(dgvVehiculos, 0, 0);
            tlVehiculos.Controls.Add(btnCrear, 0, 1);
            tlVehiculos.Controls.Add(btnSalir, 1, 1);
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
            tlVehiculos.SetColumnSpan(dgvVehiculos, 2);
            dgvVehiculos.Dock = DockStyle.Fill;
            dgvVehiculos.Location = new Point(3, 3);
            dgvVehiculos.Name = "dgvVehiculos";
            dgvVehiculos.Size = new Size(794, 390);
            dgvVehiculos.TabIndex = 0;
            // 
            // btnCrear
            // 
            btnCrear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCrear.Location = new Point(615, 399);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(101, 23);
            btnCrear.TabIndex = 1;
            btnCrear.Text = "Nuevo Vehiculo";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(722, 399);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // tsVehiculos
            // 
            tsVehiculos.Dock = DockStyle.None;
            tsVehiculos.Items.AddRange(new ToolStripItem[] { tsbEditar, tsbEliminar });
            tsVehiculos.Location = new Point(3, 0);
            tsVehiculos.Name = "tsVehiculos";
            tsVehiculos.Size = new Size(203, 25);
            tsVehiculos.TabIndex = 0;
            // 
            // tsbEditar
            // 
            tsbEditar.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbEditar.Image = (Image)resources.GetObject("tsbEditar.Image");
            tsbEditar.ImageTransparentColor = Color.Magenta;
            tsbEditar.Name = "tsbEditar";
            tsbEditar.Size = new Size(89, 22);
            tsbEditar.Text = "Editar Vehiculo";
            tsbEditar.Click += tsbEditar_Click;
            // 
            // tsbEliminar
            // 
            tsbEliminar.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbEliminar.Image = (Image)resources.GetObject("tsbEliminar.Image");
            tsbEliminar.ImageTransparentColor = Color.Magenta;
            tsbEliminar.Name = "tsbEliminar";
            tsbEliminar.Size = new Size(102, 22);
            tsbEliminar.Text = "Eliminar Vehiculo";
            tsbEliminar.Click += tsbEliminar_Click;
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
            tscVehiculos.TopToolStripPanel.ResumeLayout(false);
            tscVehiculos.TopToolStripPanel.PerformLayout();
            tscVehiculos.ResumeLayout(false);
            tscVehiculos.PerformLayout();
            tlVehiculos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVehiculos).EndInit();
            tsVehiculos.ResumeLayout(false);
            tsVehiculos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer tscVehiculos;
        private ToolStrip tsVehiculos;
        private TableLayoutPanel tlVehiculos;
        private DataGridView dgvVehiculos;
        private Button btnCrear;
        private Button btnSalir;
        private ToolStripButton tsbEditar;
        private ToolStripButton tsbEliminar;
    }
}
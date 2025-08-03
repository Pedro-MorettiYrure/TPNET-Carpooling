namespace WindowsForms
{
    partial class LocalidadLista
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalidadLista));
            tscLocalidades = new ToolStripContainer();
            tlLocalidades = new TableLayoutPanel();
            dgvLocalidad = new DataGridView();
            btnCrear = new Button();
            btnSalir = new Button();
            tsLocalidades = new ToolStrip();
            tsbEditar = new ToolStripButton();
            tsbEliminar = new ToolStripButton();
            tscLocalidades.ContentPanel.SuspendLayout();
            tscLocalidades.TopToolStripPanel.SuspendLayout();
            tscLocalidades.SuspendLayout();
            tlLocalidades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLocalidad).BeginInit();
            tsLocalidades.SuspendLayout();
            SuspendLayout();
            // 
            // tscLocalidades
            // 
            // 
            // tscLocalidades.ContentPanel
            // 
            tscLocalidades.ContentPanel.Controls.Add(tlLocalidades);
            tscLocalidades.ContentPanel.Size = new Size(800, 425);
            tscLocalidades.Dock = DockStyle.Fill;
            tscLocalidades.Location = new Point(0, 0);
            tscLocalidades.Name = "tscLocalidades";
            tscLocalidades.Size = new Size(800, 450);
            tscLocalidades.TabIndex = 0;
            tscLocalidades.Text = "toolStripContainer1";
            // 
            // tscLocalidades.TopToolStripPanel
            // 
            tscLocalidades.TopToolStripPanel.Controls.Add(tsLocalidades);
            // 
            // tlLocalidades
            // 
            tlLocalidades.ColumnCount = 2;
            tlLocalidades.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlLocalidades.ColumnStyles.Add(new ColumnStyle());
            tlLocalidades.Controls.Add(dgvLocalidad, 0, 0);
            tlLocalidades.Controls.Add(btnCrear, 0, 1);
            tlLocalidades.Controls.Add(btnSalir, 1, 1);
            tlLocalidades.Dock = DockStyle.Fill;
            tlLocalidades.Location = new Point(0, 0);
            tlLocalidades.Name = "tlLocalidades";
            tlLocalidades.RowCount = 2;
            tlLocalidades.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlLocalidades.RowStyles.Add(new RowStyle());
            tlLocalidades.Size = new Size(800, 425);
            tlLocalidades.TabIndex = 0;
            // 
            // dgvLocalidad
            // 
            dgvLocalidad.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlLocalidades.SetColumnSpan(dgvLocalidad, 2);
            dgvLocalidad.Dock = DockStyle.Fill;
            dgvLocalidad.Location = new Point(3, 3);
            dgvLocalidad.Name = "dgvLocalidad";
            dgvLocalidad.Size = new Size(794, 390);
            dgvLocalidad.TabIndex = 0;
            // 
            // btnCrear
            // 
            btnCrear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCrear.Location = new Point(558, 399);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(158, 23);
            btnCrear.TabIndex = 1;
            btnCrear.Text = "Agregar Nueva Localidad";
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
            // tsLocalidades
            // 
            tsLocalidades.Dock = DockStyle.None;
            tsLocalidades.Items.AddRange(new ToolStripItem[] { tsbEditar, tsbEliminar });
            tsLocalidades.Location = new Point(3, 0);
            tsLocalidades.Name = "tsLocalidades";
            tsLocalidades.Size = new Size(286, 25);
            tsLocalidades.TabIndex = 0;
            // 
            // tsbEditar
            // 
            tsbEditar.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbEditar.Image = (Image)resources.GetObject("tsbEditar.Image");
            tsbEditar.ImageTransparentColor = Color.Magenta;
            tsbEditar.Name = "tsbEditar";
            tsbEditar.Size = new Size(115, 22);
            tsbEditar.Text = "Editar una localidad";
            tsbEditar.Click += tsbEditar_Click;
            // 
            // tsbEliminar
            // 
            tsbEliminar.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbEliminar.Image = (Image)resources.GetObject("tsbEliminar.Image");
            tsbEliminar.ImageTransparentColor = Color.Magenta;
            tsbEliminar.Name = "tsbEliminar";
            tsbEliminar.Size = new Size(128, 22);
            tsbEliminar.Text = "Eliminar una localidad";
            tsbEliminar.Click += this.tsbEliminar_Click;
            // 
            // LocalidadLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tscLocalidades);
            Name = "LocalidadLista";
            Text = "Form1";
            Load += LocalidadLista_Load;
            tscLocalidades.ContentPanel.ResumeLayout(false);
            tscLocalidades.TopToolStripPanel.ResumeLayout(false);
            tscLocalidades.TopToolStripPanel.PerformLayout();
            tscLocalidades.ResumeLayout(false);
            tscLocalidades.PerformLayout();
            tlLocalidades.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLocalidad).EndInit();
            tsLocalidades.ResumeLayout(false);
            tsLocalidades.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer tscLocalidades;
        private TableLayoutPanel tlLocalidades;
        private DataGridView dgvLocalidad;
        private Button btnCrear;
        private Button btnSalir;
        private ToolStrip tsLocalidades;
        private ToolStripButton tsbEditar;
        private ToolStripButton tsbEliminar;
    }
}

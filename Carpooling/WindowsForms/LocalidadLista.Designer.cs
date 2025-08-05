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
            tscLocalidades = new ToolStripContainer();
            tlLocalidades = new TableLayoutPanel();
            dgvLocalidad = new DataGridView();
            btnSalir = new Button();
            btnCrear = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            tscLocalidades.ContentPanel.SuspendLayout();
            tscLocalidades.SuspendLayout();
            tlLocalidades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLocalidad).BeginInit();
            SuspendLayout();
            // 
            // tscLocalidades
            // 
            // 
            // tscLocalidades.ContentPanel
            // 
            tscLocalidades.ContentPanel.Controls.Add(tlLocalidades);
            tscLocalidades.ContentPanel.Size = new Size(800, 450);
            tscLocalidades.Dock = DockStyle.Fill;
            tscLocalidades.Location = new Point(0, 0);
            tscLocalidades.Name = "tscLocalidades";
            tscLocalidades.Size = new Size(800, 450);
            tscLocalidades.TabIndex = 0;
            tscLocalidades.Text = "toolStripContainer1";
            // 
            // tscLocalidades.TopToolStripPanel
            // 
            tscLocalidades.TopToolStripPanel.Click += tscLocalidades_TopToolStripPanel_Click;
            // 
            // tlLocalidades
            // 
            tlLocalidades.ColumnCount = 4;
            tlLocalidades.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlLocalidades.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tlLocalidades.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tlLocalidades.ColumnStyles.Add(new ColumnStyle());
            tlLocalidades.Controls.Add(dgvLocalidad, 0, 0);
            tlLocalidades.Controls.Add(btnSalir, 3, 1);
            tlLocalidades.Controls.Add(btnCrear, 2, 1);
            tlLocalidades.Controls.Add(btnEliminar, 1, 1);
            tlLocalidades.Controls.Add(btnEditar, 0, 1);
            tlLocalidades.Dock = DockStyle.Fill;
            tlLocalidades.Location = new Point(0, 0);
            tlLocalidades.Name = "tlLocalidades";
            tlLocalidades.RowCount = 2;
            tlLocalidades.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlLocalidades.RowStyles.Add(new RowStyle());
            tlLocalidades.Size = new Size(800, 450);
            tlLocalidades.TabIndex = 0;
            // 
            // dgvLocalidad
            // 
            dgvLocalidad.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlLocalidades.SetColumnSpan(dgvLocalidad, 4);
            dgvLocalidad.Dock = DockStyle.Fill;
            dgvLocalidad.Location = new Point(3, 3);
            dgvLocalidad.Name = "dgvLocalidad";
            dgvLocalidad.Size = new Size(794, 415);
            dgvLocalidad.TabIndex = 0;
            // 
            // btnSalir
            // 
            btnSalir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSalir.Location = new Point(722, 424);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(481, 424);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(158, 23);
            btnCrear.TabIndex = 1;
            btnCrear.Text = "Agregar Nueva Localidad";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(242, 424);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(158, 23);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eliminar Localidad";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(3, 424);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(158, 23);
            btnEditar.TabIndex = 4;
            btnEditar.Text = "Editar Localidad";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // LocalidadLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tscLocalidades);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LocalidadLista";
            Text = "Form1";
            Load += LocalidadLista_Load;
            tscLocalidades.ContentPanel.ResumeLayout(false);
            tscLocalidades.ResumeLayout(false);
            tscLocalidades.PerformLayout();
            tlLocalidades.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLocalidad).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer tscLocalidades;
        private TableLayoutPanel tlLocalidades;
        private DataGridView dgvLocalidad;
        private Button btnCrear;
        private Button btnSalir;
        private Button btnEliminar;
        private Button btnEditar;
    }
}

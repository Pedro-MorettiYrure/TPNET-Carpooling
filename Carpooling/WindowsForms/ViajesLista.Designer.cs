namespace WindowsForms
{
    partial class ViajesLista
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
            tsViajesLista = new ToolStripContainer();
            tlpViajesLista = new TableLayoutPanel();
            dgvViajesLista = new DataGridView();
            btnSalirViajesLista = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnCrear = new Button();
            tsViajesLista.ContentPanel.SuspendLayout();
            tsViajesLista.SuspendLayout();
            tlpViajesLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvViajesLista).BeginInit();
            SuspendLayout();
            // 
            // tsViajesLista
            // 
            // 
            // tsViajesLista.ContentPanel
            // 
            tsViajesLista.ContentPanel.Controls.Add(tlpViajesLista);
            tsViajesLista.ContentPanel.Size = new Size(800, 425);
            tsViajesLista.Dock = DockStyle.Fill;
            tsViajesLista.Location = new Point(0, 0);
            tsViajesLista.Name = "tsViajesLista";
            tsViajesLista.Size = new Size(800, 450);
            tsViajesLista.TabIndex = 0;
            tsViajesLista.Text = "toolStripContainer1";
            // 
            // tlpViajesLista
            // 
            tlpViajesLista.ColumnCount = 4;
            tlpViajesLista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpViajesLista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpViajesLista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpViajesLista.ColumnStyles.Add(new ColumnStyle());
            tlpViajesLista.Controls.Add(dgvViajesLista, 0, 0);
            tlpViajesLista.Controls.Add(btnSalirViajesLista, 3, 1);
            tlpViajesLista.Controls.Add(btnEditar, 1, 1);
            tlpViajesLista.Controls.Add(btnEliminar, 2, 1);
            tlpViajesLista.Controls.Add(btnCrear, 0, 1);
            tlpViajesLista.Dock = DockStyle.Fill;
            tlpViajesLista.Location = new Point(0, 0);
            tlpViajesLista.Name = "tlpViajesLista";
            tlpViajesLista.RowCount = 2;
            tlpViajesLista.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpViajesLista.RowStyles.Add(new RowStyle());
            tlpViajesLista.Size = new Size(800, 425);
            tlpViajesLista.TabIndex = 0;
            // 
            // dgvViajesLista
            // 
            dgvViajesLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpViajesLista.SetColumnSpan(dgvViajesLista, 4);
            dgvViajesLista.Dock = DockStyle.Fill;
            dgvViajesLista.Location = new Point(3, 3);
            dgvViajesLista.Name = "dgvViajesLista";
            dgvViajesLista.Size = new Size(794, 390);
            dgvViajesLista.TabIndex = 0;
            // 
            // btnSalirViajesLista
            // 
            btnSalirViajesLista.Location = new Point(720, 399);
            btnSalirViajesLista.Name = "btnSalirViajesLista";
            btnSalirViajesLista.Size = new Size(75, 23);
            btnSalirViajesLista.TabIndex = 1;
            btnSalirViajesLista.Text = "Salir";
            btnSalirViajesLista.UseVisualStyleBackColor = true;
            btnSalirViajesLista.Click += btnSalirViajesLista_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(242, 399);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(120, 23);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar Viaje";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(481, 399);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(120, 23);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar Viaje";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(3, 399);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(120, 23);
            btnCrear.TabIndex = 2;
            btnCrear.Text = "Agregar Viaje";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // ViajesLista
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tsViajesLista);
            Name = "ViajesLista";
            Text = "Form1";
            Load += ViajesLista_Load;
            tsViajesLista.ContentPanel.ResumeLayout(false);
            tsViajesLista.ResumeLayout(false);
            tsViajesLista.PerformLayout();
            tlpViajesLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvViajesLista).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer tsViajesLista;
        private TableLayoutPanel tlpViajesLista;
        private DataGridView dgvViajesLista;
        private Button btnSalirViajesLista;
        private Button btnCrear;
        private Button btnEditar;
        private Button btnEliminar;
    }
}
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
            btnCrear = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnCalificarPasajeros = new Button();
            btnVerSolicitudes = new Button();
            btnIniciarViaje = new Button();
            btnFinalizarViaje = new Button();
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
            tlpViajesLista.ColumnCount = 6;
            tlpViajesLista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.6542053F));
            tlpViajesLista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.990654F));
            tlpViajesLista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.5770855F));
            tlpViajesLista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.8218784F));
            tlpViajesLista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 221F));
            tlpViajesLista.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 114F));
            tlpViajesLista.Controls.Add(dgvViajesLista, 0, 0);
            tlpViajesLista.Controls.Add(btnSalirViajesLista, 0, 1);
            tlpViajesLista.Controls.Add(btnCrear, 3, 1);
            tlpViajesLista.Controls.Add(btnEliminar, 1, 1);
            tlpViajesLista.Controls.Add(btnEditar, 2, 1);
            tlpViajesLista.Controls.Add(btnCalificarPasajeros, 5, 2);
            tlpViajesLista.Controls.Add(btnVerSolicitudes, 5, 1);
            tlpViajesLista.Controls.Add(btnIniciarViaje, 4, 1);
            tlpViajesLista.Controls.Add(btnFinalizarViaje, 4, 2);
            tlpViajesLista.Dock = DockStyle.Fill;
            tlpViajesLista.Location = new Point(0, 0);
            tlpViajesLista.Name = "tlpViajesLista";
            tlpViajesLista.RowCount = 3;
            tlpViajesLista.RowStyles.Add(new RowStyle(SizeType.Percent, 91.41414F));
            tlpViajesLista.RowStyles.Add(new RowStyle());
            tlpViajesLista.RowStyles.Add(new RowStyle(SizeType.Percent, 8.585858F));
            tlpViajesLista.Size = new Size(800, 425);
            tlpViajesLista.TabIndex = 0;
            // 
            // dgvViajesLista
            // 
            dgvViajesLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tlpViajesLista.SetColumnSpan(dgvViajesLista, 6);
            dgvViajesLista.Dock = DockStyle.Fill;
            dgvViajesLista.Location = new Point(3, 3);
            dgvViajesLista.Name = "dgvViajesLista";
            dgvViajesLista.Size = new Size(794, 352);
            dgvViajesLista.TabIndex = 0;
            dgvViajesLista.SelectionChanged += dgvViajesLista_SelectionChanged;
            // 
            // btnSalirViajesLista
            // 
            btnSalirViajesLista.Location = new Point(3, 361);
            btnSalirViajesLista.Name = "btnSalirViajesLista";
            btnSalirViajesLista.Size = new Size(75, 23);
            btnSalirViajesLista.TabIndex = 1;
            btnSalirViajesLista.Text = "Salir";
            btnSalirViajesLista.UseVisualStyleBackColor = true;
            btnSalirViajesLista.Click += btnSalirViajesLista_Click;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(360, 361);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(100, 23);
            btnCrear.TabIndex = 2;
            btnCrear.Text = "Agregar Viaje";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Enabled = false;
            btnEliminar.Location = new Point(145, 361);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 23);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Cancelar Viaje";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(251, 361);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(103, 23);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar Viaje";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnCalificarPasajeros
            // 
            btnCalificarPasajeros.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalificarPasajeros.BackColor = SystemColors.MenuHighlight;
            btnCalificarPasajeros.Enabled = false;
            btnCalificarPasajeros.Location = new Point(687, 394);
            btnCalificarPasajeros.Name = "btnCalificarPasajeros";
            btnCalificarPasajeros.Size = new Size(110, 28);
            btnCalificarPasajeros.TabIndex = 6;
            btnCalificarPasajeros.Text = "CalificarPasajeros";
            btnCalificarPasajeros.UseVisualStyleBackColor = false;
            btnCalificarPasajeros.Click += btnCalificarPasajeros_Click;
            // 
            // btnVerSolicitudes
            // 
            btnVerSolicitudes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerSolicitudes.Enabled = false;
            btnVerSolicitudes.Location = new Point(687, 361);
            btnVerSolicitudes.Name = "btnVerSolicitudes";
            btnVerSolicitudes.Size = new Size(110, 27);
            btnVerSolicitudes.TabIndex = 5;
            btnVerSolicitudes.Text = "Ver Solicitudes";
            btnVerSolicitudes.UseVisualStyleBackColor = true;
            btnVerSolicitudes.Click += btnVerSolicitudes_Click;
            // 
            // btnIniciarViaje
            // 
            btnIniciarViaje.BackColor = SystemColors.Highlight;
            btnIniciarViaje.Enabled = false;
            btnIniciarViaje.Location = new Point(466, 361);
            btnIniciarViaje.Name = "btnIniciarViaje";
            btnIniciarViaje.Size = new Size(75, 23);
            btnIniciarViaje.TabIndex = 7;
            btnIniciarViaje.Text = "Iniciar Viaje";
            btnIniciarViaje.UseVisualStyleBackColor = false;
            btnIniciarViaje.Click += btnIniciarViaje_Click;
            // 
            // btnFinalizarViaje
            // 
            btnFinalizarViaje.BackColor = SystemColors.Highlight;
            btnFinalizarViaje.Enabled = false;
            btnFinalizarViaje.Location = new Point(466, 394);
            btnFinalizarViaje.Name = "btnFinalizarViaje";
            btnFinalizarViaje.Size = new Size(100, 23);
            btnFinalizarViaje.TabIndex = 8;
            btnFinalizarViaje.Text = "Finalizar Viaje";
            btnFinalizarViaje.UseVisualStyleBackColor = false;
            btnFinalizarViaje.Click += btnFinalizarViaje_Click;
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
        private Button btnVerSolicitudes;
        private Button btnCalificarPasajeros;
        private Button btnIniciarViaje;
        private Button btnFinalizarViaje;
    }
}
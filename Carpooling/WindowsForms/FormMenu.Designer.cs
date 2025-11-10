namespace WindowsForms
{
    partial class FormMenu
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
            tlMain = new TableLayoutPanel();
            lbMain = new Label();
            label1 = new Label();
            btnReportes = new Button();
            btnLocalidadLista = new Button();
            btnCerrarSesion = new Button();
            btnEditarUsuario = new Button();
            btnConvertirAConductor = new Button();
            btnViajesConductor = new Button();
            btnVehiculoLista = new Button();
            btnViajeLista = new Button();
            btnBuscarViaje = new Button();
            btnMisSolicitudes = new Button();
            tlMain.SuspendLayout();
            SuspendLayout();
            // 
            // tlMain
            // 
            tlMain.ColumnCount = 4;
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlMain.Controls.Add(lbMain, 1, 1);
            tlMain.Controls.Add(label1, 1, 2);
            tlMain.Controls.Add(btnReportes, 1, 3);
            tlMain.Controls.Add(btnLocalidadLista, 2, 3);
            tlMain.Controls.Add(btnCerrarSesion, 3, 0);
            tlMain.Controls.Add(btnEditarUsuario, 3, 1);
            tlMain.Controls.Add(btnConvertirAConductor, 3, 2);
            tlMain.Controls.Add(btnViajeLista, 2, 5);
            tlMain.Controls.Add(btnBuscarViaje, 1, 5);
            tlMain.Controls.Add(btnMisSolicitudes, 1, 6);
            tlMain.Controls.Add(btnViajesConductor, 2, 4);
            tlMain.Controls.Add(btnVehiculoLista, 1, 4);
            tlMain.Dock = DockStyle.Fill;
            tlMain.Location = new Point(0, 0);
            tlMain.Name = "tlMain";
            tlMain.RowCount = 8;
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 11.84413F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 11.8485117F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 11.85149F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 16.1139736F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 16.1139641F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 16.1139641F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 16.1139641F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlMain.Size = new Size(1123, 580);
            tlMain.TabIndex = 4;
            // 
            // lbMain
            // 
            lbMain.Anchor = AnchorStyles.None;
            lbMain.AutoSize = true;
            tlMain.SetColumnSpan(lbMain, 2);
            lbMain.FlatStyle = FlatStyle.Popup;
            lbMain.Font = new Font("Papyrus", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbMain.ForeColor = SystemColors.HotTrack;
            lbMain.ImageAlign = ContentAlignment.TopCenter;
            lbMain.Location = new Point(383, 66);
            lbMain.Name = "lbMain";
            lbMain.RightToLeft = RightToLeft.No;
            lbMain.Size = new Size(353, 66);
            lbMain.TabIndex = 2;
            lbMain.Text = "Menu principal";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            tlMain.SetColumnSpan(label1, 2);
            label1.FlatStyle = FlatStyle.Popup;
            label1.Font = new Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(283, 132);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(554, 23);
            label1.TabIndex = 3;
            label1.Text = "Seleccione una opcion:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnReportes
            // 
            btnReportes.Anchor = AnchorStyles.None;
            btnReportes.Font = new Font("Segoe UI", 11F);
            btnReportes.Location = new Point(305, 215);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(230, 55);
            btnReportes.TabIndex = 9;
            btnReportes.Text = "Generar Reportes";
            btnReportes.UseVisualStyleBackColor = true;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnLocalidadLista
            // 
            btnLocalidadLista.Anchor = AnchorStyles.None;
            btnLocalidadLista.BackgroundImageLayout = ImageLayout.Center;
            btnLocalidadLista.Font = new Font("Segoe UI", 11F);
            btnLocalidadLista.Location = new Point(585, 215);
            btnLocalidadLista.Name = "btnLocalidadLista";
            btnLocalidadLista.Size = new Size(230, 55);
            btnLocalidadLista.TabIndex = 0;
            btnLocalidadLista.Text = "Localidades";
            btnLocalidadLista.UseVisualStyleBackColor = true;
            btnLocalidadLista.Click += btnLocalidadLista_Click;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrarSesion.BackColor = Color.LightCoral;
            btnCerrarSesion.Font = new Font("Segoe UI", 11F);
            btnCerrarSesion.Location = new Point(966, 3);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(154, 40);
            btnCerrarSesion.TabIndex = 10;
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // btnEditarUsuario
            // 
            btnEditarUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditarUsuario.Font = new Font("Segoe UI", 11F);
            btnEditarUsuario.Location = new Point(966, 69);
            btnEditarUsuario.Name = "btnEditarUsuario";
            btnEditarUsuario.Size = new Size(154, 40);
            btnEditarUsuario.TabIndex = 4;
            btnEditarUsuario.Text = "Editar mis datos";
            btnEditarUsuario.UseVisualStyleBackColor = true;
            btnEditarUsuario.Click += btnEditarUsuario_Click;
            // 
            // btnConvertirAConductor
            // 
            btnConvertirAConductor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConvertirAConductor.Font = new Font("Segoe UI", 11F);
            btnConvertirAConductor.Location = new Point(966, 135);
            btnConvertirAConductor.Name = "btnConvertirAConductor";
            btnConvertirAConductor.Size = new Size(154, 55);
            btnConvertirAConductor.TabIndex = 4;
            btnConvertirAConductor.Text = "Quiero ser conductor";
            btnConvertirAConductor.UseVisualStyleBackColor = true;
            btnConvertirAConductor.Click += btnConvertirAConductor_Click;
            // 
            // btnViajesConductor
            // 
            btnViajesConductor.Anchor = AnchorStyles.None;
            btnViajesConductor.Font = new Font("Segoe UI", 11F);
            btnViajesConductor.Location = new Point(585, 305);
            btnViajesConductor.Name = "btnViajesConductor";
            btnViajesConductor.Size = new Size(230, 55);
            btnViajesConductor.TabIndex = 11;
            btnViajesConductor.Text = "Mis Viajes como Conductor";
            btnViajesConductor.UseVisualStyleBackColor = true;
            btnViajesConductor.Click += btnViajesConductor_Click;
            // 
            // btnVehiculoLista
            // 
            btnVehiculoLista.Anchor = AnchorStyles.None;
            btnVehiculoLista.Font = new Font("Segoe UI", 11F);
            btnVehiculoLista.ForeColor = SystemColors.ControlText;
            btnVehiculoLista.Location = new Point(305, 305);
            btnVehiculoLista.Name = "btnVehiculoLista";
            btnVehiculoLista.Size = new Size(230, 55);
            btnVehiculoLista.TabIndex = 1;
            btnVehiculoLista.Text = "Mis vehiculos";
            btnVehiculoLista.UseVisualStyleBackColor = true;
            btnVehiculoLista.Click += btnVehiculoLista_Click;
            // 
            // btnViajeLista
            // 
            btnViajeLista.Anchor = AnchorStyles.None;
            btnViajeLista.Font = new Font("Segoe UI", 11F);
            btnViajeLista.Location = new Point(585, 395);
            btnViajeLista.Name = "btnViajeLista";
            btnViajeLista.Size = new Size(230, 55);
            btnViajeLista.TabIndex = 4;
            btnViajeLista.Text = "Mis Viajes";
            btnViajeLista.UseVisualStyleBackColor = true;
            btnViajeLista.Click += btnViajeLista_Click;
            // 
            // btnBuscarViaje
            // 
            btnBuscarViaje.Anchor = AnchorStyles.None;
            btnBuscarViaje.Font = new Font("Segoe UI", 11F);
            btnBuscarViaje.Location = new Point(305, 395);
            btnBuscarViaje.Name = "btnBuscarViaje";
            btnBuscarViaje.Size = new Size(230, 55);
            btnBuscarViaje.TabIndex = 0;
            btnBuscarViaje.Text = "Buscar viaje";
            btnBuscarViaje.UseVisualStyleBackColor = true;
            btnBuscarViaje.Click += btnBuscarViaje_Click;
            // 
            // btnMisSolicitudes
            // 
            btnMisSolicitudes.Anchor = AnchorStyles.None;
            tlMain.SetColumnSpan(btnMisSolicitudes, 2);
            btnMisSolicitudes.Font = new Font("Segoe UI", 11F);
            btnMisSolicitudes.Location = new Point(445, 485);
            btnMisSolicitudes.Name = "btnMisSolicitudes";
            btnMisSolicitudes.Size = new Size(230, 55);
            btnMisSolicitudes.TabIndex = 8;
            btnMisSolicitudes.Text = "Ver mis solicitudes";
            btnMisSolicitudes.UseVisualStyleBackColor = true;
            btnMisSolicitudes.Click += btnMisSolicitudes_Click;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1123, 580);
            Controls.Add(tlMain);
            Name = "FormMenu";
            Text = "FormMenu";
            WindowState = FormWindowState.Maximized;
            tlMain.ResumeLayout(false);
            tlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlMain;
        private Label lbMain;
        private Button btnVehiculoLista;
        private Button btnLocalidadLista;
        private Label label1;
        private Button btnConvertirAConductor;
        private Button btnViajeLista;
        private Button btnEditarUsuario;
        private Button btnBuscarViaje;
        private Button btnMisSolicitudes;
        private Button btnReportes;
        private Button btnCerrarSesion;
        private Button btnViajesConductor;
    }
}
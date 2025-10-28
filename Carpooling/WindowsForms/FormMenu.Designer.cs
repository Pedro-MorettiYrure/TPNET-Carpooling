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
            btnMisSolicitudes = new Button();
            btnVehiculoLista = new Button();
            btnEditarUsuario = new Button();
            btnConvertirAConductor = new Button();
            btnBuscarViaje = new Button();
            btnViajeLista = new Button();
            btnCerrarSesion = new Button();
            tlMain.SuspendLayout();
            SuspendLayout();
            // 
            // tlMain
            // 
            tlMain.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tlMain.ColumnCount = 4;
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlMain.Controls.Add(lbMain, 1, 1);
            tlMain.Controls.Add(label1, 1, 2);
            tlMain.Controls.Add(btnReportes, 1, 3);
            tlMain.Controls.Add(btnLocalidadLista, 2, 3);
            tlMain.Controls.Add(btnMisSolicitudes, 2, 6);
            tlMain.Controls.Add(btnVehiculoLista, 1, 6);
            tlMain.Controls.Add(btnEditarUsuario, 1, 5);
            tlMain.Controls.Add(btnConvertirAConductor, 1, 4);
            tlMain.Controls.Add(btnBuscarViaje, 2, 4);
            tlMain.Controls.Add(btnViajeLista, 2, 5);
            tlMain.Controls.Add(btnCerrarSesion, 3, 0);
            tlMain.Location = new Point(0, 0);
            tlMain.Name = "tlMain";
            tlMain.RowCount = 7;
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 10.7142839F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.8809557F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.8809509F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.8809509F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.8809509F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.8809509F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 14.8809509F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlMain.Size = new Size(766, 374);
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
            lbMain.Location = new Point(205, 40);
            lbMain.Name = "lbMain";
            lbMain.RightToLeft = RightToLeft.No;
            lbMain.Size = new Size(353, 55);
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
            label1.Location = new Point(191, 95);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(382, 23);
            label1.TabIndex = 3;
            label1.Text = "Seleccione una opcion:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnReportes
            // 
            btnReportes.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnReportes.Location = new Point(191, 155);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(194, 45);
            btnReportes.TabIndex = 9;
            btnReportes.Text = "Generar Reportes";
            btnReportes.UseVisualStyleBackColor = true;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnLocalidadLista
            // 
            btnLocalidadLista.Location = new Point(391, 153);
            btnLocalidadLista.Name = "btnLocalidadLista";
            btnLocalidadLista.Size = new Size(182, 47);
            btnLocalidadLista.TabIndex = 0;
            btnLocalidadLista.Text = "Localidades";
            btnLocalidadLista.UseVisualStyleBackColor = true;
            btnLocalidadLista.Click += btnLocalidadLista_Click;
            // 
            // btnMisSolicitudes
            // 
            btnMisSolicitudes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnMisSolicitudes.Location = new Point(422, 318);
            btnMisSolicitudes.Name = "btnMisSolicitudes";
            btnMisSolicitudes.Size = new Size(119, 53);
            btnMisSolicitudes.TabIndex = 8;
            btnMisSolicitudes.Text = "Ver mis solicitudes";
            btnMisSolicitudes.UseVisualStyleBackColor = true;
            btnMisSolicitudes.Click += btnMisSolicitudes_Click;
            // 
            // btnVehiculoLista
            // 
            btnVehiculoLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnVehiculoLista.ForeColor = SystemColors.ControlText;
            btnVehiculoLista.Location = new Point(223, 318);
            btnVehiculoLista.Name = "btnVehiculoLista";
            btnVehiculoLista.Size = new Size(129, 53);
            btnVehiculoLista.TabIndex = 1;
            btnVehiculoLista.Text = "Mis vehiculos";
            btnVehiculoLista.UseVisualStyleBackColor = true;
            btnVehiculoLista.Click += btnVehiculoLista_Click;
            // 
            // btnEditarUsuario
            // 
            btnEditarUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnEditarUsuario.Location = new Point(227, 263);
            btnEditarUsuario.Name = "btnEditarUsuario";
            btnEditarUsuario.Size = new Size(122, 49);
            btnEditarUsuario.TabIndex = 4;
            btnEditarUsuario.Text = "Editar mis datos";
            btnEditarUsuario.UseVisualStyleBackColor = true;
            btnEditarUsuario.Click += btnEditarUsuario_Click;
            // 
            // btnConvertirAConductor
            // 
            btnConvertirAConductor.Anchor = AnchorStyles.Top;
            btnConvertirAConductor.Location = new Point(221, 208);
            btnConvertirAConductor.Name = "btnConvertirAConductor";
            btnConvertirAConductor.Size = new Size(133, 49);
            btnConvertirAConductor.TabIndex = 4;
            btnConvertirAConductor.Text = "Quiero ser conductor";
            btnConvertirAConductor.UseVisualStyleBackColor = true;
            btnConvertirAConductor.Click += btnConvertirAConductor_Click;
            // 
            // btnBuscarViaje
            // 
            btnBuscarViaje.Anchor = AnchorStyles.Top;
            btnBuscarViaje.Location = new Point(421, 208);
            btnBuscarViaje.Name = "btnBuscarViaje";
            btnBuscarViaje.Size = new Size(121, 49);
            btnBuscarViaje.TabIndex = 0;
            btnBuscarViaje.Text = "Buscar viaje";
            btnBuscarViaje.UseVisualStyleBackColor = true;
            btnBuscarViaje.Click += btnBuscarViaje_Click;
            // 
            // btnViajeLista
            // 
            btnViajeLista.Anchor = AnchorStyles.Top;
            btnViajeLista.Location = new Point(416, 263);
            btnViajeLista.Name = "btnViajeLista";
            btnViajeLista.Size = new Size(131, 47);
            btnViajeLista.TabIndex = 4;
            btnViajeLista.Text = "Mis Viajes";
            btnViajeLista.UseVisualStyleBackColor = true;
            btnViajeLista.Click += btnViajeLista_Click;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrarSesion.BackColor = Color.LightCoral;
            btnCerrarSesion.Location = new Point(670, 3);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(93, 23);
            btnCerrarSesion.TabIndex = 10;
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(766, 374);
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
    }
}
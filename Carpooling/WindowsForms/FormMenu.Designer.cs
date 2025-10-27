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
            btnEditarUsuario = new Button();
            btnMisSolicitudes = new Button();
            label1 = new Label();
            btnConvertirAConductor = new Button();
            btnVehiculoLista = new Button();
            btnBuscarViaje = new Button();
            btnViajeLista = new Button();
            btnLocalidadLista = new Button();
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
            tlMain.Controls.Add(btnEditarUsuario, 0, 0);
            tlMain.Controls.Add(btnMisSolicitudes, 3, 6);
            tlMain.Controls.Add(label1, 1, 2);
            tlMain.Controls.Add(btnConvertirAConductor, 1, 6);
            tlMain.Controls.Add(btnVehiculoLista, 0, 1);
            tlMain.Controls.Add(btnBuscarViaje, 2, 5);
            tlMain.Controls.Add(btnViajeLista, 1, 5);
            tlMain.Controls.Add(btnLocalidadLista, 1, 4);
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
            // btnEditarUsuario
            // 
            tlMain.SetColumnSpan(btnEditarUsuario, 2);
            btnEditarUsuario.Location = new Point(3, 3);
            btnEditarUsuario.Name = "btnEditarUsuario";
            btnEditarUsuario.Size = new Size(102, 25);
            btnEditarUsuario.TabIndex = 5;
            btnEditarUsuario.Text = "Editar mis datos";
            btnEditarUsuario.UseVisualStyleBackColor = true;
            btnEditarUsuario.Click += btnEditarUsuario_Click;
            // 
            // btnMisSolicitudes
            // 
            btnMisSolicitudes.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnMisSolicitudes.Location = new Point(647, 345);
            btnMisSolicitudes.Name = "btnMisSolicitudes";
            btnMisSolicitudes.Size = new Size(116, 26);
            btnMisSolicitudes.TabIndex = 8;
            btnMisSolicitudes.Text = "Ver mis solicitudes";
            btnMisSolicitudes.UseVisualStyleBackColor = true;
            btnMisSolicitudes.Click += btnMisSolicitudes_Click;
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
            // btnConvertirAConductor
            // 
            btnConvertirAConductor.Anchor = AnchorStyles.Bottom;
            tlMain.SetColumnSpan(btnConvertirAConductor, 2);
            btnConvertirAConductor.Location = new Point(316, 345);
            btnConvertirAConductor.Name = "btnConvertirAConductor";
            btnConvertirAConductor.Size = new Size(131, 26);
            btnConvertirAConductor.TabIndex = 1;
            btnConvertirAConductor.Text = "Quiero ser conductor";
            btnConvertirAConductor.UseVisualStyleBackColor = true;
            btnConvertirAConductor.Click += btnConvertirAConductor_Click;
            // 
            // btnVehiculoLista
            // 
            btnVehiculoLista.ForeColor = SystemColors.ControlText;
            btnVehiculoLista.Location = new Point(3, 43);
            btnVehiculoLista.Name = "btnVehiculoLista";
            btnVehiculoLista.Size = new Size(115, 35);
            btnVehiculoLista.TabIndex = 1;
            btnVehiculoLista.Text = "Mis vehiculos";
            btnVehiculoLista.UseVisualStyleBackColor = true;
            btnVehiculoLista.Click += btnVehiculoLista_Click;
            // 
            // btnBuscarViaje
            // 
            btnBuscarViaje.Anchor = AnchorStyles.Top;
            btnBuscarViaje.Location = new Point(421, 263);
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
            btnViajeLista.Location = new Point(222, 263);
            btnViajeLista.Name = "btnViajeLista";
            btnViajeLista.Size = new Size(131, 47);
            btnViajeLista.TabIndex = 4;
            btnViajeLista.Text = "Mis Viajes";
            btnViajeLista.UseVisualStyleBackColor = true;
            btnViajeLista.Click += btnViajeLista_Click;
            // 
            // btnLocalidadLista
            // 
            btnLocalidadLista.Anchor = AnchorStyles.Top;
            tlMain.SetColumnSpan(btnLocalidadLista, 2);
            btnLocalidadLista.Location = new Point(316, 208);
            btnLocalidadLista.Name = "btnLocalidadLista";
            btnLocalidadLista.Size = new Size(131, 47);
            btnLocalidadLista.TabIndex = 0;
            btnLocalidadLista.Text = "Localidades";
            btnLocalidadLista.UseVisualStyleBackColor = true;
            btnLocalidadLista.Click += btnLocalidadLista_Click;
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
    }
}
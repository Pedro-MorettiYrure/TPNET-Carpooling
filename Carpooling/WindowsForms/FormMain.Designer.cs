namespace WindowsForms
{
    partial class FormMain
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
            mnsPrincipal = new MenuStrip();
            mnuArchivo = new ToolStripMenuItem();
            mnuSalir = new ToolStripMenuItem();
            tlMain = new TableLayoutPanel();
            btnLocalidadLista = new Button();
            btnVehiculoLista = new Button();
            lbMain = new Label();
            mnsPrincipal.SuspendLayout();
            tlMain.SuspendLayout();
            SuspendLayout();
            // 
            // mnsPrincipal
            // 
            mnsPrincipal.Items.AddRange(new ToolStripItem[] { mnuArchivo });
            mnsPrincipal.Location = new Point(0, 0);
            mnsPrincipal.Name = "mnsPrincipal";
            mnsPrincipal.Size = new Size(800, 24);
            mnsPrincipal.TabIndex = 1;
            mnsPrincipal.Text = "menuStrip1";
            // 
            // mnuArchivo
            // 
            mnuArchivo.DropDownItems.AddRange(new ToolStripItem[] { mnuSalir });
            mnuArchivo.Name = "mnuArchivo";
            mnuArchivo.Size = new Size(60, 20);
            mnuArchivo.Text = "Archivo";
            // 
            // mnuSalir
            // 
            mnuSalir.Name = "mnuSalir";
            mnuSalir.Size = new Size(96, 22);
            mnuSalir.Text = "Salir";
            mnuSalir.Click += mnuSalir_Click;
            // 
            // tlMain
            // 
            tlMain.ColumnCount = 3;
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlMain.ColumnStyles.Add(new ColumnStyle());
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlMain.Controls.Add(btnLocalidadLista, 1, 1);
            tlMain.Controls.Add(btnVehiculoLista, 1, 2);
            tlMain.Controls.Add(lbMain, 1, 0);
            tlMain.Dock = DockStyle.Fill;
            tlMain.Location = new Point(0, 24);
            tlMain.Name = "tlMain";
            tlMain.RowCount = 3;
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlMain.Size = new Size(800, 426);
            tlMain.TabIndex = 3;
            // 
            // btnLocalidadLista
            // 
            btnLocalidadLista.Anchor = AnchorStyles.Top;
            btnLocalidadLista.Location = new Point(356, 144);
            btnLocalidadLista.Name = "btnLocalidadLista";
            btnLocalidadLista.Size = new Size(86, 23);
            btnLocalidadLista.TabIndex = 0;
            btnLocalidadLista.Text = "Localidades";
            btnLocalidadLista.UseVisualStyleBackColor = true;
            btnLocalidadLista.Click += btnLocalidadLista_Click;
            // 
            // btnVehiculoLista
            // 
            btnVehiculoLista.Anchor = AnchorStyles.Top;
            btnVehiculoLista.Location = new Point(356, 285);
            btnVehiculoLista.Name = "btnVehiculoLista";
            btnVehiculoLista.Size = new Size(86, 23);
            btnVehiculoLista.TabIndex = 1;
            btnVehiculoLista.Text = "Vehiculos";
            btnVehiculoLista.UseVisualStyleBackColor = true;
            // 
            // lbMain
            // 
            lbMain.Anchor = AnchorStyles.None;
            lbMain.AutoSize = true;
            lbMain.Location = new Point(351, 63);
            lbMain.Name = "lbMain";
            lbMain.Size = new Size(97, 15);
            lbMain.TabIndex = 2;
            lbMain.Text = "Gestionar CRUDS";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tlMain);
            Controls.Add(mnsPrincipal);
            IsMdiContainer = true;
            MainMenuStrip = mnsPrincipal;
            Name = "FormMain";
            Text = "Carpooling";
            Shown += FormMain_Shown;
            mnsPrincipal.ResumeLayout(false);
            mnsPrincipal.PerformLayout();
            tlMain.ResumeLayout(false);
            tlMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mnsPrincipal;
        private ToolStripMenuItem mnuArchivo;
        private ToolStripMenuItem mnuSalir;
        private TableLayoutPanel tlMain;
        private Button btnLocalidadLista;
        private Button btnVehiculoLista;
        private Label lbMain;
    }
}
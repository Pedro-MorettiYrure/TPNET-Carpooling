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
            btnVehiculoLista = new Button();
            btnLocalidadLista = new Button();
            label1 = new Label();
            btnConvertirAConductor = new Button();
            tlMain.SuspendLayout();
            SuspendLayout();
            // 
            // tlMain
            // 
            tlMain.ColumnCount = 3;
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlMain.ColumnStyles.Add(new ColumnStyle());
            tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlMain.Controls.Add(lbMain, 1, 0);
            tlMain.Controls.Add(btnVehiculoLista, 1, 3);
            tlMain.Controls.Add(btnLocalidadLista, 1, 2);
            tlMain.Controls.Add(label1, 1, 1);
            tlMain.Controls.Add(btnConvertirAConductor, 1, 4);
            tlMain.Dock = DockStyle.Fill;
            tlMain.Location = new Point(0, 0);
            tlMain.Name = "tlMain";
            tlMain.RowCount = 5;
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlMain.Size = new Size(766, 374);
            tlMain.TabIndex = 4;
            // 
            // lbMain
            // 
            lbMain.Anchor = AnchorStyles.None;
            lbMain.AutoSize = true;
            lbMain.FlatStyle = FlatStyle.Popup;
            lbMain.Font = new Font("Papyrus", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbMain.ForeColor = SystemColors.HotTrack;
            lbMain.ImageAlign = ContentAlignment.TopCenter;
            lbMain.Location = new Point(206, 0);
            lbMain.Name = "lbMain";
            lbMain.RightToLeft = RightToLeft.No;
            lbMain.Size = new Size(353, 74);
            lbMain.TabIndex = 2;
            lbMain.Text = "Menu principal";
            // 
            // btnVehiculoLista
            // 
            btnVehiculoLista.Anchor = AnchorStyles.Top;
            btnVehiculoLista.ForeColor = SystemColors.ControlText;
            btnVehiculoLista.Location = new Point(317, 225);
            btnVehiculoLista.Name = "btnVehiculoLista";
            btnVehiculoLista.Size = new Size(131, 47);
            btnVehiculoLista.TabIndex = 1;
            btnVehiculoLista.Text = "Vehiculos";
            btnVehiculoLista.UseVisualStyleBackColor = true;
            btnVehiculoLista.Click += btnVehiculoLista_Click;
            // 
            // btnLocalidadLista
            // 
            btnLocalidadLista.Anchor = AnchorStyles.Top;
            btnLocalidadLista.Location = new Point(317, 151);
            btnLocalidadLista.Name = "btnLocalidadLista";
            btnLocalidadLista.Size = new Size(131, 47);
            btnLocalidadLista.TabIndex = 0;
            btnLocalidadLista.Text = "Localidades";
            btnLocalidadLista.UseVisualStyleBackColor = true;
            btnLocalidadLista.Click += btnLocalidadLista_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Popup;
            label1.Font = new Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(206, 74);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(353, 23);
            label1.TabIndex = 3;
            label1.Text = "Seleccione una opcion:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnConvertirAConductor
            // 
            btnConvertirAConductor.Anchor = AnchorStyles.Top;
            btnConvertirAConductor.Location = new Point(317, 299);
            btnConvertirAConductor.Name = "btnConvertirAConductor";
            btnConvertirAConductor.Size = new Size(131, 26);
            btnConvertirAConductor.TabIndex = 1;
            btnConvertirAConductor.Text = "Quiero ser conductor";
            btnConvertirAConductor.UseVisualStyleBackColor = true;
            btnConvertirAConductor.Click += btnConvertirAConductor_Click;
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
    }
}
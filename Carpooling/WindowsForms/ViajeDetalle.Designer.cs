namespace WindowsForms
{
    partial class ViajeDetalle
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
            components = new System.ComponentModel.Container();
            dtpHora = new DateTimePicker();
            labelFechaHora = new Label();
            cbOrigen = new ComboBox();
            labelOrigen = new Label();
            labelDestino = new Label();
            cbDestino = new ComboBox();
            labelCantLugares = new Label();
            tbCantLugares = new TextBox();
            labelPrecio = new Label();
            tbPrecio = new TextBox();
            labelComentarios = new Label();
            tbComentario = new TextBox();
            btnConfirmar = new Button();
            labelVehiculo = new Label();
            cbVehiculos = new ComboBox();
            dtpFecha = new DateTimePicker();
            errorProviderViajeDetalle = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProviderViajeDetalle).BeginInit();
            SuspendLayout();
            // 
            // dtpHora
            // 
            dtpHora.CustomFormat = "HH:mm";
            dtpHora.Format = DateTimePickerFormat.Custom;
            dtpHora.Location = new Point(436, 75);
            dtpHora.Name = "dtpHora";
            dtpHora.ShowUpDown = true;
            dtpHora.Size = new Size(84, 23);
            dtpHora.TabIndex = 2;
            // 
            // labelFechaHora
            // 
            labelFechaHora.Location = new Point(128, 37);
            labelFechaHora.Name = "labelFechaHora";
            labelFechaHora.Size = new Size(94, 35);
            labelFechaHora.TabIndex = 3;
            labelFechaHora.Text = "Elije la fecha y hora de partida:";
            // 
            // cbOrigen
            // 
            cbOrigen.FormattingEnabled = true;
            cbOrigen.Location = new Point(320, 125);
            cbOrigen.Name = "cbOrigen";
            cbOrigen.Size = new Size(200, 23);
            cbOrigen.TabIndex = 4;
            // 
            // labelOrigen
            // 
            labelOrigen.AutoSize = true;
            labelOrigen.Location = new Point(128, 128);
            labelOrigen.Name = "labelOrigen";
            labelOrigen.Size = new Size(46, 15);
            labelOrigen.TabIndex = 5;
            labelOrigen.Text = "Origen:";
            // 
            // labelDestino
            // 
            labelDestino.AutoSize = true;
            labelDestino.Location = new Point(128, 189);
            labelDestino.Name = "labelDestino";
            labelDestino.Size = new Size(50, 15);
            labelDestino.TabIndex = 6;
            labelDestino.Text = "Destino:";
            // 
            // cbDestino
            // 
            cbDestino.FormattingEnabled = true;
            cbDestino.Location = new Point(320, 186);
            cbDestino.Name = "cbDestino";
            cbDestino.Size = new Size(200, 23);
            cbDestino.TabIndex = 7;
            // 
            // labelCantLugares
            // 
            labelCantLugares.AutoSize = true;
            labelCantLugares.Location = new Point(128, 302);
            labelCantLugares.Name = "labelCantLugares";
            labelCantLugares.Size = new Size(152, 15);
            labelCantLugares.TabIndex = 8;
            labelCantLugares.Text = "Cantidad de Lugares Libres:";
            // 
            // tbCantLugares
            // 
            tbCantLugares.Location = new Point(323, 299);
            tbCantLugares.Name = "tbCantLugares";
            tbCantLugares.Size = new Size(80, 23);
            tbCantLugares.TabIndex = 9;
            // 
            // labelPrecio
            // 
            labelPrecio.AutoSize = true;
            labelPrecio.Location = new Point(128, 344);
            labelPrecio.Name = "labelPrecio";
            labelPrecio.Size = new Size(43, 15);
            labelPrecio.TabIndex = 10;
            labelPrecio.Text = "Precio:";
            // 
            // tbPrecio
            // 
            tbPrecio.Location = new Point(323, 341);
            tbPrecio.Name = "tbPrecio";
            tbPrecio.Size = new Size(80, 23);
            tbPrecio.TabIndex = 11;
            // 
            // labelComentarios
            // 
            labelComentarios.AutoSize = true;
            labelComentarios.Location = new Point(128, 384);
            labelComentarios.Name = "labelComentarios";
            labelComentarios.Size = new Size(161, 15);
            labelComentarios.TabIndex = 12;
            labelComentarios.Text = "Comentarios extra (opcional)";
            // 
            // tbComentario
            // 
            tbComentario.Location = new Point(133, 415);
            tbComentario.Name = "tbComentario";
            tbComentario.Size = new Size(392, 23);
            tbComentario.TabIndex = 13;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Location = new Point(450, 466);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(75, 23);
            btnConfirmar.TabIndex = 14;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // labelVehiculo
            // 
            labelVehiculo.AutoSize = true;
            labelVehiculo.Location = new Point(128, 249);
            labelVehiculo.Name = "labelVehiculo";
            labelVehiculo.Size = new Size(55, 15);
            labelVehiculo.TabIndex = 15;
            labelVehiculo.Text = "Vehiculo:";
            // 
            // cbVehiculos
            // 
            cbVehiculos.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVehiculos.FormattingEnabled = true;
            cbVehiculos.Location = new Point(323, 245);
            cbVehiculos.Name = "cbVehiculos";
            cbVehiculos.Size = new Size(197, 23);
            cbVehiculos.TabIndex = 16;
            cbVehiculos.SelectedIndexChanged += cbVehiculos_SelectedIndexChanged;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(320, 37);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(200, 23);
            dtpFecha.TabIndex = 17;
            // 
            // errorProviderViajeDetalle
            // 
            errorProviderViajeDetalle.ContainerControl = this;
            // 
            // ViajeDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 504);
            Controls.Add(dtpFecha);
            Controls.Add(cbVehiculos);
            Controls.Add(labelVehiculo);
            Controls.Add(btnConfirmar);
            Controls.Add(tbComentario);
            Controls.Add(labelComentarios);
            Controls.Add(tbPrecio);
            Controls.Add(labelPrecio);
            Controls.Add(tbCantLugares);
            Controls.Add(labelCantLugares);
            Controls.Add(cbDestino);
            Controls.Add(labelDestino);
            Controls.Add(labelOrigen);
            Controls.Add(cbOrigen);
            Controls.Add(labelFechaHora);
            Controls.Add(dtpHora);
            Name = "ViajeDetalle";
            Text = "ViajeDetalle";
            ((System.ComponentModel.ISupportInitialize)errorProviderViajeDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DateTimePicker dtpHora;
        private Label labelFechaHora;
        private ComboBox cbOrigen;
        private Label labelOrigen;
        private Label labelDestino;
        private ComboBox cbDestino;
        private Label labelCantLugares;
        private TextBox tbCantLugares;
        private Label labelPrecio;
        private TextBox tbPrecio;
        private Label labelComentarios;
        private TextBox tbComentario;
        private Button btnConfirmar;
        private Label labelVehiculo;
        private ComboBox cbVehiculos;
        private DateTimePicker dtpFecha;
        private ErrorProvider errorProviderViajeDetalle;
    }
}
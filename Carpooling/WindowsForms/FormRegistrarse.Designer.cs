namespace WindowsForms
{
    partial class FormRegistrarse
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
            lblTitulo = new Label();
            labelEmail = new Label();
            labelContra = new Label();
            btnCrearUsuario = new Button();
            txtEmail = new TextBox();
            txtBoxContra = new TextBox();
            labelConfirmarContra = new Label();
            txtBoxConfirmaCon = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtBoxNombre = new TextBox();
            txtBoxApellido = new TextBox();
            txtBoxTele = new TextBox();
            labelTele = new Label();
            labelConductor = new Label();
            labelLicencia = new Label();
            labelVencimiento = new Label();
            textBoxLicencia = new TextBox();
            dateTimePickerVencimiento = new DateTimePicker();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(211, 40);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(259, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Registrate como pasajero\r\nAl ingresar podras solicitar para ser conductor";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Location = new Point(343, 109);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(94, 15);
            labelEmail.TabIndex = 1;
            labelEmail.Text = "Ingresa tu email:";
            // 
            // labelContra
            // 
            labelContra.AutoSize = true;
            labelContra.Location = new Point(314, 147);
            labelContra.Name = "labelContra";
            labelContra.Size = new Size(123, 15);
            labelContra.TabIndex = 2;
            labelContra.Text = "Ingresa tu contraseña:";
            // 
            // btnCrearUsuario
            // 
            btnCrearUsuario.Location = new Point(519, 307);
            btnCrearUsuario.Name = "btnCrearUsuario";
            btnCrearUsuario.Size = new Size(99, 32);
            btnCrearUsuario.TabIndex = 12;
            btnCrearUsuario.Text = "Confirmar";
            btnCrearUsuario.UseVisualStyleBackColor = true;
            btnCrearUsuario.Click += btnCrearUsuario_Click;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(443, 101);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(128, 23);
            txtEmail.TabIndex = 4;
            // 
            // txtBoxContra
            // 
            txtBoxContra.Location = new Point(443, 144);
            txtBoxContra.Name = "txtBoxContra";
            txtBoxContra.PasswordChar = '*';
            txtBoxContra.Size = new Size(128, 23);
            txtBoxContra.TabIndex = 5;
            // 
            // labelConfirmarContra
            // 
            labelConfirmarContra.AutoSize = true;
            labelConfirmarContra.Location = new Point(316, 189);
            labelConfirmarContra.Name = "labelConfirmarContra";
            labelConfirmarContra.Size = new Size(121, 15);
            labelConfirmarContra.TabIndex = 6;
            labelConfirmarContra.Text = "Confirma contraseña:";
            // 
            // txtBoxConfirmaCon
            // 
            txtBoxConfirmaCon.Location = new Point(443, 186);
            txtBoxConfirmaCon.Name = "txtBoxConfirmaCon";
            txtBoxConfirmaCon.PasswordChar = '*';
            txtBoxConfirmaCon.Size = new Size(128, 23);
            txtBoxConfirmaCon.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 109);
            label2.Name = "label2";
            label2.Size = new Size(127, 15);
            label2.TabIndex = 8;
            label2.Text = "Ingresa tu/s nombre/s:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(49, 144);
            label3.Name = "label3";
            label3.Size = new Size(107, 15);
            label3.TabIndex = 9;
            label3.Text = "Ingresa tu apellido:";
            // 
            // txtBoxNombre
            // 
            txtBoxNombre.Location = new Point(162, 106);
            txtBoxNombre.Name = "txtBoxNombre";
            txtBoxNombre.Size = new Size(128, 23);
            txtBoxNombre.TabIndex = 1;
            // 
            // txtBoxApellido
            // 
            txtBoxApellido.Location = new Point(162, 141);
            txtBoxApellido.Name = "txtBoxApellido";
            txtBoxApellido.Size = new Size(128, 23);
            txtBoxApellido.TabIndex = 2;
            // 
            // txtBoxTele
            // 
            txtBoxTele.Location = new Point(162, 186);
            txtBoxTele.Name = "txtBoxTele";
            txtBoxTele.Size = new Size(128, 23);
            txtBoxTele.TabIndex = 3;
            // 
            // labelTele
            // 
            labelTele.AutoSize = true;
            labelTele.Location = new Point(42, 194);
            labelTele.Name = "labelTele";
            labelTele.RightToLeft = RightToLeft.No;
            labelTele.Size = new Size(109, 15);
            labelTele.TabIndex = 13;
            labelTele.Text = "Ingresa tu teléfono:";
            labelTele.TextAlign = ContentAlignment.MiddleRight;
            labelTele.Click += label4_Click;
            // 
            // labelConductor
            // 
            labelConductor.AutoSize = true;
            labelConductor.Location = new Point(89, 243);
            labelConductor.Name = "labelConductor";
            labelConductor.Size = new Size(67, 15);
            labelConductor.TabIndex = 14;
            labelConductor.Text = "Conductor:";
            // 
            // labelLicencia
            // 
            labelLicencia.AutoSize = true;
            labelLicencia.Location = new Point(134, 271);
            labelLicencia.Name = "labelLicencia";
            labelLicencia.Size = new Size(76, 15);
            labelLicencia.TabIndex = 15;
            labelLicencia.Text = "Nro Licencia:";
            // 
            // labelVencimiento
            // 
            labelVencimiento.AutoSize = true;
            labelVencimiento.Location = new Point(100, 296);
            labelVencimiento.Name = "labelVencimiento";
            labelVencimiento.Size = new Size(110, 15);
            labelVencimiento.TabIndex = 16;
            labelVencimiento.Text = "Fecha Vencimiento:";
            // 
            // textBoxLicencia
            // 
            textBoxLicencia.Location = new Point(216, 263);
            textBoxLicencia.Name = "textBoxLicencia";
            textBoxLicencia.Size = new Size(100, 23);
            textBoxLicencia.TabIndex = 10;
            // 
            // dateTimePickerVencimiento
            // 
            dateTimePickerVencimiento.Format = DateTimePickerFormat.Short;
            dateTimePickerVencimiento.Location = new Point(216, 296);
            dateTimePickerVencimiento.Name = "dateTimePickerVencimiento";
            dateTimePickerVencimiento.Size = new Size(200, 23);
            dateTimePickerVencimiento.TabIndex = 11;
            // 
            // FormRegistrarse
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 372);
            Controls.Add(dateTimePickerVencimiento);
            Controls.Add(textBoxLicencia);
            Controls.Add(labelVencimiento);
            Controls.Add(labelLicencia);
            Controls.Add(labelConductor);
            Controls.Add(labelTele);
            Controls.Add(txtBoxTele);
            Controls.Add(txtBoxApellido);
            Controls.Add(txtBoxNombre);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtBoxConfirmaCon);
            Controls.Add(labelConfirmarContra);
            Controls.Add(txtBoxContra);
            Controls.Add(txtEmail);
            Controls.Add(btnCrearUsuario);
            Controls.Add(labelContra);
            Controls.Add(labelEmail);
            Controls.Add(lblTitulo);
            Name = "FormRegistrarse";
            Text = "FormRegistarse";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label labelEmail;
        private Label labelContra;
        private Button btnCrearUsuario;
        private TextBox txtEmail;
        private TextBox txtBoxContra;
        private Label labelConfirmarContra;
        private TextBox txtBoxConfirmaCon;
        private Label label2;
        private Label label3;
        private TextBox txtBoxNombre;
        private TextBox txtBoxApellido;
        private TextBox txtBoxTele;
        private Label labelTele;
        private Label labelConductor;
        private Label labelLicencia;
        private Label labelVencimiento;
        private TextBox textBoxLicencia;
        private DateTimePicker dateTimePickerVencimiento;
    }
}
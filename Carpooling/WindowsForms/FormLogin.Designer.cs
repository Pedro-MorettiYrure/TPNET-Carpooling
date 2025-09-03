namespace WindowsForms
{
    partial class FormLogin
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lnkOlvidaPass = new LinkLabel();
            btnIngresar = new Button();
            txtEmail = new TextBox();
            txtPass = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(140, 46);
            label1.Name = "label1";
            label1.Size = new Size(240, 30);
            label1.TabIndex = 0;
            label1.Text = "¡Bienvenido a Carpooling!\r\nPor favor ingrese su información de Ingreso.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(84, 133);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 1;
            label2.Text = "Email:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(84, 171);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 2;
            label3.Text = "Contraseña:";
            // 
            // lnkOlvidaPass
            // 
            lnkOlvidaPass.AutoSize = true;
            lnkOlvidaPass.Location = new Point(84, 227);
            lnkOlvidaPass.Name = "lnkOlvidaPass";
            lnkOlvidaPass.Size = new Size(119, 15);
            lnkOlvidaPass.TabIndex = 3;
            lnkOlvidaPass.TabStop = true;
            lnkOlvidaPass.Text = "Olvidé mi contraseña";
            lnkOlvidaPass.Click += lnkOlvidaPass_Click;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(370, 219);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(75, 23);
            btnIngresar.TabIndex = 4;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(177, 130);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(268, 23);
            txtEmail.TabIndex = 5;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(177, 168);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.Size = new Size(268, 23);
            txtPass.TabIndex = 6;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 373);
            Controls.Add(txtPass);
            Controls.Add(txtEmail);
            Controls.Add(btnIngresar);
            Controls.Add(lnkOlvidaPass);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormLogin";
            Text = "Ingreso";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private LinkLabel lnkOlvidaPass;
        private Button btnIngresar;
        private TextBox txtEmail;
        private TextBox txtPass;
    }
}
namespace WindowsForms
{
    partial class FormCalificar
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
            lblInstruccion = new Label();
            label2 = new Label();
            numValoracion = new NumericUpDown();
            label3 = new Label();
            txtComentarioCalificacion = new TextBox();
            btnEnviar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)numValoracion).BeginInit();
            SuspendLayout();
            // 
            // lblInstruccion
            // 
            lblInstruccion.AutoSize = true;
            lblInstruccion.Location = new Point(175, 46);
            lblInstruccion.Name = "lblInstruccion";
            lblInstruccion.Size = new Size(46, 15);
            lblInstruccion.TabIndex = 0;
            lblInstruccion.Text = "Calificá";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(92, 117);
            label2.Name = "label2";
            label2.Size = new Size(100, 15);
            label2.TabIndex = 1;
            label2.Text = "Ingrese el puntaje";
            // 
            // numValoracion
            // 
            numValoracion.Location = new Point(219, 115);
            numValoracion.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numValoracion.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numValoracion.Name = "numValoracion";
            numValoracion.Size = new Size(44, 23);
            numValoracion.TabIndex = 2;
            numValoracion.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 161);
            label3.Name = "label3";
            label3.Size = new Size(183, 15);
            label3.TabIndex = 3;
            label3.Text = "Ingrese un comentario (opcional)";
            // 
            // txtComentarioCalificacion
            // 
            txtComentarioCalificacion.Location = new Point(210, 158);
            txtComentarioCalificacion.Multiline = true;
            txtComentarioCalificacion.Name = "txtComentarioCalificacion";
            txtComentarioCalificacion.Size = new Size(192, 60);
            txtComentarioCalificacion.TabIndex = 4;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(365, 263);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(95, 36);
            btnEnviar.TabIndex = 5;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(284, 276);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FormCalificar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(483, 311);
            Controls.Add(btnCancelar);
            Controls.Add(btnEnviar);
            Controls.Add(txtComentarioCalificacion);
            Controls.Add(label3);
            Controls.Add(numValoracion);
            Controls.Add(label2);
            Controls.Add(lblInstruccion);
            Name = "FormCalificar";
            Text = "FormCalificar";
            ((System.ComponentModel.ISupportInitialize)numValoracion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblInstruccion;
        private Label label2;
        private NumericUpDown numValoracion;
        private Label label3;
        private TextBox txtComentarioCalificacion;
        private Button btnEnviar;
        private Button btnCancelar;
    }
}
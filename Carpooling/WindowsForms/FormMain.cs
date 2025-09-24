using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {}

        private void lbMain_Click(object sender, EventArgs e)
        {}

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            iniciarLogin();
        }

        private void btnRegistrar_Click_1(object sender, EventArgs e)
        {
            FormRegistrarse formRegistarse = new FormRegistrarse();
            if(formRegistarse.ShowDialog()== DialogResult.OK)
            {
                iniciarLogin();
            }
        }

        private void iniciarLogin()
        {
            
            FormLogin formLogin = new FormLogin();
            formLogin.ShowDialog();
        }
    }
}

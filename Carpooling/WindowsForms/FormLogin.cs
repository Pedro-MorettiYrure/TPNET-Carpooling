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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            //la propiedad Text de los TextBox contiene el texto escrito en ellos
            if (this.txtEmail.Text == "admin@gmail.com" && this.txtPass.Text == "admin")
            {
                MessageBox.Show("Usted ha ingresado al sistema correctamente."
                    , "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Usuario y/o contraseña incorrectos", "Login"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*Cuando complete la persistencia y acceso a la fuente de datos, reemplace el código del punto #3 para lograr una autenticación sobre los datos existentes persistidos.
            El punto #4 puede reemplazar por el uso de un servicio que envíe un mail con algún mecanismo que permita recuperar/resetear la clave. 
            Nota: puede tomar de referencia lo planteado en por el sitio web oficial de Microsoft https://learn.microsoft.com/es-es/dotnet/api/system.net.mail.smtpclient?view=net-7.0 
            */
        }

        private void lnkOlvidaPass_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Es Ud. un usuario muy descuidado, haga memoria",
            "Olvidé mi contraseña",
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
    }
}

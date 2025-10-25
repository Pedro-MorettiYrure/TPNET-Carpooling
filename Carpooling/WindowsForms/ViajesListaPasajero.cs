using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using API.Clients;
using static WindowsForms.ViajeDetalle;

namespace WindowsForms
{
    public partial class ViajesListaPasajero : Form
    {
        private readonly UsuarioDTO _usuario;
        public ViajesListaPasajero(UsuarioDTO usuario)
        {
            InitializeComponent();
            _usuario = usuario;
        }
        private void ViajesListaPasajero_Load(object sender, EventArgs e)
        {
            //this.GetAllAndLoad();
        }
    }
}

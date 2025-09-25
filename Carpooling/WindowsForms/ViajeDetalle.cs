using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;
using DTOs;

namespace WindowsForms
{
    public partial class ViajeDetalle : Form
    {
        private readonly UsuarioDTO _usuarioLogueado;
        private FormMode mode;

        public ViajeDetalle(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;
            mode = FormMode.Add;                //terminar de agregar modo
        }

        public enum FormMode
        {
            Add,
            Update
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            var fechaHora = dtpFechaHora.Value;
            var origen = cbOrigen.SelectedIndex; //index o item?
            var destino = cbDestino.SelectedItem;
            var cantLugares = int.Parse(tbCantLugares.Text);
            var precio = decimal.Parse(tbPrecio.Text);
            var comentario = tbComentario.Text;

            var dto = new ViajeDTO
            {
                FechaHora = fechaHora,
                OrigenCodPostal = origen,
                DestinoCodPostal = destino, //id o object?
                CantLugares = cantLugares,
                Precio = precio,
                Comentario = comentario,
                IdConductor = _usuarioLogueado.IdUsuario,
            };

            if (mode == FormMode.Update)
            {
                await ViajeApiClient.UpdateAsync(dto);
            }
            else 
            {
                await ViajeApiClient.AddAsync(dto); 
            }

            try
            {
                await ViajeApiClient.AddAsync(dto);
            }
            catch (Exception ex) 
            {
                
            }


        }
    }
}

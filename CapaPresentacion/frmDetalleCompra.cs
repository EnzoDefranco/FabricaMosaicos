using CapaEntidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDetalleCompra : Form
    {
        private Compra oCompra;

        public frmDetalleCompra(Compra oCompra)
        {
            InitializeComponent();
            this.oCompra = oCompra; // Asignar la variable oCompra
        }

        private void frmDetalleCompra_Load(object sender, EventArgs e)
        {
            txtNumeroDocumento.Text = oCompra.numeroDocumento;

            txtFecha.Text  = oCompra.fechaRegistro;
            txtTipoDocumento.Text = oCompra.tipoDocumento;

            txtRazonSocial.Text = oCompra.oProveedor.razonSocial;
            txtTelefono.Text = oCompra.oProveedor.telefono;
            txtDocumento.Text = oCompra.oProveedor.documento;

            TXTTOT.Text = oCompra.montoTotal.ToString();


            dt.Rows.Clear();
            foreach (DetalleCompra detalle in oCompra.oDetalleCompra)
            {
                dt.Rows.Add(new object[] { detalle.oMaterial.nombre, detalle.precioCompra,detalle.cantidad, detalle.montoTotal });
            }


        }

        private void panel1_Enter(object sender, EventArgs e)
        {

        }
    }
}
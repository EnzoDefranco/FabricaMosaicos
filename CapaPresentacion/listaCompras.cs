using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class listaCompras : Form
    {
        public listaCompras()
        {
            InitializeComponent();
        }

        private void listaCompras_Load(object sender, EventArgs e)
        {
            //Cargar la lista de compras
            List<Compra> listaCompras = new CN_Compra().Listar();
            foreach (Compra compra in listaCompras)
            {
                dt.Rows.Add(new object[] { "", compra.id, compra.oProveedor.documento, compra.numeroDocumento, compra.tipoDocumento, compra.oProveedor.razonSocial, compra.oProveedor.telefono, compra.montoTotal, compra.fechaRegistro });
            }

        }

        private void dt_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)

                return;
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.check.Width;
                var h = Properties.Resources.check.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private int selectedRowIndex = -1;

        private void dt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dt.Columns[e.ColumnIndex].Name == "btnSeleccionar") // Si la columna seleccionada es igual a "btnSeleccionar"
            {
                int indice = e.RowIndex; // Se obtiene el índice de la fila seleccionada
                if (indice >= 0) // Si el índice es mayor o igual a 0
                {
                    // Despintar la fila anteriormente seleccionada
                    if (selectedRowIndex >= 0 && selectedRowIndex < dt.Rows.Count)
                    {
                        dt.Rows[selectedRowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White; // Se asigna un color blanco al fondo de la fila seleccionada
                    }

                    // Actualizar los controles del formulario con los valores de la fila seleccionada
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dt.Rows[indice].Cells["idCompra"].Value.ToString();
                    txtNumeroDocumento.Text = dt.Rows[indice].Cells["numeroDocumento"].Value.ToString();
                    txtRazonSocial.Text = dt.Rows[indice].Cells["razonSocial"].Value.ToString();
                    txtTelefono.Text = dt.Rows[indice].Cells["telefono"].Value.ToString();
                    txtFechaRegistro.Text = dt.Rows[indice].Cells["fechaRegistro"].Value.ToString();


                    // Pintar la fila seleccionada
                    dt.Rows[indice].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow; // Se asigna un color amarillo al fondo de la fila seleccionada

                    selectedRowIndex = indice;
                }

                

            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //abrir frmCompras.cs pero que no sea modal
            frmCompras frm = new frmCompras();
            frm.ShowDialog();

        }

        //public Compra ObtenerCompra(string numero)
        //{
        //    Compra oCompra = objcd_compra.ObtenerCompra(numero);
        //    if (oCompra.id != 0)
        //    {
        //        List<DetalleCompra> oDetalleCompra = objcd_compra.ObtenerDetalleCompra(oCompra.id);
        //        oCompra.oDetalleCompra = oDetalleCompra;
        //    }
        //    return oCompra;
        //}

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Compra oCompra = new CN_Compra().ObtenerCompra(txtNumeroDocumento.Text);
            if (oCompra.id != 0)
            {
                frmDetalleCompra frm = new frmDetalleCompra(oCompra);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No se encontró la compra seleccionada.");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                string searchText = textBox2.Text.ToLower(); // Convert the search text to lowercase for case-insensitive search
                foreach (DataGridViewRow row in dt.Rows)
                {
                    bool found = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText))
                        {
                            found = true;
                            break;
                        }
                    }
                    row.Visible = found; // Set the visibility of the row based on whether the search text was found in any cell
                }
            }
        }
    }
}

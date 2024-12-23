﻿using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using Microsoft.Win32;
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
using System.Windows.Media.Media3D;

namespace CapaPresentacion
{
    public partial class listaCompras : Form
    {
        private decimal totalCompras = 0;
        List<Proveedor> listaProveedor = new CN_Proveedor().Listar();

        public listaCompras()
        {
            InitializeComponent();
        }

        private void listaCompras_Load(object sender, EventArgs e)
        {
            //Cargar la lista de compras
            CargarReporteCompra();
            CargarListaCompras();

            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"
            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Presupuesto", Texto = "Presupuesto" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"

            cbTipoDocumento.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbTipoDocumento.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbTipoDocumento.SelectedIndex = -1; // Se selecciona el primer item del ComboBox

            // Obtener la lista de proveedores
            listaProveedor = new CN_Proveedor().Listar();
            cbRazonSocial.DataSource = listaProveedor;
            cbRazonSocial.DisplayMember = "razonSocial";
            cbRazonSocial.ValueMember = "razonSocial";
            cbRazonSocial.SelectedIndex = -1;

        }

        private void CargarListaCompras()
        {
   
           (List<Compra> listaCompras, decimal totalMonto) = new CN_Compra().Listar();

            // ordenar la lista de compras por fecha de registro descendente
            listaCompras = listaCompras.OrderByDescending(x => x.fechaRegistro).ToList();

            dt.Rows.Clear();
            foreach (Compra compra in listaCompras)
            {
                dt.Rows.Add(new object[] { "", compra.id, compra.oProveedor.documento, compra.numeroDocumento, compra.tipoDocumento, compra.oProveedor.razonSocial, compra.oProveedor.telefono, compra.montoTotal, compra.fechaRegistro });
            }
            totalCompras = totalMonto;
            lblTotalVentas.Text = $"Total Compras: {totalMonto:C}";
        }
        private void CargarReporteCompra()
        {

            List<ReporteCompra> lstReporteCompra = new CN_ReporteCompra().ObtenerReporteCompra();

            //// Asume que dt es tu DataTable asociado con el DataGridView
            //dataGridViewMateriales.Rows.Clear();

            // Llena el DataTable con los resultados
            foreach (ReporteCompra reporte in lstReporteCompra)
            {
                dataGridViewMateriales.Rows.Add(new object[] { reporte.nombreMaterial, reporte.totalCantidad });
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

                    // Seleccionar el estado correspondiente en el ComboBox
                    //foreach (OpcionCombo item in cbEstado.Items) // Por cada objeto OpcionCombo en el control cbEstado
                    //{
                    //    if (item.Valor.ToString() == dt.Rows[indice].Cells["estadoValor"].Value.ToString()) // Si el valor de la propiedad Valor del objeto item es igual al valor de la celda estado de la fila seleccionada
                    //    {
                    //        cbEstado.SelectedItem = item;
                    //        break;
                    //    }
                    //}
                    foreach (OpcionCombo item in cbTipoDocumento.Items)
                    {
                        if (item.Valor.ToString() == dt.Rows[indice].Cells["tipoDocumento"].Value.ToString())
                        {
                            cbTipoDocumento.SelectedItem = item;
                            break;
                        }
                    }


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
            dataGridViewMateriales.Rows.Clear();
            CargarListaCompras();
            CargarReporteCompra();

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

        private void btnFiltrar_Click(object sender, EventArgs e)
        {

            // Asume que dt es tu DataTable asociado con el DataGridView
            dt.Rows.Clear();
            dataGridViewMateriales.Rows.Clear();





            CompraFiltro filtro = new CompraFiltro
            {
                FechaInicio = dtpFechaInicio.Value.Date,
                FechaFin = dtpFechaFin.Value.Date,
                RazonSocial = cbRazonSocial.SelectedIndex != -1 ? cbRazonSocial.SelectedValue.ToString() : string.Empty,
                filtrarPorBoleta = CbxFiltrarPorBoleta.Checked,
                filtrarPorPresupuesto = CbxFiltrarPorPresupuesto.Checked,
                filtrarPorFactura = CbxFiltrarPorFactura.Checked
            };

            //Obtener la lista de compras y su monto total
            (List<Compra> listaCompras, decimal totalMonto) = new CN_Compra().Listar(filtro);
            foreach (Compra compra in listaCompras)
            {
                dt.Rows.Add(new object[] { "", compra.id, compra.oProveedor.documento, compra.numeroDocumento, compra.tipoDocumento, compra.oProveedor.razonSocial, compra.oProveedor.telefono, compra.montoTotal, compra.fechaRegistro });

            }
            //Mostrar el total de compras
            lblTotalVentas.Text = $"Total Compras: {totalMonto:C}";
            // Llama al método ObtenerReporteCompra con razón social nula
            List<ReporteCompra> lstReporteCompra = new CN_ReporteCompra().ObtenerReporteCompra(filtro);


            // Llena el DataTable con los resultados
            foreach (ReporteCompra reporte in lstReporteCompra)
            {
                dataGridViewMateriales.Rows.Add(new object[] { reporte.nombreMaterial, reporte.totalCantidad });
            }



        }

        private void btnLim_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            dataGridViewMateriales.Rows.Clear();
            CargarListaCompras();
            CargarReporteCompra();
            CbxFiltrarPorBoleta.Checked = false;
            CbxFiltrarPorPresupuesto.Checked = false;
            CbxFiltrarPorFactura.Checked = false;
            cbRazonSocial.SelectedIndex = -1;
            dtpFechaInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Está seguro de eliminar el registro?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Quiero utilizar los dos metodos, eliminarDetalleCompra y eliminarCompra
                    bool resultado = new CN_Compra().EliminarDetalleCompra(Convert.ToInt32(txtId.Text));
                    if (resultado) {
                        bool resultado2 = new CN_Compra().EliminarCompra(Convert.ToInt32(txtId.Text));
                        if (resultado2)
                        {
                            dt.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                            dataGridViewMateriales.Rows.Clear();
                            CargarReporteCompra();
                            decimal totalCompras = new CN_Compra().CalcularTotalCompras();
                            lblTotalVentas.Text = $"Total Compras: {totalCompras:C}";
                            MessageBox.Show("Compra eliminada con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar los detalles de la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un registro", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                Compra oCompra = new Compra();
                oCompra.id = Convert.ToInt32(txtId.Text);
                oCompra.tipoDocumento = ((OpcionCombo)cbTipoDocumento.SelectedItem).Valor.ToString();
                oCompra.fechaRegistro = DateTime.Parse(txtFechaRegistro.Text).ToString("yyyy-MM-dd");


                //public bool Editar(Compra obj, out string Mensaje) // Método que devuelve una lista de objetos Usuario
                bool resultado = new CN_Compra().Editar(oCompra, out string mensaje);

                // Se actualizan los datos del material en el DataGridView indicando el nombre de la columna y el valor a actualizar
                //dt.Rows.Add(new object[] { "", compra.id, compra.oProveedor.documento, compra.numeroDocumento, compra.tipoDocumento, compra.oProveedor.razonSocial, compra.oProveedor.telefono, compra.montoTotal, compra.fechaRegistro });
                if (resultado)
                {
                    dt.Rows[Convert.ToInt32(txtIndice.Text)].Cells["idCompra"].Value = txtId.Text;
                    dt.Rows[Convert.ToInt32(txtIndice.Text)].Cells["tipoDocumento"].Value = ((OpcionCombo)cbTipoDocumento.SelectedItem).Valor.ToString();
                        dt.Rows[Convert.ToInt32(txtIndice.Text)].Cells["fechaRegistro"].Value = txtFechaRegistro.Text;
                    dataGridViewMateriales.Rows.Clear();
                    CargarReporteCompra();

                    decimal totalCompras = new CN_Compra().CalcularTotalCompras();
                    lblTotalVentas.Text = $"Total Compras: {totalCompras:C}";

                    { MessageBox.Show("Compra editada con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Por favor seleccione un registro", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTotalVentas_Click(object sender, EventArgs e)
        {

        }
    }
}

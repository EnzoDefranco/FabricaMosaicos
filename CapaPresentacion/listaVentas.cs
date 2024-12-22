using CapaEntidad;
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
    public partial class listaVentas : Form
    {
        private decimal totalVentas = 0; // Almacena el total real de las ventas
        private bool mostrarTotal = false; // Indica si el total se muestra o está oculto con ***
        List<Cliente> listaCliente = new CN_Cliente().Listar();

        public listaVentas()
        {
            InitializeComponent();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmVentas frm = new frmVentas();
            frm.ShowDialog();
            dataGridViewMateriales.Rows.Clear();
            CargarListaVentas();
            CargarReporteVenta();
        }

        private void txtNumeroDocumento_TextChanged(object sender, EventArgs e)
        {


        }

        private void listaVentas_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpFechaFin.Value = DateTime.Now; // Fecha de fin por defecto como la fecha actual
            CargarListaVentas();
            CargarReporteVenta();


            // Inicializar ComboBox para el estado
            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"
            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Presupuesto", Texto = "Presupuesto" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"


            cbTipoDocumento.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbTipoDocumento.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbTipoDocumento.SelectedIndex = -1; // Se selecciona el primer item del ComboBox

            // Inicializar ComboBox para el pago
            cbPago.Items.Add(new OpcionCombo() { Valor = "Pago", Texto = "Pago" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbPago.Items.Add(new OpcionCombo() { Valor = "Parcialmente pago", Texto = "Parcialmente pago" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"
            cbPago.Items.Add(new OpcionCombo() { Valor = "No Pago", Texto = "No Pago" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"


            cbPago.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbPago.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbPago.SelectedIndex = -1; // Se selecciona el primer item del ComboBox

            // Inicializar ComboBox para el cumplimiento
            cbCumplimiento.Items.Add(new OpcionCombo() { Valor = "Entregado", Texto = "Entregado" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbCumplimiento.Items.Add(new OpcionCombo() { Valor = "Parcialmente entregado", Texto = "Parcialmente entregado" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"
            cbCumplimiento.Items.Add(new OpcionCombo() { Valor = "No entregado", Texto = "No entregado" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"

            cbCumplimiento.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbCumplimiento.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbCumplimiento.SelectedIndex = -1; // Se selecciona el primer item del ComboBox. 0 es de enviado y 1 es de parcialmente enviado y 2 es de no enviado

            cbfPago.Items.Add(new OpcionCombo() { Valor = "Pago", Texto = "Pago" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbfPago.Items.Add(new OpcionCombo() { Valor = "Parcialmente pago", Texto = "Parcialmente pago" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"
            cbfPago.Items.Add(new OpcionCombo() { Valor = "No Pago", Texto = "No Pago" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"

            cbfPago.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbfPago.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbfPago.SelectedIndex = -1; // Se selecciona el primer item del ComboBox

            cbfCumplimiento.Items.Add(new OpcionCombo() { Valor = "Entregado", Texto = "Entregado" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbfCumplimiento.Items.Add(new OpcionCombo() { Valor = "Parcialmente entregado", Texto = "Parcialmente entregado" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"
            cbfCumplimiento.Items.Add(new OpcionCombo() { Valor = "No entregado", Texto = "No entregado" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"
            cbfCumplimiento.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbfCumplimiento.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbfCumplimiento.SelectedIndex = -1; // Se selecciona el primer item del ComboBox. 0 es de enviado y 1 es de parcialmente enviado y 2 es de no enviado


            listaCliente = new CN_Cliente().Listar();

            cbFiltroRazonSocial.DataSource = listaCliente;
            cbFiltroRazonSocial.DisplayMember = "nombreCompleto";
            cbFiltroRazonSocial.ValueMember = "nombreCompleto";
            cbFiltroRazonSocial.SelectedIndex = -1;

        }

        private void CargarListaVentas()
        {
            // Llamar al método Listar y obtener la lista de ventas y el total
            (List<Venta> listaVentas, decimal totalMonto) = new CN_Venta().Listar();

            // Ordenar la lista de ventas por fecha de registro (convertida a DateTime)
            listaVentas = listaVentas.OrderByDescending(v => DateTime.Parse(v.fechaRegistro)).ToList();


            dt.Rows.Clear();
            foreach (Venta venta in listaVentas)
            {
                dt.Rows.Add(new object[] { "", venta.id, venta.oCliente.documento, venta.numeroDocumento, venta.tipoDocumento,
                                   venta.oCliente.nombreCompleto, venta.oCliente.telefono, venta.oCliente.direccion,
                                   venta.pago, venta.cumplimiento, venta.montoTotal, venta.infoAdicional,
                                   venta.fechaRegistro });
            }
            totalVentas = totalMonto;
            // Guardar el total real y mostrarlo oculto con ***
            lblTotalVentas.Text = $"Total Ventas: {totalMonto:C}";


        }


        private void CargarReporteVenta()
        {
            List<ReporteVenta> lstReporteVenta = new CN_ReporteVenta().ObtenerReporteVenta();


            foreach (ReporteVenta reporte in lstReporteVenta)
            {
                dataGridViewMateriales.Rows.Add(new object[] { reporte.Producto, reporte.CantidadTotal });
            }
        }

        private void dt_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;


            // Lógica para dibujar el ícono de verificación en la primera columna
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
                    txtId.Text = dt.Rows[indice].Cells["idVenta"].Value.ToString();
                    txtNumeroDocumento.Text = dt.Rows[indice].Cells["numeroDocumento"].Value.ToString();
                    txtRazonSocial.Text = dt.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtTelefono.Text = dt.Rows[indice].Cells["telefono"].Value.ToString();
                    txtFechaRegistro.Text = dt.Rows[indice].Cells["fechaRegistro"].Value.ToString();
                    txtDireccion.Text = dt.Rows[indice].Cells["direccion"].Value.ToString();
                    txtInfoAdicional.Text = dt.Rows[indice].Cells["infoAdicional"].Value.ToString();



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

                    foreach (OpcionCombo item in cbPago.Items)
                    {
                        if (item.Valor.ToString() == dt.Rows[indice].Cells["pago"].Value.ToString())
                        {
                            cbPago.SelectedItem = item;
                            break;
                        }
                    }

                    foreach (OpcionCombo item in cbCumplimiento.Items)
                    {
                        if (item.Valor.ToString() == dt.Rows[indice].Cells["cumplimiento"].Value.ToString())
                        {
                            cbCumplimiento.SelectedItem = item;
                            break;
                        }
                    }


                    // Pintar la fila seleccionada
                    //dt.Rows[indice].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow; // Se asigna un color amarillo al fondo de la fila seleccionada

                    selectedRowIndex = indice;
                }



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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Está seguro de eliminar el registro?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Quiero utilizar los dos metodos, eliminarDetalleCompra y eliminarCompra
                    bool resultado = new CN_Venta().EliminarDetalleVenta(Convert.ToInt32(txtId.Text));
                    if (resultado)
                    {
                        bool resultado2 = new CN_Venta().EliminarVenta(Convert.ToInt32(txtId.Text));
                        if (resultado2)
                        {
                            dt.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                            dataGridViewMateriales.Rows.Clear();
                            CargarReporteVenta();
                            // Calcular y actualizar el total de ventas en el label
                            decimal totalMonto = new CN_Venta().CalcularTotalVentas();
                            lblTotalVentas.Text = $"Total Ventas: {totalMonto:C}";
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

        private void Limpiar()
        {
            txtId.Text = "0";
            txtNumeroDocumento.Text = "";
            txtRazonSocial.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtInfoAdicional.Text = "";
            txtFechaRegistro.Text = "";
            cbTipoDocumento.SelectedIndex = -1;
            cbPago.SelectedIndex = -1;
            cbCumplimiento.SelectedIndex = -1;
            cbFiltroRazonSocial.SelectedIndex = -1;


        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            // Limpia las filas del DataTable y del DataGridView
            dt.Rows.Clear();
            dataGridViewMateriales.Rows.Clear();

            // Construcción del filtro
            VentaFiltro filtro = new VentaFiltro
            {
                FechaInicio = dtpFechaInicio.Value.Date,
                FechaFin = dtpFechaFin.Value.Date,
                nombreCompleto = cbFiltroRazonSocial.SelectedIndex != -1 ? cbFiltroRazonSocial.SelectedValue.ToString() : string.Empty,
                filtrarPorBoleta = CbxFiltrarPorBoleta.Checked,
                filtrarPorPresupuesto = CbxFiltrarPorPresupuesto.Checked,
                filtrarPorFactura = CbxFiltrarPorFactura.Checked,
                filtrarPorPago = cbfPago.SelectedIndex != -1 ? ((OpcionCombo)cbfPago.SelectedItem).Texto : string.Empty,
                filtrarPorCumplimiento = cbfCumplimiento.SelectedIndex != -1 ? ((OpcionCombo)cbfCumplimiento.SelectedItem).Texto : string.Empty,
                filtrarPorEmpresa = CbxFiltrarPorEmpresa.Checked,
                filtrarPorParticular = CbxFiltrarPorParticular.Checked,
                filtrarPorFinalizado = chkFiltrarPorFinalizado.Checked
            };

            // Obtener la lista de ventas y el total de montos
            (List<Venta> listaVentas, decimal totalMonto) = new CN_Venta().Listar(filtro);

            // Llenar el DataTable con la lista de ventas
            foreach (Venta venta in listaVentas)
            {
                dt.Rows.Add(new object[]
                {
            "", venta.id, venta.oCliente.documento, venta.numeroDocumento, venta.tipoDocumento,
            venta.oCliente.nombreCompleto, venta.oCliente.telefono, venta.oCliente.direccion,
            venta.pago, venta.cumplimiento, venta.montoTotal, venta.infoAdicional,
            venta.fechaRegistro
                });
            }

            // Mostrar el total directamente desde el resultado de la consulta
            lblTotalVentas.Text = $"Total Ventas: {totalMonto:C}";

            // Llama al método ObtenerReporteVenta con el filtro
            List<ReporteVenta> lstReporteVenta = new CN_ReporteVenta().ObtenerReporteVenta(filtro);

            // Llenar el DataGridView con los reportes de venta
            foreach (ReporteVenta reporte in lstReporteVenta)
            {
                dataGridViewMateriales.Rows.Add(new object[] { reporte.Producto, reporte.CantidadTotal });
            }
        }




        private void btnLim_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            dataGridViewMateriales.Rows.Clear();
            CargarListaVentas();
            CargarReporteVenta();
            cbfPago.SelectedIndex = -1;
            cbfCumplimiento.SelectedIndex = -1;
            CbxFiltrarPorBoleta.Checked = false;
            CbxFiltrarPorFactura.Checked = false;
            CbxFiltrarPorPresupuesto.Checked = false;
            CbxFiltrarPorEmpresa.Checked = false;
            CbxFiltrarPorParticular.Checked = false;
            txtRazonSocial.Text = "";
            dtpFechaInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpFechaFin.Value = DateTime.Now;

            Limpiar();

        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {

            Venta oVenta = new CN_Venta().ObtenerVenta(txtNumeroDocumento.Text);
            if (oVenta.id != 0)
            {
                frmDetalleVenta frm = new frmDetalleVenta(oVenta);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No se encontró la venta seleccionada.");
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                Venta oVenta = new Venta();
                oVenta.id = Convert.ToInt32(txtId.Text);
                oVenta.tipoDocumento = ((OpcionCombo)cbTipoDocumento.SelectedItem).Valor.ToString();
                oVenta.fechaRegistro = DateTime.Parse(txtFechaRegistro.Text).ToString("yyyy-MM-dd");
                oVenta.pago = ((OpcionCombo)cbPago.SelectedItem).Valor.ToString();
                oVenta.cumplimiento = ((OpcionCombo)cbCumplimiento.SelectedItem).Valor.ToString();
                oVenta.infoAdicional = txtInfoAdicional.Text;



                //public bool Editar(Compra obj, out string Mensaje) // Método que devuelve una lista de objetos Usuario
                bool resultado = new CN_Venta().Editar(oVenta, out string mensaje);

                // Se actualizan los datos del material en el DataGridView indicando el nombre de la columna y el valor a actualizar
                //dt.Rows.Add(new object[] { "", compra.id, compra.oProveedor.documento, compra.numeroDocumento, compra.tipoDocumento, compra.oProveedor.razonSocial, compra.oProveedor.telefono, compra.montoTotal, compra.fechaRegistro });
                if (resultado)
                {
                    dt.Rows[Convert.ToInt32(txtIndice.Text)].Cells["idVenta"].Value = txtId.Text;
                    dt.Rows[Convert.ToInt32(txtIndice.Text)].Cells["tipoDocumento"].Value = ((OpcionCombo)cbTipoDocumento.SelectedItem).Valor.ToString();
                    dt.Rows[Convert.ToInt32(txtIndice.Text)].Cells["fechaRegistro"].Value = txtFechaRegistro.Text;
                    dt.Rows[Convert.ToInt32(txtIndice.Text)].Cells["pago"].Value = ((OpcionCombo)cbPago.SelectedItem).Valor.ToString();
                    dt.Rows[Convert.ToInt32(txtIndice.Text)].Cells["cumplimiento"].Value = ((OpcionCombo)cbCumplimiento.SelectedItem).Valor.ToString();
                    dt.Rows[Convert.ToInt32(txtIndice.Text)].Cells["infoAdicional"].Value = txtInfoAdicional.Text;
                    dataGridViewMateriales.Rows.Clear();
                    CargarReporteVenta();

                    // Calcular y actualizar el total de ventas en el label
                    decimal totalMonto = new CN_Venta().CalcularTotalVentas();
                    lblTotalVentas.Text = $"Total Ventas: {totalMonto:C}";


                    { MessageBox.Show("Venta editada con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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

        private void detalle2_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalVentas_Click(object sender, EventArgs e)
        {
            //mostrarTotal = !mostrarTotal; // Alterna entre mostrar u ocultar

            //lblTotalVentas.Text = mostrarTotal
            //    ? $"Total Ventas: {totalVentas:C}" // Muestra el total
            //    : "Total Ventas: ***"; // Oculta el total
        }

        private void dt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //dt.InvalidateRow(e.RowIndex);
            //dt.ClearSelection();
        }

        private void dt_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return; // Evita cabeceras

            // Accede a la DataGridView
            var dgv = sender as DataGridView;

            // Obtén valores de las celdas para verificar la lógica
            var cumplimiento = dgv.Rows[e.RowIndex].Cells["cumplimiento"].Value?.ToString();
            var pago = dgv.Rows[e.RowIndex].Cells["pago"].Value?.ToString();

            bool estaFinalizado =
                cumplimiento?.Equals("entregado", StringComparison.OrdinalIgnoreCase) == true &&
                pago?.Equals("pago", StringComparison.OrdinalIgnoreCase) == true;

            if (estaFinalizado)
            {
                // Fondo verde si está "finalizado"
                e.CellStyle.BackColor = Color.LightGreen;
                // Para que al hacer clic no cambie a otro color:
                e.CellStyle.SelectionBackColor = Color.LightGreen;
                e.CellStyle.SelectionForeColor = Color.Black;
            }
            else
            {
                // Fondo amarillo si no está "finalizado"
                e.CellStyle.BackColor = Color.LightYellow;
                e.CellStyle.SelectionBackColor = Color.LightYellow;
                e.CellStyle.SelectionForeColor = Color.Black;
            }
            dt.DefaultCellStyle.SelectionBackColor = SystemColors.Window;
            dt.DefaultCellStyle.SelectionForeColor = Color.Black;

        }

        private void dt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Recalcular estilos de esa fila inmediatamente
                dt.InvalidateRow(e.RowIndex);
                dt.Refresh();         // fuerza el repintado completo
                dt.ClearSelection();  // opcional, si no quieres que quede resaltada
            }
        }
    }
}

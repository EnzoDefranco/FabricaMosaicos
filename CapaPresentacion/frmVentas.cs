using CapaEntidad;
using System;
using CapaPresentacion.Utilidades;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using System.Windows.Documents;
using Newtonsoft.Json;



namespace CapaPresentacion
{
    public partial class frmVentas : Form
    {
        private ToolTip toolTip;
        private Timer toolTipTimer;

        private Usuario _Usuario;
        // Obtener la lista de materiales
        List<Material> listaMaterial = new CN_Material().Listar();
        // Obtener la lista de clientes
        List<Cliente> listaCliente = new CN_Cliente().Listar();


        public frmVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;

            InitializeComponent();
            InitializeToolTip();
        }
        private void InitializeToolTip()
        {
            toolTip = new ToolTip();
            toolTip.IsBalloon = true;
            toolTip.AutoPopDelay = 500; // Duración del mensaje en milisegundos
            toolTip.InitialDelay = 0;
            toolTip.ReshowDelay = 0;

            toolTipTimer = new Timer();
            toolTipTimer.Interval = 200; // Duración del mensaje en milisegundos
            toolTipTimer.Tick += ToolTipTimer_Tick;

        }
        private void ToolTipTimer_Tick(object sender, EventArgs e)
        {
            toolTip.Hide(this);
            toolTipTimer.Stop();
        }

        private void ShowToolTip(Control control, string message)
        {
            toolTip.Show(message, control, control.Width / 2, control.Height / 2);
            toolTipTimer.Start();
        }


        private void frmVentas_Load(object sender, EventArgs e)
        {


            // Inicializar ComboBox para el estado
            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"
            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Presupuesto", Texto = "Presupuesto" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"


            cbTipoDocumento.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbTipoDocumento.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbTipoDocumento.SelectedIndex = 0; // Se selecciona el primer item del ComboBox

            // Inicializar ComboBox para el pago
            cbPago.Items.Add(new OpcionCombo() { Valor = "Pagado", Texto = "Pagado" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbPago.Items.Add(new OpcionCombo() { Valor = "Parcialmente pago", Texto = "Parcialmente pago" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"
            cbPago.Items.Add(new OpcionCombo() { Valor = "No Pago", Texto = "No Pago" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"


            cbPago.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbPago.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbPago.SelectedIndex = 0; // Se selecciona el primer item del ComboBox

            // Inicializar ComboBox para el cumplimiento
            cbCumplimiento.Items.Add(new OpcionCombo() { Valor = "Entregado", Texto = "Entregado" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbCumplimiento.Items.Add(new OpcionCombo() { Valor = "Parcialmente entregado", Texto = "Parcialmente entregado" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"
            cbCumplimiento.Items.Add(new OpcionCombo() { Valor = "No enviado", Texto = "No enviado" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"

            cbCumplimiento.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbCumplimiento.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbCumplimiento.SelectedIndex = 0; // Se selecciona el primer item del ComboBox. 0 es de enviado y 1 es de parcialmente enviado y 2 es de no enviado


            txtIdCliente.Text = "0";
            txtIdProducto.Text = "0";

            // Obtener la lista de materiales
            listaMaterial = new CN_Material().Listar();

            // Cargar la lista de materiales en el ComboBox
            List<Material> materialesFiltrados = listaMaterial.Where(m => m.tipoMaterial == TipoMaterial.Producto).ToList();

            cbCodProducto.DataSource = materialesFiltrados;
            cbCodProducto.DisplayMember = "nombre";
            cbCodProducto.ValueMember = "nombre";
            cbCodProducto.SelectedIndex = -1;

            listaCliente = new CN_Cliente().Listar();
            cbRazonSocial.DataSource = listaCliente;
            cbRazonSocial.DisplayMember = "nombreCompleto";
            cbRazonSocial.ValueMember = "nombreCompleto";
            cbRazonSocial.SelectedIndex = -1;

            cbRazonSocial.DropDownStyle = ComboBoxStyle.DropDown; // Asegurarse de que el estilo sea DropDown
            cbRazonSocial.AutoCompleteMode = AutoCompleteMode.Suggest; // Configurar el modo de autocompletar
            cbRazonSocial.AutoCompleteSource = AutoCompleteSource.ListItems; //

            // Configurar el ComboBox de Materia Prima para autocompletar
            cbCodProducto.DropDownStyle = ComboBoxStyle.DropDown; // Asegurarse de que el estilo sea DropDown
            cbCodProducto.AutoCompleteMode = AutoCompleteMode.Suggest; // Configurar el modo de autocompletar
            cbCodProducto.AutoCompleteSource = AutoCompleteSource.ListItems; //

        }

        private void panel2_Enter(object sender, EventArgs e)
        {

        }

        private void cbCodProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                Material material = (Material)cbCodProducto.SelectedItem;

                if (material != null)
                {
                    txtNombreProducto.Text = material.nombre;
                    txtIdProducto.Text = material.id.ToString();
                    txtCodMp.Text = material.oCategoria.descripcion.ToString();


                }
                else
                {
                    // Mostrar mensaje de error
                    ShowToolTip(cbCodProducto, "Material no encontrado");
                }

                // Limpiar el ComboBox
                cbCodProducto.SelectedIndex = -1;

                // Enfocar el TextBox de cantidad
                //txtPrecioCompra.Focus();
            }
        }

        private void cbRazonSocial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cliente cliente = (Cliente)cbRazonSocial.SelectedItem;

                if (cliente != null)
                {
                    txtIdCliente.Text = cliente.id.ToString();
                    txtRazonSocial.Text = cliente.nombreCompleto;
                    txtDocumento.Text = cliente.documento;
                    txtDireccion.Text = cliente.direccion;
                    txtTelefono.Text = cliente.telefono;
                    txtCiudad.Text = cliente.ciudad;
                    txtClienteTipo.Text = cliente.clienteTipo;

                    if (txtClienteTipo.Text == "Empresa")
                    {
                        // Seleccionar otras opciones en los ComboBox
                        cbCumplimiento.SelectedIndex = 0; // Ejemplo: seleccionar la segunda opción
                        cbPago.SelectedIndex = 2; // Ejemplo: seleccionar la segunda opción
                        cbTipoDocumento.SelectedIndex = 0
                            ; // Ejemplo: seleccionar la segunda opción
                    }


                }
                else
                {
                    // Mostrar mensaje de error
                    ShowToolTip(cbRazonSocial, "Proveedor no encontrado");
                }

                // Limpiar el ComboBox
                cbRazonSocial.SelectedIndex = -1;

                // Enfocar el ComboBox de material
                //cbCodMaterial.Focus();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal precioVenta = 0;
            bool productoExiste = false;

            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Seleccione un material", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!decimal.TryParse(txtPrecioVenta.Text, out precioVenta))
            {
                MessageBox.Show("Ingrese un precio de venta válido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (!decimal.TryParse(txtPrecioVenta.Text, out precioVenta))
            //{
            //    MessageBox.Show("Ingrese un precio de venta válido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            foreach (DataGridViewRow row in dt.Rows)
            {
                if (row.Cells["idProducto"].Value.ToString() == txtIdProducto.Text)
                {
                    productoExiste = true;
                    break;
                }
            }
            if (!productoExiste)
            {
                dt.Rows.Add(new object[] {
                    txtIdProducto.Text,
                    txtNombreProducto.Text,
                    precioVenta.ToString("0.00"),
                    txtCantidad.Value.ToString(),
                    (precioVenta * txtCantidad.Value).ToString("0.00") //
                });
                calcularTotal();
                limpiar();
                cbCodProducto.Focus();
            }

        }


        private void limpiar()
        {
            txtIdProducto.Text = "0";
            txtCodMp.Text = "";
            txtNombreProducto.Text = "";
            txtPrecioVenta.Text = "";
            txtCantidad.Value = 1;
        }

        private void calcularTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dt.Rows)
            {
                total += decimal.Parse(row.Cells["subTotal"].Value.ToString());
            }

            TXTTOT.Text = total.ToString("N2");
        }

        private void dt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dt.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    dt.Rows.RemoveAt(indice);
                    calcularTotal();
                }

            }
        }

        private void dt_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)

                return;
            if (e.ColumnIndex == 5)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.trash.Width;
                var h = Properties.Resources.trash.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.trash, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecioVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void limpiarFormulario()
        {
            // Limpiar los campos del formulario
            txtIdCliente.Text = "0";
            txtRazonSocial.Text = "";
            txtDocumento.Text = "";
            cbTipoDocumento.SelectedIndex = 0;
            cbCodProducto.SelectedIndex = -1;
            txtNombreProducto.Text = "";
            txtIdProducto.Text = "0";
            txtCodMp.Text = "";
            txtPrecioVenta.Text = "";
            txtCantidad.Value = 1;
            dt.Rows.Clear();
            TXTTOT.Text = "0.00";
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Validar los datos de entrada
            if (Convert.ToInt32(txtIdCliente.Text) == 0)
            {
                MessageBox.Show("Seleccione un cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("Agregue al menos un material", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Crear la cadena JSON para los detalles de la compra
            var detalles = new List<object>();

            foreach (DataGridViewRow row in dt.Rows)
            {
                if (!row.IsNewRow)
                {
                    detalles.Add(new
                    {
                        idMaterial = Convert.ToInt32(row.Cells["idProducto"].Value),
                        precioVenta = Convert.ToDecimal(row.Cells["precioVenta"].Value),
                        cantidad = Convert.ToInt32(row.Cells["cantidad"].Value),
                        montototal = Convert.ToDecimal(row.Cells["subTotal"].Value)
                    });
                }
            }

            string detallesJson = JsonConvert.SerializeObject(detalles);

            // Obtener el correlativo para el número de documento
            int idCorrelativo = new CN_Venta().obtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

            // Crear el objeto Compra
            Venta obj = new Venta()
            {
                oUsuario = new Usuario() { id = 1 },
                oCliente = new Cliente() { id = Convert.ToInt32(txtIdCliente.Text) },
                tipoDocumento = ((OpcionCombo)cbTipoDocumento.SelectedItem).Texto,
                cumplimiento = ((OpcionCombo)cbCumplimiento.SelectedItem).Texto,
                pago = ((OpcionCombo)cbPago.SelectedItem).Texto,
                numeroDocumento = numeroDocumento,
                montoTotal = Convert.ToDecimal(TXTTOT.Text),
                infoAdicional = txtInfoAdicional.Text,
                oDetalleVenta = new List<DetalleVenta>(),
                fechaRegistro = dtp.Value.ToString("yyyy-MM-dd")
            };

            // Mensaje de salida y resultado
            string mensaje = string.Empty;
            bool resultado = new CN_Venta().Registrar(obj, detallesJson, out mensaje);

            if (resultado)
            {
                limpiarFormulario();
                MessageBox.Show("Venta registrada correctamente" + numeroDocumento, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult dialogResult = MessageBox.Show("¿Desea ver el detalle de la venta?", "Detalle de Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Venta oVenta = new CN_Venta().ObtenerVenta(numeroDocumento);
                    frmDetalleVenta frm = new frmDetalleVenta(oVenta);
                    frm.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Error al registrar la venta: " + mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
    
}

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

namespace CapaPresentacion
{
    public partial class frmCompras : Form
    {
        private ToolTip toolTip;
        private Timer toolTipTimer;



        private Usuario _Usuario;
        // Obtener la lista de materiales
        List<Material> listaMaterial = new CN_Material().Listar();
        // Obtener la lista de proveedores
        List<Proveedor> listaProveedor = new CN_Proveedor().Listar();

        // Filtrar por tipo de material

        public frmCompras( Usuario oUsuario = null)
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


        private void frmCompras_Load(object sender, EventArgs e)
        {
            // Inicializar ComboBox para el estado
            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"

            cbTipoDocumento.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbTipoDocumento.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbTipoDocumento.SelectedIndex = 0; // Se selecciona el primer item del ComboBox

            //Seterar fecha
            
            dtp.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtIdProveedor.Text = "0";
            txtidmp.Text = "0";

        

            // Obtener la lista de materiales
            listaMaterial = new CN_Material().Listar();

            // Cargar la lista de materiales en el ComboBox
            List<Material> materialesFiltrados = listaMaterial.Where(m => m.tipoMaterial == TipoMaterial.MateriaPrima).ToList();

            cbCodMaterial.DataSource = materialesFiltrados;
            cbCodMaterial.DisplayMember = "nombre";
            cbCodMaterial.ValueMember = "nombre";
            cbCodMaterial.SelectedIndex = -1;

            // Obtener la lista de proveedores
            listaProveedor = new CN_Proveedor().Listar();
            cbRazonSocial.DataSource = listaProveedor;
            cbRazonSocial.DisplayMember = "razonSocial";
            cbRazonSocial.ValueMember = "razonSocial";
            cbRazonSocial.SelectedIndex = -1;

            cbRazonSocial.DropDownStyle = ComboBoxStyle.DropDown; // Asegurarse de que el estilo sea DropDown
            cbRazonSocial.AutoCompleteMode = AutoCompleteMode.Suggest; // Configurar el modo de autocompletar
            cbRazonSocial.AutoCompleteSource = AutoCompleteSource.ListItems; //



            // Configurar el ComboBox de Materia Prima para autocompletar
            cbCodMaterial.DropDownStyle = ComboBoxStyle.DropDown; // Asegurarse de que el estilo sea DropDown
            cbCodMaterial.AutoCompleteMode = AutoCompleteMode.Suggest; // Configurar el modo de autocompletar
            cbCodMaterial.AutoCompleteSource = AutoCompleteSource.ListItems; // 


            


        }


        private void cbCodMaterial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                Material material = (Material)cbCodMaterial.SelectedItem;

                if (material != null)
                {
                    txtNombreMateriaPrima.Text = material.nombre;
                    txtidmp.Text = material.id.ToString();
                    txtCodMp.Text = material.oCategoria.descripcion.ToString();

               
                }
                else
                {
                    // Mostrar mensaje de error
                    ShowToolTip(cbCodMaterial, "Material no encontrado");
                }

                // Limpiar el ComboBox
                cbCodMaterial.SelectedIndex = -1;

                // Enfocar el TextBox de cantidad
                //txtPrecioCompra.Focus();
            }
        }

        private void cbRazonSocial_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) {
                Proveedor proveedor = (Proveedor)cbRazonSocial.SelectedItem;

                if (proveedor != null)
                {
                    txtIdProveedor.Text = proveedor.id.ToString();
                    txtRazonSocial.Text = proveedor.razonSocial;
                    txtDocumento.Text = proveedor.documento;

       
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
            decimal precioCompra = 0;
            decimal precioVenta = 0;
            bool productoExiste = false;

            if (int.Parse(txtidmp.Text) == 0)
            {
                MessageBox.Show("Seleccione un material", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!decimal.TryParse(txtPrecioCompra.Text, out precioCompra))
            {
                MessageBox.Show("Ingrese un precio de compra válido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (!decimal.TryParse(txtPrecioVenta.Text, out precioVenta))
            //{
            //    MessageBox.Show("Ingrese un precio de venta válido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            foreach(DataGridViewRow row in dt.Rows)
            {
                if (row.Cells["idMateriaPrimadt"].Value.ToString() == txtidmp.Text)
                {
                    productoExiste = true;
                    break;
                }
            }
            if (!productoExiste)
            {
                dt.Rows.Add(new object[] {
                    txtidmp.Text,
                    txtNombreMateriaPrima.Text,
                    precioCompra.ToString("0.00"),
                    precioVenta.ToString("0.00"),
                    txtCantidad.Value.ToString(),
                    (precioCompra * txtCantidad.Value).ToString("0.00") //
                });
                calcularTotal();
                limpiar();
                cbCodMaterial.Focus();
            }

        }

        private void limpiar()
        {
            txtidmp.Text = "0";
            txtCodMp.Text = "";
            txtNombreMateriaPrima.Text = "";
            txtPrecioCompra.Text = "";
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
                if  (indice >= 0)
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
            if (e.ColumnIndex == 6)
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

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
               e.Handled = false;
            }
            else {
                if (txtPrecioCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

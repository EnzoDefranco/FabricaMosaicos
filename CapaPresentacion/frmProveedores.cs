using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
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
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            txtId.Text = "0"; // Se pone en 0 para que al momento de guardar, sepa que es un nuevo registro
            txtRazonSocial.Text = "";
            txtDocumento.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";

        }


        private void frmProveedores_Load(object sender, EventArgs e)
        {
            // Inicializar ComboBox para el estado
            cbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"

            cbEstado.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbEstado.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbEstado.SelectedIndex = 0; // Se selecciona el primer item del ComboBox

            //// Cargar datos de materiales en el DataGridView
            //List<Material> listaMaterial = new CN_Material().Listar(); // Se obtiene la lista de materiales
            //foreach (Material material in listaMaterial) // Por cada material en la lista de materiales
            //{
            //    // Se agregan los datos del material al DataGridView respetando el orden de las columnas 
            //    dt.Rows.Add(new object[] {"",material.id,material.codigo,material.nombre,material.descripcion,material.oCategoria.id,material.oCategoria.descripcion,material.stock,material.precioCompra,material.precioVenta,
            //            material.estado == true ? 1 : 0, // Se muestra 1 si el estado es true, 0 si es false
            //            material.estado == true ? "Activo" : "No Activo", // Se muestra "Activo" si el estado es true, "No Activo" si es false

            //            (int)material.tipoMaterial, // Se muestra el valor del tipo de material
            //            material.tipoMaterial.ToString() // Se muestra el texto del tipo de material
            //    });
            //}

            // Cargar datos de proveedores en el DataGridView
            List<Proveedor> listaProveedor = new CN_Proveedor().Listar(); // Se obtiene la lista de proveedores
            foreach (Proveedor proveedor in listaProveedor) // Por cada proveedor en la lista de proveedores
            {
                // Se agregan los datos del proveedor al DataGridView respetando el orden de las columnas 
                dt.Rows.Add(new object[] { "", proveedor.id, proveedor.razonSocial, proveedor.documento, proveedor.correo, proveedor.telefono,
                    proveedor.estado == true ? 1 : 0, // Se muestra 1 si el estado es true, 0 si es false
                    proveedor.estado == true ? "Activo" : "No Activo" // Se muestra "Activo" si el estado es true, "No Activo" si es false
                });
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty; // Se inicializa la variable mensaje
            if (cbEstado.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione un estado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Proveedor objProveedor = new Proveedor() // Se crea un objeto Proveedor con los datos ingresados en los inputs
            {
                id = Convert.ToInt32(txtId.Text), // Se convierte el id a entero
                razonSocial = txtRazonSocial.Text, // Se obtiene la razon social del input
                documento = txtDocumento.Text, // Se obtiene el documento del input
                correo = txtCorreo.Text, // Se obtiene el correo del input
                telefono = txtTelefono.Text, // Se obtiene el telefono del input
                estado = ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString() == "1" ? true : false
            };
            if (objProveedor.id == 0) // Si el id del proveedor es igual a 0, se registra un nuevo proveedor
            {
                int resultado = new CN_Proveedor().Registrar(objProveedor, out mensaje);
                if (resultado > 0)
                {
                    MessageBox.Show("Proveedor registrado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Se agregan los datos del proveedor al DataGridView respetando el orden de las columnas
                    dt.Rows.Add(new object[] { "", resultado, txtRazonSocial.Text, txtDocumento.Text, txtCorreo.Text, txtTelefono.Text,
                    ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString(),((OpcionCombo)cbEstado.SelectedItem).Texto.ToString()
                });
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); // Se muestra un mensaje de error
                }
            }
            else // Si el id del proveedor es diferente de 0, se edita el proveedor
            {
                bool resultado = new CN_Proveedor().Editar(objProveedor, out mensaje);
                if (resultado)
                {
                    // Se actualizan los datos del proveedor en el DataGridView indicando el nombre de la columna y el valor a actualizar
                    DataGridViewRow row = dt.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["razonSocial"].Value = txtRazonSocial.Text;
                    row.Cells["documento"].Value = txtDocumento.Text;
                    row.Cells["correo"].Value = txtCorreo.Text;
                    row.Cells["telefono"].Value = txtTelefono.Text;
                    row.Cells["estadoValor"].Value = ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionCombo)cbEstado.SelectedItem).Texto.ToString();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }
        private int selectedRowIndex = -1;

        private void dt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dt.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    if (selectedRowIndex >= 0 && selectedRowIndex < dt.Rows.Count)
                    {
                        dt.Rows[selectedRowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    }
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dt.Rows[indice].Cells["idProveedor"].Value.ToString();
                    
                    txtRazonSocial.Text = dt.Rows[indice].Cells["razonSocial"].Value.ToString();
                    txtDocumento.Text = dt.Rows[indice].Cells["documento"].Value.ToString();
                    txtCorreo.Text = dt.Rows[indice].Cells["correo"].Value.ToString();
                    txtTelefono.Text = dt.Rows[indice].Cells["telefono"].Value.ToString();
                    foreach (OpcionCombo item in cbEstado.Items)
                    {
                        if (item.Valor.ToString() == dt.Rows[indice].Cells["estadoValor"].Value.ToString())
                        {
                            cbEstado.SelectedItem = item;
                            break;
                        }
                    }
                    dt.Rows[indice].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                    selectedRowIndex = indice;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Está seguro de eliminar el registro?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    bool resultado = new CN_Proveedor().Eliminar(Convert.ToInt32(txtId.Text), out mensaje);
                    if (resultado)
                    {
                        dt.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        MessageBox.Show("Proveedor eliminado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();

                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un registro", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }


    }
}

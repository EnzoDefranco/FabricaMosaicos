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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            txtId.Text = "0"; // Se pone en 0 para que al momento de guardar, sepa que es un nuevo registro
            txtNombreCompleto.Text = "";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtCiudad.Text = "";
            cbEstado.SelectedIndex = 0;
            cbTipoCliente.SelectedIndex = 0;


        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            // Inicializar ComboBox para el estado
            cbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"

            cbEstado.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbEstado.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbEstado.SelectedIndex = 0; // Se selecciona el primer item del ComboBox

            // Inicializar ComboBox para el tipo de cliente
            cbTipoCliente.Items.Add(new OpcionCombo() { Valor = "Particular" , Texto = "Particular" }); 
            cbTipoCliente.Items.Add(new OpcionCombo() { Valor = "Empresa", Texto = "Empresa" });

            cbTipoCliente.DisplayMember = "Texto";
            cbTipoCliente.ValueMember = "Valor";
            cbTipoCliente.SelectedIndex = 0;

            List <Cliente> listaCliente = new CN_Cliente().Listar(); // Se crea una lista de objetos Cliente y se llama al método Listar de la clase CN_Cliente
            foreach (Cliente cliente in listaCliente) // Se itera sobre cada objeto Cliente de la lista
            {
                // Se agregan los datos del proveedor al DataGridView respetando el orden de las columnas 
                dt.Rows.Add(new object[] { "", cliente.id, cliente.nombreCompleto,cliente.direccion, cliente.telefono, cliente.clienteTipo,cliente.documento, cliente.ciudad,
                    cliente.estado == true ? 1 : 0, // Se muestra 1 si el estado es true, 0 si es false
                    cliente.estado == true ? "Activo" : "No Activo" // Se muestra "Activo" si el estado es true, "No Activo" si es false,
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

            Cliente objCliente = new Cliente() // Se crea un objeto Proveedor con los datos ingresados en los inputs
            {
                id = Convert.ToInt32(txtId.Text), // Se convierte el id a entero
                nombreCompleto = txtNombreCompleto.Text, // Se obtiene el nombre del input
                direccion = txtDireccion.Text, // Se obtiene la direccion del input
                telefono = txtTelefono.Text, // Se obtiene el telefono del input
                clienteTipo = ((OpcionCombo)cbTipoCliente.SelectedItem).Valor.ToString(), // Se obtiene el tipo de cliente del ComboBox
                documento = txtDocumento.Text, // Se obtiene el documento del input
                ciudad = txtCiudad.Text, // Se obtiene la ciudad del input
                estado = ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString() == "1" ? true : false
            };
            if (objCliente.id == 0) // Si el id del proveedor es igual a 0, se registra un nuevo proveedor
            {
                int resultado = new CN_Cliente().Registrar(objCliente, out mensaje);
                if (resultado > 0)
                {
                    MessageBox.Show("Cliente registrado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Se agregan los datos del proveedor al DataGridView respetando el orden de las columnas
                    dt.Rows.Add(new object[] { "", resultado,txtNombreCompleto.Text,txtDireccion.Text, txtTelefono.Text,((OpcionCombo)cbTipoCliente.SelectedItem).Valor,txtDocumento.Text,txtCiudad.Text,
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
                bool resultado = new CN_Cliente().Editar(objCliente, out mensaje);
                if (resultado)
                {
                    // Se actualizan los datos del proveedor en el DataGridView indicando el nombre de la columna y el valor a actualizar
                    DataGridViewRow row = dt.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["nombreCompleto"].Value = txtNombreCompleto.Text;
                    row.Cells["direccion"].Value = txtDireccion.Text;
                    row.Cells["telefono"].Value = txtTelefono.Text;
                    row.Cells["clienteTipo"].Value = ((OpcionCombo)cbTipoCliente.SelectedItem).Valor.ToString();
                    row.Cells["documento"].Value = txtDocumento.Text;
                    row.Cells["ciudad"].Value = txtCiudad.Text;
                    row.Cells["estadoValor"].Value = ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionCombo)cbEstado.SelectedItem).Texto.ToString();
                    MessageBox.Show("Cliente editado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    txtId.Text = dt.Rows[indice].Cells["idCliente"].Value.ToString();
                    txtNombreCompleto.Text = dt.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtDireccion.Text = dt.Rows[indice].Cells["Direccion"].ToString();
                    txtDireccion.Text = dt.Rows[indice].Cells["direccion"].Value.ToString();
                    txtTelefono.Text = dt.Rows[indice].Cells["telefono"].Value.ToString();
                    txtDocumento.Text = dt.Rows[indice].Cells["documento"].Value.ToString();


                    foreach (OpcionCombo item in cbTipoCliente.Items)
                    {
                        if (item.Valor.ToString() == dt.Rows[indice].Cells["clienteTipo"].Value.ToString())
                        {
                            cbTipoCliente.SelectedItem = item;
                            break;
                        }
                    }
            
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


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Está seguro de eliminar el registro?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    bool resultado = new CN_Cliente().Eliminar(Convert.ToInt32(txtId.Text), out mensaje);
                    if (resultado)
                    {
                        dt.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        MessageBox.Show("Cliente eliminado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower(); // Convert the search text to lowercase for case-insensitive search
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

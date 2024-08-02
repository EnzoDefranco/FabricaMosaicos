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
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            // Inicializar ComboBox para el estado
            cbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" }); // Se añade un nuevo objeto de tipo OpcionCombo al control cbEstado, donde se le asigna un valor y un texto
            cbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            cbEstado.DisplayMember = "Texto"; // Se asigna la propiedad Texto del objeto OpcionCombo al control cbEstado
            cbEstado.ValueMember = "Valor"; // Se asigna la propiedad Valor del objeto OpcionCombo al control cbEstado
            cbEstado.SelectedIndex = 0; // Se selecciona el primer elemento del control cbEstado

            // Inicializar ComboBox para el tipo de material
            cbTipoMaterial.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Producto" }); // Se añade un nuevo objeto de tipo OpcionCombo al control cbEstado, donde se le asigna un valor y un texto
            cbTipoMaterial.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Materia Prima" }); // Se añade un nuevo objeto de tipo OpcionCombo al control cbEstado, donde se le asigna un valor y un texto

            cbTipoMaterial.DisplayMember = "Texto"; // Se asigna la propiedad Texto del objeto OpcionCombo al control cbEstado
            cbTipoMaterial.ValueMember = "Valor"; // Se asigna la propiedad Valor del objeto OpcionCombo al control cbEstado
            cbTipoMaterial.SelectedIndex = 0; // Se selecciona el primer elemento del control cbEstado

            List<Categoria> listaCategoria = new CN_Categoria().Listar(); // Se crea una lista de objetos Rol y se le asigna el valor devuelto por el método Listar de la clase CN_Rol
            foreach (Categoria categoria in listaCategoria) // Por cada objeto Rol en la lista listaRol
            {
                dt.Rows.Add(new object[] {"",categoria.id, categoria.descripcion,
                categoria.estado == true ? 1 : 0, // Se añade una nueva fila al control dt, donde se le asigna un arreglo de objetos con los valores de las propiedades del objeto Rol
                categoria.estado == true ? "Activo" : "No Activo", 
                categoria.tipoMaterial == true ? 1 : 0,
                categoria.tipoMaterial == true ? "Producto" : "Materia Prima" // Se añade una nueva fila al control dt, donde se le asigna un arreglo de objetos con los valores de las propiedades del objeto Rol
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
        private int selectedRowIndex = -1;
        private void Limpiar()
        {
            txtId.Text = "0";
            txtDescripcion.Text = "";
            cbEstado.SelectedIndex = 0;
            cbTipoMaterial.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty; // Se inicializa la variable mensaje

            Categoria objCategoria = new Categoria()
            {  // Se crea un objeto de tipo Usuario
                id = Convert.ToInt32(txtId.Text), // Se asigna el valor de la propiedad id del objeto objUsuario
                descripcion = txtDescripcion.Text, // Se asigna el valor de la propiedad documento del objeto objUsuario
                estado = Convert.ToInt32(((OpcionCombo)cbEstado.SelectedItem).Valor) == 1 ? true : false, // Se asigna el valor de la propiedad estado del objeto objUsuario
                tipoMaterial = Convert.ToInt32(((OpcionCombo)cbTipoMaterial.SelectedItem).Valor) == 1 ? true : false // Se asigna el valor de la propiedad estado del objeto objUsuario

            };

            if (objCategoria.id == 0)
            {
                int idCategoriaGenerada = new CN_Categoria().Registrar(objCategoria, out mensaje); // Se crea una variable de tipo entero y se le asigna el valor devuelto por el método Registrar de la clase CN_Usuario

                if (idCategoriaGenerada > 0) // Si el valor de la variable idUsuarioGnerado es mayor a 0
                {
                    MessageBox.Show("Usuario registrado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); // Muestra un mensaje
                    dt.Rows.Add(new object[] {"",idCategoriaGenerada, objCategoria.descripcion,
                ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString(),((OpcionCombo)cbEstado.SelectedItem).Texto.ToString(),
                ((OpcionCombo)cbTipoMaterial.SelectedItem).Valor.ToString(),((OpcionCombo)cbTipoMaterial.SelectedItem).Texto.ToString()
                    
                    
                    });
                    Limpiar(); // Llama al método Limpiar
                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); // Muestra un mensaje
                }

            }
            else
            {
                bool resultado = new CN_Categoria().Editar(objCategoria, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dt.Rows[Convert.ToInt32(txtIndice.Text)]; // Se crea una variable de tipo DataGridViewRow y se le asigna la fila seleccionada
                    row.Cells["idCategoria"].Value = txtId.Text; // Se asigna el valor de la propiedad id del objeto Usuario a la celda idUsuario de la fila seleccionada
                    row.Cells["descripcion"].Value = txtDescripcion.Text; // Se asigna el valor de la propiedad nombreCompleto del objeto Usuario a la celda nombreCompleto de la fila seleccionada
                    row.Cells["estado"].Value = ((OpcionCombo)cbEstado.SelectedItem).Texto.ToString(); // Se asigna el valor de la propiedad estado del objeto Usuario a la celda estado de la fila seleccionada
                    row.Cells["estadoValor"].Value = ((OpcionCombo)cbEstado.SelectedItem).Valor; // Se asigna el valor de la propiedad estado del objeto Usuario a la celda estado de la fila seleccionada
                    row.Cells["tipoMaterial"].Value = ((OpcionCombo)cbTipoMaterial.SelectedItem).Texto.ToString(); // Se asigna el valor de la propiedad estado del objeto Usuario a la celda estado de la fila seleccionada
                    row.Cells["tipoMaterialValor"].Value = ((OpcionCombo)cbTipoMaterial.SelectedItem).Valor; // Se asigna el valor de la propiedad estado del objeto Usuario a la celda estado de la fila seleccionada
                    MessageBox.Show("Usuario actualizado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); // Muestra un mensaje

                    Limpiar(); // Llama al método Limpiar

                }
                else
                {
                    MessageBox.Show(mensaje);
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
                    bool resultado = new CN_Categoria().Eliminar(Convert.ToInt32(txtId.Text), out mensaje);
                    if (resultado)
                    {
                        dt.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        MessageBox.Show("Usuario eliminado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar(); // Llama al método Limpiar

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
                    txtId.Text = dt.Rows[indice].Cells["idCategoria"].Value.ToString();
                    txtDescripcion.Text = dt.Rows[indice].Cells["descripcion"].Value.ToString();



                    // Seleccionar el estado correspondiente en el ComboBox
                    foreach (OpcionCombo item in cbEstado.Items) // Por cada objeto OpcionCombo en el control cbEstado
                    {
                        if (item.Valor.ToString() == dt.Rows[indice].Cells["estadoValor"].Value.ToString()) // Si el valor de la propiedad Valor del objeto item es igual al valor de la celda estado de la fila seleccionada
                        {
                            cbEstado.SelectedItem = item;
                            break;
                        }
                    }

                    foreach(OpcionCombo item in cbTipoMaterial.Items)
                    {
                        if (item.Valor.ToString() == dt.Rows[indice].Cells["tipoMaterialValor"].Value.ToString())
                        {
                            cbTipoMaterial.SelectedItem = item;
                            break;
                        }
                    }

                    // Pintar la fila seleccionada
                    dt.Rows[indice].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow; // Se asigna un color amarillo al fondo de la fila seleccionada

                    selectedRowIndex = indice;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

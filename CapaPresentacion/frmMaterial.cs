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
    public partial class frmMaterial : Form
    {
        public frmMaterial()
        {
            InitializeComponent();
            cbTipoMaterial.SelectedIndexChanged += cbTipoMaterial_SelectedIndexChanged;
        }

        private void Limpiar()
        {
            txtId.Text = "0";
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            cbCategoria.SelectedIndex = -1;
            cbEstado.SelectedIndex = 0;
            cbTipoMaterial.SelectedIndex = 0;
        }
        private void frmMaterial_Load(object sender, EventArgs e)
        {
            // Inicializar ComboBox para el estado
            cbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            cbEstado.DisplayMember = "Texto";
            cbEstado.ValueMember = "Valor";
            cbEstado.SelectedIndex = 0;

            // Inicializar ComboBox para el tipo de material
            cbTipoMaterial.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Producto" });
            cbTipoMaterial.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Materia Prima" });

            cbTipoMaterial.DisplayMember = "Texto";
            cbTipoMaterial.ValueMember = "Valor";

            // Cargar datos de categorías
            List<Categoria> listaCategoria = new CN_Categoria().Listar();
            cbCategoria.DataSource = listaCategoria;
            cbCategoria.DisplayMember = "descripcion";
            cbCategoria.ValueMember = "id";
            cbCategoria.SelectedIndex = -1;

            
          


            // Cargar datos de materiales
            List<Material> listaMaterial = new CN_Material().Listar();
            foreach (Material material in listaMaterial)
            {
                dt.Rows.Add(new object[] {"",material.id,material.codigo,material.nombre,material.descripcion,material.oCategoria.id,material.oCategoria.descripcion,material.stock,material.precioCompra,material.precioVenta,
                        material.estado == true ? 1 : 0,
                        material.estado == true ? "Activo" : "No Activo",
                        material.tipoMaterial == true ? 1 : 0,
                        material.tipoMaterial == true ? "Producto" : "Materia Prima"
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

            Material objMaterial = new Material()
            {  // Se crea un objeto de tipo Usuario
                id = Convert.ToInt32(txtId.Text), // Se asigna el valor de la propiedad id del control txtId al objeto Usuario
                codigo = txtCodigo.Text,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text, // Se asigna el valor de la propiedad nombreCompleto del control txtNombreCompleto al objeto Usuario
                oCategoria = new Categoria() { id = Convert.ToInt32(cbCategoria.SelectedValue) },
                estado = ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString() == "1" ? true : false, // Se asigna el valor de la propiedad estado del control cbEstado al objeto Usuario
                tipoMaterial = ((OpcionCombo)cbTipoMaterial.SelectedItem).Valor.ToString() == "1" ? true : false // Se asigna el valor de la propiedad tipoMaterial del control cbTipoMaterial al objeto Usuario


            };

            if (objMaterial.id == 0)
            {
                int resultado = new CN_Material().Registrar(objMaterial, out mensaje); // Se crea una variable de tipo entero y se le asigna el valor devuelto por el método Registrar de la clase CN_Usuario

                if (resultado > 0) // Si el valor de la variable idUsuarioGnerado es mayor a 0
                {
                    MessageBox.Show("Usuario registrado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); // Muestra un mensaje
                    dt.Rows.Add(new object[] {"", resultado, txtCodigo.Text, txtNombre.Text, txtDescripcion.Text, cbCategoria.SelectedValue, cbCategoria.Text, 0, 0, 0,
                    ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString(),((OpcionCombo)cbEstado.SelectedItem).Texto.ToString(),
                    ((OpcionCombo)cbTipoMaterial.SelectedItem).Valor.ToString(),((OpcionCombo)cbTipoMaterial.SelectedItem).Texto.ToString()


                        });
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); // Muestra un mensaje
                }

            }
            else
            {
                bool resultado = new CN_Material().Editar(objMaterial, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dt.Rows[Convert.ToInt32(txtIndice.Text)]; // Se crea una variable de tipo DataGridViewRow y se le asigna la fila seleccionada
                    row.Cells["codigo"].Value = txtCodigo.Text; // Se asigna el valor del control txtCodigo a la celda código de la fila seleccionada
                    row.Cells["nombre"].Value = txtNombre.Text; // Se asigna el valor del control txtNombre a la celda nombre de la fila seleccionada
                    row.Cells["descripcion"].Value = txtDescripcion.Text; // Se asigna el valor del control txtDescripcion a la celda descripción de la fila seleccionada
                    row.Cells["idCategoria"].Value = cbCategoria.SelectedValue; // Se asigna el valor del control cbCategoria a la celda idCategoria de la fila seleccionada
                    row.Cells["categoria"].Value = cbCategoria.Text; // Se asigna el valor del control cbCategoria a la celda categoria de la fila seleccionada
                    row.Cells["estadoValor"].Value = ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString(); // Se asigna el valor del control cbEstado a la celda estado de la fila seleccionada
                    row.Cells["estado"].Value = ((OpcionCombo)cbEstado.SelectedItem).Texto.ToString(); // Se asigna el valor del control cbEstado a la celda estadoTexto de la fila seleccionada
                    row.Cells["tipoMaterialValor"].Value = ((OpcionCombo)cbTipoMaterial.SelectedItem).Valor.ToString(); // Se asigna el valor del control cbTipoMaterial a la celda tipoMaterial de la fila seleccionada
                    row.Cells["tipoMaterial"].Value = ((OpcionCombo)cbTipoMaterial.SelectedItem).Texto.ToString(); // Se asigna el valor del control cbTipoMaterial a la celda tipoMaterialTexto de la fila seleccionada

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
                    txtId.Text = dt.Rows[indice].Cells["idMaterial"].Value.ToString();
                    txtCodigo.Text = dt.Rows[indice].Cells["codigo"].Value.ToString();
                    txtNombre.Text = dt.Rows[indice].Cells["nombre"].Value.ToString();
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

                    foreach (OpcionCombo item in cbTipoMaterial.Items)
                    {
                        if (item.Valor.ToString() == dt.Rows[indice].Cells["tipoMaterialValor"].Value.ToString())
                        {
                            cbTipoMaterial.SelectedItem = item;
                            break;
                        }
                    }
                    // Seleccionar la categoría correspondiente en el ComboBox
                    foreach (Categoria item in cbCategoria.Items)
                    {
                        if (item.id.ToString() == dt.Rows[indice].Cells["idCategoria"].Value.ToString())
                        {
                            cbCategoria.SelectedItem = item;
                            break;
                        }
                    }





                    // Pintar la fila seleccionada
                    dt.Rows[indice].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow; // Se asigna un color amarillo al fondo de la fila seleccionada

                    selectedRowIndex = indice;
                }
            }
        }

        private void cbTipoMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tipoMaterialSeleccionado = ((OpcionCombo)cbTipoMaterial.SelectedItem).Valor.ToString() == "1" ? 1 : 0;

            // Filtrar la lista de categorías
            List<Categoria> listaCategoriasFiltradas = new CN_Categoria().Listar()
                .Where(c => c.tipoMaterial == (tipoMaterialSeleccionado == 1)) // Convertir tipoMaterialSeleccionado a bool
                .ToList();

            // Actualizar el DataSource del ComboBox cbCategoria
            cbCategoria.DataSource = listaCategoriasFiltradas;
            cbCategoria.DisplayMember = "descripcion";
            cbCategoria.ValueMember = "id";

        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Está seguro de eliminar el registro?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    bool resultado = new CN_Material().Eliminar(Convert.ToInt32(txtId.Text), out mensaje);
                    if (resultado)
                    {
                        dt.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        MessageBox.Show("Usuario eliminado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // Asegúrate de vincular el evento al ComboBox cbTipoMaterial en el constructor del formulario

    }
}
//private void cbTipoMaterial_SelectedIndexChanged(object sender, EventArgs e)
//{

//    else
//    {
//        // Si el tipoMaterial seleccionado no es igual a 1, mostrar todas las categorías
//        cbCategoria.DataSource = listaCategoria;
//    }

//    cbCategoria.DisplayMember = "descripcion";
//    cbCategoria.ValueMember = "id";
//    cbCategoria.SelectedIndex = 0;
//}

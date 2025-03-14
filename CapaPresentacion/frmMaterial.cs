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
    public partial class frmMaterial : Form
    {
        public frmMaterial()
        {
            InitializeComponent();
            cbTipoMaterial.SelectedIndexChanged += cbTipoMaterial_SelectedIndexChanged; //Al cambiar el valor del tipo de material, cambio los valores del desplegable de categorías.
            //Se carga al inicializar el form ya que se necesita que esté listo apenas se abre el mismo.

        }

        //Función para limpiar los inputs y deseleccionar las filas de la tabla
        private void Limpiar()
        {
            txtId.Text = "0"; // Se pone en 0 para que al momento de guardar, sepa que es un nuevo registro
            txtCodigo.Text = string.Empty; // Se vacia el input de código
            txtNombre.Text = string.Empty; // Se vacia el input de nombre
            txtDescripcion.Text = string.Empty; // Se vacia el input de descripción
            cbCategoria.SelectedIndex = -1; //Se pone en -1, para que al principio este sin ningun dato, y dependiendo de cbTipoMaterial, se carguen las categorias pertenecientes al mismo 
            cbEstado.SelectedIndex = 0; // Se pone en 0 para que ya aparezca cargado el estado 'Activo'
            cbTipoMaterial.SelectedIndex = -1; // Se pone en -1, para que al principio este sin ningun dato

        }

        // Se inicializan algunos valores al cargar el formulario, que dependen de que el formulario esté listo para recibir datos
        private void frmMaterial_Load(object sender, EventArgs e)
        {
            // Inicializar ComboBox para el estado
            cbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Activo"
            cbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "No Activo"

            cbEstado.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbEstado.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbEstado.SelectedIndex = 0; // Se selecciona el primer item del ComboBox

            // Inicializar ComboBox para el tipo de material
            cbTipoMaterial.Items.Add(new OpcionCombo() { Valor = (int)TipoMaterial.Producto, Texto = "Producto" }); // Se agrega un nuevo item al ComboBox, con el valor 1 y el texto "Producto"
            cbTipoMaterial.Items.Add(new OpcionCombo() { Valor = (int)TipoMaterial.MateriaPrima, Texto = "Materia Prima" }); // Se agrega un nuevo item al ComboBox, con el valor 0 y el texto "Materia Prima"


            cbTipoMaterial.DisplayMember = "Texto"; // Se muestra el texto en el ComboBox
            cbTipoMaterial.ValueMember = "Valor"; // Se guarda el valor en el ComboBox
            cbTipoMaterial.SelectedIndex = -1; // No se selecciona ningún item del ComboBox

            // Cargar datos de categorías
            List<Categoria> listaCategoria = new CN_Categoria().Listar(); // Se obtiene la lista de categorías
            cbCategoria.DataSource = listaCategoria; // Se asigna la lista de categorías al ComboBox
            cbCategoria.DisplayMember = "descripcion"; // Se muestra la descripción en el ComboBox
            cbCategoria.ValueMember = "id"; // Se guarda el id en el ComboBox
            cbCategoria.SelectedIndex = -1; // No se selecciona ningún item del ComboBox

            
            // Cargar datos de materiales en el DataGridView
            List<Material> listaMaterial = new CN_Material().Listar(); // Se obtiene la lista de materiales
            foreach (Material material in listaMaterial) // Por cada material en la lista de materiales
            {
                // Se agregan los datos del material al DataGridView respetando el orden de las columnas 
                dt.Rows.Add(new object[] {"",material.id,material.codigo,material.nombre,material.descripcion,material.oCategoria.id,material.oCategoria.descripcion,material.stock,material.precioCompra,material.precioVenta,
                        material.estado == true ? 1 : 0, // Se muestra 1 si el estado es true, 0 si es false
                        material.estado == true ? "Activo" : "No Activo", // Se muestra "Activo" si el estado es true, "No Activo" si es false

                        (int)material.tipoMaterial, // Se muestra el valor del tipo de material
                        material.tipoMaterial.ToString() // Se muestra el texto del tipo de material
                });
            }

        }

        // CODIGO PREDETERMINADO PARA AGREAGR ICONO DE CHECK A LA COLUMNA
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




        //Cuando se hace clic en el botón guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty; // Se inicializa la variable mensaje

            // Verificar si se ha seleccionado un tipo de material
            if (cbTipoMaterial.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione un tipo de material", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si se ha seleccionado una categoría
            if (cbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione una categoría", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Material objMaterial = new Material() // Se crea un objeto Material con los datos ingresados en los inputs
            {
                id = Convert.ToInt32(txtId.Text), // Se convierte el id a entero
                codigo = txtCodigo.Text, // Se obtiene el código del input
                nombre = txtNombre.Text, // Se obtiene el nombre del input
                descripcion = txtDescripcion.Text, // Se obtiene la descripción del input
                oCategoria = new Categoria() { id = Convert.ToInt32(cbCategoria.SelectedValue) }, // Crea una instancia de la clase Categoria con el id seleccionado en el ComboBox. Con el cbCategoria.SelectedValue se obtiene el id de la categoría seleccionada y se convierte a entero
                estado = ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString() == "1" ? true : false,
                tipoMaterial = (TipoMaterial)((OpcionCombo)cbTipoMaterial.SelectedItem).Valor // Asigna el valor del tipo de material seleccionado en el ComboBox a la propiedad tipoMaterial del objeto Material. Con el cbTipoMaterial.SelectedItem se obtiene el item seleccionado en el ComboBox, se convierte a OpcionCombo y se obtiene el valor del item seleccionado
                /*Casting y Acceso a Propiedad: Se hace un casting del SelectedItem a OpcionCombo para acceder a su propiedad Valor. Luego, se convierte este valor a TipoMaterial(un enumerador).*/
                //2.Uso de SelectedValue vs SelectedItem:
                //•	SelectedValue: Directamente obtiene el valor del elemento seleccionado en el ComboBox. Este valor se convierte a un entero.
                //•	SelectedItem: Obtiene el objeto completo seleccionado en el ComboBox. Luego, se hace un casting a OpcionCombo para acceder a su propiedad Valor.

            };

            if (objMaterial.id == 0) // Si el id del material es igual a 0, se registra un nuevo material
            {
                int resultado = new CN_Material().Registrar(objMaterial, out mensaje);

                if (resultado > 0)
                {
                    MessageBox.Show("Material registrado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Se agregan los datos del material al DataGridView respetando el orden de las columnas
                    dt.Rows.Add(new object[] {"", resultado, txtCodigo.Text, txtNombre.Text, txtDescripcion.Text, cbCategoria.SelectedValue, cbCategoria.Text, 0, 0, 0,
                ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString(),((OpcionCombo)cbEstado.SelectedItem).Texto.ToString(),
                (int)objMaterial.tipoMaterial, objMaterial.tipoMaterial.ToString()
            });
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); // Se muestra un mensaje de error
                }
            }
            else // Si el id del material es diferente de 0, se edita el material
            {
                bool resultado = new CN_Material().Editar(objMaterial, out mensaje);
                if (resultado)
                {
                    // Se actualizan los datos del material en el DataGridView indicando el nombre de la columna y el valor a actualizar
                    DataGridViewRow row = dt.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["codigo"].Value = txtCodigo.Text;
                    row.Cells["nombre"].Value = txtNombre.Text;
                    row.Cells["descripcion"].Value = txtDescripcion.Text;
                    row.Cells["idCategoria"].Value = cbCategoria.SelectedValue;
                    row.Cells["categoria"].Value = cbCategoria.Text;
                    row.Cells["estadoValor"].Value = ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionCombo)cbEstado.SelectedItem).Texto.ToString();
                    row.Cells["tipoMaterialValor"].Value = (int)objMaterial.tipoMaterial;
                    row.Cells["tipoMaterial"].Value = objMaterial.tipoMaterial.ToString();

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
            if (cbTipoMaterial.SelectedIndex == 0 || cbTipoMaterial.SelectedIndex == 1)
            {
                cbCategoria.Enabled = true;

                if (cbTipoMaterial.SelectedItem != null)
                {
                    TipoMaterial tipoMaterialSeleccionado = (TipoMaterial)((OpcionCombo)cbTipoMaterial.SelectedItem).Valor; // Obtener el valor del tipo de material seleccionado

                    // Filtrar la lista de categorías
                    List<Categoria> listaCategoriasFiltradas = new CN_Categoria().Listar() // Obtener la lista de categorías
                        .Where(c => c.tipoMaterial == tipoMaterialSeleccionado) // Filtrar la lista de categorías por el tipo de material seleccionado
                        .ToList();

                    // Actualizar el DataSource del ComboBox cbCategoria
                    cbCategoria.DataSource = listaCategoriasFiltradas; // Asignar la lista de categorías filtradas al ComboBox
                    cbCategoria.DisplayMember = "descripcion"; // Mostrar la descripción en el ComboBox
                    cbCategoria.ValueMember = "id"; // Guardar el id en el ComboBox
                }
            }
            else
            {
                cbCategoria.Enabled = false;
                cbCategoria.DataSource = null; // Limpiar el DataSource si no está habilitado
            }

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
                        MessageBox.Show("Material eliminado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

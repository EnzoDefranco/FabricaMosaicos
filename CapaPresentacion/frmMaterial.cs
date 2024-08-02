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
            cbTipoMaterial.SelectedIndexChanged += cbTipoMaterial_SelectedIndexChanged; //Al cambiar el valor del tipo de material, cambio los valores del desplegable de categorías.
            //Se carga al inicializar el form ya que se necesita que esté listo apenas se abre el mismo.

        }

        private void Limpiar()
        {
            txtId.Text = "0"; // 
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            cbCategoria.SelectedIndex = -1; //Se pone en -1, para que al principio este sin ningun dato, y dependiendo de cbTipoMaterial, se carguen las categorias pertenecientes al mismo 
            cbEstado.SelectedIndex = 0; // Se pone en 0 para que ya aparezca cargado el estado 'Activo'
            cbTipoMaterial.SelectedIndex = -1; // Se pone en -1, para que al principio este sin ningun dato

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
            cbTipoMaterial.Items.Add(new OpcionCombo() { Valor = (int)TipoMaterial.Producto, Texto = "Producto" });
            cbTipoMaterial.Items.Add(new OpcionCombo() { Valor = (int)TipoMaterial.MateriaPrima, Texto = "Materia Prima" });


            cbTipoMaterial.DisplayMember = "Texto";
            cbTipoMaterial.ValueMember = "Valor";
            cbTipoMaterial.SelectedIndex = -1;

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

                        (int)material.tipoMaterial,
                        material.tipoMaterial.ToString()
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

            Material objMaterial = new Material()
            {
                id = Convert.ToInt32(txtId.Text),
                codigo = txtCodigo.Text,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                oCategoria = new Categoria() { id = Convert.ToInt32(cbCategoria.SelectedValue) },
                estado = ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString() == "1" ? true : false,
                tipoMaterial = (TipoMaterial)((OpcionCombo)cbTipoMaterial.SelectedItem).Valor
            };

            if (objMaterial.id == 0)
            {
                int resultado = new CN_Material().Registrar(objMaterial, out mensaje);

                if (resultado > 0)
                {
                    MessageBox.Show("Material registrado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dt.Rows.Add(new object[] {"", resultado, txtCodigo.Text, txtNombre.Text, txtDescripcion.Text, cbCategoria.SelectedValue, cbCategoria.Text, 0, 0, 0,
                ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString(),((OpcionCombo)cbEstado.SelectedItem).Texto.ToString(),
                (int)objMaterial.tipoMaterial, objMaterial.tipoMaterial.ToString()
            });
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool resultado = new CN_Material().Editar(objMaterial, out mensaje);
                if (resultado)
                {
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
                    TipoMaterial tipoMaterialSeleccionado = (TipoMaterial)((OpcionCombo)cbTipoMaterial.SelectedItem).Valor;

                    // Filtrar la lista de categorías
                    List<Categoria> listaCategoriasFiltradas = new CN_Categoria().Listar()
                        .Where(c => c.tipoMaterial == tipoMaterialSeleccionado)
                        .ToList();

                    // Actualizar el DataSource del ComboBox cbCategoria
                    cbCategoria.DataSource = listaCategoriasFiltradas;
                    cbCategoria.DisplayMember = "descripcion";
                    cbCategoria.ValueMember = "id";
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using CapaEntidad;
using CapaPresentacion.Utilidades;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
            //Se están agregando dos objetos de tipo OpcionCombo al control cbEstado.Estos objetos tienen dos propiedades: Valor y Texto.El primer objeto tiene un valor de 1 y un texto de "Activo", mientras que el segundo objeto tiene un valor de 0 y un texto de "No Activo".
        {
            cbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" }); // Se añade un nuevo objeto de tipo OpcionCombo al control cbEstado, donde se le asigna un valor y un texto
            cbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            cbEstado.DisplayMember = "Texto"; // Se asigna la propiedad Texto del objeto OpcionCombo al control cbEstado
            cbEstado.ValueMember = "Valor"; // Se asigna la propiedad Valor del objeto OpcionCombo al control cbEstado
            cbEstado.SelectedIndex = 0; // Se selecciona el primer elemento del control cbEstado

            List <Rol> listaRol = new CN_Rol().Listar(); // Se crea una lista de objetos Rol y se le asigna el valor devuelto por el método Listar de la clase CN_Rol
            foreach (Rol rol in listaRol) // Por cada objeto Rol en la lista listaRol
            {
                cbRol.Items.Add(new OpcionCombo() { Valor = rol.id, Texto = rol.descripcion }); // Se añade un nuevo objeto de tipo OpcionCombo al control cbRol, donde se le asigna un valor y un texto
            }
            cbRol.DisplayMember = "Texto"; // Se asigna la propiedad Texto del objeto OpcionCombo al control cbEstado
            cbRol.ValueMember = "Valor"; // Se asigna la propiedad Valor del objeto OpcionCombo al control cbEstado
            cbRol.SelectedIndex = 0; // Se selecciona el primer elemento del control cbEstado

            //foreach (DataGridViewColumn columna in dt.Columns)
            //{
            //    if (columna.Visible == true && columna.Name != "btnSeleccionar")
            //    {
            //        cb_busqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            //    }
            //}
            //cb_busqueda.DisplayMember = "Texto";
            //cb_busqueda.ValueMember = "Valor";
            //cb_busqueda.SelectedIndex = 0;

            //Mostrar usuarios

            List<Usuario> listaUsuario = new CN_Usuario().Listar(); // Se crea una lista de objetos Rol y se le asigna el valor devuelto por el método Listar de la clase CN_Rol
            foreach (Usuario usuario in listaUsuario) // Por cada objeto Rol en la lista listaRol
            {
                dt.Rows.Add(new object[] {"",usuario.id,usuario.documento,usuario.razonSocial,usuario.correo,usuario.clave,
                usuario.oRol.id, usuario.oRol.descripcion,
                usuario.estado == true ? 1 : 0,
                usuario.estado == true ? "Activo" : "No Activo"
            });

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty; // Se inicializa la variable mensaje

            Usuario objUsuario = new Usuario() {  // Se crea un objeto de tipo Usuario
                id = Convert.ToInt32(txtId.Text), // Se asigna el valor de la propiedad id del objeto objUsuario
                documento = txtNroDocumento.Text, // Se asigna el valor de la propiedad documento del objeto objUsuario
                razonSocial = txtRazonSocial.Text, // Se asigna el valor de la propiedad razonSocial del objeto objUsuario
                correo = txtCorreo.Text, // Se asigna el valor de la propiedad correo del objeto objUsuario
                clave = txtClave.Text, // Se asigna el valor de la propiedad clave del objeto objUsuario
                oRol = new Rol() { id = Convert.ToInt32(((OpcionCombo)cbRol.SelectedItem).Valor) }, // Se asigna un nuevo objeto de tipo Rol al objeto objUsuario
                estado = Convert.ToInt32(((OpcionCombo)cbEstado.SelectedItem).Valor) == 1 ? true : false // Se asigna el valor de la propiedad estado del objeto objUsuario
            };

            if (objUsuario.id == 0)
            {
                int idUsuarioGnerado = new CN_Usuario().Registrar(objUsuario, out mensaje); // Se crea una variable de tipo entero y se le asigna el valor devuelto por el método Registrar de la clase CN_Usuario

                if (idUsuarioGnerado > 0) // Si el valor de la variable idUsuarioGnerado es mayor a 0
                {
                    MessageBox.Show("Usuario registrado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); // Muestra un mensaje
                    dt.Rows.Add(new object[] {"",idUsuarioGnerado,txtNroDocumento.Text,txtRazonSocial.Text,txtCorreo.Text,txtClave.Text,
                ((OpcionCombo)cbRol.SelectedItem).Valor.ToString(),((OpcionCombo)cbRol.SelectedItem).Texto.ToString(),
                ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString(),((OpcionCombo)cbEstado.SelectedItem).Texto.ToString()});

                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); // Muestra un mensaje
                }

            }
            else
            {
                bool resultado = new CN_Usuario().Editar(objUsuario, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dt.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["id"].Value = txtId.Text;
                    row.Cells["documento"].Value = txtNroDocumento.Text;
                    row.Cells["nombreCompleto"].Value = txtRazonSocial.Text;
                    row.Cells["correo"].Value = txtCorreo.Text;
                    row.Cells["clave"].Value = txtClave.Text;
                    row.Cells["idRol"].Value = ((OpcionCombo)cbRol.SelectedItem).Valor.ToString();
                    row.Cells["rol"].Value = ((OpcionCombo)cbRol.SelectedItem).Texto.ToString();
                    row.Cells["estadoValor"].Value = ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString();
                    row.Cells["estado"].Value = ((OpcionCombo)cbEstado.SelectedItem).Texto.ToString();
                    //MessageBox.Show("Usuario actualizado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); // Muestra un mensaje

                    Limpiar(); // Llama al método Limpiar

                }
                else
                {
                    MessageBox.Show(mensaje);
                }

            }

           

            Limpiar(); // Llama al método Limpiar

        }

        private void Limpiar()
        {
            txtId.Text = "0";
            txtNroDocumento.Text = "";
            txtRazonSocial.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
            txtConfirmarClave.Text = "";
            cbRol.SelectedIndex = 0;
            cbEstado.SelectedIndex = 0;
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

        private void dt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dt.Columns[e.ColumnIndex].Name == "btnSeleccionar") // Si la columna seleccionada es igual a "btnSeleccionar"
            {
                int indice = e.RowIndex; // Se obtiene el índice de la fila seleccionada
                if (indice >= 0) // Si el índice es mayor o igual a 0
                {
    
                    if (selectedRowIndex >= 0) 
                    {
                        dt.Rows[selectedRowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White; // Se asigna un color blanco al fondo de la fila seleccionada
                    }
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dt.Rows[indice].Cells["id"].Value.ToString();
                    txtNroDocumento.Text = dt.Rows[indice].Cells["documento"].Value.ToString();
                    txtRazonSocial.Text = dt.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtCorreo.Text = dt.Rows[indice].Cells["correo"].Value.ToString();
                    txtClave.Text = dt.Rows[indice].Cells["clave"].Value.ToString();
                    txtConfirmarClave.Text = dt.Rows[indice].Cells["clave"].Value.ToString();
       
                    foreach (OpcionCombo item in cbRol.Items)
                    {
                        if (item.Valor.ToString() == dt.Rows[indice].Cells["idRol"].Value.ToString())
                        {
                            cbRol.SelectedItem = item;
                            break;
                        }
                    }

                    foreach (OpcionCombo item in cbEstado.Items)
                    {
                        if (item.Valor.ToString() == dt.Rows[indice].Cells["estado"].Value.ToString())
                        {
                            cbEstado.SelectedItem = item;
                            break;
                        }
                    }

                    // Pintar la fila seleccionada
                    dt.Rows[indice].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow; // Se asigna un color amarillo al fondo de la fila seleccionada
                     
                    selectedRowIndex = indice;
                }
            }
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    // Despintar la fila anteriormente seleccionada
                    if (selectedRowIndex >= 0)
                    {
                        dt.Rows[selectedRowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    }

                    txtId.Text = dt.Rows[indice].Cells["id"].Value.ToString();
                    txtNroDocumento.Text = dt.Rows[indice].Cells["documento"].Value.ToString();
                    txtRazonSocial.Text = dt.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtCorreo.Text = dt.Rows[indice].Cells["correo"].Value.ToString();
                    txtClave.Text = dt.Rows[indice].Cells["clave"].Value.ToString();
                    txtConfirmarClave.Text = dt.Rows[indice].Cells["clave"].Value.ToString();
                    cbRol.SelectedValue = dt.Rows[indice].Cells["idRol"].Value.ToString();
                    cbEstado.SelectedValue = dt.Rows[indice].Cells["estado"].Value.ToString();

                    // Pintar la fila seleccionada
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
                    bool resultado = new CN_Usuario().Eliminar(Convert.ToInt32(txtId.Text), out mensaje);
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
    }
}

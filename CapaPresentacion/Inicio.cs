using CapaEntidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using FontAwesome.Sharp;

namespace CapaPresentacion
{

    public partial class Inicio : Form
    {
        private static Usuario usuarioActual; // Variable de tipo Usuario que almacena el usuario actual
        private static IconMenuItem menuActivo = null; // Variable de tipo IconMenuItem, inicializada en null
        private static Form formularioActivo = null; // Variable de tipo Form, inicializada en null
        public Inicio(Usuario objUsuario)
        {
            usuarioActual = objUsuario; // El constructor SIEMPRE se ejecuta primero
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e) // Método que se ejecuta al cargar el formulario
        {
            List<Permiso> listaPermisos = new CN_Permiso().Listar(usuarioActual.id); // Se crea una lista de objetos Permiso y se le asigna el valor devuelto por el método Listar de la clase CN_Permiso
            foreach (IconMenuItem iconmenu in menu.Items)
            {
                bool encontrado = listaPermisos.Any(m => m.nombreMenu == iconmenu.Name); // Verifica
                if (encontrado)
                {
                    iconmenu.Visible = true;
                }
                else
                {
                    iconmenu.Visible = false;
                }
            }
            lbl_usuario.Text = usuarioActual.razonSocial; // Asigna el valor de la propiedad razonSocial del objeto usuarioActual al control lbl_usuario
        }

        private void abrirFormulario(IconMenuItem menu, Form formulario) // Método que recibe un objeto de tipo IconMenuItem y un objeto de tipo Form
        {
            if (menuActivo != null) // Si la variable menuActivo es diferente de null
            {
                menuActivo.BackColor = Color.White; // Asigna un color al fondo del control menuActivo
            }
            menu.BackColor = Color.Silver; // Asigna un color al fondo del control menu
            menuActivo = menu; // Asigna el valor del control menu a la variable menuActivo

            if (formularioActivo != null) // Si la variable formularioActivo es diferente de null
            {
                formularioActivo.Close(); // Cierra el formulario activo
            }
            formularioActivo = formulario; // Asigna el valor del formulario a la variable formularioActivo
            formulario.TopLevel = false; // Indica que el formulario no es de nivel superior
            formulario.FormBorderStyle = FormBorderStyle.None; // Indica que el formulario no tiene borde
            formulario.Dock = DockStyle.Fill; // Indica que el formulario se ajusta al tamaño del contenedor
            formulario.BackColor = Color.SteelBlue; // Asigna un color al fondo del formulario

            contenedor.Controls.Add(formulario); // Agrega el formulario al control panel_contenedor
            formulario.Show(); // Muestra el formulario

        }

        private void menu_usuario_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmUsuarios()); // Llama al método abrirFormulario y le pasa como parámetros el control que generó el evento y un objeto de tipo frmUsuarios
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmClientes()); // Llama al método abrirFormulario y le pasa como parámetros el control que generó el evento y un objeto de tipo frmClientes
        }

        private void menuProveedores_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmProveedores()); // Llama al método abrirFormulario y le pasa como parámetros el control que generó el evento y un objeto de tipo frmProveedores
        }

        private void menuReportes_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmReportes()); // Llama al método abrirFormulario y le pasa como parámetros el control que generó el evento y un objeto de tipo frmReportes
        }

        private void subMenuCategoria_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmCategoria()); // Llama al método abrirFormulario y le pasa como parámetros el control que generó el evento y un objeto de tipo frmCategorias
        }

        private void subMenuProducto_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmProducto()); // Llama al método abrirFormulario y le pasa como parámetros el control que generó el evento y un objeto de tipo frmProductos
        }

        private void subMenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmVentas()); // Llama al método abrirFormulario y le pasa como parámetros el control que generó el evento y un objeto de tipo frmVenta
        }

        private void subMenuDetalleVenta_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmDetalleVenta()); // Llama al método abrirFormulario y le pasa como parámetros el control que generó el evento y un objeto de tipo frmDetalleVenta
        }

        private void subMenuRegistraCompra_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmCompras()); // Llama al método abrirFormulario y le pasa como parámetros el control que generó el evento y un objeto de tipo frmCompras
        }

        private void subMenuDetalleCompra_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmDetalleCompra()); // Llama al método abrirFormulario y le pasa como parámetros el control que generó el evento y un objeto de tipo frmDetalleCompra
        }
    }
}

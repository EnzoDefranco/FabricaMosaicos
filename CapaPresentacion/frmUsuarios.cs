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

            foreach (DataGridViewColumn columna in dt.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cb_busqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cb_busqueda.DisplayMember = "Texto";
            cb_busqueda.ValueMember = "Valor";
            cb_busqueda.SelectedIndex = 0;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dt.Rows.Add(new object[] {"",txtId.Text,txtNroDocumento.Text,txtRazonSocial.Text,txtCorreo.Text,txtClave.Text,
            ((OpcionCombo)cbRol.SelectedItem).Valor.ToString(),((OpcionCombo)cbRol.SelectedItem).Texto.ToString(),
            ((OpcionCombo)cbEstado.SelectedItem).Valor.ToString(),((OpcionCombo)cbEstado.SelectedItem).Texto.ToString()});
            Limpiar();
         

        }

        private void Limpiar()
        {
            txtId.Text = "0";
            txtNroDocumento.Text = "";
            txtRazonSocial.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
            cbRol.SelectedIndex = 0;
            cbEstado.SelectedIndex = 0;
        }
    }
}

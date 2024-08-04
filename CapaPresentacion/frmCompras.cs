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
        private Usuario _Usuario;
        // Obtener la lista de materiales
        List<Material> listaMaterial = new CN_Material().Listar();

        // Filtrar por tipo de material

        public frmCompras( Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
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
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dtp.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtIdProveedor.Text = "0";
            txtidmp.Text = "0";

        

            // Obtener la lista de materiales
            listaMaterial = new CN_Material().Listar();

            // Cargar la lista de materiales en el ComboBox
            List<Material> materialesFiltrados = listaMaterial.Where(m => m.tipoMaterial == TipoMaterial.MateriaPrima).ToList();

            cbCodMaterial.DataSource = materialesFiltrados;
            cbCodMaterial.DisplayMember = "codigo";
            cbCodMaterial.ValueMember = "codigo";
            cbCodMaterial.SelectedIndex = -1;


            // Configurar el ComboBox de Materia Prima para autocompletar
            cbCodMaterial.DropDownStyle = ComboBoxStyle.DropDown; // Asegurarse de que el estilo sea DropDown
            cbCodMaterial.AutoCompleteMode = AutoCompleteMode.Suggest; // Configurar el modo de autocompletar
            cbCodMaterial.AutoCompleteSource = AutoCompleteSource.ListItems; // Con



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
                    txtCodMp.Text = material.codigo;

                    // Cambiar el color de fondo del ComboBox a verde
                    cbCodMaterial.BackColor = Color.LightGreen;
                }
                else
                {
                    // Si el material no existe, cambiar el color de fondo a rojo (o cualquier otro color)
                    cbCodMaterial.BackColor = Color.LightCoral;
                }

                // Limpiar el ComboBox
                cbCodMaterial.SelectedIndex = -1;

                // Enfocar el TextBox de cantidad
                txtCantidad.Focus();
            }
        }
    }
}

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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.KeyPreview = true; // Habilita la propiedad KeyPreview del formulario
            this.KeyDown += new KeyEventHandler(btn_entrar_KeyDown); // Asigna el evento KeyDown al formulario

        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario
        }

        private void btn_entrar_Click(object sender, EventArgs e)
        {
            Inicio formInicio = new Inicio();
            formInicio.Show(); // Muestra el formulario Inicio
            this.Hide(); // Oculta el formulario Login

            formInicio.FormClosing += frm_closing; // Asigna el evento frm_closing al formulario Inicio

        }

        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txt_nrodocumento.Text = ""; // Limpia el campo txt_nrodocumento
            txt_contrasena.Text = ""; // Limpia el campo txt_contrasena
            this.Show(); // Muestra el formulario Login
        }

        private void btn_entrar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Llama al método que maneja el clic del botón Entrar
                btn_entrar_Click(sender, e);
            }
        }
    }
}

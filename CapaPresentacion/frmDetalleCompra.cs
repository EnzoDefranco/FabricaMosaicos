using CapaEntidad;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDetalleCompra : Form
    {
        private Compra oCompra;

        public frmDetalleCompra(Compra oCompra)
        {
            InitializeComponent();
            this.oCompra = oCompra; // Asignar la variable oCompra
        }

        private void frmDetalleCompra_Load(object sender, EventArgs e)
        {
            txtNumeroDocumento.Text = oCompra.numeroDocumento;
            txtFecha.Text = oCompra.fechaRegistro;
            txtTipoDocumento.Text = oCompra.tipoDocumento;
            txtRazonSocial.Text = oCompra.oProveedor.razonSocial;
            txtTelefono.Text = oCompra.oProveedor.telefono;
            txtDocumento.Text = oCompra.oProveedor.documento;
            TXTTOT.Text = oCompra.montoTotal.ToString();

            dt.Rows.Clear();
            foreach (DetalleCompra detalle in oCompra.oDetalleCompra)
            {
                dt.Rows.Add(new object[] { detalle.oMaterial.nombre, detalle.precioCompra, detalle.cantidad, detalle.montoTotal });
            }
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumeroDocumento.Text))
            {
                MessageBox.Show("No se puede descargar el documento, porque no se ha registrado el número de documento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string Texto_Html = Properties.Resources.Remito.ToString();
            Texto_Html = Texto_Html.Replace("@numeroDocumento", txtNumeroDocumento.Text);
            Texto_Html = Texto_Html.Replace("@tipoDocumento", txtTipoDocumento.Text);
            Texto_Html = Texto_Html.Replace("@FECHA", txtFecha.Text);
            Texto_Html = Texto_Html.Replace("@CLIENTE", txtRazonSocial.Text);
            Texto_Html = Texto_Html.Replace("@DOCUMENTO", txtDocumento.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dt.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["materiaPrima"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["precioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["subTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }

            Texto_Html = Texto_Html.Replace("@FILAS", filas);
            Texto_Html = Texto_Html.Replace("@TOTAL", TXTTOT.Text);

            SaveFileDialog saveFile = new SaveFileDialog
            {
                FileName = string.Format("Documento_{0}.pdf", txtNumeroDocumento.Text),
                Filter = "PDF files|*.pdf"
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Descargando documento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
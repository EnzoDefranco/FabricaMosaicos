using CapaEntidad;
using Microsoft.Reporting.WinForms;
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
    public partial class detalleVenta : Form
    {
        private Venta oVenta;

        public detalleVenta(Venta oVenta)
        {
            InitializeComponent();
            this.oVenta = oVenta; // Asignar la variable oVenta
        }

        private void detalleVenta_Load(object sender, EventArgs e)
        {


            // Llenar el DataTable con los detalles de la venta
            DataTable dtVenta = new DataTable();
            dtVenta.Columns.Add("oMaterial", typeof(string));
            dtVenta.Columns.Add("PrecioVenta", typeof(decimal));
            dtVenta.Columns.Add("Cantidad", typeof(decimal));
            dtVenta.Columns.Add("montoTotal", typeof(decimal));

            foreach (DetalleVenta detalle in oVenta.oDetalleVenta)
            {
                dtVenta.Rows.Add(detalle.oMaterial.nombre, detalle.precioVenta, detalle.cantidad, detalle.montoTotal);
            }

            // Configurar el ReportViewer para usar el informe VentaInforme.rdlc
            reportViewer1.LocalReport.ReportEmbeddedResource = "CapaPresentacion.InformeVenta.rdlc"; // Asegúrate que el nombre del informe sea correcto

            // Asignar el DataTable como fuente de datos
            ReportDataSource rds = new ReportDataSource("DetalleVenta", dtVenta);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            ReportParameter[] reportParameters = new ReportParameter[]
            {
                new ReportParameter("numeroDocumento", oVenta.numeroDocumento),
                new ReportParameter("tipoDocumento", oVenta.tipoDocumento),
                new ReportParameter("fechaRegistro", oVenta.fechaRegistro.ToString())
            };

            reportViewer1.LocalReport.SetParameters(reportParameters);

            // Refrescar el reporte para mostrar los datos
            this.reportViewer1.RefreshReport();
        }
    }
}

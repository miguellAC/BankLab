using System;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;

namespace Ejemplo.GUI.Reportes
{
    public partial class FrmVisorReporte : BaseForm
    {
        public FrmVisorReporte()
        {
            InitializeComponent();
            cmbTipoReporte.Items.AddRange(new object[] { "Cartera", "Movimientos", "Clientes" });
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            MostrarMensaje("Reporte generado.");
        }

        private void btnExportarCSV_Click(object sender, EventArgs e)
        {
            ReporteHelper.ExportarACSV(dgvResultados, "reporte.csv");
        }

        private void btnExportarHTML_Click(object sender, EventArgs e)
        {
            ReporteHelper.ExportarAHTML(dgvResultados, "reporte.html", "Reporte " + cmbTipoReporte.SelectedItem);
        }
    }
}

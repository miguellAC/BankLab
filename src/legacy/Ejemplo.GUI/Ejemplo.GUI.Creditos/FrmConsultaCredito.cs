using System;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;

namespace Ejemplo.GUI.Creditos
{
    public partial class FrmConsultaCredito : BaseForm
    {
        public FrmConsultaCredito()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Consulta de crédito realizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

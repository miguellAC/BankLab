using System;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;

namespace Ejemplo.GUI.Cuentas
{
    public partial class FrmSaldos : BaseForm
    {
        public FrmSaldos()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            lblSaldo.Text = "$ 1,500.00";
        }
    }
}

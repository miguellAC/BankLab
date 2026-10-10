using System;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;

namespace Ejemplo.GUI.Cuentas
{
    public partial class FrmMovimientos : BaseForm
    {
        public FrmMovimientos()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Movimientos consultados.", "Movimientos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

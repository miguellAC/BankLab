using System;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;

namespace Ejemplo.GUI.Cuentas
{
    public partial class FrmConsultaCuentas : BaseForm
    {
        public FrmConsultaCuentas()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Dummy search
            MessageBox.Show("Búsqueda simulada exitosa.", "Cuentas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

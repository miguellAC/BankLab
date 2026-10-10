using System;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;

namespace Ejemplo.GUI.Creditos
{
    public partial class FrmCartera : BaseForm
    {
        public FrmCartera()
        {
            InitializeComponent();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            MostrarMensaje("Exportación iniciada.");
        }
    }
}

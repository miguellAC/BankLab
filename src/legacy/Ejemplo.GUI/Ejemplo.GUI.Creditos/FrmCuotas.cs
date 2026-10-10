using System;
using System.Drawing;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;

namespace Ejemplo.GUI.Creditos
{
    public partial class FrmCuotas : BaseForm
    {
        private int _operacionId;

        public FrmCuotas(int operacionId)
        {
            InitializeComponent();
            _operacionId = operacionId;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarCuotas();
        }

        private void CargarCuotas()
        {
            // Simulate data binding and colors
        }
    }
}

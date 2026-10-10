using System;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;
using Ejemplo.WCF.Agentes;

namespace Ejemplo.GUI.Creditos
{
    public partial class FrmRegistroPago : BaseForm
    {
        private PagosAgente _pagosAgente;

        public FrmRegistroPago()
        {
            InitializeComponent();
            _pagosAgente = new PagosAgente();
            cmbTipoPago.Items.AddRange(new object[] { "Efectivo", "Transferencia", "Cheque" });
            cmbCanal.Items.AddRange(new object[] { "Ventanilla", "Banca Web", "Cajero" });
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (MostrarConfirmacion("¿Está seguro de registrar el pago?"))
            {
                MostrarMensaje("Pago registrado exitosamente.");
            }
        }
    }
}

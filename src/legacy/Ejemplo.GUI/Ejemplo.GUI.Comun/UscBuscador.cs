using System;
using System.Windows.Forms;

namespace Ejemplo.GUI.Comun
{
    public partial class UscBuscador : UserControl
    {
        public event EventHandler<BusquedaEventArgs> BusquedaSolicitada;

        public UscBuscador()
        {
            InitializeComponent();
            cmbTipoBusqueda.Items.AddRange(new object[] { "Identificación", "Nombre", "Número Operación" });
            cmbTipoBusqueda.SelectedIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaSolicitada?.Invoke(this, new BusquedaEventArgs(txtBusqueda.Text, cmbTipoBusqueda.SelectedItem.ToString()));
        }
    }

    public class BusquedaEventArgs : EventArgs
    {
        public string Texto { get; }
        public string TipoBusqueda { get; }

        public BusquedaEventArgs(string texto, string tipoBusqueda)
        {
            Texto = texto;
            TipoBusqueda = tipoBusqueda;
        }
    }
}

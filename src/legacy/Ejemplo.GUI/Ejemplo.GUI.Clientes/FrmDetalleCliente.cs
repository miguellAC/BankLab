using System;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;

namespace Ejemplo.GUI.Clientes
{
    public partial class FrmDetalleCliente : BaseForm
    {
        private int _clienteId;

        public FrmDetalleCliente(int clienteId)
        {
            InitializeComponent();
            _clienteId = clienteId;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarDetalles();
        }

        private void CargarDetalles()
        {
            txtNombre.Text = "Cliente de Prueba";
            txtIdentificacion.Text = "1234567890";
        }
    }
}

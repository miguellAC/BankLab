using System;
using System.Globalization;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;
using Ejemplo.WCF.Agentes;

namespace Ejemplo.GUI.Cuentas
{
    public partial class FrmSaldos : BaseForm
    {
        // El agente se crea la primera vez que se consulta (el constructor de la ventana no abre ningun canal).
        private CuentasAgente _agente;

        public FrmSaldos()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            int cuentaId;
            if (!int.TryParse(txtCuenta.Text.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out cuentaId) || cuentaId <= 0)
            {
                MostrarError("Ingrese un Id de cuenta numerico mayor que cero.");
                return;
            }

            btnConsultar.Enabled = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                if (_agente == null)
                    _agente = new CuentasAgente();

                // Llamada sincrona (suficiente para el laboratorio; bloquea la ventana mientras responde el servicio).
                var respuesta = _agente.ConsultarSaldoCuenta(cuentaId);

                if (respuesta == null || !respuesta.Exitosa || respuesta.Datos == null)
                {
                    lblSaldo.Text = "$ 0.00";
                    MostrarError(respuesta != null && !string.IsNullOrEmpty(respuesta.Mensaje)
                        ? respuesta.Mensaje
                        : "El servicio no devolvio datos.");
                    return;
                }

                lblSaldo.Text = "$ " + respuesta.Datos.SaldoDisponible.ToString("N2", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                // Incluye el caso "host WCF apagado": el agente lanza InvalidOperationException con instrucciones.
                lblSaldo.Text = "$ 0.00";
                MostrarError(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnConsultar.Enabled = true;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_agente != null)
            {
                _agente.Dispose();
                _agente = null;
            }
            base.OnFormClosed(e);
        }
    }
}

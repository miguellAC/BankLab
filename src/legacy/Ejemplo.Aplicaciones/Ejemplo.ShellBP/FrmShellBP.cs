using System;
using System.Drawing;
using System.Windows.Forms;
using Ejemplo.GUI.Clientes;
using Ejemplo.GUI.Creditos;
using Ejemplo.GUI.Cuentas;
using Ejemplo.GUI.Reportes;

namespace Ejemplo.ShellBP
{
    public partial class FrmShellBP : Form
    {
        private readonly ModuleLoader _loader = new ModuleLoader();
        private Form _formularioActivo = null;

        public FrmShellBP()
        {
            InitializeComponent();
            InicializarRegistroModulos();
        }

        private void InicializarRegistroModulos()
        {
            _loader.RegistrarTipo("Buscar Clientes", typeof(FrmBuscarCliente));
            _loader.RegistrarTipo("Consultar Cuentas", typeof(FrmConsultaCuentas));
            _loader.RegistrarTipo("Consultar Saldos", typeof(FrmSaldos));
            _loader.RegistrarTipo("Operaciones Crédito", typeof(FrmConsultaCredito));
            _loader.RegistrarTipo("Cuotas y Pagos", typeof(FrmCuotas));
            _loader.RegistrarTipo("Registrar Pagos", typeof(FrmRegistroPago));
            _loader.RegistrarTipo("Informe Cartera", typeof(FrmCartera));
            _loader.RegistrarTipo("Visor Reportes", typeof(FrmVisorReporte));

            _loader.CargarModulos();
        }

        private void CargarFormularioEnPanel(Type formType)
        {
            try
            {
                if (_formularioActivo != null)
                {
                    _formularioActivo.Close();
                    _formularioActivo.Dispose();
                    _formularioActivo = null;
                }

                _formularioActivo = (Form)Activator.CreateInstance(formType);
                _formularioActivo.TopLevel = false;
                _formularioActivo.FormBorderStyle = FormBorderStyle.None;
                _formularioActivo.Dock = DockStyle.Fill;

                panelContenedor.Controls.Clear();
                panelContenedor.Controls.Add(_formularioActivo);
                _formularioActivo.Show();

                lblEstadoModulo.Text = $"Módulo activo: {_formularioActivo.Text}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando módulo: {ex.Message}", "Error ShellBP",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            CargarFormularioEnPanel(typeof(FrmBuscarCliente));
        }

        private void BtnCuentas_Click(object sender, EventArgs e)
        {
            CargarFormularioEnPanel(typeof(FrmConsultaCuentas));
        }

        private void BtnCreditos_Click(object sender, EventArgs e)
        {
            CargarFormularioEnPanel(typeof(FrmConsultaCredito));
        }

        private void BtnPagos_Click(object sender, EventArgs e)
        {
            CargarFormularioEnPanel(typeof(FrmRegistroPago));
        }

        private void BtnCartera_Click(object sender, EventArgs e)
        {
            CargarFormularioEnPanel(typeof(FrmCartera));
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            CargarFormularioEnPanel(typeof(FrmVisorReporte));
        }
    }
}

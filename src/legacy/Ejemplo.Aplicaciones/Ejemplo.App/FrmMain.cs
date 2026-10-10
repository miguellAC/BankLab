using System;
using System.Windows.Forms;

namespace Ejemplo.App
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            lblUsuario.Text = $"Usuario: {UserSession.Usuario}";
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void MenuCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar la sesión actual?", "Cerrar Sesión",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void MenuSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuBuscarCliente_Click(object sender, EventArgs e)
        {
            NavigationManager.AbrirFormulario(this, "BuscarCliente");
        }

        private void MenuConsultarCuentas_Click(object sender, EventArgs e)
        {
            NavigationManager.AbrirFormulario(this, "ConsultaCuentas");
        }

        private void MenuConsultarSaldos_Click(object sender, EventArgs e)
        {
            NavigationManager.AbrirFormulario(this, "ConsultaSaldos");
        }

        private void MenuConsultarCredito_Click(object sender, EventArgs e)
        {
            NavigationManager.AbrirFormulario(this, "ConsultaCredito");
        }

        private void MenuCuotasCredito_Click(object sender, EventArgs e)
        {
            NavigationManager.AbrirFormulario(this, "CuotasCredito");
        }

        private void MenuRegistroPago_Click(object sender, EventArgs e)
        {
            NavigationManager.AbrirFormulario(this, "RegistroPago");
        }

        private void MenuCartera_Click(object sender, EventArgs e)
        {
            NavigationManager.AbrirFormulario(this, "ReporteCartera");
        }

        private void MenuVisorReportes_Click(object sender, EventArgs e)
        {
            NavigationManager.AbrirFormulario(this, "VisorReportes");
        }

        private void TreeViewMenu_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                NavigationManager.AbrirFormulario(this, e.Node.Tag.ToString());
            }
        }
    }
}

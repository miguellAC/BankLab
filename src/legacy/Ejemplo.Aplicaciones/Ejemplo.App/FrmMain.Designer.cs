namespace Ejemplo.App
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuArchivo;
        private System.Windows.Forms.ToolStripMenuItem menuCerrarSesion;
        private System.Windows.Forms.ToolStripMenuItem menuSalir;
        private System.Windows.Forms.ToolStripMenuItem menuClientes;
        private System.Windows.Forms.ToolStripMenuItem menuBuscarCliente;
        private System.Windows.Forms.ToolStripMenuItem menuCuentas;
        private System.Windows.Forms.ToolStripMenuItem menuConsultarCuentas;
        private System.Windows.Forms.ToolStripMenuItem menuConsultarSaldos;
        private System.Windows.Forms.ToolStripMenuItem menuCreditos;
        private System.Windows.Forms.ToolStripMenuItem menuConsultarCredito;
        private System.Windows.Forms.ToolStripMenuItem menuCuotasCredito;
        private System.Windows.Forms.ToolStripMenuItem menuRegistroPago;
        private System.Windows.Forms.ToolStripMenuItem menuCartera;
        private System.Windows.Forms.ToolStripMenuItem menuReportes;
        private System.Windows.Forms.ToolStripMenuItem menuVisorReportes;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblUsuario;
        private System.Windows.Forms.ToolStripStatusLabel lblSeparador;
        private System.Windows.Forms.ToolStripStatusLabel lblFecha;
        private System.Windows.Forms.Panel panelNavegacion;
        private System.Windows.Forms.TreeView treeViewMenu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBuscarCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCuentas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConsultarCuentas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConsultarSaldos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCreditos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConsultarCredito = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCuotasCredito = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRegistroPago = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCartera = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVisorReportes = new System.Windows.Forms.ToolStripMenuItem();

            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSeparador = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFecha = new System.Windows.Forms.ToolStripStatusLabel();

            this.panelNavegacion = new System.Windows.Forms.Panel();
            this.treeViewMenu = new System.Windows.Forms.TreeView();

            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelNavegacion.SuspendLayout();
            this.SuspendLayout();

            // MenuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuArchivo, this.menuClientes, this.menuCuentas, this.menuCreditos, this.menuReportes
            });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1024, 24);
            this.menuStrip.TabIndex = 0;

            // Archivo
            this.menuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuCerrarSesion, this.menuSalir });
            this.menuArchivo.Text = "Archivo";
            this.menuCerrarSesion.Text = "Cerrar Sesión";
            this.menuCerrarSesion.Click += new System.EventHandler(this.MenuCerrarSesion_Click);
            this.menuSalir.Text = "Salir";
            this.menuSalir.Click += new System.EventHandler(this.MenuSalir_Click);

            // Clientes
            this.menuClientes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuBuscarCliente });
            this.menuClientes.Text = "Clientes";
            this.menuBuscarCliente.Text = "Buscar Cliente";
            this.menuBuscarCliente.Click += new System.EventHandler(this.MenuBuscarCliente_Click);

            // Cuentas
            this.menuCuentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuConsultarCuentas, this.menuConsultarSaldos });
            this.menuCuentas.Text = "Cuentas";
            this.menuConsultarCuentas.Text = "Consultar Cuentas";
            this.menuConsultarCuentas.Click += new System.EventHandler(this.MenuConsultarCuentas_Click);
            this.menuConsultarSaldos.Text = "Consultar Saldos";
            this.menuConsultarSaldos.Click += new System.EventHandler(this.MenuConsultarSaldos_Click);

            // Créditos
            this.menuCreditos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuConsultarCredito, this.menuCuotasCredito, this.menuRegistroPago, this.menuCartera
            });
            this.menuCreditos.Text = "Créditos";
            this.menuConsultarCredito.Text = "Detalle Operación";
            this.menuConsultarCredito.Click += new System.EventHandler(this.MenuConsultarCredito_Click);
            this.menuCuotasCredito.Text = "Consulta de Cuotas";
            this.menuCuotasCredito.Click += new System.EventHandler(this.MenuCuotasCredito_Click);
            this.menuRegistroPago.Text = "Registrar Pago";
            this.menuRegistroPago.Click += new System.EventHandler(this.MenuRegistroPago_Click);
            this.menuCartera.Text = "Informe de Cartera";
            this.menuCartera.Click += new System.EventHandler(this.MenuCartera_Click);

            // Reportes
            this.menuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuVisorReportes });
            this.menuReportes.Text = "Reportes";
            this.menuVisorReportes.Text = "Visor General de Reportes";
            this.menuVisorReportes.Click += new System.EventHandler(this.MenuVisorReportes_Click);

            // Panel de Navegación lateral
            this.panelNavegacion.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelNavegacion.Width = 220;
            this.panelNavegacion.Controls.Add(this.treeViewMenu);

            // TreeView Menu
            this.treeViewMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            var nodeClientes = new System.Windows.Forms.TreeNode("Gestión Clientes");
            nodeClientes.Nodes.Add(new System.Windows.Forms.TreeNode("Buscar Cliente") { Tag = "BuscarCliente" });

            var nodeCuentas = new System.Windows.Forms.TreeNode("Gestión Cuentas");
            nodeCuentas.Nodes.Add(new System.Windows.Forms.TreeNode("Consultar Cuentas") { Tag = "ConsultaCuentas" });
            nodeCuentas.Nodes.Add(new System.Windows.Forms.TreeNode("Consultar Saldos") { Tag = "ConsultaSaldos" });

            var nodeCreditos = new System.Windows.Forms.TreeNode("Gestión Créditos");
            nodeCreditos.Nodes.Add(new System.Windows.Forms.TreeNode("Detalle Operación") { Tag = "ConsultaCredito" });
            nodeCreditos.Nodes.Add(new System.Windows.Forms.TreeNode("Consulta Cuotas") { Tag = "CuotasCredito" });
            nodeCreditos.Nodes.Add(new System.Windows.Forms.TreeNode("Registrar Pago") { Tag = "RegistroPago" });
            nodeCreditos.Nodes.Add(new System.Windows.Forms.TreeNode("Informe Cartera") { Tag = "ReporteCartera" });

            var nodeReportes = new System.Windows.Forms.TreeNode("Reportes");
            nodeReportes.Nodes.Add(new System.Windows.Forms.TreeNode("Visor Reportes") { Tag = "VisorReportes" });

            this.treeViewMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
                nodeClientes, nodeCuentas, nodeCreditos, nodeReportes
            });
            this.treeViewMenu.ExpandAll();
            this.treeViewMenu.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewMenu_NodeMouseDoubleClick);

            // StatusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblUsuario, this.lblSeparador, this.lblFecha
            });
            this.statusStrip.Location = new System.Drawing.Point(0, 700);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1024, 22);
            this.lblUsuario.Text = "Usuario: -";
            this.lblSeparador.Spring = true;
            this.lblFecha.Text = "Fecha: -";

            // FrmMain Form
            this.ClientSize = new System.Drawing.Size(1024, 722);
            this.Controls.Add(this.panelNavegacion);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Banco Ejemplo - BankLab App";

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelNavegacion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

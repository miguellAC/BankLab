namespace Ejemplo.ShellBP
{
    partial class FrmShellBP
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolStrip toolStripBarra;
        private System.Windows.Forms.ToolStripButton btnClientes;
        private System.Windows.Forms.ToolStripButton btnCuentas;
        private System.Windows.Forms.ToolStripButton btnCreditos;
        private System.Windows.Forms.ToolStripButton btnPagos;
        private System.Windows.Forms.ToolStripButton btnCartera;
        private System.Windows.Forms.ToolStripButton btnReportes;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblEstadoModulo;

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
            this.toolStripBarra = new System.Windows.Forms.ToolStrip();
            this.btnClientes = new System.Windows.Forms.ToolStripButton();
            this.btnCuentas = new System.Windows.Forms.ToolStripButton();
            this.btnCreditos = new System.Windows.Forms.ToolStripButton();
            this.btnPagos = new System.Windows.Forms.ToolStripButton();
            this.btnCartera = new System.Windows.Forms.ToolStripButton();
            this.btnReportes = new System.Windows.Forms.ToolStripButton();

            this.panelContenedor = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblEstadoModulo = new System.Windows.Forms.ToolStripStatusLabel();

            this.toolStripBarra.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // toolStripBarra
            this.toolStripBarra.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripBarra.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.btnClientes,
                new System.Windows.Forms.ToolStripSeparator(),
                this.btnCuentas,
                new System.Windows.Forms.ToolStripSeparator(),
                this.btnCreditos,
                this.btnPagos,
                new System.Windows.Forms.ToolStripSeparator(),
                this.btnCartera,
                this.btnReportes
            });
            this.toolStripBarra.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarra.Name = "toolStripBarra";
            this.toolStripBarra.Size = new System.Drawing.Size(1024, 38);
            this.toolStripBarra.TabIndex = 0;

            // Buttons
            this.btnClientes.Text = "👤 Clientes";
            this.btnClientes.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClientes.Click += new System.EventHandler(this.BtnClientes_Click);

            this.btnCuentas.Text = "🏦 Cuentas";
            this.btnCuentas.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCuentas.Click += new System.EventHandler(this.BtnCuentas_Click);

            this.btnCreditos.Text = "💳 Créditos";
            this.btnCreditos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCreditos.Click += new System.EventHandler(this.BtnCreditos_Click);

            this.btnPagos.Text = "💰 Registrar Pago";
            this.btnPagos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPagos.Click += new System.EventHandler(this.BtnPagos_Click);

            this.btnCartera.Text = "📊 Cartera";
            this.btnCartera.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCartera.Click += new System.EventHandler(this.BtnCartera_Click);

            this.btnReportes.Text = "📄 Reportes";
            this.btnReportes.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReportes.Click += new System.EventHandler(this.BtnReportes_Click);

            // panelContenedor
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 38);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1024, 662);
            this.panelContenedor.TabIndex = 1;

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblEstadoModulo });
            this.statusStrip.Location = new System.Drawing.Point(0, 700);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1024, 22);
            this.lblEstadoModulo.Text = "Listo. Seleccione un módulo en la barra superior.";

            // FrmShellBP
            this.ClientSize = new System.Drawing.Size(1024, 722);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripBarra);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Banco Ejemplo - ShellBP (Cargador Modular)";

            this.toolStripBarra.ResumeLayout(false);
            this.toolStripBarra.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

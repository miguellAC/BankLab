namespace Ejemplo.GUI.Clientes
{
    partial class FrmBuscarCliente
    {
        private System.ComponentModel.IContainer components = null;
        private Comun.UscBuscador uscBuscador;
        private System.Windows.Forms.DataGridView dgvClientes;
        private Comun.UscPaginacion uscPaginacion;

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
            this.uscBuscador = new Ejemplo.GUI.Comun.UscBuscador();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.uscPaginacion = new Ejemplo.GUI.Comun.UscPaginacion();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // uscBuscador
            // 
            this.uscBuscador.Dock = System.Windows.Forms.DockStyle.Top;
            this.uscBuscador.Location = new System.Drawing.Point(0, 0);
            this.uscBuscador.Name = "uscBuscador";
            this.uscBuscador.Size = new System.Drawing.Size(800, 45);
            this.uscBuscador.TabIndex = 1;
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClientes.Location = new System.Drawing.Point(0, 45);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(800, 500);
            this.dgvClientes.TabIndex = 2;
            this.dgvClientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClientes_CellDoubleClick);
            // 
            // uscPaginacion
            // 
            this.uscPaginacion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uscPaginacion.Location = new System.Drawing.Point(0, 545);
            this.uscPaginacion.Name = "uscPaginacion";
            this.uscPaginacion.Size = new System.Drawing.Size(800, 35);
            this.uscPaginacion.TabIndex = 3;
            // 
            // FrmBuscarCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.uscPaginacion);
            this.Controls.Add(this.uscBuscador);
            this.Name = "FrmBuscarCliente";
            this.Text = "Buscar Clientes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

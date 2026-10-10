namespace Ejemplo.GUI.Clientes
{
    partial class FrmDetalleCliente
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox gbPersonales;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtIdentificacion;
        private System.Windows.Forms.TabControl tcProductos;
        private System.Windows.Forms.TabPage tpCuentas;
        private System.Windows.Forms.TabPage tpCreditos;
        private System.Windows.Forms.DataGridView dgvCuentas;
        private System.Windows.Forms.DataGridView dgvCreditos;

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
            this.gbPersonales = new System.Windows.Forms.GroupBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.tcProductos = new System.Windows.Forms.TabControl();
            this.tpCuentas = new System.Windows.Forms.TabPage();
            this.dgvCuentas = new System.Windows.Forms.DataGridView();
            this.tpCreditos = new System.Windows.Forms.TabPage();
            this.dgvCreditos = new System.Windows.Forms.DataGridView();
            this.gbPersonales.SuspendLayout();
            this.tcProductos.SuspendLayout();
            this.tpCuentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentas)).BeginInit();
            this.tpCreditos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreditos)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPersonales
            // 
            this.gbPersonales.Controls.Add(this.txtIdentificacion);
            this.gbPersonales.Controls.Add(this.txtNombre);
            this.gbPersonales.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPersonales.Location = new System.Drawing.Point(0, 0);
            this.gbPersonales.Name = "gbPersonales";
            this.gbPersonales.Size = new System.Drawing.Size(800, 100);
            this.gbPersonales.TabIndex = 1;
            this.gbPersonales.TabStop = false;
            this.gbPersonales.Text = "Datos Personales";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(20, 30);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(300, 23);
            this.txtNombre.TabIndex = 0;
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.Location = new System.Drawing.Point(340, 30);
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.ReadOnly = true;
            this.txtIdentificacion.Size = new System.Drawing.Size(200, 23);
            this.txtIdentificacion.TabIndex = 1;
            // 
            // tcProductos
            // 
            this.tcProductos.Controls.Add(this.tpCuentas);
            this.tcProductos.Controls.Add(this.tpCreditos);
            this.tcProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcProductos.Location = new System.Drawing.Point(0, 100);
            this.tcProductos.Name = "tcProductos";
            this.tcProductos.SelectedIndex = 0;
            this.tcProductos.Size = new System.Drawing.Size(800, 480);
            this.tcProductos.TabIndex = 2;
            // 
            // tpCuentas
            // 
            this.tpCuentas.Controls.Add(this.dgvCuentas);
            this.tpCuentas.Location = new System.Drawing.Point(4, 24);
            this.tpCuentas.Name = "tpCuentas";
            this.tpCuentas.Padding = new System.Windows.Forms.Padding(3);
            this.tpCuentas.Size = new System.Drawing.Size(792, 452);
            this.tpCuentas.TabIndex = 0;
            this.tpCuentas.Text = "Cuentas";
            this.tpCuentas.UseVisualStyleBackColor = true;
            // 
            // dgvCuentas
            // 
            this.dgvCuentas.AllowUserToAddRows = false;
            this.dgvCuentas.AllowUserToDeleteRows = false;
            this.dgvCuentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCuentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCuentas.Location = new System.Drawing.Point(3, 3);
            this.dgvCuentas.Name = "dgvCuentas";
            this.dgvCuentas.ReadOnly = true;
            this.dgvCuentas.Size = new System.Drawing.Size(786, 446);
            this.dgvCuentas.TabIndex = 0;
            // 
            // tpCreditos
            // 
            this.tpCreditos.Controls.Add(this.dgvCreditos);
            this.tpCreditos.Location = new System.Drawing.Point(4, 24);
            this.tpCreditos.Name = "tpCreditos";
            this.tpCreditos.Padding = new System.Windows.Forms.Padding(3);
            this.tpCreditos.Size = new System.Drawing.Size(792, 452);
            this.tpCreditos.TabIndex = 1;
            this.tpCreditos.Text = "Créditos";
            this.tpCreditos.UseVisualStyleBackColor = true;
            // 
            // dgvCreditos
            // 
            this.dgvCreditos.AllowUserToAddRows = false;
            this.dgvCreditos.AllowUserToDeleteRows = false;
            this.dgvCreditos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCreditos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCreditos.Location = new System.Drawing.Point(3, 3);
            this.dgvCreditos.Name = "dgvCreditos";
            this.dgvCreditos.ReadOnly = true;
            this.dgvCreditos.Size = new System.Drawing.Size(786, 446);
            this.dgvCreditos.TabIndex = 0;
            // 
            // FrmDetalleCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tcProductos);
            this.Controls.Add(this.gbPersonales);
            this.Name = "FrmDetalleCliente";
            this.Text = "Detalle de Cliente";
            this.gbPersonales.ResumeLayout(false);
            this.gbPersonales.PerformLayout();
            this.tcProductos.ResumeLayout(false);
            this.tpCuentas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentas)).EndInit();
            this.tpCreditos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreditos)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

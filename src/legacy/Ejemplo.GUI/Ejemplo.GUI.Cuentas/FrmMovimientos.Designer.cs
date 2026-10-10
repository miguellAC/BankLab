namespace Ejemplo.GUI.Cuentas
{
    partial class FrmMovimientos
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox gbCabecera;
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView dgvMovimientos;
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
            this.gbCabecera = new System.Windows.Forms.GroupBox();
            this.lblCuenta = new System.Windows.Forms.Label();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            this.uscPaginacion = new Ejemplo.GUI.Comun.UscPaginacion();
            this.gbCabecera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            this.SuspendLayout();
            // 
            // gbCabecera
            // 
            this.gbCabecera.Controls.Add(this.btnConsultar);
            this.gbCabecera.Controls.Add(this.dtpHasta);
            this.gbCabecera.Controls.Add(this.lblHasta);
            this.gbCabecera.Controls.Add(this.dtpDesde);
            this.gbCabecera.Controls.Add(this.lblDesde);
            this.gbCabecera.Controls.Add(this.txtCuenta);
            this.gbCabecera.Controls.Add(this.lblCuenta);
            this.gbCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCabecera.Location = new System.Drawing.Point(0, 0);
            this.gbCabecera.Name = "gbCabecera";
            this.gbCabecera.Size = new System.Drawing.Size(800, 80);
            this.gbCabecera.TabIndex = 1;
            this.gbCabecera.TabStop = false;
            this.gbCabecera.Text = "Criterios de Búsqueda";
            // 
            // lblCuenta
            // 
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.Location = new System.Drawing.Point(20, 30);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(48, 15);
            this.lblCuenta.TabIndex = 0;
            this.lblCuenta.Text = "Cuenta:";
            // 
            // txtCuenta
            // 
            this.txtCuenta.Location = new System.Drawing.Point(80, 27);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(150, 23);
            this.txtCuenta.TabIndex = 1;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(250, 30);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(42, 15);
            this.lblDesde.TabIndex = 2;
            this.lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(300, 27);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(100, 23);
            this.dtpDesde.TabIndex = 3;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(420, 30);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(40, 15);
            this.lblHasta.TabIndex = 4;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(470, 27);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(100, 23);
            this.dtpHasta.TabIndex = 5;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(600, 25);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(100, 25);
            this.btnConsultar.TabIndex = 6;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.AllowUserToAddRows = false;
            this.dgvMovimientos.AllowUserToDeleteRows = false;
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovimientos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMovimientos.Location = new System.Drawing.Point(0, 80);
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.ReadOnly = true;
            this.dgvMovimientos.Size = new System.Drawing.Size(800, 465);
            this.dgvMovimientos.TabIndex = 2;
            // 
            // uscPaginacion
            // 
            this.uscPaginacion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uscPaginacion.Location = new System.Drawing.Point(0, 545);
            this.uscPaginacion.Name = "uscPaginacion";
            this.uscPaginacion.Size = new System.Drawing.Size(800, 35);
            this.uscPaginacion.TabIndex = 3;
            // 
            // FrmMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.dgvMovimientos);
            this.Controls.Add(this.uscPaginacion);
            this.Controls.Add(this.gbCabecera);
            this.Name = "FrmMovimientos";
            this.Text = "Movimientos de Cuenta";
            this.gbCabecera.ResumeLayout(false);
            this.gbCabecera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

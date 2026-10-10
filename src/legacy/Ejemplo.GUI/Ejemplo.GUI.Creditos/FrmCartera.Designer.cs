namespace Ejemplo.GUI.Creditos
{
    partial class FrmCartera
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ComboBox cmbSucursal;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.ComboBox cmbRiesgo;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.DataGridView dgvCartera;

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
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.cmbSucursal = new System.Windows.Forms.ComboBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.cmbRiesgo = new System.Windows.Forms.ComboBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.dgvCartera = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartera)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(30, 20);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(100, 23);
            this.dtpFecha.TabIndex = 1;
            // 
            // cmbSucursal
            // 
            this.cmbSucursal.FormattingEnabled = true;
            this.cmbSucursal.Location = new System.Drawing.Point(150, 20);
            this.cmbSucursal.Name = "cmbSucursal";
            this.cmbSucursal.Size = new System.Drawing.Size(120, 23);
            this.cmbSucursal.TabIndex = 2;
            this.cmbSucursal.Text = "Sucursal";
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(290, 20);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(120, 23);
            this.cmbEstado.TabIndex = 3;
            this.cmbEstado.Text = "Estado";
            // 
            // cmbRiesgo
            // 
            this.cmbRiesgo.FormattingEnabled = true;
            this.cmbRiesgo.Location = new System.Drawing.Point(430, 20);
            this.cmbRiesgo.Name = "cmbRiesgo";
            this.cmbRiesgo.Size = new System.Drawing.Size(120, 23);
            this.cmbRiesgo.TabIndex = 4;
            this.cmbRiesgo.Text = "Riesgo";
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(570, 18);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(75, 25);
            this.btnExportar.TabIndex = 5;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // dgvCartera
            // 
            this.dgvCartera.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCartera.Location = new System.Drawing.Point(30, 60);
            this.dgvCartera.Name = "dgvCartera";
            this.dgvCartera.Size = new System.Drawing.Size(740, 480);
            this.dgvCartera.TabIndex = 6;
            // 
            // FrmCartera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.dgvCartera);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.cmbRiesgo);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.cmbSucursal);
            this.Controls.Add(this.dtpFecha);
            this.Name = "FrmCartera";
            this.Text = "Cartera de Crédito";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCartera)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

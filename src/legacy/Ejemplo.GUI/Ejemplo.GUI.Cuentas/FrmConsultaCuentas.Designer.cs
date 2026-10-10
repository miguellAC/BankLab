namespace Ejemplo.GUI.Cuentas
{
    partial class FrmConsultaCuentas
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtClienteId;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvCuentas;
        private System.Windows.Forms.Label lblClienteId;

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
            this.txtClienteId = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvCuentas = new System.Windows.Forms.DataGridView();
            this.lblClienteId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblClienteId
            // 
            this.lblClienteId.AutoSize = true;
            this.lblClienteId.Location = new System.Drawing.Point(20, 20);
            this.lblClienteId.Name = "lblClienteId";
            this.lblClienteId.Size = new System.Drawing.Size(61, 15);
            this.lblClienteId.TabIndex = 0;
            this.lblClienteId.Text = "Cliente ID:";
            // 
            // txtClienteId
            // 
            this.txtClienteId.Location = new System.Drawing.Point(100, 17);
            this.txtClienteId.Name = "txtClienteId";
            this.txtClienteId.Size = new System.Drawing.Size(150, 23);
            this.txtClienteId.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(260, 16);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 25);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvCuentas
            // 
            this.dgvCuentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCuentas.Location = new System.Drawing.Point(20, 60);
            this.dgvCuentas.Name = "dgvCuentas";
            this.dgvCuentas.Size = new System.Drawing.Size(740, 480);
            this.dgvCuentas.TabIndex = 3;
            // 
            // FrmConsultaCuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.dgvCuentas);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtClienteId);
            this.Controls.Add(this.lblClienteId);
            this.Name = "FrmConsultaCuentas";
            this.Text = "Consulta de Cuentas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

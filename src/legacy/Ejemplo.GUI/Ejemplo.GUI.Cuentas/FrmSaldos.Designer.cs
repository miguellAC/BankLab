namespace Ejemplo.GUI.Cuentas
{
    partial class FrmSaldos
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label lblSaldoTexto;
        private System.Windows.Forms.Label lblSaldo;

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
            this.lblCuenta = new System.Windows.Forms.Label();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.lblSaldoTexto = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCuenta
            // 
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.Location = new System.Drawing.Point(50, 50);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(113, 15);
            this.lblCuenta.TabIndex = 1;
            this.lblCuenta.Text = "Número de Cuenta:";
            // 
            // txtCuenta
            // 
            this.txtCuenta.Location = new System.Drawing.Point(180, 47);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(200, 23);
            this.txtCuenta.TabIndex = 2;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(400, 45);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(100, 25);
            this.btnConsultar.TabIndex = 3;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // lblSaldoTexto
            // 
            this.lblSaldoTexto.AutoSize = true;
            this.lblSaldoTexto.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSaldoTexto.Location = new System.Drawing.Point(50, 150);
            this.lblSaldoTexto.Name = "lblSaldoTexto";
            this.lblSaldoTexto.Size = new System.Drawing.Size(171, 25);
            this.lblSaldoTexto.TabIndex = 4;
            this.lblSaldoTexto.Text = "Saldo Disponible:";
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSaldo.ForeColor = System.Drawing.Color.Green;
            this.lblSaldo.Location = new System.Drawing.Point(230, 150);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(65, 25);
            this.lblSaldo.TabIndex = 5;
            this.lblSaldo.Text = "$ 0.00";
            // 
            // FrmSaldos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.lblSaldoTexto);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.txtCuenta);
            this.Controls.Add(this.lblCuenta);
            this.Name = "FrmSaldos";
            this.Text = "Consulta de Saldos";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

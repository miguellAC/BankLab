namespace Ejemplo.GUI.Creditos
{
    partial class FrmRegistroPago
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label lblTipoPago;
        private System.Windows.Forms.ComboBox cmbTipoPago;
        private System.Windows.Forms.Label lblCanal;
        private System.Windows.Forms.ComboBox cmbCanal;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Button btnConfirmar;

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
            this.lblOperacion = new System.Windows.Forms.Label();
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.lblTipoPago = new System.Windows.Forms.Label();
            this.cmbTipoPago = new System.Windows.Forms.ComboBox();
            this.lblCanal = new System.Windows.Forms.Label();
            this.cmbCanal = new System.Windows.Forms.ComboBox();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Location = new System.Drawing.Point(50, 50);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(110, 15);
            this.lblOperacion.TabIndex = 1;
            this.lblOperacion.Text = "Número Operación:";
            // 
            // txtOperacion
            // 
            this.txtOperacion.Location = new System.Drawing.Point(200, 47);
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.Size = new System.Drawing.Size(200, 23);
            this.txtOperacion.TabIndex = 2;
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new System.Drawing.Point(50, 90);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(46, 15);
            this.lblMonto.TabIndex = 3;
            this.lblMonto.Text = "Monto:";
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(200, 87);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(200, 23);
            this.txtMonto.TabIndex = 4;
            // 
            // lblTipoPago
            // 
            this.lblTipoPago.AutoSize = true;
            this.lblTipoPago.Location = new System.Drawing.Point(50, 130);
            this.lblTipoPago.Name = "lblTipoPago";
            this.lblTipoPago.Size = new System.Drawing.Size(81, 15);
            this.lblTipoPago.TabIndex = 5;
            this.lblTipoPago.Text = "Tipo de Pago:";
            // 
            // cmbTipoPago
            // 
            this.cmbTipoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPago.FormattingEnabled = true;
            this.cmbTipoPago.Location = new System.Drawing.Point(200, 127);
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(200, 23);
            this.cmbTipoPago.TabIndex = 6;
            // 
            // lblCanal
            // 
            this.lblCanal.AutoSize = true;
            this.lblCanal.Location = new System.Drawing.Point(50, 170);
            this.lblCanal.Name = "lblCanal";
            this.lblCanal.Size = new System.Drawing.Size(39, 15);
            this.lblCanal.TabIndex = 7;
            this.lblCanal.Text = "Canal:";
            // 
            // cmbCanal
            // 
            this.cmbCanal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCanal.FormattingEnabled = true;
            this.cmbCanal.Location = new System.Drawing.Point(200, 167);
            this.cmbCanal.Name = "cmbCanal";
            this.cmbCanal.Size = new System.Drawing.Size(200, 23);
            this.cmbCanal.TabIndex = 8;
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(50, 210);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(65, 15);
            this.lblReferencia.TabIndex = 9;
            this.lblReferencia.Text = "Referencia:";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(200, 207);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(200, 23);
            this.txtReferencia.TabIndex = 10;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(200, 260);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(100, 30);
            this.btnConfirmar.TabIndex = 11;
            this.btnConfirmar.Text = "Confirmar Pago";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // FrmRegistroPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtReferencia);
            this.Controls.Add(this.lblReferencia);
            this.Controls.Add(this.cmbCanal);
            this.Controls.Add(this.lblCanal);
            this.Controls.Add(this.cmbTipoPago);
            this.Controls.Add(this.lblTipoPago);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.lblMonto);
            this.Controls.Add(this.txtOperacion);
            this.Controls.Add(this.lblOperacion);
            this.Name = "FrmRegistroPago";
            this.Text = "Registro de Pago";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

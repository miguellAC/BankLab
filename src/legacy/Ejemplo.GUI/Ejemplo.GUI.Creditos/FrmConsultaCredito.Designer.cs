namespace Ejemplo.GUI.Creditos
{
    partial class FrmConsultaCredito
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNumeroOperacion;
        private System.Windows.Forms.TextBox txtNumeroOperacion;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.GroupBox gbDetalles;
        private System.Windows.Forms.GroupBox gbSaldos;

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
            this.lblNumeroOperacion = new System.Windows.Forms.Label();
            this.txtNumeroOperacion = new System.Windows.Forms.TextBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.gbDetalles = new System.Windows.Forms.GroupBox();
            this.gbSaldos = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // lblNumeroOperacion
            // 
            this.lblNumeroOperacion.AutoSize = true;
            this.lblNumeroOperacion.Location = new System.Drawing.Point(30, 30);
            this.lblNumeroOperacion.Name = "lblNumeroOperacion";
            this.lblNumeroOperacion.Size = new System.Drawing.Size(110, 15);
            this.lblNumeroOperacion.TabIndex = 1;
            this.lblNumeroOperacion.Text = "Número Operación:";
            // 
            // txtNumeroOperacion
            // 
            this.txtNumeroOperacion.Location = new System.Drawing.Point(150, 27);
            this.txtNumeroOperacion.Name = "txtNumeroOperacion";
            this.txtNumeroOperacion.Size = new System.Drawing.Size(200, 23);
            this.txtNumeroOperacion.TabIndex = 2;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(370, 25);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(100, 25);
            this.btnConsultar.TabIndex = 3;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // gbDetalles
            // 
            this.gbDetalles.Location = new System.Drawing.Point(30, 80);
            this.gbDetalles.Name = "gbDetalles";
            this.gbDetalles.Size = new System.Drawing.Size(740, 200);
            this.gbDetalles.TabIndex = 4;
            this.gbDetalles.TabStop = false;
            this.gbDetalles.Text = "Detalles de Operación";
            // 
            // gbSaldos
            // 
            this.gbSaldos.Location = new System.Drawing.Point(30, 300);
            this.gbSaldos.Name = "gbSaldos";
            this.gbSaldos.Size = new System.Drawing.Size(740, 200);
            this.gbSaldos.TabIndex = 5;
            this.gbSaldos.TabStop = false;
            this.gbSaldos.Text = "Saldos";
            // 
            // FrmConsultaCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.gbSaldos);
            this.Controls.Add(this.gbDetalles);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.txtNumeroOperacion);
            this.Controls.Add(this.lblNumeroOperacion);
            this.Name = "FrmConsultaCredito";
            this.Text = "Consulta de Crédito";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

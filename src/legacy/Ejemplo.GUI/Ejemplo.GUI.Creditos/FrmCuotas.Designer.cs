namespace Ejemplo.GUI.Creditos
{
    partial class FrmCuotas
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.CheckBox chkSoloPendientes;
        private System.Windows.Forms.DataGridView dgvCuotas;

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
            this.chkSoloPendientes = new System.Windows.Forms.CheckBox();
            this.dgvCuotas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuotas)).BeginInit();
            this.SuspendLayout();
            // 
            // chkSoloPendientes
            // 
            this.chkSoloPendientes.AutoSize = true;
            this.chkSoloPendientes.Location = new System.Drawing.Point(30, 20);
            this.chkSoloPendientes.Name = "chkSoloPendientes";
            this.chkSoloPendientes.Size = new System.Drawing.Size(110, 19);
            this.chkSoloPendientes.TabIndex = 1;
            this.chkSoloPendientes.Text = "Solo Pendientes";
            this.chkSoloPendientes.UseVisualStyleBackColor = true;
            // 
            // dgvCuotas
            // 
            this.dgvCuotas.AllowUserToAddRows = false;
            this.dgvCuotas.AllowUserToDeleteRows = false;
            this.dgvCuotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCuotas.Location = new System.Drawing.Point(30, 60);
            this.dgvCuotas.Name = "dgvCuotas";
            this.dgvCuotas.ReadOnly = true;
            this.dgvCuotas.Size = new System.Drawing.Size(740, 480);
            this.dgvCuotas.TabIndex = 2;
            // 
            // FrmCuotas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.dgvCuotas);
            this.Controls.Add(this.chkSoloPendientes);
            this.Name = "FrmCuotas";
            this.Text = "Plan de Cuotas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuotas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

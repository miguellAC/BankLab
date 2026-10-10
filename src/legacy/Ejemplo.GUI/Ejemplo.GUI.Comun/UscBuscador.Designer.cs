namespace Ejemplo.GUI.Comun
{
    partial class UscBuscador
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.ComboBox cmbTipoBusqueda;
        private System.Windows.Forms.Button btnBuscar;

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
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.cmbTipoBusqueda = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Location = new System.Drawing.Point(140, 10);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(200, 23);
            this.txtBusqueda.TabIndex = 1;
            // 
            // cmbTipoBusqueda
            // 
            this.cmbTipoBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoBusqueda.FormattingEnabled = true;
            this.cmbTipoBusqueda.Location = new System.Drawing.Point(10, 10);
            this.cmbTipoBusqueda.Name = "cmbTipoBusqueda";
            this.cmbTipoBusqueda.Size = new System.Drawing.Size(120, 23);
            this.cmbTipoBusqueda.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(350, 9);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 25);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // UscBuscador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cmbTipoBusqueda);
            this.Controls.Add(this.txtBusqueda);
            this.Name = "UscBuscador";
            this.Size = new System.Drawing.Size(440, 45);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

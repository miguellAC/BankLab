namespace Ejemplo.GUI.Comun
{
    partial class UscPaginacion
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnPrimera;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnUltima;

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
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnPrimera = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnUltima = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(120, 10);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(65, 15);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Página 1 de 1";
            // 
            // btnPrimera
            // 
            this.btnPrimera.Location = new System.Drawing.Point(10, 5);
            this.btnPrimera.Name = "btnPrimera";
            this.btnPrimera.Size = new System.Drawing.Size(25, 25);
            this.btnPrimera.TabIndex = 1;
            this.btnPrimera.Text = "<<";
            this.btnPrimera.UseVisualStyleBackColor = true;
            this.btnPrimera.Click += new System.EventHandler(this.btnPrimera_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(40, 5);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(25, 25);
            this.btnAnterior.TabIndex = 2;
            this.btnAnterior.Text = "<";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(220, 5);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(25, 25);
            this.btnSiguiente.TabIndex = 3;
            this.btnSiguiente.Text = ">";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnUltima
            // 
            this.btnUltima.Location = new System.Drawing.Point(250, 5);
            this.btnUltima.Name = "btnUltima";
            this.btnUltima.Size = new System.Drawing.Size(25, 25);
            this.btnUltima.TabIndex = 4;
            this.btnUltima.Text = ">>";
            this.btnUltima.UseVisualStyleBackColor = true;
            this.btnUltima.Click += new System.EventHandler(this.btnUltima_Click);
            // 
            // UscPaginacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUltima);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.btnPrimera);
            this.Controls.Add(this.lblInfo);
            this.Name = "UscPaginacion";
            this.Size = new System.Drawing.Size(300, 35);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

namespace Ejemplo.GUI.Reportes
{
    partial class FrmVisorReporte
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipoReporte;
        private System.Windows.Forms.Panel pnlParametros;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.Button btnExportarCSV;
        private System.Windows.Forms.Button btnExportarHTML;

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
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipoReporte = new System.Windows.Forms.ComboBox();
            this.pnlParametros = new System.Windows.Forms.Panel();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.btnExportarCSV = new System.Windows.Forms.Button();
            this.btnExportarHTML = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(30, 20);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(95, 15);
            this.lblTipo.TabIndex = 1;
            this.lblTipo.Text = "Tipo de Reporte:";
            // 
            // cmbTipoReporte
            // 
            this.cmbTipoReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoReporte.FormattingEnabled = true;
            this.cmbTipoReporte.Location = new System.Drawing.Point(150, 17);
            this.cmbTipoReporte.Name = "cmbTipoReporte";
            this.cmbTipoReporte.Size = new System.Drawing.Size(200, 23);
            this.cmbTipoReporte.TabIndex = 2;
            // 
            // pnlParametros
            // 
            this.pnlParametros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlParametros.Location = new System.Drawing.Point(30, 60);
            this.pnlParametros.Name = "pnlParametros";
            this.pnlParametros.Size = new System.Drawing.Size(740, 80);
            this.pnlParametros.TabIndex = 3;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(30, 150);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(100, 30);
            this.btnGenerar.TabIndex = 4;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(30, 200);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.Size = new System.Drawing.Size(740, 300);
            this.dgvResultados.TabIndex = 5;
            // 
            // btnExportarCSV
            // 
            this.btnExportarCSV.Location = new System.Drawing.Point(30, 520);
            this.btnExportarCSV.Name = "btnExportarCSV";
            this.btnExportarCSV.Size = new System.Drawing.Size(100, 30);
            this.btnExportarCSV.TabIndex = 6;
            this.btnExportarCSV.Text = "Exportar CSV";
            this.btnExportarCSV.UseVisualStyleBackColor = true;
            this.btnExportarCSV.Click += new System.EventHandler(this.btnExportarCSV_Click);
            // 
            // btnExportarHTML
            // 
            this.btnExportarHTML.Location = new System.Drawing.Point(150, 520);
            this.btnExportarHTML.Name = "btnExportarHTML";
            this.btnExportarHTML.Size = new System.Drawing.Size(100, 30);
            this.btnExportarHTML.TabIndex = 7;
            this.btnExportarHTML.Text = "Exportar HTML";
            this.btnExportarHTML.UseVisualStyleBackColor = true;
            this.btnExportarHTML.Click += new System.EventHandler(this.btnExportarHTML_Click);
            // 
            // FrmVisorReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnExportarHTML);
            this.Controls.Add(this.btnExportarCSV);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.pnlParametros);
            this.Controls.Add(this.cmbTipoReporte);
            this.Controls.Add(this.lblTipo);
            this.Name = "FrmVisorReporte";
            this.Text = "Visor de Reportes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

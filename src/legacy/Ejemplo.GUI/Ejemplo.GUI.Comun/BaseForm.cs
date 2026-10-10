using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ejemplo.GUI.Comun
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
            this.Font = new Font("Segoe UI", 9f);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Text = $"Banco Ejemplo - {this.Text}";
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool MostrarConfirmacion(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}

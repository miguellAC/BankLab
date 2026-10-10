using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ejemplo.App
{
    public partial class FrmLogin : Form
    {
        private int _intentosFallidos = 0;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if ((user == "admin" && pass == "admin123") || 
                (user == "consulta" && pass == "consulta123"))
            {
                UserSession.Usuario = user;
                UserSession.FechaLogin = DateTime.Now;
                UserSession.EstaAutenticado = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                _intentosFallidos++;
                lblMensaje.Text = $"Credenciales inválidas. Intentos: {_intentosFallidos}/3";
                lblMensaje.ForeColor = Color.Red;
                txtPassword.Clear();
                txtPassword.Focus();

                if (_intentosFallidos >= 3)
                {
                    MessageBox.Show("Límite de intentos alcanzado. Cerrando la aplicación.", 
                        "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

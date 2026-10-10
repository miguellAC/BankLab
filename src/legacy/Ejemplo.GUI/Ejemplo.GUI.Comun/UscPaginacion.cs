using System;
using System.Windows.Forms;

namespace Ejemplo.GUI.Comun
{
    public partial class UscPaginacion : UserControl
    {
        public event EventHandler PaginaCambiada;
        private int _currentPage = 1;

        public UscPaginacion()
        {
            InitializeComponent();
        }

        public void ActualizarPaginacion(int totalRegistros, int pageSize, int currentPage)
        {
            _currentPage = currentPage;
            int totalPages = (int)Math.Ceiling((double)totalRegistros / pageSize);
            if (totalPages == 0) totalPages = 1;
            lblInfo.Text = $"Página {currentPage} de {totalPages} ({totalRegistros} registros)";
        }

        private void btnPrimera_Click(object sender, EventArgs e) { _currentPage = 1; OnPaginaCambiada(); }
        private void btnAnterior_Click(object sender, EventArgs e) { if (_currentPage > 1) _currentPage--; OnPaginaCambiada(); }
        private void btnSiguiente_Click(object sender, EventArgs e) { _currentPage++; OnPaginaCambiada(); }
        private void btnUltima_Click(object sender, EventArgs e) { OnPaginaCambiada(); }

        private void OnPaginaCambiada()
        {
            PaginaCambiada?.Invoke(this, EventArgs.Empty);
        }
    }
}

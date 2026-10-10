using System;
using System.Windows.Forms;
using Ejemplo.GUI.Comun;
using Ejemplo.WCF.Agentes;
using Ejemplo.Comun.DTOs;
using System.Collections.Generic;

namespace Ejemplo.GUI.Clientes
{
    public partial class FrmBuscarCliente : BaseForm
    {
        private ClientesAgente _clientesAgente;

        public FrmBuscarCliente()
        {
            InitializeComponent();
            _clientesAgente = new ClientesAgente();
            uscBuscador.BusquedaSolicitada += UscBuscador_BusquedaSolicitada;
            uscPaginacion.PaginaCambiada += UscPaginacion_PaginaCambiada;
        }

        private void UscBuscador_BusquedaSolicitada(object sender, BusquedaEventArgs e)
        {
            BuscarClientes();
        }

        private void UscPaginacion_PaginaCambiada(object sender, EventArgs e)
        {
            BuscarClientes();
        }

        private void BuscarClientes()
        {
            // Placeholder: Call WCF agent to get clients
            // List<ClienteDTO> clientes = _clientesAgente.BuscarClientes(...);
            var clientes = new List<ClienteDTO>(); 
            dgvClientes.DataSource = clientes;
            uscPaginacion.ActualizarPaginacion(clientes.Count, 10, 1);
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int clienteId = (int)dgvClientes.Rows[e.RowIndex].Cells["ClienteId"].Value;
                FrmDetalleCliente frm = new FrmDetalleCliente(clienteId);
                frm.ShowDialog();
            }
        }
    }
}

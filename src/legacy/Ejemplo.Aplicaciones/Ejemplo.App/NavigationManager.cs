using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ejemplo.GUI.Clientes;
using Ejemplo.GUI.Creditos;
using Ejemplo.GUI.Cuentas;
using Ejemplo.GUI.Reportes;

namespace Ejemplo.App
{
    public static class NavigationManager
    {
        private static readonly Dictionary<string, Type> _registry = new Dictionary<string, Type>()
        {
            { "BuscarCliente", typeof(FrmBuscarCliente) },
            { "ConsultaCuentas", typeof(FrmConsultaCuentas) },
            { "ConsultaSaldos", typeof(FrmSaldos) },
            { "ConsultaCredito", typeof(FrmConsultaCredito) },
            { "CuotasCredito", typeof(FrmCuotas) },
            { "RegistroPago", typeof(FrmRegistroPago) },
            { "ReporteCartera", typeof(FrmCartera) },
            { "VisorReportes", typeof(FrmVisorReporte) }
        };

        public static Form AbrirFormulario(Form mdiParent, string key)
        {
            if (!_registry.TryGetValue(key, out Type formType))
            {
                MessageBox.Show($"Formulario no registrado para la clave: {key}", "Error de Navegación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            foreach (Form openForm in mdiParent.MdiChildren)
            {
                if (openForm.GetType() == formType)
                {
                    openForm.BringToFront();
                    openForm.Activate();
                    return openForm;
                }
            }

            try
            {
                Form formInstance = (Form)Activator.CreateInstance(formType);
                formInstance.MdiParent = mdiParent;
                formInstance.Show();
                return formInstance;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error instanciando {formType.Name}: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}

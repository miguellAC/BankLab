using System;
using System.Windows.Forms;
using Ejemplo.GUI.Cuentas;

namespace Ejemplo.Saldo.Cliente
{
    /// <summary>
    /// Cliente minimo del laboratorio "Saldo": abre unicamente FrmSaldos (proyecto Ejemplo.GUI.Cuentas),
    /// que consume el servicio mediante CuentasAgente. No contiene logica propia.
    /// Se creo porque Ejemplo.App y Ejemplo.ShellBP referencian GUI.Clientes/Creditos/Reportes, cuyas
    /// dependencias (agentes y BLL de otros modulos) no compilan todavia.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmSaldos());
        }
    }
}

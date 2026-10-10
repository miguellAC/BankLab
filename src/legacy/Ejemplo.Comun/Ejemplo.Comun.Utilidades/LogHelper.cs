using System;
using System.Diagnostics;

namespace Ejemplo.Comun.Utilidades
{
    public static class LogHelper
    {
        public static void Informacion(string mensaje)
        {
            Trace.TraceInformation($"[INFO] {DateTime.Now}: {mensaje}");
        }

        public static void Advertencia(string mensaje)
        {
            Trace.TraceWarning($"[WARN] {DateTime.Now}: {mensaje}");
        }

        public static void Error(string mensaje, Exception ex = null)
        {
            string log = $"[ERROR] {DateTime.Now}: {mensaje}";
            if (ex != null)
            {
                log += $"\nException: {ex.Message}\nStackTrace: {ex.StackTrace}";
            }
            Trace.TraceError(log);
        }

        public static void Debug(string mensaje)
        {
            System.Diagnostics.Debug.WriteLine($"[DEBUG] {DateTime.Now}: {mensaje}");
        }
    }
}

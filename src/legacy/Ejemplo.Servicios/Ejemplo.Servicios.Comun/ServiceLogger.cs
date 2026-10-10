using System;

namespace Ejemplo.Servicios.Comun
{
    public class ServiceLogger
    {
        public static void LogInfo(string serviceName, string message, int eventId)
        {
            Console.WriteLine($"[INFO] {serviceName} ({eventId}): {message}");
        }

        public static void LogError(string serviceName, string message, int eventId, Exception ex)
        {
            Console.WriteLine($"[ERROR] {serviceName} ({eventId}): {message} - {ex.Message}");
        }
    }
}

using System;

namespace Ejemplo.App
{
    public static class UserSession
    {
        public static string Usuario { get; set; } = "Invitado";
        public static DateTime FechaLogin { get; set; } = DateTime.Now;
        public static bool EstaAutenticado { get; set; } = false;
    }
}

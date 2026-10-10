# Guía de IIS y Servicios de Windows

## Internet Information Services (IIS)

IIS es el servidor web extensible de Microsoft para Windows. Es fundamental para hospedar aplicaciones .NET Framework.

### Conceptos Clave IIS
- **Sites (Sitios):** Contenedores principales ligados a un puerto/host (ej. puerto 80).
- **Applications (Aplicaciones):** Subrutas dentro de un sitio (ej. `/BankLabWeb`).
- **App Pools (Grupos de aplicaciones):** Aislan procesos. Cada App Pool corre bajo una identidad específica (ej. `ApplicationPoolIdentity`, `NetworkService`, o usuario de dominio). Determina la versión del CLR (ej. v4.0).
- **Bindings:** Enlaces de red (HTTP/HTTPS/NetTcp) y puertos.

### Hospedar WCF en IIS + WAS
Para hospedar servicios NetTcp (como en BankLab) en IIS:
1. Instalar Windows Process Activation Service (WAS).
2. Asegurar que las "Non-HTTP Activation" features estén habilitadas en Windows.
3. Configurar el binding `net.tcp` en el sitio web (ej. `808:*`).
4. Habilitar `net.tcp` en los "Enabled Protocols" de la aplicación.
5. El `.svc` (Service File) actúa como punto de entrada (ej. `ClientesService.svc`).

### Permisos de Base de Datos
Si el WCF en IIS usa `Integrated Security=True` hacia SQL Server, se debe dar permisos de login en la base de datos a la identidad del App Pool (ej. `IIS APPPOOL\BankLabAppPool`).

## Servicios de Windows (Windows Services)

Alternativa para procesos de background que deben arrancar junto al sistema operativo sin un usuario logueado.

### Implementación
- Heredan de `System.ServiceProcess.ServiceBase`.
- Implementan `OnStart()` y `OnStop()`.
- Se requiere un `ProjectInstaller` para la instalación.

### Gestión
```cmd
# Instalar
sc create BankLabService binPath= "C:\Ruta\BankLabService.exe" start= auto
# Iniciar
net start BankLabService
```

### Truco: Modo Consola para Depuración
Depurar un Servicio Windows real es tedioso (requiere Attach to Process). Patrón recomendado:
```csharp
static void Main(string[] args)
{
    if (Environment.UserInteractive)
    {
        // Corre como consola (F5 en Visual Studio)
        Console.WriteLine("Iniciando modo consola...");
        var s = new MiServicio();
        s.Iniciar();
        Console.ReadLine();
        s.Detener();
    }
    else
    {
        // Corre como Servicio Windows real
        ServiceBase.Run(new MiServicio());
    }
}
```

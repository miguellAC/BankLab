# Diagnóstico y Resolución de Errores Comunes

## Errores de Base de Datos y Conexión

### 1. The login is from an untrusted domain and cannot be used with Integrated authentication
- **Causa:** Intentando usar `Integrated Security=SSPI` hacia un SQL Server en Docker u otro dominio.
- **Solución:** Usa SQL Server Authentication cambiando a `User Id=sa;Password=BankLab2024!`.

### 2. A network-related or instance-specific error occurred... (Timeout)
- **Causa:** SQL Server no está ejecutándose o el puerto 1433 está bloqueado.
- **Solución:** Verifica que el contenedor de Docker esté activo (`docker ps`).

### 3. Invalid object name 'dbo.Clientes'
- **Causa:** La base de datos existe pero los scripts no se ejecutaron o se ejecutaron en el orden incorrecto.
- **Solución:** Ejecuta el script `tools\setup-database.ps1`.

### 4. Could not find stored procedure 'sp_Clientes_Buscar'
- **Causa:** El SP no fue creado.
- **Solución:** Revisa los scripts de creación de base de datos.

### 5. Transaction (Process ID) was deadlocked
- **Causa:** Concurrencia alta compitiendo por los mismos recursos sin índices adecuados.
- **Solución:** Capturar el grafo del deadlock con Profiler o Extended Events. Revisar el orden de acceso a tablas.

### 6. The INSERT statement conflicted with the FOREIGN KEY constraint
- **Causa:** Intentando insertar un hijo cuyo padre no existe (ej. un Crédito con un ClienteId inválido).
- **Solución:** Verifica que el dato de referencia exista antes de insertar.

## Errores WCF y Comunicación

### 7. No connection could be made because the target machine actively refused it (127.0.0.1:8808)
- **Causa:** El servicio `WCF.Host` no se está ejecutando.
- **Solución:** Inicia el proyecto `Ejemplo.WCF.Host` (Consola).

### 8. The request channel timed out while waiting for a reply
- **Causa:** Operación en la base de datos toma más tiempo del configurado, o un breakpoint detenido en el host.
- **Solución:** Aumentar `sendTimeout` y `receiveTimeout` en el `App.config`.

### 9. Address already in use (Port conflicts)
- **Causa:** Otra aplicación (u otra instancia del WCF.Host) está ocupando el puerto 8808.
- **Solución:** Cierra instancias huérfanas (`taskkill /F /IM Ejemplo.WCF.Host.exe` en consola).

### 10. The communication object, System.ServiceModel.Channels.ServiceChannel, cannot be used for communication because it is in the Faulted state
- **Causa:** Ocurrió una excepción no controlada previamente en ese proxy y se intentó reusar.
- **Solución:** Recrear el canal (el Agente debe instanciar un nuevo `ChannelFactory`).

## Errores de Compilación y Ensamblados

### 11. Could not load file or assembly 'Ejemplo.Comun' or one of its dependencies
- **Causa:** Orden de compilación incorrecto o falta copiar la DLL.
- **Solución:** Recompilar `Ejemplo.Comun` primero y verificar las referencias.

### 12. Type or namespace could not be found
- **Causa:** Falta un `using` o falta la referencia del proyecto (Project Reference).
- **Solución:** Clic derecho en References -> Add Reference -> Seleccionar el proyecto correcto según la arquitectura.

### 13. System.InvalidOperationException: The 'Microsoft.ACE.OLEDB.12.0' provider is not registered on the local machine
- **Causa:** Procesando un Excel en arquitectura de 64 bits sin tener los drivers instalados, o la app corre en 32 bits y los drivers son 64.
- **Solución:** Cambiar el Target Platform (x86/x64) en las propiedades del proyecto, o instalar el Runtime de Access.

### 14. Crystal Decisions.CrystalReports.Engine.ReportDocument threw an exception
- **Causa:** Runtime de Crystal Reports no instalado en la máquina.
- **Solución:** Instalar el paquete redistribuible de SAP Crystal Reports.

### 15. The version of the .NET Framework is not installed (4.8)
- **Causa:** Solo tienes .NET 8 o versiones antiguas como 4.5.
- **Solución:** Instalar ".NET Framework 4.8 Developer Pack" usando el Visual Studio Installer.

## Errores Web e IIS

### 16. HTTP Error 500.19 - Internal Server Error (Configuración inválida)
- **Causa:** Problemas de formato en `Web.config` (ej. tag mal cerrado).
- **Solución:** Valida el XML del `Web.config`.

### 17. IIS Application Pool Stopped automatically
- **Causa:** Demasiados errores fatales consecutivos (ej. error de permisos de identidad).
- **Solución:** Revisar el Visor de Eventos (Event Viewer) de Windows bajo "Windows Logs -> Application".

### 18. Cross-Origin Request Blocked (CORS) - .NET 8 API
- **Causa:** Llamando al API desde un dominio web diferente sin configurar CORS.
- **Solución:** Agregar `app.UseCors()` en `Program.cs` del API moderna.

### 19. Unable to start Kestrel (Puerto en uso)
- **Causa:** El puerto 5000/5001 ya está en uso.
- **Solución:** Cambiar el puerto en `launchSettings.json` o buscar y matar el proceso (`netstat -ano | findstr 5000`).

### 20. Swagger/OpenAPI UI not loading
- **Causa:** Falta de rutas correctas o ejecutando fuera de entorno Development.
- **Solución:** Verificar que `app.UseSwaggerUI()` esté configurado correctamente en `Program.cs`.

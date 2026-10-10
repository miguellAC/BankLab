# Configuración de Ambientes

Manejar correctamente las configuraciones entre desarrollo, pruebas y producción es crítico.

## .NET Framework 4.8 (Legacy)

### App.config y Web.config
Los valores se configuran en XML usando `<connectionStrings>` y `<appSettings>`.
Para leerlos: `ConfigurationManager.ConnectionStrings["BankLab"].ConnectionString`.

### Transformaciones (Config Transforms)
En proyectos web/WCF, se utilizan transformaciones (ej. `Web.Release.config`) para modificar valores automáticamente al publicar.

## .NET 8 (Moderno)

### appsettings.json
El estándar actual es JSON, fuertemente tipado.
Para leerlos, se inyecta `IConfiguration` o preferiblemente se usa el Patrón Opciones (`IOptions<T>`).

### Variables de Entorno (Environment Variables)
.NET 8 permite sobrescribir cualquier valor del appsettings.json con variables de entorno (muy útil en Docker/Kubernetes).
Ejemplo: `ConnectionStrings__BankLabDb="Server=prod;..."`

## Seguridad (¡Importante!)
**NUNCA** guardes contraseñas reales, tokens de API o cadenas de conexión de producción en el control de código fuente (Git/TFVC).
- Usa archivos `.example` como plantilla.
- En desarrollo local: usa `User Secrets` (secrets.json).
- En producción: usa Azure Key Vault, AWS Secrets Manager o Variables de Entorno seguras.

## Ejercicios

### Ejercicio 1: Modificar la conexión sin recompilar
1. Cierra todas las aplicaciones.
2. Ve a la carpeta `bin\Debug` del `WCF.Host`.
3. Edita `Ejemplo.WCF.Host.exe.config` cambiando el servidor de SQL a una IP errónea.
4. Ejecuta el Host, observa cómo falla sin necesidad de tocar código fuente.

### Ejercicio 2: Cambiar el tamaño de página (.NET 8)
1. Modifica `appsettings.Development.json`.
2. Establece un tamaño de página de `5`.
3. Inicia la API y prueba el endpoint en Swagger. Observa cómo retorna solo 5 registros.

# Instalación y Ejecución de BankLab

Esta guía te guiará paso a paso para configurar tu entorno local.

## 1. Prerrequisitos
- Visual Studio 2022 (con cargas de trabajo de Desarrollo de Escritorio .NET y Desarrollo Web ASP.NET)
- .NET 8 SDK
- Docker Desktop (Para la base de datos local)
- SQL Server Management Studio (SSMS) o Azure Data Studio

## 2. Configurar la Base de Datos (SQL Server en Docker)
Ejecuta en PowerShell:
```powershell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=BankLab2024!" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

## 3. Crear Estructuras de Datos
Ejecuta el script proporcionado para crear la DB y objetos:
```powershell
.\tools\setup-database.ps1
```

## 4. Compilar la Solución Legacy
El orden de compilación es crítico debido a las dependencias:
1. Abre Visual Studio y carga `Ejemplo.Comun.sln`. Compila.
2. Carga `Ejemplo.Componentes.sln`. Compila.
3. Carga `Ejemplo.WCFServicios.sln`. Compila.

## 5. Iniciar el Backend Legacy
Para que las aplicaciones gráficas funcionen, el servicio WCF debe estar ejecutándose:
1. Desde VS, ejecuta `Ejemplo.WCF.Host` o usa el script:
```powershell
.\tools\run-wcf-host.ps1
```
Debe aparecer una consola negra indicando "El servicio está listo en net.tcp://localhost:8808..."

## 6. Iniciar Aplicaciones Cliente (GUI)
1. Carga `Ejemplo.App.sln`.
2. Compila y ejecuta. Se abrirá la aplicación Windows Forms.
3. Prueba la búsqueda de clientes para validar la conexión end-to-end.

## 7. Iniciar la API Moderna
1. Carga `BankLab.Modern.sln`.
2. O ejecuta en consola:
```powershell
.\tools\run-api.ps1
```
3. Se abrirá el navegador en la interfaz de Swagger (ej. `http://localhost:5000/swagger`).

## 8. Solución de Problemas Comunes
Ver el archivo [11-diagnostico-errores.md](11-diagnostico-errores.md) para un listado detallado de soluciones.

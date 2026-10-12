# BankLab - Laboratorio Bancario (Banco Ejemplo)

## ¿Qué es BankLab?
BankLab es un proyecto de laboratorio y estudio que simula un sistema Core Bancario simplificado. 
Demuestra la arquitectura típica de sistemas heredados ("Legacy") basados en .NET Framework 4.8 y WCF, coexistiendo con componentes de modernización basados en .NET 8.

## Prerrequisitos
- Visual Studio 2022 (cargas de trabajo .NET Desktop, Web)
- .NET 8 SDK
- Docker Desktop (Para SQL Server local)

## Inicio Rápido (Quick Start)
1. **Levantar Base de Datos con Docker Compose:**
   ```bash
   docker compose up -d
   ```
2. **Crear Estructura y Semilla:** 
   Ejecutar el script PowerShell desde el host:
   ```powershell
   .\tools\setup-database.ps1
   ```
   *(O alternativamente dentro del contenedor ejecutando `docker exec -it banklab-sqlserver /docker-entrypoint-initdb.d/init-in-container.sh`)*.
3. **Compilar Core:** Abrir y compilar en orden `Ejemplo.Comun.sln`, `Ejemplo.Componentes.sln`, `Ejemplo.WCFServicios.sln`.
4. **Ejecutar Servicios:** Iniciar el Host WCF con `.\tools\run-wcf-host.ps1`.
5. **Usar la App:** Abrir y ejecutar el proyecto UI `Ejemplo.Aplicaciones.sln` (`App.exe` o `ShellBP.exe`).
6. **API Moderna:** Ejecutar `.\tools\run-api.ps1` para iniciar la API REST en .NET 8.

Para instrucciones detalladas, revisa la [Guía de Instalación Completa](docs/10-instalacion-ejecucion.md).

## Mapa de Soluciones Principales
| Solución | Descripción | Tecnología |
|----------|-------------|------------|
| `Ejemplo.Comun` | Entidades y utilidades transversales | .NET 4.8 |
| `Ejemplo.Componentes` | Reglas de Negocio (BLL) y Acceso a Datos (DAS/DAL/DAT) | .NET 4.8 |
| `Ejemplo.WCFServicios`| Interfaces y servicios (Host) net.tcp | .NET 4.8 |
| `Ejemplo.App` | Cliente Windows Forms principal | .NET 4.8 |
| `BankLab.Modern` | Nueva API para integración REST | .NET 8 |

## Licencia
Uso educativo exclusivo. Los datos, escenarios y la entidad "Banco Ejemplo" son puramente ficticios.

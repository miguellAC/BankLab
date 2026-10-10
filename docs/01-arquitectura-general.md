# Arquitectura General de BankLab

## Visión General
BankLab es un laboratorio bancario simulado para "Banco Ejemplo". La plataforma demuestra la coexistencia de aplicaciones legacy y modernas, estructurando el sistema en múltiples soluciones para representar un ecosistema empresarial realista.

## Soluciones
El sistema se compone de 10 soluciones:
- 9 Soluciones Legacy (.NET Framework 4.8)
- 1 Solución Moderna (.NET 8)

### Diagrama de Arquitectura

```mermaid
flowchart TD
    subgraph Moderno [.NET 8]
        API[BankLab.Modern API]
    end

    subgraph Presentacion [GUI Legacy]
        App[Ejemplo.App]
        ShellBP[Ejemplo.ShellBP]
        Web[Ejemplo.PortalesWeb]
    end

    subgraph Servicios [WCF]
        WCFHost[WCF Host / IIS]
    end

    subgraph NegocioDatos [Lógica y Datos]
        BLL[BLL - Business Logic]
        DAS[DAS - Transaction Layer]
        DAL[DAL - Data Access Logic]
        DAT[DAT - Data Access Tech]
    end

    subgraph BD [Base de Datos]
        SQL[(BankLabDB)]
    end

    App -->|WCF NetTcp Agent| WCFHost
    ShellBP -->|WCF NetTcp Agent| WCFHost
    Web -->|WCF Agent| WCFHost
    API -->|ADO.NET| SQL

    WCFHost --> BLL
    BLL --> DAS
    DAS --> DAL
    DAL --> DAT
    DAT --> SQL
```

## Stack Tecnológico por Solución
- **Ejemplo.Comun**: .NET Fx 4.8 (DTOs, Utilidades, Extensiones)
- **Ejemplo.Componentes**: .NET Fx 4.8 (BLL, DAS, DAL, DAT)
- **Ejemplo.WCFServicios**: .NET Fx 4.8 (Contratos, Implementación WCF)
- **Ejemplo.App / ShellBP**: .NET Fx 4.8 (WinForms)
- **Ejemplo.PortalesWeb**: .NET Fx 4.8 (ASP.NET MVC 5)
- **BankLab.Modern**: .NET 8 (ASP.NET Core Web API)
- **Base de Datos**: SQL Server 2022+ (Procedimientos Almacenados)

## Flujo de Datos Legacy
El patrón de capas legacy sigue una estructura estricta:
GUI → WCF Agent → WCF Service → BLL (Business Logic) → DAS (Data Access Services / Transaction) → DAL (Data Access Logic) → DAT (Data Access Technology) → SQL Server (Stored Procedures)

## Flujo de Datos Moderno
El enfoque moderno simplifica las capas aprovechando inyección de dependencias:
HTTP Client → ASP.NET Core Controller → Application Service → Repository (ADO.NET puro) → SQL Server (Stored Procedures)

Ambas aproximaciones comparten la misma base de datos relacional `BankLabDB`.

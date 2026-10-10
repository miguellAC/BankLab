# Decisiones Arquitectónicas (ADRs)

Este documento registra los "Architecture Decision Records" (ADRs) de BankLab.

## ADR 1: Host WCF como Consola Auto-hospedada
- **Contexto:** Se requiere hospedar servicios WCF para entornos locales de estudiantes.
- **Decisión:** Usar una aplicación de Consola (`Ejemplo.WCF.Host`) en lugar de IIS+WAS para desarrollo local.
- **Consecuencias:** Fácil depuración con un solo F5. Falta de auto-reinicio y features de IIS.

## ADR 2: Tecnología Web Legacy
- **Contexto:** Se requiere mostrar un portal web en el ecosistema legacy.
- **Decisión:** Usar ASP.NET MVC 5 (.NET 4.8) llamando a WCF (Agentes) en el controlador, evitando lógica de datos directa.
- **Consecuencias:** Reutilización de BLL, pero serialización doble (SQL->BLL->WCF->MVC->JSON).

## ADR 3: Estrategia de Referencias de Ensamblados
- **Contexto:** Manejo de dependencias DLL.
- **Decisión:** Usar "Project References" estrictamente en el orden definido. Prohibición de referencias circulares (ej. BLL no puede referenciar a Agentes).
- **Consecuencias:** Tiempos de compilación predecibles, arquitectura limpia asegurada por el compilador.

## ADR 4: Separación Legacy / Moderno
- **Contexto:** Introducción de .NET 8.
- **Decisión:** El código moderno (`BankLab.Modern`) vivirá en soluciones separadas y NO interactuará con el código WCF ni .NET 4.8. Ambos compartirán la misma base de datos SQL.
- **Consecuencias:** Duplicación de modelos de datos, pero aislamiento seguro (patrón Strangler Fig).

## ADR 5: Frameworks de Pruebas
- **Contexto:** Implementación de pruebas automatizadas.
- **Decisión:** Usar `MSTest` para proyectos legacy (por compatibilidad histórica) y `xUnit` para la API Moderna.
- **Consecuencias:** Dos estilos de sintaxis a mantener (`[TestMethod]` vs `[Fact]`).

## ADR 6: Reportes con Fallback
- **Contexto:** Complejidad de instalar Crystal Reports.
- **Decisión:** Proveer componentes UI con DataGridView de exportación a CSV como fallback si el sistema de Crystal falla.
- **Consecuencias:** Menor dependencia de librerías de terceros; peor presentación de reportes por defecto.

## ADR 7: Control Transaccional en DAS
- **Contexto:** Manejo de transacciones en escenarios multi-tabla (ej. Créditos y Cuotas).
- **Decisión:** El `SqlTransaction` se inicia en la capa DAS, pasándose a DAL y luego a DAT. NUNCA se inician transacciones SQL explícitas en los Stored Procedures para evitar conflictos.
- **Consecuencias:** La capa intermedia controla qué métodos particionan transacciones. Complejidad al pasar el objeto `DbConnection`.

## ADR 8: Contenerización de Base de Datos
- **Contexto:** Configuración local de SQL Server.
- **Decisión:** Usar SQL Server 2022 en Docker.
- **Consecuencias:** Ambientes limpios garantizados, no ensucia el host de Windows del desarrollador.

## ADR 9: Persistencia en .NET 8
- **Contexto:** Elección de ORM moderno.
- **Decisión:** Se rechaza Entity Framework Core. Se mantiene ADO.NET usando Dapper o primitivas para reaprovechar la gran inversión en Stored Procedures preexistentes.
- **Consecuencias:** Más código manual (boilerplace) para mapear DTOs. Mayor control de ejecución.

## ADR 10: Patrón Dual Shell (App y ShellBP)
- **Contexto:** Sistemas monolíticos de UI.
- **Decisión:** Separar interfaces bancarias internas (`ShellBP` - BackOffice) de interfaces ligeras o canales (App). Compartirán controles `USC`.
- **Consecuencias:** Reutilización parcial, separación de responsabilidades visuales.

# Fase 1 - Línea base de compilación (legacy)

> **Aviso:** ninguna de estas compilaciones equivale a una validación con Visual Studio / MSBuild en Windows con las herramientas de .NET Framework 4.8. Son evidencia aproximada obtenida en Linux. Falta validar en Windows.

Entorno: Mono (`xbuild`/`mcs`) en Linux, proyecto a proyecto con
`/p:TargetFrameworkVersion=v4.5` (no hay targeting pack 4.8 ni MSBuild de Visual Studio).
Es una aproximación: los errores de código (CS0117, CS0246, ...) son los que reportaría
el compilador de Windows; los de plataforma/referencias de GUI no se consideran concluyentes.
Pendiente confirmar con Visual Studio 2022 / MSBuild en Windows.

## Resultado inicial (commit baseline)

| Proyecto | Resultado |
|---|---|
| Comun.Enumeraciones, DTOs, Utilidades, Validaciones | Compilan |
| WCF.Contratos | Error: `CuotaCreditoDTO`, `PagoRealizadoDTO`, namespace `DTOs.Pagos`, `MovimientoCuentaDTO` no existen |
| Componentes.DAT | Error: falta referencia a `System.Xml` (AuditoriaDAT.cs:68); tras corregirla, 26 errores de miembros de DTO inexistentes |
| DAL, DAS, BLL | Fallan por cascada desde DAT |
| WCF.Servicios, Agentes, Host | Fallan por cascada (Contratos / Componentes) |
| GUI.* | No concluyente con Mono (error de plataforma de xbuild); referencias rotas confirmadas por inspección |
| App, ShellBP | Fallan por cascada desde WCF.Contratos |

## Errores de DAT por archivo (tras añadir System.Xml)

CarteraDAT 4, ClientesDAT 8, CreditosDAT 8, CuentasDAT 4, ImportacionDAT 1, PagosDAT 1.
Causa común: el DAT asigna propiedades que los DTO no tienen (p. ej. `SaldoContable`,
`TipoCuenta`, `PaginacionDTO.Data` frente a `Items`, `ClienteDTO.Nombres`, `CuotaDTO.Capital`).

## Confirmado por inspección estática (no por compilador)

- Miembros inexistentes en BLL/Servicio: `RespuestaOperacion.Exito/Error/CrearExito`, `LogUtil`.
- Contrato `ConsultarSaldoCuenta` (decimal) vs Agente (`SaldoDTO`) vs Servicio.

## Segunda medición: MSBuild + Roslyn contra referencias de .NET 4.8 (Linux)

Entorno: `dotnet msbuild` 8.0 con `FrameworkPathOverride=/usr/lib/mono/4.8-api` (ensamblados de referencia
4.8 incluidos con Mono), sin forzar el framework de los proyectos (se usa v4.8 de los `.csproj`).
Sigue sin ser Visual Studio en Windows, pero usa la lógica real de MSBuild y el compilador Roslyn,
más completo que `mcs`. Revisa y corrige la medición anterior:

- **`System.Xml` en DAT (AuditoriaDAT.cs:68): no se reproduce** con Roslyn/4.8; era un artefacto de `mcs` (4.5 forzado). Se retira como hallazgo.
- **DAT: 28 errores** (no 26): CarteraDAT 4, ClientesDAT 8, CreditosDAT 8, CuentasDAT 6, ImportacionDAT 1, PagosDAT 1.
  `mcs` no mostró en CuentasDAT que `MovimientoDTO` no tiene `Fecha` ni `TipoMovimiento` (líneas 85 y 87).
- **WCF.Contratos: 6 errores**: `CuotaCreditoDTO`, `MovimientoCuentaDTO`, `PagoRealizadoDTO`, `PagoDTO` y el namespace `DTOs.Pagos`.
- **Configuración de proyecto (confirmado con MSBuild real, ya no atribuible a Mono):** 10 `.csproj` no definen
  `OutputPath` por configuración (sin PropertyGroup `Debug|AnyCPU`/`Release|AnyCPU`): `GUI.Comun`, `GUI.Clientes`,
  `GUI.Cuentas`, `GUI.Creditos`, `GUI.Reportes`, `Web.Portal*` (3) y `Servicios.Comun`/`ProcesadorPagos`.
  Resultado: "The BaseOutputPath/OutputPath property is not set". Esto bloquearía también la compilación en Visual Studio.
- El resto de proyectos legacy falla en cascada desde DAT/Contratos.

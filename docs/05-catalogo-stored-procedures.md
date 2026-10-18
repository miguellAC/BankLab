# Catálogo de Stored Procedures

> Generado a partir de los scripts reales de `database/04-stored-procedures/` (fase 1). Sustituye al catálogo anterior,
> cuyos nombres (`sp_Cuentas_ObtenerSaldo`, `sp_Clientes_Buscar`, ...) **no existen** en los scripts SQL.
> Los parámetros, el uso de transacciones y TRY/CATCH se extrajeron del texto de cada script; el contrato de columnas
> de los resultsets **solo se ha verificado a mano para el módulo Cuentas** (ver abajo). Para el resto, el script manda.
> La columna "Llamado desde C# legacy" indica si el nombre aparece como cadena en `src/legacy` (no implica que ese código compile).

El acceso a datos del entorno **legacy** se realiza mediante estos procedimientos. La API .NET 8 (`src/modern`) **no** los usa en todos los casos:
`CuentaRepository` ejecuta SQL en línea (fuera del alcance de la fase 1).


## Módulo: Auditoría (`sp_auditoria.sql`)

| Procedimiento | Parámetros | Transacción explícita | TRY/CATCH | Llamado desde C# legacy | Descripción |
|---|---|---|---|---|---|
| `sp_RegistrarAuditoria` | `@TipoEntidad VARCHAR(50)`<br>`@EntidadId INT`<br>`@Accion VARCHAR(20)`<br>`@ValorAnterior VARCHAR(MAX) = NULL`<br>`@ValorNuevo VARCHAR(MAX) = NULL`<br>`@DireccionIP VARCHAR(50) = NULL`<br>`@NombreEquipo VARCHAR(100) = NULL`<br>`@Usuario VARCHAR(50) = NULL`<br>`@Equipo VARCHAR(100) = NULL` | No | Sí | Sí |  |
| `sp_ConsultarAuditoriaEntidad` | `@TipoEntidad VARCHAR(50)`<br>`@EntidadId INT`<br>`@FechaDesde DATE = NULL`<br>`@FechaHasta DATE = NULL`<br>`@Desde DATE = NULL`<br>`@Hasta DATE = NULL` | No | No | Sí |  |
| `sp_ConsultarAuditoriaUsuario` | `@Usuario VARCHAR(50)`<br>`@FechaDesde DATE`<br>`@FechaHasta DATE` | No | No | No |  |

## Módulo: Cartera (`sp_cartera.sql`)

| Procedimiento | Parámetros | Transacción explícita | TRY/CATCH | Llamado desde C# legacy | Descripción |
|---|---|---|---|---|---|
| `sp_ReporteCartera` | `@FechaCorte DATE = NULL`<br>`@SucursalId INT = NULL`<br>`@Estado VARCHAR(20) = NULL`<br>`@CategoriaRiesgo CHAR(1) = NULL` | No | No | No |  |
| `sp_ReporteCarteraVencida` | `@DiasVencidoMinimo INT = 1` | No | No | No |  |
| `sp_ReporteResumenRiesgo` | — | No | No | No |  |

## Módulo: Clientes (`sp_clientes.sql`)

| Procedimiento | Parámetros | Transacción explícita | TRY/CATCH | Llamado desde C# legacy | Descripción |
|---|---|---|---|---|---|
| `sp_ConsultarClientePorIdentificacion` | `@TipoIdentificacionId INT = NULL`<br>`@NumeroIdentificacion VARCHAR(20)`<br>`@TipoIdentificacion INT = NULL` | No | No | Sí | Consulta datos básicos de un cliente por su identificación. |
| `sp_ConsultarClientePorId` | `@ClienteId INT` | No | No | Sí | Consulta el detalle completo de un cliente por su ID. |
| `sp_BuscarClientes` | `@Nombres VARCHAR(100) = NULL`<br>`@Apellidos VARCHAR(100) = NULL`<br>`@NumeroIdentificacion VARCHAR(20) = NULL`<br>`@Estado VARCHAR(20) = NULL`<br>`@SucursalId INT = NULL`<br>`@PageNumber INT = 1`<br>`@PageSize INT = 20` | No | No | Sí | Búsqueda paginada de clientes. |
| `sp_RegistrarCliente` | `@TipoIdentificacionId INT = NULL`<br>`@NumeroIdentificacion VARCHAR(20)`<br>`@PrimerNombre VARCHAR(50) = NULL`<br>`@SegundoNombre VARCHAR(50) = NULL`<br>`@PrimerApellido VARCHAR(50) = NULL`<br>`@SegundoApellido VARCHAR(50) = NULL`<br>`@Nombres VARCHAR(100) = NULL`<br>`@Apellidos VARCHAR(100) = NULL`<br>`@TipoIdentificacion INT = NULL`<br>`@FechaNacimiento DATE = NULL`<br>`@Genero CHAR(1) = 'O'`<br>`@Email VARCHAR(100) = NULL`<br>`@Telefono VARCHAR(20) = NULL`<br>`@SucursalId INT = 1`<br>`@Direccion VARCHAR(200) = NULL`<br>`@Ciudad VARCHAR(100) = 'Bogotá'`<br>`@Usuario VARCHAR(50) = NULL`<br>`@UsuarioAuditoria VARCHAR(50) = NULL`<br>`@ClienteId INT OUTPUT` | Sí | Sí | Sí | Registra un cliente junto con sus direcciones y contactos. |
| `sp_ConsultarProductosCliente` | `@ClienteId INT` | No | No | No | Consulta cuentas y operaciones de crédito de un cliente. |

## Módulo: Créditos (`sp_creditos.sql`)

| Procedimiento | Parámetros | Transacción explícita | TRY/CATCH | Llamado desde C# legacy | Descripción |
|---|---|---|---|---|---|
| `sp_ConsultarOperacionCredito` | `@OperacionId INT = NULL`<br>`@NumeroOperacion VARCHAR(20) = NULL` | No | No | Sí | Consulta el detalle completo de una operación de crédito. |
| `sp_ConsultarSaldosVigenteVencido` | `@OperacionId INT` | No | No | Sí | Consulta los saldos vigentes y vencidos de una operación. |
| `sp_ConsultarCuotasOperacion` | `@OperacionId INT`<br>`@SoloVencidas BIT = 0`<br>`@SoloPendientes BIT = 0` | No | No | Sí | Consulta el plan de cuotas de una operación. |
| `sp_ConsultarCuotasPendientesVencidas` | `@ClienteId INT = NULL` | No | No | Sí | Consulta cuotas vencidas o pendientes de clientes. |

## Módulo: Cuentas (`sp_cuentas.sql`)

| Procedimiento | Parámetros | Transacción explícita | TRY/CATCH | Llamado desde C# legacy | Descripción |
|---|---|---|---|---|---|
| `sp_ConsultarCuentasCliente` | `@ClienteId INT` | No | No | Sí | Consulta las cuentas de un cliente específico. |
| `sp_ConsultarMovimientosCuenta` | `@CuentaId INT`<br>`@FechaDesde DATE = NULL`<br>`@FechaHasta DATE = NULL`<br>`@Desde DATE = NULL`<br>`@Hasta DATE = NULL`<br>`@PageNumber INT = 1`<br>`@PageSize INT = 50` | No | No | Sí | Consulta movimientos de cuenta con paginación. |
| `sp_ConsultarSaldoCuenta` | `@CuentaId INT` | No | No | Sí | Consulta el saldo actual y disponible de una cuenta. |

## Módulo: Diferimientos (`sp_diferimientos.sql`)

| Procedimiento | Parámetros | Transacción explícita | TRY/CATCH | Llamado desde C# legacy | Descripción |
|---|---|---|---|---|---|
| `sp_ConsultarClientesIncluidosDiferimiento` | `@SolicitudDifId INT` | No | No | No |  |
| `sp_ConsultarClientesExcluidosDiferimiento` | `@SolicitudDifId INT` | No | No | No |  |
| `sp_ConsultarOperacionesDiferidas` | `@SolicitudDifId INT = NULL`<br>`@ClienteId INT = NULL` | No | No | No |  |

## Módulo: Importación (`sp_importacion.sql`)

| Procedimiento | Parámetros | Transacción explícita | TRY/CATCH | Llamado desde C# legacy | Descripción |
|---|---|---|---|---|---|
| `sp_CrearLoteImportacion` | `@NombreArchivo VARCHAR(200)`<br>`@TipoImportacion VARCHAR(30)`<br>`@TotalRegistros INT`<br>`@Usuario VARCHAR(50)`<br>`@LoteId INT OUTPUT` | Sí | Sí | Sí | Crea un nuevo lote de importación. |
| `sp_InsertarDetalleImportacion` | `@LoteId INT`<br>`@NumeroLinea INT`<br>`@ContenidoLinea VARCHAR(MAX)` | No | Sí | Sí | Inserta un registro detalle para un lote. |
| `sp_ProcesarLoteImportacion` | `@LoteId INT`<br>`@Usuario VARCHAR(50)` | Sí | Sí | Sí | Marca un lote como procesado. |
| `sp_ConsultarEstadoLote` | `@LoteId INT` | No | No | Sí | Consulta el estado de un lote. |

## Módulo: Pagos (`sp_pagos.sql`)

| Procedimiento | Parámetros | Transacción explícita | TRY/CATCH | Llamado desde C# legacy | Descripción |
|---|---|---|---|---|---|
| `sp_RegistrarPago` | `@OperacionId INT`<br>`@MontoPago DECIMAL(18,2)`<br>`@TipoPago VARCHAR(20)`<br>`@CanalPago VARCHAR(20)`<br>`@CuentaOrigenId INT = NULL`<br>`@Referencia VARCHAR(50) = NULL`<br>`@Usuario VARCHAR(50)`<br>`@PagoId BIGINT OUTPUT` | Sí | Sí | Sí | Registra y aplica un pago a una operación de crédito. |
| `sp_ConsultarPagosOperacion` | `@OperacionId INT`<br>`@FechaDesde DATE = NULL`<br>`@FechaHasta DATE = NULL` | No | No | Sí | Consulta pagos de una operación. |

Total: **27** procedimientos.

> **Discrepancia pendiente (fase 2):** `CarteraDAT.cs` (líneas 19 y 63) llama a `sp_ObtenerReporteCartera` y `sp_ObtenerReporteCarteraVencida`,
> que no existen; en SQL se llaman `sp_ReporteCartera` y `sp_ReporteCarteraVencida`. No se ha corregido (módulo ajeno a la rebanada).

## Contrato de resultados verificado (módulo Cuentas)

Verificado contra `CuentasDAT.cs` (cada columna que lee el DAT existe en el SELECT del SP):

- `sp_ConsultarSaldoCuenta(@CuentaId)`: un resultset con `CuentaId, NumeroCuenta, Saldo, SaldoContable (alias de Saldo), SaldoDisponible, Estado, FechaUltimoMovimiento`. `CuentasDAT` mapea a `SaldoDTO` usando `Saldo` (no el alias).
- `sp_ConsultarCuentasCliente(@ClienteId)`: un resultset con los datos de `CuentaDTO` (incluye alias `TipoCuenta`, `Sucursal`, `SaldoActual` que el DAT ya no usa).
- `sp_ConsultarMovimientosCuenta(@CuentaId, @FechaDesde/@Desde, @FechaHasta/@Hasta, @PageNumber, @PageSize)`: **dos resultsets**: 1) `Total` (y `TotalRegistros`); 2) filas de movimientos con los campos de `MovimientoDTO` (más alias `TipoMovimiento`, `Fecha`, `SaldoDespues`).

## Ejemplo de uso (verificado contra el script, no ejecutado en SQL Server)

```sql
EXEC dbo.sp_ConsultarSaldoCuenta @CuentaId = 1;
```


## Consideraciones de Rendimiento
- Los procedimientos de búsqueda (`_Buscar`) utilizan `OFFSET` y `FETCH NEXT` en lugar de `TOP` para permitir una paginación eficiente en la base de datos, evitando transferir miles de registros a la capa intermedia.
- Los procedimientos transaccionales verifican `@@TRANCOUNT` para comportarse correctamente tanto si se llaman dentro de una transacción desde DAS, como si se llaman independientemente.

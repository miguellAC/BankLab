# Fase 1 - Errores de la compilación completa (sin rebanada)

Compilación **sin** `SliceSaldo`: es lo que ven las soluciones normales. Medición en Linux con MSBuild + Roslyn contra los
ensamblados de referencia de .NET Framework 4.8 que trae Mono (`FrameworkPathOverride=/usr/lib/mono/4.8-api`).
**No es una validación con Visual Studio / MSBuild en Windows** y debe repetirse allí (`tools\build-completo.ps1`).

## Qué cambió respecto a la línea base (y por qué)

| Elemento | Línea base | Ahora | Motivo |
|---|---|---|---|
| `WCF.Contratos` | 6 errores | **5** | Se corrigió `ICuentasServicio` (tipo inexistente `MovimientoCuentaDTO`). Quedan los de Créditos y Pagos |
| `Componentes.DAT` | 28 errores | **20** | Cuentas: 6 → 0 (mapeo a las columnas del SP y propiedades reales de los DTO). Clientes: 8 → 6 (`Data` → `Items`, corrección compartida declarada) |
| `GUI.Comun`, `GUI.Cuentas` | `OutputPath` no definido | Compilan `GUI.Comun`; `GUI.Cuentas` solo falla por Contratos | Se añadieron los PropertyGroups `Debug/Release|AnyCPU` |
| `GUI.Clientes`, `GUI.Creditos`, `GUI.Reportes` | refs rotas + sin `OutputPath` | Siguen sin `OutputPath` | Fuera del alcance (módulos ajenos) |
| `GUI.Cuentas` | no dependía de WCF | Depende de `Agentes`, `Contratos` y `DTOs` | Necesario para que `FrmSaldos` llame al agente. En la compilación completa hereda los errores de `Contratos`; en la rebanada compila |

No se forzó que el inventario permaneciera idéntico: solo desaparecieron los errores de Cuentas y los 2 de `Data`→`Items` en `ClientesDAT`.

## Errores que permanecen (módulos ajenos a Cuentas)

### Ejemplo.WCF.Contratos

- `ICreditosServicio.cs(18,33): error CS0246: The type or namespace name 'CuotaCreditoDTO' could not be found`
- `IPagosServicio.cs(12,28): error CS0246: The type or namespace name 'PagoRealizadoDTO' could not be found`
- `IPagosServicio.cs(12,60): error CS0246: The type or namespace name 'PagoDTO' could not be found`
- `IPagosServicio.cs(15,33): error CS0246: The type or namespace name 'PagoRealizadoDTO' could not be found`
- `IPagosServicio.cs(3,26): error CS0234: The type or namespace name 'Pagos' does not exist in the namespace 'Ejemplo.Comun.DTOs'`

### Ejemplo.Componentes.DAT

- `CarteraDAT.cs(35,33): error CS0117: 'ReporteCarteraDTO' does not contain a definition for 'SaldoCapital'`
- `CarteraDAT.cs(36,33): error CS0117: 'ReporteCarteraDTO' does not contain a definition for 'DiasMora'`
- `CarteraDAT.cs(76,33): error CS0117: 'ReporteCarteraDTO' does not contain a definition for 'SaldoCapital'`
- `CarteraDAT.cs(77,33): error CS0117: 'ReporteCarteraDTO' does not contain a definition for 'DiasMora'`
- `ClientesDAT.cs(134,78): error CS1061: 'ClienteDTO' does not contain a definition for 'Nombres'`
- `ClientesDAT.cs(135,80): error CS1061: 'ClienteDTO' does not contain a definition for 'Apellidos'`
- `ClientesDAT.cs(136,89): error CS1061: 'ClienteDTO' does not contain a definition for 'TipoIdentificacion'`
- `ClientesDAT.cs(161,17): error CS0117: 'ClienteDTO' does not contain a definition for 'Nombres'`
- `ClientesDAT.cs(162,17): error CS0117: 'ClienteDTO' does not contain a definition for 'Apellidos'`
- `ClientesDAT.cs(163,17): error CS0117: 'ClienteDTO' does not contain a definition for 'TipoIdentificacion'`
- `CreditosDAT.cs(113,33): error CS0117: 'CuotaDTO' does not contain a definition for 'Capital'`
- `CreditosDAT.cs(114,33): error CS0117: 'CuotaDTO' does not contain a definition for 'Interes'`
- `CreditosDAT.cs(155,33): error CS0117: 'CuotaDTO' does not contain a definition for 'Capital'`
- `CreditosDAT.cs(156,33): error CS0117: 'CuotaDTO' does not contain a definition for 'Interes'`
- `CreditosDAT.cs(32,33): error CS0117: 'OperacionCreditoDTO' does not contain a definition for 'MontoOriginal'`
- `CreditosDAT.cs(33,33): error CS0117: 'OperacionCreditoDTO' does not contain a definition for 'TasaInteres'`
- `CreditosDAT.cs(70,33): error CS0117: 'SaldoOperacionDTO' does not contain a definition for 'SaldoVigente'`
- `CreditosDAT.cs(71,33): error CS0117: 'SaldoOperacionDTO' does not contain a definition for 'SaldoVencido'`
- `ImportacionDAT.cs(97,33): error CS0117: 'LoteImportacionDTO' does not contain a definition for 'FechaRegistro'`
- `PagosDAT.cs(63,33): error CS0117: 'PagoDTO' does not contain a definition for 'Monto'`

DAL, DAS, BLL, `WCF.Servicios`, `WCF.Host` y las aplicaciones fallan en cascada por estos errores (y `GUI.Clientes/Creditos/Reportes` por `OutputPath`).
Patrones comunes que se corregirán en la fase 2: DTO sin las propiedades que usa el DAT (`Nombres`, `Capital`, `Interes`, `SaldoCapital`, ...),
tipos de contrato inexistentes (`CuotaCreditoDTO`, `PagoRealizadoDTO`, namespace `DTOs.Pagos`) y, por inspección estática (aún no reportado por el
compilador porque el DAT falla antes), el uso de `RespuestaOperacion.Exito/Error/CrearExito` y `LogUtil` en las BLL y servicios de los demás módulos.

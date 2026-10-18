# Fase 1 - Rebanada "Saldo": exclusiones y uso

La rebanada es una **solución de laboratorio independiente** (`src/legacy/Ejemplo.Saldo/Ejemplo.Saldo.sln`) que reutiliza el código
existente. Las soluciones normales no se modifican. Los archivos excluidos **no se borran ni se editan**: se quitan de la
compilación solo cuando se pasa la propiedad `SliceSaldo=true` (definida en `src/legacy/Directory.Build.targets`).
No se activa por el nombre de la solución.

## Cómo se activa
- `tools\build-saldo.ps1` (compila la solución con `/p:SliceSaldo=true`).
- `tools\abrir-saldo-vs.ps1` (abre Visual Studio con la variable de entorno `SliceSaldo=true` solo para ese proceso).
- Manual: `msbuild src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.sln /p:SliceSaldo=true`.

Al compilar con la rebanada activa, cada proyecto afectado imprime `[REBANADA SALDO ACTIVA] <proyecto>: N archivo(s) excluido(s): ...`.

## Archivos excluidos (36 en 7 proyectos)
| Proyecto | N | Archivos excluidos |
|---|---|---|
| `Ejemplo.Componentes.DAT` | 6 | `ClientesDAT.cs`, `CreditosDAT.cs`, `PagosDAT.cs`, `CarteraDAT.cs`, `ImportacionDAT.cs`, `AuditoriaDAT.cs` |
| `Ejemplo.Componentes.DAL` | 6 | `ClientesDAL.cs`, `CreditosDAL.cs`, `PagosDAL.cs`, `CarteraDAL.cs`, `ImportacionDAL.cs`, `AuditoriaDAL.cs` |
| `Ejemplo.Componentes.DAS` | 6 | `ClientesDAS.cs`, `CreditosDAS.cs`, `PagosDAS.cs`, `CarteraDAS.cs`, `ImportacionDAS.cs`, `AuditoriaDAS.cs` |
| `Ejemplo.Componentes.BLL` | 6 | `ClientesBLL.cs`, `CreditosBLL.cs`, `PagosBLL.cs`, `CarteraBLL.cs`, `ImportacionBLL.cs`, `AuditoriaBLL.cs` |
| `Ejemplo.WCF.Contratos` | 4 | `IClientesServicio.cs`, `ICreditosServicio.cs`, `IPagosServicio.cs`, `ICarteraServicio.cs` |
| `Ejemplo.WCF.Servicios` | 4 | `ClientesServicio.cs`, `CreditosServicio.cs`, `PagosServicio.cs`, `CarteraServicio.cs` |
| `Ejemplo.WCF.Agentes` | 4 | `ClientesAgente.cs`, `CreditosAgente.cs`, `PagosAgente.cs`, `CarteraAgente.cs` |

No se excluye nada en `Comun.*`, `GUI.*`, ni en los proyectos nuevos. En cada proyecto se conserva: `DAT` (`ConexionHelper`, `CuentasDAT`),
`DAL` (`CuentasDAL`), `DAS` (`TransaccionHelper`, `CuentasDAS`), `BLL` (`CuentasBLL`), `Contratos` (`ICuentasServicio`), `Servicios` (`CuentasServicio`),
`Agentes` (`AgenteBase`, `CuentasAgente`).

## Por qué hay proyectos nuevos de host y cliente
- **Host** (`Ejemplo.Saldo.Host`): `Ejemplo.WCF.Host/Program.cs` instancia los 5 servicios con `typeof(...)`; con 4 excluidos no compilaría. El host nuevo (≈40 líneas, sin lógica) aloja solo `CuentasServicio`.
- **Cliente** (`Ejemplo.Saldo.Cliente`): se evaluó reutilizar `Ejemplo.App` y `Ejemplo.ShellBP`. No es viable sin tocarlos: ambos referencian `GUI.Clientes`, `GUI.Creditos` y `GUI.Reportes`
  (referencian agentes/BLL de módulos excluidos y no tienen `OutputPath`), y `ShellBP` registra sus formularios con `typeof(...)` de esos proyectos.
  El cliente nuevo (≈20 líneas) solo abre `FrmSaldos` del proyecto existente `Ejemplo.GUI.Cuentas`.

## Cómo reincorporar un módulo (fase 2)
Borrar sus líneas `Compile Remove` y `SliceSaldoExcluido` del `Directory.Build.targets`, corregir sus errores (ver `fase1-errores-compilacion-completa.md`) y,
cuando todo compile, eliminar el archivo y los dos proyectos `Ejemplo.Saldo.*`.

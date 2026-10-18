# Fase 1 - Informe: consulta de saldo de extremo a extremo (rebanada "Saldo")

Estado honesto: **la rebanada compila (en Linux) y el recorrido WCF→servicio→BLL→DAS→DAL→DAT se ejercitó hasta el intento de conexión a SQL Server,
pero la consulta de saldo con datos reales NO se ha ejecutado** (no hay SQL Server ni Windows en el entorno de trabajo).

## 1. Qué se cambió
- Contrato `ICuentasServicio` alineado con agente/BLL/DAS/DAL/DAT: `ConsultarSaldoCuenta → RespuestaOperacion<SaldoDTO>` y
  `ConsultarMovimientosCuenta(cuentaId, fechaDesde, fechaHasta, pageNumber, pageSize) → RespuestaOperacion<PaginacionDTO<MovimientoDTO>>`.
  Consumidores verificados: solo `CuentasAgente` (ya tenía esa forma), `CuentasServicio` y el host. Ningún GUI, test ni portal lo usa. No se cambió ningún DTO ni el esquema SQL.
- `CuentasBLL`: `CrearExitosa/CrearError` (los métodos reales de `RespuestaOperacion`).
- `CuentasServicio`: reenvía la respuesta de la BLL (antes la envolvía dos veces con métodos y tipos inexistentes) y usa `LogHelper`.
- `CuentasDAT`: mapea las columnas reales de cada SP a las propiedades existentes de los DTO (`Saldo`, `NombreTipoCuenta`, `FechaMovimiento`, ...); `Data` → `Items`.
  `ClientesDAT`: solo `Data` → `Items` (2 líneas, corrección compartida).
- `ConexionHelper`: la cadena sale de `BANKLAB_CONNECTION_STRING` o de `connectionStrings["BankLabDB"]`; sin credenciales en el código. Si falta o es la plantilla (`COMPLETAR`) lanza un mensaje claro.
- `FrmSaldos`: llama a `CuentasAgente.ConsultarSaldoCuenta`; valida el Id, muestra el saldo disponible o el mensaje de error. La etiqueta pasó de "Número de Cuenta" a "Id de Cuenta" porque la operación recibe `CuentaId`.
- `GUI.Comun` y `GUI.Cuentas`: `OutputPath` por configuración; `GUI.Cuentas` referencia Agentes/Contratos/DTOs.
- Nuevo: `Directory.Build.targets` (rebanada, solo con `SliceSaldo=true`), `Ejemplo.Saldo.sln`, `Ejemplo.Saldo.Host`, `Ejemplo.Saldo.Cliente`, scripts `tools\build-saldo.ps1`, `abrir-saldo-vs.ps1`, `build-completo.ps1`.
- Documentación: catálogo de SP regenerado desde los scripts reales, exclusiones, errores de la compilación completa.

## 2. Compilación de la rebanada
`dotnet msbuild Ejemplo.Saldo.sln -p:SliceSaldo=true` (Linux, Roslyn + referencias 4.8 de Mono): **0 errores**, 1 advertencia ya existente (`AgenteBase.cs(104,43)` CS0168), 14 proyectos incluidos Host y Cliente.
Desde una copia limpia del árbol final. **No es una validación en Visual Studio/Windows.**

## 3. Compilación completa (sin rebanada)
Ver `fase1-errores-compilacion-completa.md`: `WCF.Contratos` 5 errores y `Componentes.DAT` 20, todos de módulos ajenos a Cuentas; el resto falla en cascada.
La rebanada no oculta nada: la compilación normal sigue fallando por esos módulos.

## 4. Pruebas realmente ejecutadas (Linux/Mono, no Windows)
1. Compilación de la rebanada y de la compilación completa (arriba).
2. Verificación estática: las 35 columnas que lee `CuentasDAT` existen en el SELECT de su SP (script de comparación).
3. Host `Ejemplo.Saldo.Host.exe` bajo Mono + agente real por NetTcp (`net.tcp://localhost:8808`):
   - `saldo(0)`, `movimientos(0)`, `cuentas(0)`: respuesta de validación de la BLL ("ID de cuenta inválido", "ID de cliente inválido"), con `Datos` nulo: recorrido cliente→host→servicio→BLL correcto.
   - `saldo(1)` con SQL Server inaccesible (variable de entorno): `Exitosa=false`, "Error al consultar saldo."; la traza del host muestra el error de conexión de `SqlClient` desde `ConsultarSaldoCuenta` del DAT: el recorrido llegó hasta el DAT.
   - `saldo(1)` con la cadena de plantilla: la traza muestra el mensaje de `ConexionHelper` pidiendo completar la cadena.
4. Serialización con `DataContractSerializer` (Mono): `RespuestaOperacion<SaldoDTO>` con datos: correcto. `RespuestaOperacion<PaginacionDTO<MovimientoDTO>>`: **falla** (ver riesgo).

## 5. Riesgo importante: `PaginacionDTO.TotalPages`
`TotalPages` es un `[DataMember]` de solo lectura y no se puede serializar (`InvalidDataContractException: No set method for property 'TotalPages'`).
Es la misma clase que usa `ICuentasServicio.ConsultarMovimientosCuenta`, así que probablemente **el host de .NET Framework no abrirá**, o fallará la operación de movimientos (el saldo no usa esa clase).
Bajo Mono el host abrió, por lo que no lo demuestra para Windows. Nadie en el código legacy lee `TotalPages`.
Corrección propuesta, **no aplicada** (es un cambio de DTO público compartido): quitar `[DataMember]` de `TotalPages` (`docs/parche-opcional-paginacion.patch`; con ese cambio la serialización pasó bajo Mono).
Si el host falla al abrir en Windows con ese error, aplicar `git apply docs\parche-opcional-paginacion.patch` y recompilar.

## 6. Pruebas pendientes de ejecutar en Windows
1. Abrir/compilar la rebanada con Visual Studio 2022 (`tools\build-saldo.ps1` o `tools\abrir-saldo-vs.ps1`) y la compilación completa (`tools\build-completo.ps1`) y comparar con los errores documentados.
2. Levantar SQL Server, crear la BD y ejecutar `sp_ConsultarSaldoCuenta` con un `CuentaId` del seed.
3. Arrancar el host y comprobar que abre (riesgo de la sección 5).
4. Abrir el cliente, consultar un Id existente y comparar el saldo con SQL; probar un Id inexistente y el host apagado.
5. Confirmar que `Ejemplo.Saldo.Cliente` y `FrmSaldos` se ven y responden bien (WinForms no se ejecutó).
6. Ejecutar los scripts `tools\*.ps1` (PowerShell no estaba disponible aquí; no se han ejecutado).
7. Pruebas existentes de `tests\` y la API .NET 8 (no tocadas ni ejecutadas).

## 7. Pasos para ejecutar (PowerShell, desde la raíz del repositorio)
1. **SQL Server**: `docker compose up -d` (usa la contraseña definida en `docker-compose.yml`) o su SQL Server local en el puerto 1433.
2. **Base de datos**: `.\tools\setup-database.ps1` (añada `-Password '<su contraseña>'` si no usa la del compose). Compruebe:
   `sqlcmd -S localhost,1433 -U sa -P '<su contraseña>' -C -d BankLabDB -Q "SELECT TOP 3 CuentaId, NumeroCuenta, Saldo, SaldoDisponible FROM Cuentas ORDER BY CuentaId"` y luego
   `EXEC sp_ConsultarSaldoCuenta @CuentaId = <id>` (anote el valor).
3. **Cadena de conexión** (en la misma ventana donde abrirá el host, sin editar archivos):
   `$env:BANKLAB_CONNECTION_STRING = "Server=localhost,1433;Database=BankLabDB;User Id=sa;Password=<su contraseña>;TrustServerCertificate=True;"`
   (alternativa: reemplazar `COMPLETAR` en `Ejemplo.Saldo.Host\App.config`; no suba ese cambio al repositorio).
4. **Compilar**: `.\tools\build-saldo.ps1` (debe imprimir `[REBANADA SALDO ACTIVA]` por proyecto y terminar sin errores).
5. **Host WCF** (misma ventana): `.\src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.Host\bin\Debug\Ejemplo.Saldo.Host.exe`
   Debe mostrar `[OK] Escuchando en: net.tcp://localhost:8808/BancoEjemplo/Cuentas`. Si Windows pide permiso de firewall, acepte el acceso privado.
6. **Cliente**: `.\src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.Cliente\bin\Debug\Ejemplo.Saldo.Cliente.exe`; escriba el `CuentaId`, pulse **Consultar** y compare "Saldo Disponible" con `SaldoDisponible` de SQL.

## 8. Credenciales
Los archivos nuevos o modificados no incluyen contraseñas reales (solo la plantilla `COMPLETAR`). Ya existían en el repositorio, sin tocar, referencias a la contraseña de laboratorio en:
`docker-compose.yml`, `tools/setup-database.ps1` (valor por defecto), `database/init-in-container.sh`, `docs/10-instalacion-ejecucion.md`, `Ejemplo.WCF.Host/App.config`, `Ejemplo.App/App.config` y `appsettings.json` de la API.

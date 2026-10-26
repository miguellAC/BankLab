# ============================================================================
# BankLab - Compila SIN rebanada, proyecto a proyecto, e imprime los errores unicos.
# Sirve para comparar con docs/fase1-errores-compilacion-completa.md.
# NOTA: SliceSaldo fue retirado en feature/saldo-modulo-normal; todos los modulos
# compilan en la solucion normal sin ninguna propiedad especial.
# No modifica nada; los proyectos que fallen se informan y el script continua.
# ============================================================================
param([string]$Configuration = "Debug", [string]$Salida = "")

. (Join-Path $PSScriptRoot "_msbuild.ps1")
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$legacy = Join-Path $repoRoot "src\legacy"
$msbuild = Get-MSBuildPath
Remove-Item Env:\SliceSaldo -ErrorAction SilentlyContinue   # garantiza que la rebanada NO este activa

$proyectos = @(
 "Ejemplo.Comun\Ejemplo.Comun.DTOs\Ejemplo.Comun.DTOs.csproj",
 "Ejemplo.WCFServicios\Ejemplo.WCF.Contratos\Ejemplo.WCF.Contratos.csproj",
 "Ejemplo.Componentes\Ejemplo.Componentes.DAT\Ejemplo.Componentes.DAT.csproj",
 "Ejemplo.Componentes\Ejemplo.Componentes.DAL\Ejemplo.Componentes.DAL.csproj",
 "Ejemplo.Componentes\Ejemplo.Componentes.DAS\Ejemplo.Componentes.DAS.csproj",
 "Ejemplo.Componentes\Ejemplo.Componentes.BLL\Ejemplo.Componentes.BLL.csproj",
 "Ejemplo.WCFServicios\Ejemplo.WCF.Servicios\Ejemplo.WCF.Servicios.csproj",
 "Ejemplo.WCFServicios\Ejemplo.WCF.Agentes\Ejemplo.WCF.Agentes.csproj",
 "Ejemplo.WCFServicios\Ejemplo.WCF.Host\Ejemplo.WCF.Host.csproj",
 "Ejemplo.GUI\Ejemplo.GUI.Comun\Ejemplo.GUI.Comun.csproj",
 "Ejemplo.GUI\Ejemplo.GUI.Cuentas\Ejemplo.GUI.Cuentas.csproj",
 "Ejemplo.GUI\Ejemplo.GUI.Clientes\Ejemplo.GUI.Clientes.csproj",
 "Ejemplo.GUI\Ejemplo.GUI.Creditos\Ejemplo.GUI.Creditos.csproj",
 "Ejemplo.GUI\Ejemplo.GUI.Reportes\Ejemplo.GUI.Reportes.csproj",
 "Ejemplo.Aplicaciones\Ejemplo.App\Ejemplo.App.csproj",
 "Ejemplo.Aplicaciones\Ejemplo.ShellBP\Ejemplo.ShellBP.csproj"
)

$informe = @()
foreach ($p in $proyectos) {
    $ruta = Join-Path $legacy $p
    $log = & $msbuild $ruta /p:Configuration=$Configuration /v:q /nologo 2>&1
    $errores = $log | Where-Object { $_ -match " error " } | ForEach-Object { ($_ -replace "\s*\[.*\]\s*$", "") -replace [regex]::Escape($legacy + "\"), "" } | Sort-Object -Unique
    $informe += "=== $p : $($errores.Count) error(es) unicos"
    $informe += $errores
}
if ($Salida) { $informe | Set-Content -Path $Salida -Encoding UTF8; Write-Host "Informe guardado en $Salida" } else { $informe }

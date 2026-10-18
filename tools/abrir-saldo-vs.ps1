# ============================================================================
# BankLab - Abre Ejemplo.Saldo.sln en Visual Studio con la rebanada ACTIVADA
# (define SliceSaldo=true solo para ese proceso de Visual Studio; MSBuild lee las
# variables de entorno como propiedades). No cambia ninguna configuracion global.
# ============================================================================
$ErrorActionPreference = "Stop"

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$sln = Join-Path $repoRoot "src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.sln"

$vswhere = Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"
$devenv = $null
if (Test-Path $vswhere) {
    $devenv = & $vswhere -latest -property productPath | Select-Object -First 1
}
if (-not $devenv) { throw "No se encontro devenv.exe. Abra Visual Studio manualmente con la variable SliceSaldo=true definida." }

$env:SliceSaldo = "true"
Write-Host "Abriendo $sln con SliceSaldo=true ..."
Start-Process -FilePath $devenv -ArgumentList "`"$sln`""

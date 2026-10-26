# ============================================================================
# BankLab - Abre Ejemplo.Saldo.sln en Visual Studio.
# NOTA: el mecanismo SliceSaldo fue retirado en la fase 8-11 (feature/saldo-modulo-normal).
# Ya no es necesario definir SliceSaldo=true; la solucion compila normalmente
# incluyendo todos los modulos (Cartera, Pagos, Creditos, Clientes).
# ============================================================================
$ErrorActionPreference = "Stop"

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$sln = Join-Path $repoRoot "src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.sln"

$vswhere = Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"
$devenv = $null
if (Test-Path $vswhere) {
    $devenv = & $vswhere -latest -property productPath | Select-Object -First 1
}
if (-not $devenv) { throw "No se encontro devenv.exe. Abra Visual Studio manualmente." }

Write-Host "Abriendo $sln ..."
Start-Process -FilePath $devenv -ArgumentList "`"$sln`""

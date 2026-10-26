# ============================================================================
# BankLab - Compila la solucion Ejemplo.Saldo.sln (modulo Saldo completo).
# El mecanismo SliceSaldo fue retirado en la fase 8-11 (feature/saldo-modulo-normal).
# Todos los modulos (Cartera, Pagos, Creditos, Clientes) compilan en la solucion normal.
# Los proyectos son de estilo clasico (sin PackageReference): no requiere restore.
# ============================================================================
param([string]$Configuration = "Debug")

$ErrorActionPreference = "Stop"
. (Join-Path $PSScriptRoot "_msbuild.ps1")

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$sln = Join-Path $repoRoot "src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.sln"
$msbuild = Get-MSBuildPath

Write-Host "MSBuild: $msbuild"
& $msbuild $sln /p:Configuration=$Configuration /v:minimal /nologo
if ($LASTEXITCODE -ne 0) { throw "La compilacion de Ejemplo.Saldo fallo (codigo $LASTEXITCODE)." }

Write-Host ""
Write-Host "Solucion Saldo compilada. Salidas:" -ForegroundColor Green
Write-Host "  Host      : src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.Host\bin\$Configuration\Ejemplo.Saldo.Host.exe"
Write-Host "  HostIIS   : src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.HostIIS\bin\Ejemplo.Saldo.HostIIS.dll"
Write-Host "  Cliente   : src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.Cliente\bin\$Configuration\Ejemplo.Saldo.Cliente.exe"

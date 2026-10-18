# ============================================================================
# BankLab - Compila la REBANADA "Saldo" (Ejemplo.Saldo.sln) con SliceSaldo=true.
# Solo esta solucion usa la rebanada; las soluciones normales no se tocan.
# Los proyectos son de estilo clasico (sin PackageReference): no requiere restore.
# ============================================================================
param([string]$Configuration = "Debug")

$ErrorActionPreference = "Stop"
. (Join-Path $PSScriptRoot "_msbuild.ps1")

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$sln = Join-Path $repoRoot "src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.sln"
$msbuild = Get-MSBuildPath

Write-Host "MSBuild: $msbuild"
& $msbuild $sln /p:SliceSaldo=true /p:Configuration=$Configuration /v:minimal /nologo
if ($LASTEXITCODE -ne 0) { throw "La compilacion de la rebanada fallo (codigo $LASTEXITCODE)." }

Write-Host ""
Write-Host "Rebanada compilada. Salidas:" -ForegroundColor Green
Write-Host "  Host   : src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.Host\bin\$Configuration\Ejemplo.Saldo.Host.exe"
Write-Host "  Cliente: src\legacy\Ejemplo.Saldo\Ejemplo.Saldo.Cliente\bin\$Configuration\Ejemplo.Saldo.Cliente.exe"

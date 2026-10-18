# Script to run all test projects in the solution

Write-Host "Iniciando ejecución de pruebas..." -ForegroundColor Cyan

# Check if dotnet CLI is available
if (!(Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Host "ERROR: dotnet CLI no está instalado o no está en el PATH." -ForegroundColor Red
    exit 1
}

# 1. Run tests for Modern API (.NET 8)
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$modernSolution = Join-Path $repoRoot "src\modern\BankLab.Modern\BankLab.Modern.sln"
if (Test-Path $modernSolution) {
    Write-Host "Ejecutando pruebas para Modern API..." -ForegroundColor Yellow
    dotnet test $modernSolution --logger "console;verbosity=detailed"
} else {
    Write-Host "No se encontró solución moderna para probar." -ForegroundColor DarkGray
}

# 2. Run tests for Legacy (might require vstest.console.exe if dotnet test doesn't pick up .NET 4.8)
$legacySolution = Join-Path $repoRoot "tests\Ejemplo.Tests.sln"
if (Test-Path $legacySolution) {
    Write-Host "Ejecutando pruebas para Legacy (intentando con dotnet test)..." -ForegroundColor Yellow
    dotnet test $legacySolution
}

Write-Host "Pruebas finalizadas." -ForegroundColor Green

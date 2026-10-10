# Script to build and run the .NET 8 API

$projectPath = "..\BankLab.Modern\BankLab.Modern.API\BankLab.Modern.API.csproj"

Write-Host "Iniciando proceso para API Moderna (.NET 8)..." -ForegroundColor Cyan

# Check if dotnet CLI is available
if (!(Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Host "ERROR: dotnet CLI no está instalado o no está en el PATH." -ForegroundColor Red
    exit 1
}

# We assume the user runs this from the tools folder, but let's make sure the project exists
if (!(Test-Path $projectPath)) {
    Write-Host "WARNING: No se encuentra $projectPath. Asegúrese de que la estructura de carpetas coincida." -ForegroundColor Yellow
    # Fallback, just try to run in the presumed modern folder
    $apiFolder = "..\BankLab.Modern.API"
    if (Test-Path $apiFolder) {
        Set-Location $apiFolder
        dotnet run
    }
} else {
    Write-Host "Ejecutando API..." -ForegroundColor Green
    dotnet run --project $projectPath
}

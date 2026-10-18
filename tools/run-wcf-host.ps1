# Script to build and run the WCF Host Console Application

# Rutas relativas a la ubicacion del script (no al directorio actual).
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$wcfRoot = Join-Path $repoRoot "src\legacy\Ejemplo.WCFServicios"
$solutionPath = Join-Path $wcfRoot "Ejemplo.WCFServicios.sln"
$projectPath = Join-Path $wcfRoot "Ejemplo.WCF.Host\Ejemplo.WCF.Host.csproj"
$exePath = Join-Path $wcfRoot "Ejemplo.WCF.Host\bin\Debug\Ejemplo.WCF.Host.exe"

Write-Host "Iniciando proceso para WCF Host..." -ForegroundColor Cyan

# 1. Build the solution using MSBuild (requires Developer Command Prompt or MSBuild in PATH)
# Since we might not have MSBuild in PATH, we use dotnet build as a fallback if it's SDK style, 
# but for .NET 4.8 we typically need MSBuild. We will try to find msbuild.

$msbuild = "msbuild.exe"
if (!(Get-Command $msbuild -ErrorAction SilentlyContinue)) {
    # Try to find common VS paths
    $vsWhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
    if (Test-Path $vsWhere) {
        $msbuildPath = & $vsWhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\Current\Bin\MSBuild.exe
        if ($msbuildPath) {
            $msbuild = $msbuildPath
        }
    }
}

if (!(Get-Command $msbuild -ErrorAction SilentlyContinue) -and !(Test-Path $msbuild)) {
    Write-Host "WARNING: msbuild.exe no encontrado. Si el host no compila, ábralo en Visual Studio." -ForegroundColor Yellow
} else {
    Write-Host "Compilando Host..."
    & $msbuild $solutionPath /p:Configuration=Debug
}

# 2. Run the executable
if (Test-Path $exePath) {
    Write-Host "Ejecutando $exePath..." -ForegroundColor Green
    Start-Process -FilePath $exePath
    Write-Host "WCF Host iniciado en una nueva ventana."
} else {
    Write-Host "ERROR: No se encontró el ejecutable. Verifique que la solución compile correctamente." -ForegroundColor Red
}

# Setup BankLab Database Script
# This script initializes the SQL Server database using sqlcmd

$server = "localhost"
$username = "sa"
$password = "BankLab2024!"
$database = "BankLabDB"

Write-Host "Iniciando configuración de Base de Datos BankLab en $server..." -ForegroundColor Cyan

# Check if sqlcmd is available
if (!(Get-Command sqlcmd -ErrorAction SilentlyContinue)) {
    Write-Host "ERROR: sqlcmd no está instalado o no está en el PATH." -ForegroundColor Red
    exit 1
}

# 1. Crear Base de Datos
$createDbScript = "
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '$database')
BEGIN
    CREATE DATABASE [$database];
END
"
Write-Host "Creando base de datos $database..."
sqlcmd -S $server -U $username -P $password -C -Q $createDbScript

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error creando la base de datos." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "¡Base de datos verificada/creada exitosamente!" -ForegroundColor Green
Write-Host "NOTA: En un entorno real, aquí se iterarían los scripts .sql de la carpeta /database" -ForegroundColor Yellow

# Example to execute scripts from a folder (commented out as there are no sql files yet)
# $scriptsFolder = "..\database\"
# if (Test-Path $scriptsFolder) {
#    Get-ChildItem $scriptsFolder -Filter "*.sql" | Sort-Object Name | ForEach-Object {
#        Write-Host "Ejecutando script: $($_.Name)..."
#        sqlcmd -S $server -d $database -U $username -P $password -C -i $_.FullName
#    }
# }

Write-Host "Proceso finalizado." -ForegroundColor Green

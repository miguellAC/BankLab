# ============================================================================
# Banco Ejemplo - BankLab
# Script: setup-database.ps1
# Descripción: Inicializa la base de datos BankLabDB y ejecuta todos los scripts
#              en el orden estricto de dependencias:
#              1. Creación de Base de Datos
#              2. Tablas y Esquemas (02-schema)
#              3. Restricciones e Índices (03-constraints)
#              4. Procedimientos Almacenados (04-stored-procedures)
#              5. Datos Ficticios / Semilla (05-seed-data)
#
#              Detecta automáticamente si se dispone de 'sqlcmd' localmente
#              o si se debe ejecutar mediante el contenedor Docker 'banklab-sqlserver'.
# ============================================================================

param(
    [string]$Server = "localhost",
    [string]$Username = "sa",
    [string]$Password = "BankLab2024!",
    [string]$Database = "BankLabDB",
    [string]$ContainerName = "banklab-sqlserver",
    [switch]$SkipSeed = $false,
    [switch]$Recreate = $false
)

$ErrorActionPreference = "Stop"

Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host "        Banco Ejemplo - Inicializador de BankLabDB        " -ForegroundColor Cyan
Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host "Servidor:    $Server"
Write-Host "Usuario:     $Username"
Write-Host "Base:        $Database"

# Determinar método de ejecución (sqlcmd local vs docker exec)
$useDocker = $false
$localSqlCmd = Get-Command sqlcmd -ErrorAction SilentlyContinue

if ($localSqlCmd) {
    Write-Host "Modo:        Local (sqlcmd.exe detectado en PATH)" -ForegroundColor Green
} else {
    # Comprobar si docker está disponible y el contenedor corriendo
    $dockerCmd = Get-Command docker -ErrorAction SilentlyContinue
    if ($dockerCmd) {
        $containerStatus = docker ps --filter "name=$ContainerName" --format "{{.Status}}"
        if ($containerStatus -and $containerStatus -match "Up") {
            $useDocker = $true
            Write-Host "Modo:        Docker ('sqlcmd' no encontrado en host -> usando contenedor $ContainerName)" -ForegroundColor Yellow
        } else {
            Write-Host "`n[ERROR] Ni 'sqlcmd' está en el host ni el contenedor Docker '$ContainerName' está corriendo." -ForegroundColor Red
            Write-Host "Por favor asegúrese de ejecutar primero:" -ForegroundColor Yellow
            Write-Host "   docker compose up -d" -ForegroundColor White
            exit 1
        }
    } else {
        Write-Host "`n[ERROR] No se encontró 'sqlcmd' ni 'docker' en el sistema." -ForegroundColor Red
        exit 1
    }
}
Write-Host ""

function Invoke-SqlScript {
    param(
        [string]$FilePath,
        [string]$TargetDb = ""
    )
    $fileName = Split-Path $FilePath -Leaf
    Write-Host "  -> Ejecutando: $fileName ... " -NoNewline

    if ($useDocker) {
        # El directorio local 'database' está montado en /docker-entrypoint-initdb.d dentro del contenedor
        # Convertir ruta relativa a ruta dentro del contenedor
        $relativePath = $FilePath.Substring($dbPath.Length).Replace("\", "/")
        $containerPath = "/docker-entrypoint-initdb.d$relativePath"

        $cmdArgs = @("exec", $ContainerName, "/opt/mssql-tools18/bin/sqlcmd", "-S", "localhost", "-U", $Username, "-P", $Password, "-C", "-b", "-i", $containerPath)
        if ($TargetDb) {
            $cmdArgs += @("-d", $TargetDb)
        }

        $res = & docker @cmdArgs 2>&1
        if ($LASTEXITCODE -ne 0) {
            Write-Host "[FALLO]" -ForegroundColor Red
            $res | ForEach-Object { Write-Host $_ -ForegroundColor Red }
            throw "Error al ejecutar el script $fileName en el contenedor"
        } else {
            Write-Host "[OK]" -ForegroundColor Green
        }
    } else {
        $argsList = @("-S", $Server, "-U", $Username, "-P", $Password, "-C", "-b", "-i", $FilePath)
        if ($TargetDb) {
            $argsList += @("-d", $TargetDb)
        }

        $process = Start-Process -FilePath "sqlcmd" -ArgumentList $argsList -NoNewWindow -Wait -PassThru -RedirectStandardError "sqlcmd_err.tmp"
        if ($process.ExitCode -ne 0) {
            Write-Host "[FALLO]" -ForegroundColor Red
            if (Test-Path "sqlcmd_err.tmp") {
                Get-Content "sqlcmd_err.tmp" | Write-Host -ForegroundColor Red
                Remove-Item "sqlcmd_err.tmp" -Force
            }
            throw "Error al ejecutar el script $fileName"
        } else {
            Write-Host "[OK]" -ForegroundColor Green
            if (Test-Path "sqlcmd_err.tmp") { Remove-Item "sqlcmd_err.tmp" -Force }
        }
    }
}

$dbPath = (Resolve-Path (Join-Path $PSScriptRoot "..\database")).Path
if (-not (Test-Path $dbPath)) {
    throw "No se encontró la carpeta 'database' en la ruta esperada: $dbPath"
}

try {
    # 1. Creación de la base de datos
    Write-Host "`n[Paso 1/5] Creación de Base de Datos..." -ForegroundColor Yellow
    if ($Recreate) {
        Write-Host "  -> Recreando base de datos '$Database'..." -ForegroundColor Yellow
        $dropSql = "IF EXISTS (SELECT 1 FROM sys.databases WHERE name = '$Database') BEGIN ALTER DATABASE [$Database] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$Database]; END"
        if ($useDocker) {
            docker exec $ContainerName /opt/mssql-tools18/bin/sqlcmd -S localhost -U $Username -P $Password -C -Q $dropSql | Out-Null
        } else {
            sqlcmd -S $Server -U $Username -P $Password -C -Q $dropSql | Out-Null
        }
    }
    Invoke-SqlScript -FilePath (Join-Path $dbPath "01-create-database.sql")

    # 2. Esquemas y Tablas
    Write-Host "`n[Paso 2/5] Creación de Tablas (02-schema)..." -ForegroundColor Yellow
    $schemaFiles = Get-ChildItem (Join-Path $dbPath "02-schema") -Filter "*.sql" | Sort-Object Name
    foreach ($file in $schemaFiles) {
        Invoke-SqlScript -FilePath $file.FullName -TargetDb $Database
    }

    # 3. Restricciones e Índices
    Write-Host "`n[Paso 3/5] Creación de Restricciones e Índices (03-constraints)..." -ForegroundColor Yellow
    $constraintFiles = Get-ChildItem (Join-Path $dbPath "03-constraints") -Filter "*.sql" | Sort-Object Name
    foreach ($file in $constraintFiles) {
        Invoke-SqlScript -FilePath $file.FullName -TargetDb $Database
    }

    # 4. Procedimientos Almacenados
    Write-Host "`n[Paso 4/5] Creación de Procedimientos Almacenados (04-stored-procedures)..." -ForegroundColor Yellow
    $spFiles = Get-ChildItem (Join-Path $dbPath "04-stored-procedures") -Filter "*.sql" | Sort-Object Name
    foreach ($file in $spFiles) {
        Invoke-SqlScript -FilePath $file.FullName -TargetDb $Database
    }

    # 5. Carga de Datos Ficticios (Seed)
    if (-not $SkipSeed) {
        Write-Host "`n[Paso 5/5] Carga de Datos Semilla (05-seed-data)..." -ForegroundColor Yellow
        $seedFiles = Get-ChildItem (Join-Path $dbPath "05-seed-data") -Filter "*.sql" | Sort-Object Name
        foreach ($file in $seedFiles) {
            Invoke-SqlScript -FilePath $file.FullName -TargetDb $Database
        }
    } else {
        Write-Host "`n[Paso 5/5] Carga de datos omitida por parámetro -SkipSeed." -ForegroundColor DarkGray
    }

    Write-Host "`n==========================================================" -ForegroundColor Green
    Write-Host " ¡Base de datos BankLabDB configurada exitosamente!      " -ForegroundColor Green
    Write-Host "==========================================================" -ForegroundColor Green
}
catch {
    Write-Host "`n[ERROR CRÍTICO] $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

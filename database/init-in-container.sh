# ============================================================================
# Banco Ejemplo - BankLab
# Script: init-in-container.sh
# Descripción: Inicializa la base de datos ejecutando los scripts SQL dentro
#              del contenedor Docker de SQL Server usando sqlcmd nativo.
# ============================================================================

#!/bin/bash
set -e

echo "Esperando a que SQL Server esté listo..."
until /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "BankLab2024!" -C -Q "SELECT 1" &> /dev/null || /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "BankLab2024!" -Q "SELECT 1" &> /dev/null; do
    sleep 2
done

echo "SQL Server activo. Iniciando despliegue de BankLabDB..."

SQLCMD_BIN="/opt/mssql-tools18/bin/sqlcmd"
SQLCMD_FLAGS="-C -b"

if [ ! -f "$SQLCMD_BIN" ]; then
    SQLCMD_BIN="/opt/mssql-tools/bin/sqlcmd"
    SQLCMD_FLAGS="-b"
fi

# 1. Crear Base
$SQLCMD_BIN -S localhost -U sa -P "BankLab2024!" $SQLCMD_FLAGS -i /docker-entrypoint-initdb.d/01-create-database.sql

# 2. Esquemas
for f in $(ls /docker-entrypoint-initdb.d/02-schema/*.sql | sort); do
    echo "Ejecutando: $f"
    $SQLCMD_BIN -S localhost -U sa -P "BankLab2024!" $SQLCMD_FLAGS -d BankLabDB -i "$f"
done

# 3. Constraints
for f in $(ls /docker-entrypoint-initdb.d/03-constraints/*.sql | sort); do
    echo "Ejecutando: $f"
    $SQLCMD_BIN -S localhost -U sa -P "BankLab2024!" $SQLCMD_FLAGS -d BankLabDB -i "$f"
done

# 4. Stored Procedures
for f in $(ls /docker-entrypoint-initdb.d/04-stored-procedures/*.sql | sort); do
    echo "Ejecutando: $f"
    $SQLCMD_BIN -S localhost -U sa -P "BankLab2024!" $SQLCMD_FLAGS -d BankLabDB -i "$f"
done

# 5. Seed Data
for f in $(ls /docker-entrypoint-initdb.d/05-seed-data/*.sql | sort); do
    echo "Ejecutando: $f"
    $SQLCMD_BIN -S localhost -U sa -P "BankLab2024!" $SQLCMD_FLAGS -d BankLabDB -i "$f"
done

echo "¡BankLabDB desplegada con éxito en el contenedor Docker!"

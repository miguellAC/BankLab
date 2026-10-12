-- 03-cuentas-productos.sql
-- Generates 3,000 accounts for clients
-- Approx: 3000 Cuentas, 3300 TitularesCuenta

SET NOCOUNT ON;
GO

USE BankLabDB;
GO

PRINT 'Poblando tablas de cuentas y productos...';

-- Generate 3000 Cuentas (using clients 201 to 2000, and some multiple)
-- Schema Cuentas: CuentaId, NumeroCuenta, ClienteId, ProductoId, TipoCuentaId, SucursalId, Saldo, SaldoDisponible, Estado, FechaApertura
SET IDENTITY_INSERT Cuentas ON;
WITH Nums AS (
    SELECT TOP (3000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO Cuentas (CuentaId, NumeroCuenta, ClienteId, ProductoId, TipoCuentaId, SucursalId, Saldo, SaldoDisponible, Estado, FechaApertura)
SELECT 
    N AS CuentaId,
    '2200' + RIGHT('0000000000' + CAST(N AS VARCHAR), 10) AS NumeroCuenta,
    ((N - 1) % 1800) + 201 AS ClienteId,
    CASE 
        WHEN N % 100 < 50 THEN 1 -- Cuenta Ahorros
        WHEN N % 100 < 80 THEN 2 -- Cuenta Corriente
        ELSE 3 -- CDT
    END AS ProductoId,
    CASE 
        WHEN N % 100 < 50 THEN 1 -- Ahorros
        WHEN N % 100 < 80 THEN 2 -- Corriente
        ELSE 3 -- CDT
    END AS TipoCuentaId,
    (ABS(CHECKSUM(NEWID())) % 10) + 1 AS SucursalId,
    CASE 
        WHEN (N % 100) >= 50 AND (N % 100) < 80 THEN 500000 + (ABS(CHECKSUM(NEWID())) % 10000000) -- Corriente
        ELSE ABS(CHECKSUM(NEWID())) % 50000000 -- Otros
    END AS Saldo,
    CASE 
        WHEN (N % 100) >= 50 AND (N % 100) < 80 THEN 500000 + (ABS(CHECKSUM(NEWID())) % 10000000)
        ELSE ABS(CHECKSUM(NEWID())) % 50000000
    END AS SaldoDisponible,
    CASE 
        WHEN N % 100 < 85 THEN 'ACTIVA'
        WHEN N % 100 < 93 THEN 'INACTIVA'
        WHEN N % 100 < 98 THEN 'BLOQUEADA'
        ELSE 'CANCELADA'
    END AS Estado,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 2000), GETDATE()) AS FechaApertura
FROM Nums;
SET IDENTITY_INSERT Cuentas OFF;

-- TitularesCuenta (Principal)
-- Schema TitularesCuenta: TitularId, CuentaId, ClienteId, TipoTitular, FechaAsignacion, Activo
INSERT INTO TitularesCuenta (CuentaId, ClienteId, TipoTitular)
SELECT 
    CuentaId,
    ClienteId,
    'TITULAR' AS TipoTitular
FROM Cuentas;

-- TitularesCuenta (Cotitular para el 10%)
-- Must ensure distinct ClienteId per account
INSERT INTO TitularesCuenta (CuentaId, ClienteId, TipoTitular)
SELECT 
    CuentaId,
    ((ClienteId + 500) % 1800) + 201 AS ClienteId,
    'COTITULAR' AS TipoTitular
FROM Cuentas
WHERE CuentaId % 10 = 0 AND ((ClienteId + 500) % 1800) + 201 <> ClienteId;

PRINT 'Cuentas pobladas exitosamente.';
GO

-- 03-cuentas-productos.sql
-- Generates 3,000 accounts for clients
-- Approx: 3000 Cuentas, 3300 TitularesCuenta

SET NOCOUNT ON;
GO

PRINT 'Poblando tablas de cuentas y productos...';

-- Generate 3000 Cuentas (using clients 201 to 2000, and some multiple)
SET IDENTITY_INSERT Cuentas ON;
WITH Nums AS (
    SELECT TOP (3000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO Cuentas (Id, SucursalId, TipoCuentaId, NumeroCuenta, Saldo, SaldoCanje, Estado, FechaApertura)
SELECT 
    N AS Id,
    (ABS(CHECKSUM(NEWID())) % 10) + 1 AS SucursalId,
    CASE 
        WHEN N % 100 < 50 THEN 1 -- Ahorros 50%
        WHEN N % 100 < 80 THEN 2 -- Corriente 30%
        ELSE 3 -- CDT 20%
    END AS TipoCuentaId,
    '2200' + RIGHT('0000000000' + CAST(N AS VARCHAR), 10) AS NumeroCuenta,
    CASE 
        WHEN (N % 100) >= 50 AND (N % 100) < 80 THEN 500000 + (ABS(CHECKSUM(NEWID())) % 10000000) -- Corriente
        ELSE ABS(CHECKSUM(NEWID())) % 50000000 -- Otros
    END AS Saldo,
    0 AS SaldoCanje,
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
INSERT INTO TitularesCuenta (CuentaId, ClienteId, TipoTitularidad)
SELECT 
    Id AS CuentaId,
    -- Map Account 1..3000 to Client 201..2000 (leaves clients 1-200 without products)
    (Id % 1800) + 201 AS ClienteId,
    'TITULAR' AS TipoTitularidad
FROM Cuentas;

-- TitularesCuenta (Cotitular para el 10%)
INSERT INTO TitularesCuenta (CuentaId, ClienteId, TipoTitularidad)
SELECT 
    Id AS CuentaId,
    -- Ensure it's a different client
    ((Id + 500) % 1800) + 201 AS ClienteId,
    'COTITULAR' AS TipoTitularidad
FROM Cuentas
WHERE Id % 10 = 0;

PRINT 'Cuentas pobladas exitosamente.';
GO

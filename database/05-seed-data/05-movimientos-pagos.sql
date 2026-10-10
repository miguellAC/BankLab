-- 05-movimientos-pagos.sql
-- Generates transaction data: Movimientos, Pagos, AplicacionesPagos

SET NOCOUNT ON;
GO

PRINT 'Poblando tablas de movimientos y pagos...';

-- Movimientos
SET IDENTITY_INSERT Movimientos ON;
WITH Nums AS (
    SELECT TOP (30000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO Movimientos (Id, CuentaId, TipoTransaccionId, Monto, Naturaleza, FechaMovimiento, Referencia, SaldoAnterior, SaldoPosterior, Descripcion)
SELECT 
    N AS Id,
    (N % 3000) + 1 AS CuentaId,
    (N % 9) + 1 AS TipoTransaccionId,
    10000 + (ABS(CHECKSUM(NEWID())) % 9990000) AS Monto,
    CASE WHEN ((N % 9) + 1) IN (1, 4, 6, 8, 9) THEN 'C' ELSE 'D' END AS Naturaleza,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 1095), GETDATE()) AS FechaMovimiento,
    'REF-' + CAST(N AS VARCHAR) AS Referencia,
    0 AS SaldoAnterior, -- Simplified
    0 AS SaldoPosterior, -- Simplified
    'Movimiento generado ' + CAST(N AS VARCHAR) AS Descripcion
FROM Nums;
SET IDENTITY_INSERT Movimientos OFF;

-- Pagos
SET IDENTITY_INSERT Pagos ON;
WITH PagosData AS (
    SELECT 
        C.Id AS CuotaId,
        C.OperacionId,
        C.TotalCuota,
        C.FechaVencimiento,
        ROW_NUMBER() OVER(ORDER BY C.Id) AS PagoN
    FROM Cuotas C
    WHERE C.Estado = 'PAGADA'
)
INSERT INTO Pagos (Id, OperacionId, FechaPago, MontoTotal, CanalPago, Referencia, Estado)
SELECT 
    PagoN AS Id,
    OperacionId,
    ISNULL(DATEADD(DAY, - (ABS(CHECKSUM(NEWID())) % 5), FechaVencimiento), GETDATE()) AS FechaPago,
    TotalCuota AS MontoTotal,
    CHOOSE((PagoN % 4) + 1, 'TAQUILLA', 'PORTAL_WEB', 'APP_MOVIL', 'CORRESPONSAL') AS CanalPago,
    'PAG-' + CAST(PagoN AS VARCHAR) AS Referencia,
    CASE WHEN PagoN % 100 = 0 THEN 'REVERSADO' ELSE 'APLICADO' END AS Estado
FROM PagosData;
SET IDENTITY_INSERT Pagos OFF;

-- AplicacionesPagos
INSERT INTO AplicacionesPagos (PagoId, CuotaId, MontoAplicadoCapital, MontoAplicadoIntereses, MontoAplicadoMora, MontoAplicadoOtros)
SELECT 
    P.Id AS PagoId,
    C.Id AS CuotaId,
    C.Capital AS MontoAplicadoCapital,
    C.Intereses AS MontoAplicadoIntereses,
    0 AS MontoAplicadoMora,
    0 AS MontoAplicadoOtros
FROM Pagos P
JOIN Cuotas C ON P.OperacionId = C.OperacionId AND P.MontoTotal = C.TotalCuota
WHERE P.Estado = 'APLICADO';

PRINT 'Movimientos y pagos poblados exitosamente.';
GO

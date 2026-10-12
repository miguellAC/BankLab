-- 05-movimientos-pagos.sql
-- Generates transaction data: Movimientos, Pagos, AplicacionesPagos

SET NOCOUNT ON;
GO

USE BankLabDB;
GO

PRINT 'Poblando tablas de movimientos y pagos...';

-- Movimientos
-- Schema: MovimientoId, CuentaId, TipoTransaccionId, Monto, SaldoAnterior, SaldoPosterior, Descripcion, Referencia, CanalOrigen, FechaMovimiento, FechaValor, UsuarioCreacion
SET IDENTITY_INSERT Movimientos ON;
WITH Nums AS (
    SELECT TOP (30000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO Movimientos (MovimientoId, CuentaId, TipoTransaccionId, Monto, SaldoAnterior, SaldoPosterior, Descripcion, Referencia, CanalOrigen, FechaMovimiento, FechaValor)
SELECT 
    N AS MovimientoId,
    ((N - 1) % 3000) + 1 AS CuentaId,
    ((N - 1) % 9) + 1 AS TipoTransaccionId,
    10000 + (ABS(CHECKSUM(NEWID())) % 9990000) AS Monto,
    0 AS SaldoAnterior,
    0 AS SaldoPosterior,
    'Movimiento generado ' + CAST(N AS VARCHAR) AS Descripcion,
    'REF-' + CAST(N AS VARCHAR) AS Referencia,
    CHOOSE(((N - 1) % 6) + 1, 'OFICINA', 'WEB', 'MOVIL', 'ATM', 'INTERBANCARIO', 'BATCH') AS CanalOrigen,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 1095), GETDATE()) AS FechaMovimiento,
    CAST(DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 1095), GETDATE()) AS DATE) AS FechaValor
FROM Nums;
SET IDENTITY_INSERT Movimientos OFF;

-- Pagos
-- Schema: PagoId, OperacionId, CuentaOrigenId, MontoPago, FechaPago, FechaAplicacion, TipoPago, CanalPago, Referencia, Estado, UsuarioCreacion
-- Check: TipoPago IN ('ORDINARIO','EXTRAORDINARIO','ANTICIPADO','TOTAL')
-- Check: CanalPago IN ('CAJA','DEBITO_AUTOMATICO','WEB','INTERBANCARIO','BATCH')
-- Check: Estado IN ('APLICADO','REVERSADO','PENDIENTE')
SET IDENTITY_INSERT Pagos ON;
WITH PagosData AS (
    SELECT 
        C.CuotaId,
        C.OperacionId,
        C.ValorCuota,
        C.CapitalCuota,
        C.InteresCuota,
        C.FechaVencimiento,
        ROW_NUMBER() OVER(ORDER BY C.CuotaId) AS PagoN
    FROM Cuotas C
    WHERE C.Estado = 'PAGADA'
)
INSERT INTO Pagos (PagoId, OperacionId, CuentaOrigenId, MontoPago, FechaPago, FechaAplicacion, TipoPago, CanalPago, Referencia, Estado)
SELECT 
    PagoN AS PagoId,
    OperacionId,
    NULL AS CuentaOrigenId,
    ValorCuota AS MontoPago,
    ISNULL(DATEADD(DAY, - (ABS(CHECKSUM(NEWID())) % 5), FechaVencimiento), GETDATE()) AS FechaPago,
    CAST(ISNULL(DATEADD(DAY, - (ABS(CHECKSUM(NEWID())) % 5), FechaVencimiento), GETDATE()) AS DATE) AS FechaAplicacion,
    'ORDINARIO' AS TipoPago,
    CHOOSE((PagoN % 5) + 1, 'CAJA', 'DEBITO_AUTOMATICO', 'WEB', 'INTERBANCARIO', 'BATCH') AS CanalPago,
    'PAG-' + CAST(PagoN AS VARCHAR) AS Referencia,
    CASE WHEN PagoN % 100 = 0 THEN 'REVERSADO' ELSE 'APLICADO' END AS Estado
FROM PagosData;
SET IDENTITY_INSERT Pagos OFF;

-- AplicacionesPagos
-- Schema: AplicacionPagoId, PagoId, CuotaId, MontoCapital, MontoInteres, MontoSeguro, MontoTotal, FechaAplicacion
WITH PagosData AS (
    SELECT 
        C.CuotaId,
        C.OperacionId,
        C.CapitalCuota,
        C.InteresCuota,
        C.ValorCuota,
        ROW_NUMBER() OVER(ORDER BY C.CuotaId) AS PagoN
    FROM Cuotas C
    WHERE C.Estado = 'PAGADA'
)
INSERT INTO AplicacionesPagos (PagoId, CuotaId, MontoCapital, MontoInteres, MontoSeguro, MontoTotal)
SELECT 
    P.PagoId,
    D.CuotaId,
    D.CapitalCuota AS MontoCapital,
    D.InteresCuota AS MontoInteres,
    0 AS MontoSeguro,
    D.ValorCuota AS MontoTotal
FROM Pagos P
JOIN PagosData D ON P.PagoId = D.PagoN
WHERE P.Estado = 'APLICADO';

PRINT 'Movimientos y pagos poblados exitosamente.';
GO

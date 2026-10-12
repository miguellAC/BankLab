-- 04-creditos-cuotas.sql
-- Generates credit data: Solicitudes, Operaciones, Condiciones, Cuotas, Saldos, Garantias
-- Uses French amortization for realistic installments.

SET NOCOUNT ON;
GO

USE BankLabDB;
GO

PRINT 'Poblando tablas de créditos...';

-- 1500 SolicitudesCredito
-- Schema: SolicitudId, ClienteId, ProductoCreditoId, EmpleadoId, MontoSolicitado, PlazoMeses, IngresosMensuales, Estado, FechaRadicacion, ObservacionesResolucion, UsuarioCreacion, FechaCreacion
SET IDENTITY_INSERT SolicitudesCredito ON;
WITH Nums AS (
    SELECT TOP (1500) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO SolicitudesCredito (SolicitudId, ClienteId, ProductoCreditoId, EmpleadoId, MontoSolicitado, PlazoMeses, IngresosMensuales, Estado, FechaRadicacion, ObservacionesResolucion)
SELECT 
    N AS SolicitudId,
    (N % 1800) + 201 AS ClienteId,
    CASE 
        WHEN N % 100 < 60 THEN 1 -- Libre Inversión (60%)
        WHEN N % 100 < 80 THEN 2 -- Hipotecario (20%)
        WHEN N % 100 < 95 THEN 3 -- Vehículo (15%)
        ELSE 4 -- Rotativo (5%)
    END AS ProductoCreditoId,
    (N % 29) + 1 AS EmpleadoId,
    CASE 
        WHEN N % 100 < 60 THEN 5000000 + (ABS(CHECKSUM(NEWID())) % 20000000) -- Libre Inversión
        WHEN N % 100 < 80 THEN 100000000 + (ABS(CHECKSUM(NEWID())) % 200000000) -- Hipotecario
        WHEN N % 100 < 95 THEN 30000000 + (ABS(CHECKSUM(NEWID())) % 50000000) -- Vehículo
        ELSE 2000000 + (ABS(CHECKSUM(NEWID())) % 10000000) -- Rotativo
    END AS MontoSolicitado,
    CASE 
        WHEN N % 100 < 60 THEN 36 -- Libre Inversión
        WHEN N % 100 < 80 THEN 120 -- Hipotecario
        WHEN N % 100 < 95 THEN 60 -- Vehículo
        ELSE 24 -- Rotativo
    END AS PlazoMeses,
    4000000 + (ABS(CHECKSUM(NEWID())) % 8000000) AS IngresosMensuales,
    CASE 
        WHEN N % 100 < 40 THEN 'APROBADA'
        WHEN N % 100 < 65 THEN 'RECHAZADA'
        WHEN N % 100 < 80 THEN 'EN_ESTUDIO'
        WHEN N % 100 < 90 THEN 'RADICADA'
        WHEN N % 100 < 95 THEN 'DESISTIDA'
        ELSE 'VENCIDA'
    END AS Estado,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 1000), GETDATE()) AS FechaRadicacion,
    'Solicitud autogenerada' AS ObservacionesResolucion
FROM Nums;
SET IDENTITY_INSERT SolicitudesCredito OFF;

-- 1000 OperacionesCredito (Approved solicitudes + migrated)
-- Schema: OperacionId, NumeroOperacion, SolicitudId, ClienteId, ProductoCreditoId, MontoDesembolsado, TasaAnual, PlazoMeses, NumeroCuotas, FechaDesembolso, FechaVencimiento, Estado, SucursalId
SET IDENTITY_INSERT OperacionesCredito ON;
WITH Aprobadas AS (
    SELECT SolicitudId, ClienteId, ProductoCreditoId, MontoSolicitado, PlazoMeses
    FROM SolicitudesCredito WHERE Estado = 'APROBADA'
),
Nums AS (
    SELECT TOP (1000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO OperacionesCredito (OperacionId, NumeroOperacion, SolicitudId, ClienteId, ProductoCreditoId, MontoDesembolsado, TasaAnual, PlazoMeses, NumeroCuotas, FechaDesembolso, FechaVencimiento, Estado, SucursalId)
SELECT 
    N.N AS OperacionId,
    'CRE' + RIGHT('0000000000' + CAST(N.N AS VARCHAR), 10) AS NumeroOperacion,
    A.SolicitudId,
    ISNULL(A.ClienteId, (N.N % 1800) + 201) AS ClienteId,
    ISNULL(A.ProductoCreditoId, (N.N % 4) + 1) AS ProductoCreditoId,
    ISNULL(A.MontoSolicitado, 10000000) AS MontoDesembolsado,
    P.TasaBaseAnual AS TasaAnual,
    ISNULL(A.PlazoMeses, P.PlazoMinMeses + 12) AS PlazoMeses,
    ISNULL(A.PlazoMeses, P.PlazoMinMeses + 12) AS NumeroCuotas,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 1500), GETDATE()) AS FechaDesembolso,
    DATEADD(MONTH, ISNULL(A.PlazoMeses, P.PlazoMinMeses + 12), DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 1500), GETDATE())) AS FechaVencimiento,
    CASE 
        WHEN N.N % 100 < 70 THEN 'VIGENTE'
        WHEN N.N % 100 < 85 THEN 'MORA'
        WHEN N.N % 100 < 95 THEN 'CANCELADO'
        WHEN N.N % 100 < 98 THEN 'CASTIGADO'
        ELSE 'REESTRUCTURADO'
    END AS Estado,
    (N.N % 10) + 1 AS SucursalId
FROM Nums N
LEFT JOIN Aprobadas A ON N.N = A.SolicitudId
JOIN ProductosCredito P ON P.ProductoCreditoId = ISNULL(A.ProductoCreditoId, (N.N % 4) + 1);
SET IDENTITY_INSERT OperacionesCredito OFF;

-- CondicionesCredito
-- Schema: OperacionId, PeriodoGraciaMeses, TasaMoratoria, DiaCorte, FormaPago
INSERT INTO CondicionesCredito (OperacionId, PeriodoGraciaMeses, TasaMoratoria, DiaCorte, FormaPago)
SELECT 
    O.OperacionId,
    0 AS PeriodoGraciaMeses,
    O.TasaAnual * 1.5 AS TasaMoratoria,
    (O.OperacionId % 28) + 1 AS DiaCorte,
    'CAJA' AS FormaPago
FROM OperacionesCredito O;

-- Cuotas loop & Amortization
DECLARE @OpId INT, @Monto DECIMAL(18,2), @TasaAnual DECIMAL(8,4), @Plazo INT, @Estado VARCHAR(20), @FechaDesembolso DATE;
DECLARE @TasaMes DECIMAL(18,6), @CuotaFija DECIMAL(18,2);
DECLARE @Mes INT, @CapitalPendiente DECIMAL(18,2), @InteresMes DECIMAL(18,2), @CapitalMes DECIMAL(18,2);
DECLARE @FechaCuota DATE, @EstadoCuota VARCHAR(20);

DECLARE cur_Operaciones CURSOR FOR 
SELECT O.OperacionId, O.MontoDesembolsado, O.TasaAnual, O.PlazoMeses, O.Estado, O.FechaDesembolso
FROM OperacionesCredito O;

OPEN cur_Operaciones;
FETCH NEXT FROM cur_Operaciones INTO @OpId, @Monto, @TasaAnual, @Plazo, @Estado, @FechaDesembolso;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @TasaMes = (@TasaAnual / 100.0) / 12.0;
    
    IF @TasaMes > 0
        SET @CuotaFija = @Monto * @TasaMes * POWER(1 + @TasaMes, @Plazo) / (POWER(1 + @TasaMes, @Plazo) - 1);
    ELSE
        SET @CuotaFija = @Monto / @Plazo;

    SET @CapitalPendiente = @Monto;
    SET @Mes = 1;

    WHILE @Mes <= @Plazo
    BEGIN
        SET @InteresMes = @CapitalPendiente * @TasaMes;
        SET @CapitalMes = @CuotaFija - @InteresMes;
        IF @Mes = @Plazo SET @CapitalMes = @CapitalPendiente; -- Ajuste última cuota
        
        SET @FechaCuota = DATEADD(MONTH, @Mes, @FechaDesembolso);
        
        IF @Estado = 'CANCELADO'
            SET @EstadoCuota = 'PAGADA';
        ELSE IF @Estado = 'MORA' OR @Estado = 'CASTIGADO'
        BEGIN
            IF @FechaCuota < GETDATE() SET @EstadoCuota = CHOOSE((@Mes % 2)+1, 'PAGADA', 'VENCIDA');
            ELSE SET @EstadoCuota = 'PENDIENTE';
        END
        ELSE
        BEGIN
            IF @FechaCuota < GETDATE() SET @EstadoCuota = 'PAGADA';
            ELSE SET @EstadoCuota = 'PENDIENTE';
        END

        -- Schema Cuotas: CuotaId, OperacionId, NumeroCuota, FechaVencimiento, CapitalCuota, InteresCuota, SeguroCuota, ValorCuota, SaldoCapital, CapitalPagado, InteresPagado, SeguroPagado, Estado, FechaPago
        INSERT INTO Cuotas (OperacionId, NumeroCuota, FechaVencimiento, CapitalCuota, InteresCuota, SeguroCuota, ValorCuota, SaldoCapital, CapitalPagado, InteresPagado, SeguroPagado, Estado, FechaPago)
        VALUES (
            @OpId, @Mes, @FechaCuota, 
            @CapitalMes, @InteresMes, 0, 
            @CapitalMes + @InteresMes, 
            @CapitalPendiente - @CapitalMes,
            CASE WHEN @EstadoCuota = 'PAGADA' THEN @CapitalMes ELSE 0 END,
            CASE WHEN @EstadoCuota = 'PAGADA' THEN @InteresMes ELSE 0 END,
            0,
            @EstadoCuota,
            CASE WHEN @EstadoCuota = 'PAGADA' THEN @FechaCuota ELSE NULL END
        );

        SET @CapitalPendiente = @CapitalPendiente - @CapitalMes;
        SET @Mes = @Mes + 1;
    END

    -- SaldosOperacion
    -- Schema: OperacionId, SaldoCapitalVigente, SaldoCapitalVencido, SaldoInteresVigente, SaldoInteresVencido, SaldoSeguro, SaldoTotal, DiasEnMora, FechaUltimoPago, FechaActualizacion
    DECLARE @CapVigente DECIMAL(18,2) = ISNULL((SELECT SUM(CapitalCuota) FROM Cuotas WHERE OperacionId = @OpId AND Estado = 'PENDIENTE'), 0);
    DECLARE @CapVencido DECIMAL(18,2) = ISNULL((SELECT SUM(CapitalCuota) FROM Cuotas WHERE OperacionId = @OpId AND Estado = 'VENCIDA'), 0);
    DECLARE @IntVigente DECIMAL(18,2) = ISNULL((SELECT SUM(InteresCuota) FROM Cuotas WHERE OperacionId = @OpId AND Estado = 'PENDIENTE'), 0);
    DECLARE @IntVencido DECIMAL(18,2) = ISNULL((SELECT SUM(InteresCuota) FROM Cuotas WHERE OperacionId = @OpId AND Estado = 'VENCIDA'), 0);
    DECLARE @DiasMora INT = CASE WHEN @CapVencido > 0 THEN DATEDIFF(DAY, (SELECT MIN(FechaVencimiento) FROM Cuotas WHERE OperacionId = @OpId AND Estado = 'VENCIDA'), GETDATE()) ELSE 0 END;

    INSERT INTO SaldosOperacion (OperacionId, SaldoCapitalVigente, SaldoCapitalVencido, SaldoInteresVigente, SaldoInteresVencido, SaldoSeguro, SaldoTotal, DiasEnMora, FechaUltimoPago, FechaActualizacion)
    VALUES (@OpId, @CapVigente, @CapVencido, @IntVigente, @IntVencido, 0, @CapVigente + @CapVencido + @IntVigente + @IntVencido, @DiasMora, GETDATE(), GETDATE());

    FETCH NEXT FROM cur_Operaciones INTO @OpId, @Monto, @TasaAnual, @Plazo, @Estado, @FechaDesembolso;
END
CLOSE cur_Operaciones;
DEALLOCATE cur_Operaciones;

-- Garantias
-- Schema: OperacionId, TipoGarantiaId, Descripcion, ValorGarantia, FechaConstitucion, FechaVencimiento, Estado
INSERT INTO Garantias (OperacionId, TipoGarantiaId, Descripcion, ValorGarantia, FechaConstitucion, FechaVencimiento, Estado)
SELECT 
    O.OperacionId,
    CASE WHEN P.Codigo = 'CRED-HIP' THEN 1 ELSE 2 END AS TipoGarantiaId,
    'Garantía ' + P.Nombre + ' para operación ' + O.NumeroOperacion AS Descripcion,
    O.MontoDesembolsado * 1.5 AS ValorGarantia,
    O.FechaDesembolso,
    O.FechaVencimiento,
    'VIGENTE' AS Estado
FROM OperacionesCredito O
JOIN ProductosCredito P ON O.ProductoCreditoId = P.ProductoCreditoId
WHERE P.RequiereGarantia = 1 AND O.Estado IN ('VIGENTE', 'MORA', 'REESTRUCTURADO');

PRINT 'Créditos y cuotas poblados exitosamente.';
GO

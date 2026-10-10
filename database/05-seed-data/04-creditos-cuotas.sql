-- 04-creditos-cuotas.sql
-- Generates credit data: Solicitudes, Operaciones, Condiciones, Cuotas, Saldos, Garantias
-- Uses French amortization for realistic installments.

SET NOCOUNT ON;
GO

PRINT 'Poblando tablas de créditos...';

-- 1500 SolicitudesCredito
SET IDENTITY_INSERT SolicitudesCredito ON;
WITH Nums AS (
    SELECT TOP (1500) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO SolicitudesCredito (Id, ClienteId, ProductoCreditoId, MontoSolicitado, PlazoMeses, Estado, FechaSolicitud, Observaciones)
SELECT 
    N AS Id,
    (N % 1800) + 201 AS ClienteId,
    CASE 
        WHEN N % 100 < 60 THEN 1 -- Libre Inversión (60%)
        WHEN N % 100 < 80 THEN 2 -- Hipotecario (20%)
        WHEN N % 100 < 95 THEN 3 -- Vehículo (15%)
        ELSE 4 -- Rotativo (5%)
    END AS ProductoCreditoId,
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
    CASE 
        WHEN N % 100 < 40 THEN 'APROBADA'
        WHEN N % 100 < 65 THEN 'RECHAZADA'
        WHEN N % 100 < 80 THEN 'EN_ESTUDIO'
        WHEN N % 100 < 90 THEN 'RADICADA'
        WHEN N % 100 < 95 THEN 'DESISTIDA'
        ELSE 'VENCIDA'
    END AS Estado,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 1000), GETDATE()) AS FechaSolicitud,
    'Solicitud autogenerada' AS Observaciones
FROM Nums;
SET IDENTITY_INSERT SolicitudesCredito OFF;

-- 1000 OperacionesCredito (Approved solicitudes + migrated)
SET IDENTITY_INSERT OperacionesCredito ON;
WITH Aprobadas AS (
    SELECT Id, ClienteId, ProductoCreditoId, MontoSolicitado, PlazoMeses
    FROM SolicitudesCredito WHERE Estado = 'APROBADA'
),
Nums AS (
    SELECT TOP (1000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO OperacionesCredito (Id, SolicitudId, ClienteId, ProductoCreditoId, NumeroOperacion, MontoAprobado, SaldoCapital, Estado, FechaDesembolso)
SELECT 
    N.N AS Id,
    A.Id AS SolicitudId, -- Will be NULL for some
    ISNULL(A.ClienteId, (N.N % 1800) + 201) AS ClienteId,
    ISNULL(A.ProductoCreditoId, (N.N % 4) + 1) AS ProductoCreditoId,
    'CRE' + RIGHT('0000000000' + CAST(N.N AS VARCHAR), 10) AS NumeroOperacion,
    ISNULL(A.MontoSolicitado, 10000000) AS MontoAprobado,
    ISNULL(A.MontoSolicitado, 10000000) AS SaldoCapital, -- Will update later based on cuotas
    CASE 
        WHEN N.N % 100 < 70 THEN 'VIGENTE'
        WHEN N.N % 100 < 85 THEN 'MORA'
        WHEN N.N % 100 < 95 THEN 'CANCELADO'
        WHEN N.N % 100 < 98 THEN 'CASTIGADO'
        ELSE 'REESTRUCTURADO'
    END AS Estado,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 1500), GETDATE()) AS FechaDesembolso
FROM Nums N
LEFT JOIN Aprobadas A ON N.N = A.Id;
SET IDENTITY_INSERT OperacionesCredito OFF;

-- CondicionesCredito
INSERT INTO CondicionesCredito (OperacionId, TasaInteres, PlazoMeses, DiaPago, CuotaCalculada)
SELECT 
    O.Id AS OperacionId,
    P.TasaAnual AS TasaInteres,
    ISNULL(S.PlazoMeses, P.PlazoMinimoMeses + 12) AS PlazoMeses,
    (O.Id % 28) + 1 AS DiaPago,
    0 AS CuotaCalculada -- Will calculate
FROM OperacionesCredito O
JOIN ProductosCredito P ON O.ProductoCreditoId = P.Id
LEFT JOIN SolicitudesCredito S ON O.SolicitudId = S.Id;

-- Calculation variables for Cuotas loop
DECLARE @OpId INT, @Monto DECIMAL(18,2), @TasaAnual DECIMAL(5,2), @Plazo INT, @Estado VARCHAR(20), @FechaDesembolso DATE;
DECLARE @TasaMes DECIMAL(18,6), @CuotaFija DECIMAL(18,2);
DECLARE @Mes INT, @CapitalPendiente DECIMAL(18,2), @InteresMes DECIMAL(18,2), @CapitalMes DECIMAL(18,2);
DECLARE @FechaCuota DATE, @EstadoCuota VARCHAR(20);

DECLARE cur_Operaciones CURSOR FOR 
SELECT O.Id, O.MontoAprobado, C.TasaInteres, C.PlazoMeses, O.Estado, O.FechaDesembolso
FROM OperacionesCredito O
JOIN CondicionesCredito C ON O.Id = C.OperacionId;

OPEN cur_Operaciones;
FETCH NEXT FROM cur_Operaciones INTO @OpId, @Monto, @TasaAnual, @Plazo, @Estado, @FechaDesembolso;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @TasaMes = (@TasaAnual / 100.0) / 12.0;
    
    -- Cuota = P * r * (1+r)^n / ((1+r)^n - 1)
    IF @TasaMes > 0
        SET @CuotaFija = @Monto * @TasaMes * POWER(1 + @TasaMes, @Plazo) / (POWER(1 + @TasaMes, @Plazo) - 1);
    ELSE
        SET @CuotaFija = @Monto / @Plazo;

    UPDATE CondicionesCredito SET CuotaCalculada = @CuotaFija WHERE OperacionId = @OpId;

    SET @CapitalPendiente = @Monto;
    SET @Mes = 1;

    WHILE @Mes <= @Plazo
    BEGIN
        SET @InteresMes = @CapitalPendiente * @TasaMes;
        SET @CapitalMes = @CuotaFija - @InteresMes;
        IF @Mes = @Plazo SET @CapitalMes = @CapitalPendiente; -- Ajuste última cuota
        
        SET @FechaCuota = DATEADD(MONTH, @Mes, @FechaDesembolso);
        
        -- Determine Cuota status
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

        INSERT INTO Cuotas (OperacionId, NumeroCuota, FechaVencimiento, Capital, Intereses, Seguros, OtrosCargos, TotalCuota, Estado)
        VALUES (@OpId, @Mes, @FechaCuota, @CapitalMes, @InteresMes, 0, 0, @CapitalMes + @InteresMes, @EstadoCuota);

        SET @CapitalPendiente = @CapitalPendiente - @CapitalMes;
        SET @Mes = @Mes + 1;
    END

    -- Update SaldoOperacion
    DECLARE @CapitalVencido DECIMAL(18,2) = ISNULL((SELECT SUM(Capital) FROM Cuotas WHERE OperacionId = @OpId AND Estado = 'VENCIDA'), 0);
    DECLARE @CapitalPendienteCalc DECIMAL(18,2) = ISNULL((SELECT SUM(Capital) FROM Cuotas WHERE OperacionId = @OpId AND Estado IN ('PENDIENTE', 'VENCIDA')), 0);
    DECLARE @InteresVencido DECIMAL(18,2) = ISNULL((SELECT SUM(Intereses) FROM Cuotas WHERE OperacionId = @OpId AND Estado = 'VENCIDA'), 0);
    DECLARE @InteresMora DECIMAL(18,2) = CASE WHEN @CapitalVencido > 0 THEN @CapitalVencido * 0.05 ELSE 0 END;
    DECLARE @DiasMora INT = CASE WHEN @CapitalVencido > 0 THEN DATEDIFF(DAY, (SELECT MIN(FechaVencimiento) FROM Cuotas WHERE OperacionId = @OpId AND Estado = 'VENCIDA'), GETDATE()) ELSE 0 END;

    UPDATE OperacionesCredito SET SaldoCapital = @CapitalPendienteCalc WHERE Id = @OpId;

    INSERT INTO SaldosOperacion (OperacionId, CapitalPendiente, CapitalVencido, InteresCorrientePendiente, InteresMora, OtrosConceptos, DiasMora, FechaUltimaActualizacion)
    VALUES (@OpId, @CapitalPendienteCalc, @CapitalVencido, 0, @InteresMora, 0, @DiasMora, GETDATE());

    FETCH NEXT FROM cur_Operaciones INTO @OpId, @Monto, @TasaAnual, @Plazo, @Estado, @FechaDesembolso;
END
CLOSE cur_Operaciones;
DEALLOCATE cur_Operaciones;

-- Garantias
INSERT INTO Garantias (OperacionId, TipoGarantiaId, Descripcion, ValorCobertura, FechaConstitucion, Estado)
SELECT 
    O.Id AS OperacionId,
    CASE WHEN P.Codigo = 'CRED-HIP' THEN 1 ELSE 2 END AS TipoGarantiaId,
    'Garantía ' + P.Nombre + ' para operación ' + O.NumeroOperacion,
    O.MontoAprobado * 1.5 AS ValorCobertura,
    O.FechaDesembolso,
    'VIGENTE'
FROM OperacionesCredito O
JOIN ProductosCredito P ON O.ProductoCreditoId = P.Id
WHERE P.RequiereGarantia = 1 AND O.Estado IN ('VIGENTE', 'MORA', 'REESTRUCTURADO');

PRINT 'Créditos y cuotas poblados exitosamente.';
GO

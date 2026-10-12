USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarOperacionCredito
-- Descripción: Consulta el detalle completo de una operación de crédito.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarOperacionCredito]
    @OperacionId INT = NULL,
    @NumeroOperacion VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 1. Detalle de Operación
    SELECT 
        oc.OperacionId,
        oc.NumeroOperacion,
        oc.SolicitudId,
        oc.ClienteId,
        (c.PrimerNombre + ' ' + ISNULL(c.SegundoNombre + ' ', '') + c.PrimerApellido + ISNULL(' ' + c.SegundoApellido, '')) AS NombreCliente,
        oc.ProductoCreditoId,
        pc.Nombre AS NombreProductoCredito,
        oc.MontoDesembolsado,
        oc.MontoDesembolsado AS MontoOriginal, -- Alias de compatibilidad
        oc.TasaAnual,
        oc.TasaAnual AS TasaInteres,         -- Alias de compatibilidad
        oc.PlazoMeses,
        oc.NumeroCuotas,
        oc.FechaDesembolso,
        oc.FechaVencimiento,
        oc.Estado,
        oc.Estado AS EstadoOperacion,         -- Alias
        oc.SucursalId,
        s.Nombre AS NombreSucursal,
        cc.TasaMoratoria,
        cc.DiaCorte,
        cc.FormaPago,
        so.SaldoTotal
    FROM OperacionesCredito oc
    INNER JOIN ProductosCredito pc ON oc.ProductoCreditoId = pc.ProductoCreditoId
    INNER JOIN Clientes c ON oc.ClienteId = c.ClienteId
    LEFT JOIN Sucursales s ON oc.SucursalId = s.SucursalId
    LEFT JOIN CondicionesCredito cc ON oc.OperacionId = cc.OperacionId
    LEFT JOIN SaldosOperacion so ON oc.OperacionId = so.OperacionId
    WHERE (@OperacionId IS NOT NULL AND oc.OperacionId = @OperacionId)
       OR (@NumeroOperacion IS NOT NULL AND oc.NumeroOperacion = @NumeroOperacion);

    -- 2. Garantías de la Operación
    SELECT 
        g.GarantiaId,
        g.OperacionId,
        g.TipoGarantiaId,
        tg.Nombre AS TipoGarantia,
        g.Descripcion,
        g.ValorGarantia,
        g.FechaConstitucion,
        g.FechaVencimiento,
        g.Estado
    FROM Garantias g
    INNER JOIN TiposGarantia tg ON g.TipoGarantiaId = tg.TipoGarantiaId
    INNER JOIN OperacionesCredito oc ON g.OperacionId = oc.OperacionId
    WHERE (@OperacionId IS NOT NULL AND oc.OperacionId = @OperacionId)
       OR (@NumeroOperacion IS NOT NULL AND oc.NumeroOperacion = @NumeroOperacion);
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarSaldosVigenteVencido
-- Descripción: Consulta los saldos vigentes y vencidos de una operación.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarSaldosVigenteVencido]
    @OperacionId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        so.SaldoOperacionId,
        so.OperacionId,
        so.SaldoCapitalVigente,
        so.SaldoCapitalVigente AS SaldoVigente, -- Alias de compatibilidad
        so.SaldoCapitalVencido,
        so.SaldoCapitalVencido AS SaldoVencido, -- Alias de compatibilidad
        so.SaldoInteresVigente,
        so.SaldoInteresVencido,
        so.SaldoSeguro,
        so.SaldoTotal,
        so.DiasEnMora,
        so.FechaUltimoPago,
        so.FechaActualizacion
    FROM SaldosOperacion so
    WHERE so.OperacionId = @OperacionId;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarCuotasOperacion
-- Descripción: Consulta el plan de cuotas de una operación.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarCuotasOperacion]
    @OperacionId INT,
    @SoloVencidas BIT = 0,
    @SoloPendientes BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CuotaId,
        c.OperacionId,
        c.NumeroCuota,
        c.FechaVencimiento,
        c.CapitalCuota,
        c.CapitalCuota AS Capital, -- Alias
        c.InteresCuota,
        c.InteresCuota AS Interes, -- Alias
        c.SeguroCuota,
        c.ValorCuota,
        c.SaldoCapital,
        c.CapitalPagado,
        c.InteresPagado,
        c.SeguroPagado,
        c.Estado,
        c.FechaPago
    FROM Cuotas c
    WHERE c.OperacionId = @OperacionId
      AND (@SoloVencidas = 0 OR c.Estado = 'VENCIDA')
      AND (@SoloPendientes = 0 OR c.Estado IN ('PENDIENTE', 'PARCIAL', 'VENCIDA'))
    ORDER BY c.NumeroCuota ASC;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarCuotasPendientesVencidas
-- Descripción: Consulta cuotas vencidas o pendientes de clientes.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarCuotasPendientesVencidas]
    @ClienteId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CuotaId,
        c.OperacionId,
        oc.NumeroOperacion,
        oc.ClienteId,
        (cli.PrimerNombre + ' ' + cli.PrimerApellido) AS NombreCliente,
        c.NumeroCuota,
        c.FechaVencimiento,
        c.CapitalCuota,
        c.CapitalCuota AS Capital,
        c.InteresCuota,
        c.InteresCuota AS Interes,
        c.SeguroCuota,
        c.ValorCuota,
        c.SaldoCapital,
        c.CapitalPagado,
        c.InteresPagado,
        c.SeguroPagado,
        c.Estado,
        c.FechaPago
    FROM Cuotas c
    INNER JOIN OperacionesCredito oc ON c.OperacionId = oc.OperacionId
    INNER JOIN Clientes cli ON oc.ClienteId = cli.ClienteId
    WHERE (@ClienteId IS NULL OR oc.ClienteId = @ClienteId)
      AND c.Estado IN ('PENDIENTE', 'PARCIAL', 'VENCIDA')
    ORDER BY c.FechaVencimiento ASC;
END;
GO

USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarOperacionCredito
-- Descripción: Consulta el detalle de una operación de crédito.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarOperacionCredito]
    @OperacionId INT = NULL,
    @NumeroOperacion VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        oc.OperacionId,
        oc.NumeroOperacion,
        oc.MontoAprobado,
        oc.FechaDesembolso,
        oc.FechaVencimiento,
        oc.Estado AS EstadoOperacion,
        pc.Nombre AS ProductoCredito,
        c.PrimerNombre + ' ' + c.PrimerApellido AS NombreCliente,
        c.NumeroIdentificacion,
        cc.TasaInteres,
        cc.PlazoMeses,
        cc.DiaPago,
        so.SaldoTotal
    FROM OperacionesCredito oc
    INNER JOIN ProductosCredito pc ON oc.ProductoCreditoId = pc.ProductoCreditoId
    INNER JOIN Clientes c ON oc.ClienteId = c.ClienteId
    INNER JOIN CondicionesCredito cc ON oc.OperacionId = cc.OperacionId
    INNER JOIN SaldosOperacion so ON oc.OperacionId = so.OperacionId
    WHERE (@OperacionId IS NOT NULL AND oc.OperacionId = @OperacionId)
       OR (@NumeroOperacion IS NOT NULL AND oc.NumeroOperacion = @NumeroOperacion);

    -- Garantías
    SELECT g.* 
    FROM GarantiasCredito g
    INNER JOIN OperacionesCredito oc ON g.OperacionId = oc.OperacionId
    WHERE (@OperacionId IS NOT NULL AND oc.OperacionId = @OperacionId)
       OR (@NumeroOperacion IS NOT NULL AND oc.NumeroOperacion = @NumeroOperacion);
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarSaldosVigenteVencido
-- Descripción: Consulta los saldos vigentes y vencidos.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarSaldosVigenteVencido]
    @OperacionId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        so.SaldoCapitalVigente,
        so.SaldoCapitalVencido,
        so.SaldoInteresVigente,
        so.SaldoInteresVencido,
        so.SaldoTotal,
        so.DiasEnMora
    FROM SaldosOperacion so
    WHERE so.OperacionId = @OperacionId;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarCuotasOperacion
-- Descripción: Consulta cuotas de una operación.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarCuotasOperacion]
    @OperacionId INT,
    @SoloVencidas BIT = 0,
    @SoloPendientes BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        NumeroCuota,
        FechaVencimiento,
        CapitalEsperado,
        InteresEsperado,
        SeguroEsperado,
        TotalCuota,
        CapitalPagado,
        InteresPagado,
        SeguroPagado,
        TotalPagado,
        Estado,
        DiasAtraso
    FROM Cuotas
    WHERE OperacionId = @OperacionId
      AND (@SoloVencidas = 0 OR Estado = 'VENCIDA')
      AND (@SoloPendientes = 0 OR Estado IN ('PENDIENTE', 'PARCIAL', 'VENCIDA'))
    ORDER BY NumeroCuota;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarCuotasPendientesVencidas
-- Descripción: Consulta todas las cuotas pendientes o vencidas (opcionalmente por cliente).
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarCuotasPendientesVencidas]
    @ClienteId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        cu.CuotaId,
        cu.NumeroCuota,
        oc.NumeroOperacion,
        pc.Nombre AS ProductoCredito,
        c.PrimerNombre + ' ' + c.PrimerApellido AS Cliente,
        cu.FechaVencimiento,
        cu.TotalCuota - cu.TotalPagado AS SaldoPendienteCuota,
        cu.Estado
    FROM Cuotas cu
    INNER JOIN OperacionesCredito oc ON cu.OperacionId = oc.OperacionId
    INNER JOIN ProductosCredito pc ON oc.ProductoCreditoId = pc.ProductoCreditoId
    INNER JOIN Clientes c ON oc.ClienteId = c.ClienteId
    WHERE cu.Estado IN ('PENDIENTE', 'PARCIAL', 'VENCIDA')
      AND (@ClienteId IS NULL OR c.ClienteId = @ClienteId)
    ORDER BY cu.FechaVencimiento ASC;
END;
GO

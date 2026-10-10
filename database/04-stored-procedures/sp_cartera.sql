USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_ReporteCartera
-- Descripción: Genera un reporte detallado de cartera.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ReporteCartera]
    @FechaCorte DATE = NULL,
    @SucursalId INT = NULL,
    @Estado VARCHAR(20) = NULL,
    @CategoriaRiesgo CHAR(1) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @FechaCorte IS NULL SET @FechaCorte = GETDATE();

    SELECT 
        pc.Nombre AS TipoProducto,
        cr.Categoria AS CategoriaRiesgo,
        COUNT(oc.OperacionId) AS NumeroOperaciones,
        SUM(so.SaldoTotal) AS SaldoTotal,
        SUM(so.SaldoCapitalVigente) AS CapitalVigente,
        SUM(so.SaldoCapitalVencido) AS CapitalVencido
    FROM OperacionesCredito oc
    INNER JOIN Clientes c ON oc.ClienteId = c.ClienteId
    INNER JOIN ProductosCredito pc ON oc.ProductoCreditoId = pc.ProductoCreditoId
    INNER JOIN SaldosOperacion so ON oc.OperacionId = so.OperacionId
    INNER JOIN Sucursales s ON c.SucursalId = s.SucursalId
    LEFT JOIN (
        SELECT OperacionId, Categoria,
               ROW_NUMBER() OVER(PARTITION BY OperacionId ORDER BY FechaClasificacion DESC) as rn
        FROM ClasificacionesRiesgo
    ) cr ON oc.OperacionId = cr.OperacionId AND cr.rn = 1
    WHERE oc.FechaDesembolso <= @FechaCorte
      AND (@SucursalId IS NULL OR s.SucursalId = @SucursalId)
      AND (@Estado IS NULL OR oc.Estado = @Estado)
      AND (@CategoriaRiesgo IS NULL OR cr.Categoria = @CategoriaRiesgo)
    GROUP BY GROUPING SETS (
        (pc.Nombre, cr.Categoria),
        (pc.Nombre),
        ()
    )
    ORDER BY pc.Nombre, cr.Categoria;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ReporteCarteraVencida
-- Descripción: Genera un reporte de cartera vencida.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ReporteCarteraVencida]
    @DiasVencidoMinimo INT = 1
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        oc.NumeroOperacion,
        c.PrimerNombre + ' ' + c.PrimerApellido AS Cliente,
        c.NumeroIdentificacion,
        s.Nombre AS Sucursal,
        so.DiasEnMora,
        so.SaldoCapitalVencido,
        so.SaldoInteresVencido,
        (so.SaldoCapitalVencido + so.SaldoInteresVencido) AS TotalVencido
    FROM SaldosOperacion so
    INNER JOIN OperacionesCredito oc ON so.OperacionId = oc.OperacionId
    INNER JOIN Clientes c ON oc.ClienteId = c.ClienteId
    INNER JOIN Sucursales s ON c.SucursalId = s.SucursalId
    WHERE so.DiasEnMora >= @DiasVencidoMinimo
      AND so.SaldoCapitalVencido > 0
    ORDER BY so.DiasEnMora DESC;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ReporteResumenRiesgo
-- Descripción: Genera resumen de cartera por riesgo.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ReporteResumenRiesgo]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ISNULL(cr.Categoria, 'SIN CLASIFICAR') AS CategoriaRiesgo,
        COUNT(oc.OperacionId) AS NumeroOperaciones,
        SUM(so.SaldoTotal) AS SaldoTotal
    FROM OperacionesCredito oc
    INNER JOIN SaldosOperacion so ON oc.OperacionId = so.OperacionId
    LEFT JOIN (
        SELECT OperacionId, Categoria,
               ROW_NUMBER() OVER(PARTITION BY OperacionId ORDER BY FechaClasificacion DESC) as rn
        FROM ClasificacionesRiesgo
    ) cr ON oc.OperacionId = cr.OperacionId AND cr.rn = 1
    WHERE oc.Estado IN ('VIGENTE', 'MORA')
    GROUP BY ISNULL(cr.Categoria, 'SIN CLASIFICAR')
    ORDER BY CategoriaRiesgo ASC;
END;
GO

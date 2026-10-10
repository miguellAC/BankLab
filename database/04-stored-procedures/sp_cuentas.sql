USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarCuentasCliente
-- Descripción: Consulta las cuentas de un cliente específico.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarCuentasCliente]
    @ClienteId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CuentaId,
        c.NumeroCuenta,
        pb.Nombre AS NombreProducto,
        tc.Nombre AS TipoCuenta,
        c.SaldoActual,
        c.SaldoDisponible,
        c.Estado,
        s.Nombre AS Sucursal
    FROM Cuentas c
    INNER JOIN ProductosBancarios pb ON c.ProductoBancarioId = pb.ProductoBancarioId
    INNER JOIN TiposCuenta tc ON c.TipoCuentaId = tc.TipoCuentaId
    INNER JOIN Sucursales s ON c.SucursalId = s.SucursalId
    WHERE c.ClienteId = @ClienteId;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarMovimientosCuenta
-- Descripción: Consulta movimientos de cuenta con paginación.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarMovimientosCuenta]
    @CuentaId INT,
    @FechaDesde DATE = NULL,
    @FechaHasta DATE = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 50
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @FechaDesde IS NULL SET @FechaDesde = DATEADD(DAY, -30, GETDATE());
    IF @FechaHasta IS NULL SET @FechaHasta = GETDATE();

    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

    SELECT 
        m.MovimientoId,
        m.FechaMovimiento,
        tm.Nombre AS TipoMovimiento,
        m.Monto,
        m.SaldoDespues,
        m.Referencia,
        m.Descripcion
    FROM MovimientosCuenta m
    INNER JOIN TiposMovimiento tm ON m.TipoMovimientoId = tm.TipoMovimientoId
    WHERE m.CuentaId = @CuentaId
      AND CAST(m.FechaMovimiento AS DATE) BETWEEN @FechaDesde AND @FechaHasta
    ORDER BY m.FechaMovimiento DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarSaldoCuenta
-- Descripción: Consulta el saldo actual de una cuenta.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarSaldoCuenta]
    @CuentaId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.SaldoActual,
        c.SaldoDisponible,
        c.Estado,
        (SELECT MAX(FechaMovimiento) FROM MovimientosCuenta WHERE CuentaId = c.CuentaId) AS FechaUltimoMovimiento
    FROM Cuentas c
    WHERE c.CuentaId = @CuentaId;
END;
GO

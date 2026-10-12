USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarCuentasCliente
-- Descripción: Consulta las cuentas de un cliente específico.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarCuentasCliente]
    @ClienteId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CuentaId,
        c.NumeroCuenta,
        c.ClienteId,
        (cli.PrimerNombre + ' ' + cli.PrimerApellido) AS NombreCliente,
        c.ProductoId,
        pb.Nombre AS NombreProducto,
        c.TipoCuentaId,
        tc.Nombre AS NombreTipoCuenta,
        tc.Nombre AS TipoCuenta, -- Alias compatibilidad
        c.SucursalId,
        s.Nombre AS NombreSucursal,
        s.Nombre AS Sucursal,    -- Alias compatibilidad
        c.Saldo,
        c.Saldo AS SaldoActual,  -- Alias compatibilidad
        c.SaldoDisponible,
        c.Estado,
        c.FechaApertura,
        c.Moneda
    FROM Cuentas c
    INNER JOIN Clientes cli ON c.ClienteId = cli.ClienteId
    INNER JOIN ProductosBancarios pb ON c.ProductoId = pb.ProductoId
    INNER JOIN TiposCuenta tc ON c.TipoCuentaId = tc.TipoCuentaId
    LEFT JOIN Sucursales s ON c.SucursalId = s.SucursalId
    WHERE c.ClienteId = @ClienteId;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarMovimientosCuenta
-- Descripción: Consulta movimientos de cuenta con paginación.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarMovimientosCuenta]
    @CuentaId INT,
    @FechaDesde DATE = NULL,
    @FechaHasta DATE = NULL,
    @Desde DATE = NULL, -- Alias
    @Hasta DATE = NULL, -- Alias
    @PageNumber INT = 1,
    @PageSize INT = 50
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @FDesde DATE = COALESCE(@FechaDesde, @Desde, DATEADD(DAY, -30, GETDATE()));
    DECLARE @FHasta DATE = COALESCE(@FechaHasta, @Hasta, GETDATE());
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

    -- Total para paginación
    SELECT COUNT(*) AS Total, COUNT(*) AS TotalRegistros
    FROM Movimientos
    WHERE CuentaId = @CuentaId
      AND CAST(FechaMovimiento AS DATE) BETWEEN @FDesde AND @FHasta;

    -- Movimientos
    SELECT 
        m.MovimientoId,
        m.CuentaId,
        m.TipoTransaccionId,
        tt.Nombre AS NombreTipoTransaccion,
        tt.Nombre AS TipoMovimiento, -- Alias
        tt.Naturaleza,
        m.Monto,
        m.SaldoAnterior,
        m.SaldoPosterior,
        m.SaldoPosterior AS SaldoDespues, -- Alias
        m.Descripcion,
        m.Referencia,
        m.CanalOrigen,
        m.FechaMovimiento,
        m.FechaMovimiento AS Fecha,       -- Alias
        m.FechaValor
    FROM Movimientos m
    INNER JOIN TiposTransaccion tt ON m.TipoTransaccionId = tt.TipoTransaccionId
    WHERE m.CuentaId = @CuentaId
      AND CAST(m.FechaMovimiento AS DATE) BETWEEN @FDesde AND @FHasta
    ORDER BY m.FechaMovimiento DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarSaldoCuenta
-- Descripción: Consulta el saldo actual y disponible de una cuenta.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarSaldoCuenta]
    @CuentaId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CuentaId,
        c.NumeroCuenta,
        c.Saldo,
        c.Saldo AS SaldoContable,       -- Alias compatibilidad
        c.SaldoDisponible,
        c.Estado,
        (SELECT MAX(FechaMovimiento) FROM Movimientos WHERE CuentaId = c.CuentaId) AS FechaUltimoMovimiento
    FROM Cuentas c
    WHERE c.CuentaId = @CuentaId;
END;
GO

USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_RegistrarPago
-- Descripción: Registra y aplica un pago a una operación de crédito.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_RegistrarPago]
    @OperacionId INT,
    @MontoPago DECIMAL(18,2),
    @TipoPago VARCHAR(20),
    @CanalPago VARCHAR(20),
    @CuentaOrigenId INT = NULL,
    @Referencia VARCHAR(50) = NULL,
    @Usuario VARCHAR(50),
    @PagoId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validaciones
    IF @MontoPago <= 0
    BEGIN
        RAISERROR('El monto de pago debe ser mayor a cero.', 16, 1);
        RETURN;
    END
    
    DECLARE @EstadoOperacion VARCHAR(20);
    SELECT @EstadoOperacion = Estado FROM OperacionesCredito WHERE OperacionId = @OperacionId;
    
    IF @EstadoOperacion NOT IN ('VIGENTE', 'MORA')
    BEGIN
        RAISERROR('La operación no está en estado válido para recibir pagos.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Insertar el pago principal
        INSERT INTO Pagos (OperacionId, MontoTotal, FechaPago, TipoPago, CanalPago, CuentaOrigenId, Referencia, UsuarioRegistro)
        VALUES (@OperacionId, @MontoPago, GETDATE(), @TipoPago, @CanalPago, @CuentaOrigenId, @Referencia, @Usuario);
        
        SET @PagoId = SCOPE_IDENTITY();
        
        -- Cursor para aplicar a cuotas
        DECLARE @CuotaId INT, @NumeroCuota INT, @CapitalPendiente DECIMAL(18,2), @InteresPendiente DECIMAL(18,2), @SeguroPendiente DECIMAL(18,2);
        DECLARE @MontoRestante DECIMAL(18,2) = @MontoPago;
        DECLARE @AplicadoCapital DECIMAL(18,2), @AplicadoInteres DECIMAL(18,2), @AplicadoSeguro DECIMAL(18,2);
        
        DECLARE curCuotas CURSOR FOR
        SELECT 
            CuotaId, NumeroCuota,
            (CapitalEsperado - CapitalPagado),
            (InteresEsperado - InteresPagado),
            (SeguroEsperado - SeguroPagado)
        FROM Cuotas
        WHERE OperacionId = @OperacionId AND Estado IN ('VENCIDA', 'PENDIENTE', 'PARCIAL')
        ORDER BY CASE Estado WHEN 'VENCIDA' THEN 1 ELSE 2 END, FechaVencimiento ASC;
        
        OPEN curCuotas;
        FETCH NEXT FROM curCuotas INTO @CuotaId, @NumeroCuota, @CapitalPendiente, @InteresPendiente, @SeguroPendiente;
        
        WHILE @@FETCH_STATUS = 0 AND @MontoRestante > 0
        BEGIN
            SET @AplicadoSeguro = 0;
            SET @AplicadoInteres = 0;
            SET @AplicadoCapital = 0;
            
            -- Aplicar a Seguro
            IF @MontoRestante > 0 AND @SeguroPendiente > 0
            BEGIN
                SET @AplicadoSeguro = CASE WHEN @MontoRestante >= @SeguroPendiente THEN @SeguroPendiente ELSE @MontoRestante END;
                SET @MontoRestante = @MontoRestante - @AplicadoSeguro;
            END
            
            -- Aplicar a Interes
            IF @MontoRestante > 0 AND @InteresPendiente > 0
            BEGIN
                SET @AplicadoInteres = CASE WHEN @MontoRestante >= @InteresPendiente THEN @InteresPendiente ELSE @MontoRestante END;
                SET @MontoRestante = @MontoRestante - @AplicadoInteres;
            END
            
            -- Aplicar a Capital
            IF @MontoRestante > 0 AND @CapitalPendiente > 0
            BEGIN
                SET @AplicadoCapital = CASE WHEN @MontoRestante >= @CapitalPendiente THEN @CapitalPendiente ELSE @MontoRestante END;
                SET @MontoRestante = @MontoRestante - @AplicadoCapital;
            END
            
            -- Actualizar Cuota
            UPDATE Cuotas
            SET 
                SeguroPagado = SeguroPagado + @AplicadoSeguro,
                InteresPagado = InteresPagado + @AplicadoInteres,
                CapitalPagado = CapitalPagado + @AplicadoCapital,
                TotalPagado = TotalPagado + @AplicadoSeguro + @AplicadoInteres + @AplicadoCapital,
                Estado = CASE WHEN (TotalCuota - (TotalPagado + @AplicadoSeguro + @AplicadoInteres + @AplicadoCapital)) <= 0 THEN 'PAGADA' ELSE 'PARCIAL' END
            WHERE CuotaId = @CuotaId;
            
            -- Registrar Aplicación de Pago
            IF (@AplicadoSeguro + @AplicadoInteres + @AplicadoCapital) > 0
            BEGIN
                INSERT INTO AplicacionesPagos (PagoId, CuotaId, MontoAplicadoCapital, MontoAplicadoInteres, MontoAplicadoSeguro)
                VALUES (@PagoId, @CuotaId, @AplicadoCapital, @AplicadoInteres, @AplicadoSeguro);
            END
            
            FETCH NEXT FROM curCuotas INTO @CuotaId, @NumeroCuota, @CapitalPendiente, @InteresPendiente, @SeguroPendiente;
        END
        
        CLOSE curCuotas;
        DEALLOCATE curCuotas;
        
        -- Actualizar Saldos Operacion
        DECLARE @TotalCapitalAplicado DECIMAL(18,2), @TotalInteresAplicado DECIMAL(18,2);
        SELECT 
            @TotalCapitalAplicado = ISNULL(SUM(MontoAplicadoCapital), 0),
            @TotalInteresAplicado = ISNULL(SUM(MontoAplicadoInteres), 0)
        FROM AplicacionesPagos WHERE PagoId = @PagoId;
        
        UPDATE SaldosOperacion
        SET 
            SaldoCapitalVigente = CASE WHEN SaldoCapitalVigente >= @TotalCapitalAplicado THEN SaldoCapitalVigente - @TotalCapitalAplicado ELSE 0 END,
            SaldoInteresVigente = CASE WHEN SaldoInteresVigente >= @TotalInteresAplicado THEN SaldoInteresVigente - @TotalInteresAplicado ELSE 0 END,
            SaldoTotal = SaldoTotal - (@TotalCapitalAplicado + @TotalInteresAplicado)
        WHERE OperacionId = @OperacionId;
        
        -- Verificar si se canceló la operación (todas las cuotas pagadas)
        IF NOT EXISTS (SELECT 1 FROM Cuotas WHERE OperacionId = @OperacionId AND Estado IN ('PENDIENTE', 'PARCIAL', 'VENCIDA'))
        BEGIN
            UPDATE OperacionesCredito SET Estado = 'CANCELADO' WHERE OperacionId = @OperacionId;
        END
        
        -- Auditoría
        INSERT INTO RegistrosAuditoria (TipoEntidad, EntidadId, Accion, ValorNuevo, Usuario, FechaRegistro)
        VALUES ('Pago', @PagoId, 'CREAR', 'Pago procesado por monto ' + CAST(@MontoPago AS VARCHAR), @Usuario, GETDATE());
        
        COMMIT TRANSACTION;
        
        -- Retornar el detalle de aplicación
        SELECT * FROM AplicacionesPagos WHERE PagoId = @PagoId;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        IF CURSOR_STATUS('global', 'curCuotas') >= -1
        BEGIN
            CLOSE curCuotas;
            DEALLOCATE curCuotas;
        END
        
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarPagosOperacion
-- Descripción: Consulta pagos de una operación.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarPagosOperacion]
    @OperacionId INT,
    @FechaDesde DATE = NULL,
    @FechaHasta DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.PagoId,
        p.FechaPago,
        p.MontoTotal,
        p.TipoPago,
        p.CanalPago,
        p.Referencia,
        p.UsuarioRegistro,
        a.CuotaId,
        a.MontoAplicadoCapital,
        a.MontoAplicadoInteres,
        a.MontoAplicadoSeguro
    FROM Pagos p
    LEFT JOIN AplicacionesPagos a ON p.PagoId = a.PagoId
    WHERE p.OperacionId = @OperacionId
      AND (@FechaDesde IS NULL OR CAST(p.FechaPago AS DATE) >= @FechaDesde)
      AND (@FechaHasta IS NULL OR CAST(p.FechaPago AS DATE) <= @FechaHasta)
    ORDER BY p.FechaPago DESC, a.CuotaId ASC;
END;
GO

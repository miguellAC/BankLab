USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_RegistrarPago
-- Descripción: Registra y aplica un pago a una operación de crédito.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_RegistrarPago]
    @OperacionId INT,
    @MontoPago DECIMAL(18,2),
    @TipoPago VARCHAR(20),
    @CanalPago VARCHAR(20),
    @CuentaOrigenId INT = NULL,
    @Referencia VARCHAR(50) = NULL,
    @Usuario VARCHAR(50),
    @PagoId BIGINT OUTPUT
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
        -- FechaAplicacion = fecha actual (DATE), Estado inicial = APLICADO
        INSERT INTO Pagos (OperacionId, CuentaOrigenId, MontoPago, FechaPago, FechaAplicacion, TipoPago, CanalPago, Referencia, Estado, UsuarioCreacion)
        VALUES (@OperacionId, @CuentaOrigenId, @MontoPago, GETDATE(), CAST(GETDATE() AS DATE), @TipoPago, @CanalPago, @Referencia, 'APLICADO', @Usuario);

        SET @PagoId = SCOPE_IDENTITY();

        -- Cursor para aplicar a cuotas (seguro > interés > capital)
        DECLARE @CuotaId BIGINT, @NumeroCuota INT;
        DECLARE @CapitalPendiente DECIMAL(18,2), @InteresPendiente DECIMAL(18,2), @SeguroPendiente DECIMAL(18,2);
        DECLARE @MontoRestante DECIMAL(18,2) = @MontoPago;
        DECLARE @AplicadoCapital DECIMAL(18,2), @AplicadoInteres DECIMAL(18,2), @AplicadoSeguro DECIMAL(18,2);

        DECLARE curCuotas CURSOR FOR
        SELECT
            CuotaId, NumeroCuota,
            (CapitalCuota - CapitalPagado),
            (InteresCuota - InteresPagado),
            (SeguroCuota  - SeguroPagado)
        FROM Cuotas
        WHERE OperacionId = @OperacionId AND Estado IN ('VENCIDA', 'PENDIENTE', 'PARCIAL')
        ORDER BY CASE Estado WHEN 'VENCIDA' THEN 1 ELSE 2 END, FechaVencimiento ASC;

        OPEN curCuotas;
        FETCH NEXT FROM curCuotas INTO @CuotaId, @NumeroCuota, @CapitalPendiente, @InteresPendiente, @SeguroPendiente;

        WHILE @@FETCH_STATUS = 0 AND @MontoRestante > 0
        BEGIN
            SET @AplicadoSeguro  = 0;
            SET @AplicadoInteres = 0;
            SET @AplicadoCapital = 0;

            -- Aplicar a Seguro
            IF @MontoRestante > 0 AND @SeguroPendiente > 0
            BEGIN
                SET @AplicadoSeguro = CASE WHEN @MontoRestante >= @SeguroPendiente THEN @SeguroPendiente ELSE @MontoRestante END;
                SET @MontoRestante  = @MontoRestante - @AplicadoSeguro;
            END

            -- Aplicar a Interés
            IF @MontoRestante > 0 AND @InteresPendiente > 0
            BEGIN
                SET @AplicadoInteres = CASE WHEN @MontoRestante >= @InteresPendiente THEN @InteresPendiente ELSE @MontoRestante END;
                SET @MontoRestante   = @MontoRestante - @AplicadoInteres;
            END

            -- Aplicar a Capital
            IF @MontoRestante > 0 AND @CapitalPendiente > 0
            BEGIN
                SET @AplicadoCapital = CASE WHEN @MontoRestante >= @CapitalPendiente THEN @CapitalPendiente ELSE @MontoRestante END;
                SET @MontoRestante   = @MontoRestante - @AplicadoCapital;
            END

            -- Actualizar Cuota
            -- Cuota se considera PAGADA cuando el total pagado iguala ValorCuota
            UPDATE Cuotas
            SET
                SeguroPagado  = SeguroPagado  + @AplicadoSeguro,
                InteresPagado = InteresPagado + @AplicadoInteres,
                CapitalPagado = CapitalPagado + @AplicadoCapital,
                Estado = CASE
                             WHEN (ValorCuota - (SeguroPagado + InteresPagado + CapitalPagado + @AplicadoSeguro + @AplicadoInteres + @AplicadoCapital)) <= 0
                             THEN 'PAGADA'
                             ELSE 'PARCIAL'
                         END,
                FechaPago = CASE
                                WHEN (ValorCuota - (SeguroPagado + InteresPagado + CapitalPagado + @AplicadoSeguro + @AplicadoInteres + @AplicadoCapital)) <= 0
                                THEN CAST(GETDATE() AS DATE)
                                ELSE FechaPago
                            END
            WHERE CuotaId = @CuotaId;

            -- Registrar Aplicación de Pago
            IF (@AplicadoSeguro + @AplicadoInteres + @AplicadoCapital) > 0
            BEGIN
                INSERT INTO AplicacionesPagos (PagoId, CuotaId, MontoCapital, MontoInteres, MontoSeguro, MontoTotal)
                VALUES (@PagoId, @CuotaId, @AplicadoCapital, @AplicadoInteres, @AplicadoSeguro,
                        @AplicadoCapital + @AplicadoInteres + @AplicadoSeguro);
            END

            FETCH NEXT FROM curCuotas INTO @CuotaId, @NumeroCuota, @CapitalPendiente, @InteresPendiente, @SeguroPendiente;
        END

        CLOSE curCuotas;
        DEALLOCATE curCuotas;

        -- Actualizar Saldos Operación
        DECLARE @TotalCapitalAplicado DECIMAL(18,2), @TotalInteresAplicado DECIMAL(18,2);
        SELECT
            @TotalCapitalAplicado = ISNULL(SUM(MontoCapital), 0),
            @TotalInteresAplicado = ISNULL(SUM(MontoInteres), 0)
        FROM AplicacionesPagos WHERE PagoId = @PagoId;

        UPDATE SaldosOperacion
        SET
            SaldoCapitalVigente = CASE WHEN SaldoCapitalVigente >= @TotalCapitalAplicado THEN SaldoCapitalVigente - @TotalCapitalAplicado ELSE 0 END,
            SaldoInteresVigente = CASE WHEN SaldoInteresVigente >= @TotalInteresAplicado THEN SaldoInteresVigente - @TotalInteresAplicado ELSE 0 END,
            SaldoTotal          = CASE WHEN SaldoTotal >= (@TotalCapitalAplicado + @TotalInteresAplicado) THEN SaldoTotal - (@TotalCapitalAplicado + @TotalInteresAplicado) ELSE 0 END,
            FechaActualizacion  = GETDATE()
        WHERE OperacionId = @OperacionId;

        -- Verificar si se canceló la operación (todas las cuotas pagadas)
        IF NOT EXISTS (SELECT 1 FROM Cuotas WHERE OperacionId = @OperacionId AND Estado IN ('PENDIENTE', 'PARCIAL', 'VENCIDA'))
        BEGIN
            UPDATE OperacionesCredito SET Estado = 'CANCELADO' WHERE OperacionId = @OperacionId;
        END

        -- Auditoría
        INSERT INTO RegistrosAuditoria (TipoEntidad, EntidadId, Accion, ValorNuevo, UsuarioAuditoria, FechaAuditoria)
        VALUES ('Pago', CAST(@PagoId AS INT), 'CREAR',
                'Pago procesado por monto ' + CAST(@MontoPago AS VARCHAR),
                @Usuario, GETDATE());

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
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarPagosOperacion]
    @OperacionId INT,
    @FechaDesde DATE = NULL,
    @FechaHasta DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.PagoId,
        p.FechaPago,
        p.FechaAplicacion,
        p.MontoPago,
        p.TipoPago,
        p.CanalPago,
        p.Referencia,
        p.Estado,
        p.UsuarioCreacion,
        a.CuotaId,
        a.MontoCapital         AS MontoAplicadoCapital,
        a.MontoInteres         AS MontoAplicadoInteres,
        a.MontoSeguro          AS MontoAplicadoSeguro,
        a.MontoTotal           AS MontoAplicadoTotal
    FROM Pagos p
    LEFT JOIN AplicacionesPagos a ON p.PagoId = a.PagoId
    WHERE p.OperacionId = @OperacionId
      AND (@FechaDesde IS NULL OR CAST(p.FechaPago AS DATE) >= @FechaDesde)
      AND (@FechaHasta IS NULL OR CAST(p.FechaPago AS DATE) <= @FechaHasta)
    ORDER BY p.FechaPago DESC, a.CuotaId ASC;
END;
GO

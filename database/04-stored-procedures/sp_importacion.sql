USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_CrearLoteImportacion
-- Descripción: Crea un nuevo lote de importación.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_CrearLoteImportacion]
    @NombreArchivo VARCHAR(200),
    @TipoImportacion VARCHAR(30),
    @TotalRegistros INT,
    @Usuario VARCHAR(50),
    @LoteId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO LotesImportacion (NombreArchivo, TipoImportacion, TotalRegistros, Estado, FechaRecepcion, UsuarioCreacion)
        VALUES (@NombreArchivo, @TipoImportacion, @TotalRegistros, 'RECIBIDO', GETDATE(), @Usuario);

        SET @LoteId = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ==========================================================
-- Procedimiento: sp_InsertarDetalleImportacion
-- Descripción: Inserta un registro detalle para un lote.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_InsertarDetalleImportacion]
    @LoteId INT,
    @NumeroLinea INT,
    @ContenidoLinea VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO DetallesImportacion (LoteId, NumeroLinea, ContenidoLinea, Estado)
        VALUES (@LoteId, @NumeroLinea, @ContenidoLinea, 'PENDIENTE');
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ==========================================================
-- Procedimiento: sp_ProcesarLoteImportacion
-- Descripción: Marca un lote como procesado.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ProcesarLoteImportacion]
    @LoteId INT,
    @Usuario VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE LotesImportacion
        SET Estado = 'COMPLETADO',
            FechaProcesamiento = GETDATE()
        WHERE LoteId = @LoteId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarEstadoLote
-- Descripción: Consulta el estado de un lote.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarEstadoLote]
    @LoteId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        LoteId,
        NombreArchivo,
        TipoImportacion,
        TotalRegistros,
        RegistrosProcesados,
        RegistrosExitosos,
        RegistrosConError,
        Estado,
        FechaRecepcion,
        UsuarioCreacion,
        FechaProcesamiento,
        Observaciones
    FROM LotesImportacion
    WHERE LoteId = @LoteId;
END;
GO

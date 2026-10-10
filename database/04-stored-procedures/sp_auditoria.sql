USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_RegistrarAuditoria
-- Descripción: Registra un evento de auditoría.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_RegistrarAuditoria]
    @TipoEntidad VARCHAR(50),
    @EntidadId INT,
    @Accion VARCHAR(20),
    @ValorAnterior VARCHAR(MAX) = NULL,
    @ValorNuevo VARCHAR(MAX) = NULL,
    @DireccionIP VARCHAR(50) = NULL,
    @NombreEquipo VARCHAR(100) = NULL,
    @Usuario VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        INSERT INTO RegistrosAuditoria (
            TipoEntidad, 
            EntidadId, 
            Accion, 
            ValorAnterior, 
            ValorNuevo, 
            DireccionIP, 
            NombreEquipo, 
            Usuario, 
            FechaRegistro
        )
        VALUES (
            @TipoEntidad, 
            @EntidadId, 
            @Accion, 
            @ValorAnterior, 
            @ValorNuevo, 
            @DireccionIP, 
            @NombreEquipo, 
            @Usuario, 
            GETDATE()
        );
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarAuditoriaEntidad
-- Descripción: Consulta los registros de auditoría de una entidad.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarAuditoriaEntidad]
    @TipoEntidad VARCHAR(50),
    @EntidadId INT,
    @FechaDesde DATE = NULL,
    @FechaHasta DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        AuditoriaId,
        TipoEntidad,
        EntidadId,
        Accion,
        ValorAnterior,
        ValorNuevo,
        DireccionIP,
        NombreEquipo,
        Usuario,
        FechaRegistro
    FROM RegistrosAuditoria
    WHERE TipoEntidad = @TipoEntidad
      AND EntidadId = @EntidadId
      AND (@FechaDesde IS NULL OR CAST(FechaRegistro AS DATE) >= @FechaDesde)
      AND (@FechaHasta IS NULL OR CAST(FechaRegistro AS DATE) <= @FechaHasta)
    ORDER BY FechaRegistro DESC;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarAuditoriaUsuario
-- Descripción: Consulta los registros de auditoría de un usuario.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarAuditoriaUsuario]
    @Usuario VARCHAR(50),
    @FechaDesde DATE,
    @FechaHasta DATE
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        AuditoriaId,
        TipoEntidad,
        EntidadId,
        Accion,
        ValorAnterior,
        ValorNuevo,
        DireccionIP,
        NombreEquipo,
        Usuario,
        FechaRegistro
    FROM RegistrosAuditoria
    WHERE Usuario = @Usuario
      AND CAST(FechaRegistro AS DATE) BETWEEN @FechaDesde AND @FechaHasta
    ORDER BY FechaRegistro DESC;
END;
GO

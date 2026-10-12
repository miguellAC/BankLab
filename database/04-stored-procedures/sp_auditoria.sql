USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_RegistrarAuditoria
-- DescripciÃ³n: Registra un evento de auditorÃ­a.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_RegistrarAuditoria]
    @TipoEntidad VARCHAR(50),
    @EntidadId INT,
    @Accion VARCHAR(20),
    @ValorAnterior VARCHAR(MAX) = NULL,
    @ValorNuevo VARCHAR(MAX) = NULL,
    @DireccionIP VARCHAR(50) = NULL,
    @NombreEquipo VARCHAR(100) = NULL,
    @Usuario VARCHAR(50) = NULL,
    @Equipo VARCHAR(100) = NULL -- Soporte para alias de NombreEquipo
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Usr VARCHAR(50) = COALESCE(@Usuario, SYSTEM_USER);
    DECLARE @Eq VARCHAR(100) = COALESCE(@NombreEquipo, @Equipo, HOST_NAME());

    BEGIN TRY
        INSERT INTO RegistrosAuditoria (
            TipoEntidad, 
            EntidadId, 
            Accion, 
            ValorAnterior, 
            ValorNuevo, 
            DireccionIP, 
            NombreEquipo, 
            UsuarioAuditoria, 
            FechaAuditoria
        )
        VALUES (
            @TipoEntidad, 
            @EntidadId, 
            @Accion, 
            @ValorAnterior, 
            @ValorNuevo, 
            @DireccionIP, 
            @Eq, 
            @Usr, 
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
-- DescripciÃ³n: Consulta los registros de auditorÃ­a de una entidad.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarAuditoriaEntidad]
    @TipoEntidad VARCHAR(50),
    @EntidadId INT,
    @FechaDesde DATE = NULL,
    @FechaHasta DATE = NULL,
    @Desde DATE = NULL, -- Alias
    @Hasta DATE = NULL  -- Alias
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @FDesde DATE = COALESCE(@FechaDesde, @Desde);
    DECLARE @FHasta DATE = COALESCE(@FechaHasta, @Hasta);
    
    SELECT 
        AuditoriaId,
        TipoEntidad,
        EntidadId,
        Accion,
        ValorAnterior,
        ValorNuevo,
        DireccionIP,
        NombreEquipo,
        UsuarioAuditoria,
        FechaAuditoria
    FROM RegistrosAuditoria
    WHERE TipoEntidad = @TipoEntidad
      AND EntidadId = @EntidadId
      AND (@FDesde IS NULL OR CAST(FechaAuditoria AS DATE) >= @FDesde)
      AND (@FHasta IS NULL OR CAST(FechaAuditoria AS DATE) <= @FHasta)
    ORDER BY FechaAuditoria DESC;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarAuditoriaUsuario
-- DescripciÃ³n: Consulta los registros de auditorÃ­a de un usuario.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarAuditoriaUsuario]
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
        UsuarioAuditoria,
        FechaAuditoria
    FROM RegistrosAuditoria
    WHERE UsuarioAuditoria = @Usuario
      AND CAST(FechaAuditoria AS DATE) BETWEEN @FechaDesde AND @FechaHasta
    ORDER BY FechaAuditoria DESC;
END;
GO


-- 07-auditoria.sql
-- Generates audit records and report execution history

SET NOCOUNT ON;
GO

USE BankLabDB;
GO

PRINT 'Poblando tablas de auditoría...';

-- RegistrosAuditoria
-- Columns: AuditoriaId(BIGINT IDENTITY), TipoEntidad, EntidadId(INT), Accion, ValorAnterior, ValorNuevo,
--          DireccionIP, NombreEquipo, UsuarioAuditoria, FechaAuditoria
-- Note: No IDENTITY_INSERT needed — we let the identity column auto-generate.
WITH Nums AS (
    SELECT TOP (5000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO RegistrosAuditoria (TipoEntidad, EntidadId, Accion, ValorAnterior, ValorNuevo, DireccionIP, NombreEquipo, UsuarioAuditoria, FechaAuditoria)
SELECT
    CHOOSE((N % 4) + 1, 'Clientes', 'Cuentas', 'OperacionesCredito', 'Pagos') AS TipoEntidad,
    (N % 1000) + 1                                                              AS EntidadId,
    CHOOSE((N % 3) + 1, 'CREAR', 'MODIFICAR', 'CONSULTAR')                     AS Accion,
    CASE
        WHEN (N % 3) + 1 = 2 THEN '{"estado": "INACTIVO"}'
        ELSE NULL
    END AS ValorAnterior,
    CASE
        WHEN (N % 3) + 1 = 1 THEN '{"id": ' + CAST(N AS VARCHAR) + '}'
        WHEN (N % 3) + 1 = 2 THEN '{"estado": "ACTIVO"}'
        ELSE NULL
    END AS ValorNuevo,
    '192.168.1.' + CAST((N % 200) + 1 AS VARCHAR)  AS DireccionIP,
    'WS-' + CAST((N % 20) + 1 AS VARCHAR)           AS NombreEquipo,
    'User' + CAST((N % 10) + 1 AS VARCHAR)           AS UsuarioAuditoria,
    DATEADD(HOUR, -(N % 17520), GETDATE())           AS FechaAuditoria
FROM Nums;

-- HistorialEjecuciones
-- Columns: EjecucionId(INT IDENTITY), ReporteId(INT FK CatalogosReporte), EmpleadoId(INT FK Empleados NOT NULL),
--          Parametros, FechaEjecucion, DuracionSegundos, Registros, Estado, MensajeError
-- We use only EmpleadoIds that exist (1-10 from seed data).
WITH Nums AS (
    SELECT TOP (20) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a
)
INSERT INTO HistorialEjecuciones (ReporteId, EmpleadoId, Parametros, FechaEjecucion, DuracionSegundos, Registros, Estado)
SELECT
    (N % 5) + 1                                     AS ReporteId,
    (N % 10) + 1                                    AS EmpleadoId,
    '{"FechaCorte": "2024-01-01"}'                  AS Parametros,
    DATEADD(DAY, -N, GETDATE())                     AS FechaEjecucion,
    150 + (N * 50)                                  AS DuracionSegundos,
    100 + (N * 1000)                                AS Registros,
    'EXITOSA'                                        AS Estado
FROM Nums;

PRINT 'Auditoría poblada exitosamente.';
GO

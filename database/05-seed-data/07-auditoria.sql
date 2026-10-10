-- 07-auditoria.sql
-- Generates audit records and report execution history

SET NOCOUNT ON;
GO

PRINT 'Poblando tablas de auditoría...';

-- RegistrosAuditoria
SET IDENTITY_INSERT RegistrosAuditoria ON;
WITH Nums AS (
    SELECT TOP (5000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO RegistrosAuditoria (Id, Entidad, EntidadId, Accion, Usuario, IpOrigen, FechaHora, ValorAnterior, ValorNuevo)
SELECT 
    N AS Id,
    CHOOSE((N % 4) + 1, 'Clientes', 'Cuentas', 'OperacionesCredito', 'Pagos') AS Entidad,
    CAST((N % 1000) + 1 AS VARCHAR) AS EntidadId,
    CHOOSE((N % 3) + 1, 'CREAR', 'MODIFICAR', 'CONSULTAR') AS Accion,
    'User' + CAST((N % 10) + 1 AS VARCHAR) AS Usuario,
    '192.168.1.' + CAST((N % 200) + 1 AS VARCHAR) AS IpOrigen,
    DATEADD(HOUR, -(N % 17520), GETDATE()) AS FechaHora,
    CASE 
        WHEN (N % 3) + 1 = 2 THEN '{"estado": "INACTIVO"}' -- Solo para MODIFICAR
        ELSE NULL 
    END AS ValorAnterior,
    CASE 
        WHEN (N % 3) + 1 = 1 THEN '{"id": ' + CAST(N AS VARCHAR) + '}'
        WHEN (N % 3) + 1 = 2 THEN '{"estado": "ACTIVO"}'
        ELSE NULL 
    END AS ValorNuevo
FROM Nums;
SET IDENTITY_INSERT RegistrosAuditoria OFF;

-- HistorialEjecuciones (Reportes)
SET IDENTITY_INSERT HistorialEjecuciones ON;
WITH Nums AS (
    SELECT TOP (20) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a 
)
INSERT INTO HistorialEjecuciones (Id, ReporteId, FechaEjecucion, UsuarioEjecuta, ParametrosUtilizados, TiempoEjecucionMs, FilasRetornadas)
SELECT 
    N AS Id,
    (N % 5) + 1 AS ReporteId,
    DATEADD(DAY, -N, GETDATE()) AS FechaEjecucion,
    'admin' AS UsuarioEjecuta,
    '{"FechaCorte": "2024-01-01"}' AS ParametrosUtilizados,
    150 + (N * 50) AS TiempoEjecucionMs,
    100 + (N * 1000) AS FilasRetornadas
FROM Nums;
SET IDENTITY_INSERT HistorialEjecuciones OFF;

PRINT 'Auditoría poblada exitosamente.';
GO

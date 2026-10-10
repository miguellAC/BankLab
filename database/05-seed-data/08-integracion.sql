-- 08-integracion.sql
-- Generates integration data: LotesImportacion, DetallesImportacion, MensajesIntegracion

SET NOCOUNT ON;
GO

PRINT 'Poblando tablas de integración...';

-- LotesImportacion
SET IDENTITY_INSERT LotesImportacion ON;
WITH Nums AS (
    SELECT TOP (10) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a 
)
INSERT INTO LotesImportacion (Id, TipoArchivo, NombreArchivo, FechaCarga, Estado, RegistrosTotales, RegistrosProcesados, RegistrosErrores, UsuarioCarga)
SELECT 
    N AS Id,
    CHOOSE((N % 2) + 1, 'NOMINA', 'RECAUDO') AS TipoArchivo,
    'Archivo_' + CAST(N AS VARCHAR) + '.csv' AS NombreArchivo,
    DATEADD(DAY, -N, GETDATE()) AS FechaCarga,
    CASE 
        WHEN N % 3 = 0 THEN 'PROCESADO'
        WHEN N % 3 = 1 THEN 'CON_ERRORES'
        ELSE 'PENDIENTE'
    END AS Estado,
    50 AS RegistrosTotales,
    CASE 
        WHEN N % 3 = 0 THEN 50
        WHEN N % 3 = 1 THEN 45
        ELSE 0
    END AS RegistrosProcesados,
    CASE 
        WHEN N % 3 = 1 THEN 5
        ELSE 0
    END AS RegistrosErrores,
    'sistema' AS UsuarioCarga
FROM Nums;
SET IDENTITY_INSERT LotesImportacion OFF;

-- DetallesImportacion
SET IDENTITY_INSERT DetallesImportacion ON;
WITH Nums AS (
    SELECT TOP (500) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO DetallesImportacion (Id, LoteId, FilaArchivo, DatosFila, Estado, MensajeError)
SELECT 
    N AS Id,
    ((N - 1) / 50) + 1 AS LoteId,
    ((N - 1) % 50) + 1 AS FilaArchivo,
    '{"cuenta": "2200000001", "monto": 150000}' AS DatosFila,
    CASE 
        WHEN N % 10 = 0 THEN 'ERROR'
        ELSE 'PROCESADO'
    END AS Estado,
    CASE 
        WHEN N % 10 = 0 THEN 'Cuenta no existe o inactiva'
        ELSE NULL
    END AS MensajeError
FROM Nums;
SET IDENTITY_INSERT DetallesImportacion OFF;

-- MensajesIntegracion
SET IDENTITY_INSERT MensajesIntegracion ON;
WITH Nums AS (
    SELECT TOP (50) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a 
)
INSERT INTO MensajesIntegracion (Id, TipoMensaje, SistemaOrigen, SistemaDestino, Payload, Prioridad, Estado, FechaCreacion, Intentos)
SELECT 
    N AS Id,
    CHOOSE((N % 3) + 1, 'NOTIFICACION_SMS', 'REPORTE_CENRIESGO', 'ALERTA_FRAUDE') AS TipoMensaje,
    'CORE' AS SistemaOrigen,
    CHOOSE((N % 3) + 1, 'SMS_GATEWAY', 'BUREAU', 'FRAUD_SYS') AS SistemaDestino,
    '{"clienteId": 100, "data": "test"}' AS Payload,
    CASE WHEN N % 5 = 0 THEN 'ALTA' ELSE 'NORMAL' END AS Prioridad,
    CASE 
        WHEN N % 4 = 0 THEN 'ENVIADO'
        WHEN N % 4 = 1 THEN 'PENDIENTE'
        ELSE 'ERROR'
    END AS Estado,
    DATEADD(HOUR, -N, GETDATE()) AS FechaCreacion,
    CASE WHEN N % 4 = 2 THEN 3 ELSE 1 END AS Intentos
FROM Nums;
SET IDENTITY_INSERT MensajesIntegracion OFF;

PRINT 'Integración poblada exitosamente.';
GO

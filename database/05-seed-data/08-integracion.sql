-- 08-integracion.sql
-- Generates integration data: LotesImportacion, DetallesImportacion, MensajesIntegracion

SET NOCOUNT ON;
GO

USE BankLabDB;
GO

PRINT 'Poblando tablas de integración...';

-- LotesImportacion
-- Columns: LoteId(INT IDENTITY), NombreArchivo, TipoImportacion(CHECK: PAGOS,CLIENTES,OPERACIONES,SALDOS),
--          TotalRegistros, RegistrosProcesados, RegistrosExitosos, RegistrosConError,
--          Estado(CHECK: RECIBIDO,EN_PROCESO,COMPLETADO,COMPLETADO_CON_ERRORES,ERROR),
--          FechaRecepcion, FechaProcesamiento, UsuarioCreacion, Observaciones
WITH Nums AS (
    SELECT TOP (10) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a
)
INSERT INTO LotesImportacion (NombreArchivo, TipoImportacion, TotalRegistros, RegistrosProcesados, RegistrosExitosos, RegistrosConError, Estado, FechaRecepcion, FechaProcesamiento, UsuarioCreacion)
SELECT
    'Archivo_' + CAST(N AS VARCHAR) + '.csv'            AS NombreArchivo,
    CHOOSE((N % 4) + 1, 'PAGOS', 'CLIENTES', 'OPERACIONES', 'SALDOS') AS TipoImportacion,
    50                                                    AS TotalRegistros,
    CASE WHEN N % 3 = 0 THEN 50 WHEN N % 3 = 1 THEN 45 ELSE 0 END AS RegistrosProcesados,
    CASE WHEN N % 3 = 0 THEN 50 WHEN N % 3 = 1 THEN 45 ELSE 0 END AS RegistrosExitosos,
    CASE WHEN N % 3 = 1 THEN 5  ELSE 0 END               AS RegistrosConError,
    CASE
        WHEN N % 3 = 0 THEN 'COMPLETADO'
        WHEN N % 3 = 1 THEN 'COMPLETADO_CON_ERRORES'
        ELSE 'RECIBIDO'
    END                                                   AS Estado,
    DATEADD(DAY, -N, GETDATE())                           AS FechaRecepcion,
    CASE WHEN N % 3 <> 2 THEN DATEADD(HOUR, -N, GETDATE()) ELSE NULL END AS FechaProcesamiento,
    'sistema'                                             AS UsuarioCreacion
FROM Nums;

-- DetallesImportacion
-- Columns: DetalleImportId(BIGINT IDENTITY), LoteId(FK), NumeroLinea, ContenidoLinea,
--          Estado(CHECK: PENDIENTE,PROCESADO,ERROR,DUPLICADO), MensajeError, ReferenciaEntidad, FechaProcesamiento
WITH Nums AS (
    SELECT TOP (100) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO DetallesImportacion (LoteId, NumeroLinea, ContenidoLinea, Estado, MensajeError, FechaProcesamiento)
SELECT
    ((N - 1) / 10) + 1                                    AS LoteId,
    ((N - 1) % 10) + 1                                    AS NumeroLinea,
    '{"cuenta": "2200000001", "monto": 150000}'            AS ContenidoLinea,
    CASE WHEN N % 10 = 0 THEN 'ERROR' ELSE 'PROCESADO' END AS Estado,
    CASE WHEN N % 10 = 0 THEN 'Cuenta no existe o inactiva' ELSE NULL END AS MensajeError,
    CASE WHEN N % 10 <> 0 THEN GETDATE() ELSE NULL END     AS FechaProcesamiento
FROM Nums;

-- MensajesIntegracion
-- Columns: MensajeId(BIGINT IDENTITY), CodigoMensaje(VARCHAR UNIQUE), SistemaOrigen, SistemaDestino,
--          TipoMensaje(CHECK: PAGO,CONSULTA,NOTIFICACION,ACTUALIZACION), Contenido,
--          Estado(CHECK: RECIBIDO,EN_PROCESO,PROCESADO,ERROR,RECHAZADO),
--          Prioridad(INT 1-10), IntentosProc, MaxIntentos,
--          FechaRecepcion, FechaProcesamiento, MensajeError, UsuarioProcesamiento
WITH Nums AS (
    SELECT TOP (50) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a
)
INSERT INTO MensajesIntegracion (CodigoMensaje, SistemaOrigen, SistemaDestino, TipoMensaje, Contenido, Estado, Prioridad, IntentosProc, MaxIntentos, FechaRecepcion, FechaProcesamiento, UsuarioProcesamiento)
SELECT
    'MSG-' + RIGHT('0000' + CAST(N AS VARCHAR), 4)          AS CodigoMensaje,
    'CORE'                                                    AS SistemaOrigen,
    CHOOSE((N % 3) + 1, 'SMS_GATEWAY', 'BUREAU', 'FRAUD_SYS') AS SistemaDestino,
    CHOOSE((N % 4) + 1, 'PAGO', 'CONSULTA', 'NOTIFICACION', 'ACTUALIZACION') AS TipoMensaje,
    '{"clienteId": 100, "data": "test"}'                     AS Contenido,
    CASE
        WHEN N % 4 = 0 THEN 'PROCESADO'
        WHEN N % 4 = 1 THEN 'RECIBIDO'
        WHEN N % 4 = 2 THEN 'ERROR'
        ELSE 'EN_PROCESO'
    END                                                       AS Estado,
    CASE WHEN N % 5 = 0 THEN 1 ELSE 5 END                    AS Prioridad,
    CASE WHEN N % 4 = 2 THEN 3 ELSE 1 END                    AS IntentosProc,
    3                                                         AS MaxIntentos,
    DATEADD(HOUR, -N, GETDATE())                              AS FechaRecepcion,
    CASE WHEN N % 4 IN (0, 2) THEN DATEADD(HOUR, -N + 1, GETDATE()) ELSE NULL END AS FechaProcesamiento,
    CASE WHEN N % 4 IN (0, 2, 3) THEN 'sistema' ELSE NULL END AS UsuarioProcesamiento
FROM Nums;

PRINT 'Integración poblada exitosamente.';
GO

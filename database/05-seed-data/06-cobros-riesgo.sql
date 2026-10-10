-- 06-cobros-riesgo.sql
-- Generates collections and risk data: Cobros, SaldosVencidos, ClasificacionesRiesgo, HistorialRiesgo, etc.

SET NOCOUNT ON;
GO

PRINT 'Poblando tablas de cobros y riesgo...';

-- Cobros (Collection actions)
SET IDENTITY_INSERT Cobros ON;
WITH OperacionesMora AS (
    SELECT Id AS OperacionId, ClienteId
    FROM OperacionesCredito 
    WHERE Estado IN ('MORA', 'CASTIGADO')
),
Nums AS (
    SELECT TOP (2000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO Cobros (Id, OperacionId, FechaGestion, TipoGestion, Resultado, CompromisoPago, FechaCompromiso, Observaciones, UsuarioGestion)
SELECT 
    N.N AS Id,
    OM.OperacionId,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 365), GETDATE()) AS FechaGestion,
    CHOOSE((N.N % 4) + 1, 'LLAMADA', 'SMS', 'EMAIL', 'VISITA') AS TipoGestion,
    CHOOSE((N.N % 3) + 1, 'CONTACTO_DIRECTO', 'MENSAJE_TERCERO', 'NO_CONTACTO') AS Resultado,
    CASE WHEN N.N % 3 = 0 THEN 1 ELSE 0 END AS CompromisoPago,
    CASE WHEN N.N % 3 = 0 THEN DATEADD(DAY, 5 + (N.N % 10), GETDATE()) ELSE NULL END AS FechaCompromiso,
    'Gestión de cobro generada' AS Observaciones,
    'Gestor' + CAST((N.N % 5) + 1 AS VARCHAR) AS UsuarioGestion
FROM Nums N
JOIN OperacionesMora OM ON N.N % (SELECT COUNT(*) FROM OperacionesMora) = (OM.OperacionId % (SELECT COUNT(*) FROM OperacionesMora));
SET IDENTITY_INSERT Cobros OFF;

-- SaldosVencidos
SET IDENTITY_INSERT SaldosVencidos ON;
WITH Nums AS (
    SELECT TOP (1500) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO SaldosVencidos (Id, OperacionId, FechaCorte, CapitalVencido, InteresVencido, InteresMora, DiasMora)
SELECT 
    N AS Id,
    (SELECT TOP 1 Id FROM OperacionesCredito WHERE Estado = 'MORA' ORDER BY NEWID()) AS OperacionId,
    DATEADD(MONTH, -(N % 12), GETDATE()) AS FechaCorte,
    50000 + (N * 1000) AS CapitalVencido,
    10000 + (N * 200) AS InteresVencido,
    5000 + (N * 100) AS InteresMora,
    30 + (N % 90) AS DiasMora
FROM Nums;
SET IDENTITY_INSERT SaldosVencidos OFF;

-- ClasificacionesRiesgo
SET IDENTITY_INSERT ClasificacionesRiesgo ON;
WITH ClientesCredito AS (
    SELECT DISTINCT ClienteId FROM OperacionesCredito
)
INSERT INTO ClasificacionesRiesgo (Id, ClienteId, Calificacion, FechaCalificacion, ProbabilidadIncumplimiento, PuntajeScoring)
SELECT 
    ROW_NUMBER() OVER(ORDER BY ClienteId) AS Id,
    ClienteId,
    CASE 
        WHEN ClienteId % 100 < 50 THEN 'A'
        WHEN ClienteId % 100 < 70 THEN 'B'
        WHEN ClienteId % 100 < 85 THEN 'C'
        WHEN ClienteId % 100 < 95 THEN 'D'
        ELSE 'E'
    END AS Calificacion,
    GETDATE() AS FechaCalificacion,
    (ClienteId % 20) * 0.05 AS ProbabilidadIncumplimiento,
    850 - (ClienteId % 400) AS PuntajeScoring
FROM ClientesCredito;
SET IDENTITY_INSERT ClasificacionesRiesgo OFF;

-- HistorialRiesgo
INSERT INTO HistorialRiesgo (ClienteId, CalificacionAnterior, CalificacionNueva, FechaCambio, Motivo)
SELECT TOP (1000)
    ClienteId,
    'A' AS CalificacionAnterior,
    Calificacion AS CalificacionNueva,
    DATEADD(DAY, - (ClienteId % 300), GETDATE()) AS FechaCambio,
    'Actualización mensual' AS Motivo
FROM ClasificacionesRiesgo
WHERE Calificacion != 'A';

-- SolicitudesDiferimiento
SET IDENTITY_INSERT SolicitudesDiferimiento ON;
INSERT INTO SolicitudesDiferimiento (Id, TipoDiferimientoId, FechaSolicitud, Estado, Justificacion) VALUES
(1, 1, GETDATE(), 'APROBADO', 'Campaña emergencia 2024'),
(2, 2, GETDATE(), 'EN_REVISION', 'Calamidad por ola invernal'),
(3, 3, GETDATE(), 'APROBADO', 'Programa alivio especial');
SET IDENTITY_INSERT SolicitudesDiferimiento OFF;

-- ClientesIncluidosDiferimiento
INSERT INTO ClientesIncluidosDiferimiento (SolicitudDiferimientoId, ClienteId, FechaInclusion)
SELECT TOP 200 
    1 AS SolicitudDiferimientoId,
    ClienteId,
    GETDATE() AS FechaInclusion
FROM ClasificacionesRiesgo;

-- ClientesExcluidosDiferimiento
INSERT INTO ClientesExcluidosDiferimiento (SolicitudDiferimientoId, ClienteId, MotivoExclusion)
SELECT TOP 50 
    1 AS SolicitudDiferimientoId,
    ClienteId,
    'Mora mayor a 90 días previa a la campaña' AS MotivoExclusion
FROM ClasificacionesRiesgo 
WHERE Calificacion IN ('D', 'E');

-- OperacionesDiferidas
INSERT INTO OperacionesDiferidas (SolicitudDiferimientoId, OperacionId, MesesDiferidos, SaldoCongelado, NuevoPlazo)
SELECT TOP 150 
    1 AS SolicitudDiferimientoId,
    O.Id AS OperacionId,
    6 AS MesesDiferidos,
    O.SaldoCapital AS SaldoCongelado,
    C.PlazoMeses + 6 AS NuevoPlazo
FROM OperacionesCredito O
JOIN CondicionesCredito C ON O.Id = C.OperacionId
WHERE O.Estado = 'VIGENTE';

PRINT 'Riesgo y cobros poblados exitosamente.';
GO

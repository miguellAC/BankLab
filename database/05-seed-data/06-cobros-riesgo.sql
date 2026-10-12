-- 06-cobros-riesgo.sql
-- Generates collections and risk data: Cobros, SaldosVencidos, ClasificacionesRiesgo, HistorialRiesgo, etc.

SET NOCOUNT ON;
GO

USE BankLabDB;
GO

PRINT 'Poblando tablas de cobros y riesgo...';

-- Cobros (Collection actions)
-- Schema: CobroId, OperacionId, EmpleadoId, TipoGestion, Resultado, Observaciones, FechaGestion, FechaCompromisoPago, MontoCompromiso, UsuarioCreacion
-- Checks: TipoGestion IN ('LLAMADA','VISITA','CARTA','EMAIL','SMS')
--         Resultado IN ('CONTACTADO','NO_CONTACTADO','PROMESA_PAGO','RECHAZO','ILOCALIZABLE')
SET IDENTITY_INSERT Cobros ON;
WITH OperacionesMora AS (
    SELECT OperacionId, ClienteId
    FROM OperacionesCredito 
    WHERE Estado IN ('MORA', 'CASTIGADO')
),
Nums AS (
    SELECT TOP (2000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO Cobros (CobroId, OperacionId, EmpleadoId, TipoGestion, Resultado, Observaciones, FechaGestion, FechaCompromisoPago, MontoCompromiso)
SELECT 
    N.N AS CobroId,
    OM.OperacionId,
    (N.N % 29) + 1 AS EmpleadoId,
    CHOOSE((N.N % 5) + 1, 'LLAMADA', 'SMS', 'EMAIL', 'VISITA', 'CARTA') AS TipoGestion,
    CHOOSE((N.N % 5) + 1, 'CONTACTADO', 'NO_CONTACTADO', 'PROMESA_PAGO', 'RECHAZO', 'ILOCALIZABLE') AS Resultado,
    'Gestión de cobro generada' AS Observaciones,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 365), GETDATE()) AS FechaGestion,
    CASE WHEN N.N % 5 = 2 THEN DATEADD(DAY, 5 + (N.N % 10), GETDATE()) ELSE NULL END AS FechaCompromisoPago,
    CASE WHEN N.N % 5 = 2 THEN 500000.00 ELSE NULL END AS MontoCompromiso
FROM Nums N
CROSS JOIN (SELECT TOP 1 OperacionId FROM OperacionesMora) OM;
SET IDENTITY_INSERT Cobros OFF;

-- SaldosVencidos
-- Schema: SaldoVencidoId, OperacionId, FechaCorte, CapitalVencido, InteresVencido, DiasVencido, Rango, FechaRegistro
-- Check: Rango IN ('1-30','31-60','61-90','91-180','181-360','MAS_360')
SET IDENTITY_INSERT SaldosVencidos ON;
WITH Nums AS (
    SELECT TOP (1500) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
),
OpsMora AS (
    SELECT OperacionId FROM OperacionesCredito WHERE Estado = 'MORA'
)
INSERT INTO SaldosVencidos (SaldoVencidoId, OperacionId, FechaCorte, CapitalVencido, InteresVencido, DiasVencido, Rango)
SELECT 
    N AS SaldoVencidoId,
    ISNULL((SELECT TOP 1 OperacionId FROM OpsMora ORDER BY NEWID()), 1) AS OperacionId,
    CAST(DATEADD(MONTH, -(N % 12), GETDATE()) AS DATE) AS FechaCorte,
    50000 + (N * 1000) AS CapitalVencido,
    10000 + (N * 200) AS InteresVencido,
    30 + (N % 90) AS DiasVencido,
    CASE 
        WHEN (30 + (N % 90)) <= 30 THEN '1-30'
        WHEN (30 + (N % 90)) <= 60 THEN '31-60'
        WHEN (30 + (N % 90)) <= 90 THEN '61-90'
        ELSE '91-180'
    END AS Rango
FROM Nums;
SET IDENTITY_INSERT SaldosVencidos OFF;

-- ClasificacionesRiesgo
-- Schema: ClasificacionId, ClienteId, Categoria, Puntaje, FechaClasificacion, Vigente, Observaciones
-- Check: Categoria IN ('A','B','C','D','E')
SET IDENTITY_INSERT ClasificacionesRiesgo ON;
WITH ClientesCredito AS (
    SELECT DISTINCT ClienteId FROM OperacionesCredito
)
INSERT INTO ClasificacionesRiesgo (ClasificacionId, ClienteId, Categoria, Puntaje, FechaClasificacion, Vigente)
SELECT 
    ROW_NUMBER() OVER(ORDER BY ClienteId) AS ClasificacionId,
    ClienteId,
    CASE 
        WHEN ClienteId % 100 < 50 THEN 'A'
        WHEN ClienteId % 100 < 70 THEN 'B'
        WHEN ClienteId % 100 < 85 THEN 'C'
        WHEN ClienteId % 100 < 95 THEN 'D'
        ELSE 'E'
    END AS Categoria,
    850 - (ClienteId % 400) AS Puntaje,
    CAST(GETDATE() AS DATE) AS FechaClasificacion,
    1 AS Vigente
FROM ClientesCredito;
SET IDENTITY_INSERT ClasificacionesRiesgo OFF;

-- HistorialRiesgo
-- Schema: HistorialRiesgoId, ClienteId, CategoriaAnterior, CategoriaNueva, PuntajeAnterior, PuntajeNuevo, MotivoTransicion, FechaCambio
INSERT INTO HistorialRiesgo (ClienteId, CategoriaAnterior, CategoriaNueva, PuntajeAnterior, PuntajeNuevo, MotivoTransicion, FechaCambio)
SELECT TOP (1000)
    ClienteId,
    'A' AS CategoriaAnterior,
    Categoria AS CategoriaNueva,
    800 AS PuntajeAnterior,
    Puntaje AS PuntajeNuevo,
    'Actualización mensual' AS MotivoTransicion,
    DATEADD(DAY, - (ClienteId % 300), GETDATE()) AS FechaCambio
FROM ClasificacionesRiesgo
WHERE Categoria != 'A';

-- SolicitudesDiferimiento
-- Schema: SolicitudDifId, TipoDiferimientoId, Nombre, Descripcion, FechaInicio, FechaFin, Estado, CuotasDiferidas
-- Check: Estado IN ('ACTIVA','CERRADA','CANCELADA')
SET IDENTITY_INSERT SolicitudesDiferimiento ON;
INSERT INTO SolicitudesDiferimiento (SolicitudDifId, TipoDiferimientoId, Nombre, Descripcion, FechaInicio, FechaFin, Estado, CuotasDiferidas) VALUES
(1, 1, 'Campaña Emergencia 2024', 'Campaña emergencia 2024', CAST(GETDATE() AS DATE), CAST(DATEADD(MONTH, 6, GETDATE()) AS DATE), 'ACTIVA', 6),
(2, 2, 'Calamidad Ola Invernal', 'Calamidad por ola invernal', CAST(GETDATE() AS DATE), CAST(DATEADD(MONTH, 3, GETDATE()) AS DATE), 'ACTIVA', 3),
(3, 3, 'Programa Alivio Especial', 'Programa alivio especial', CAST(GETDATE() AS DATE), CAST(DATEADD(MONTH, 12, GETDATE()) AS DATE), 'ACTIVA', 12);
SET IDENTITY_INSERT SolicitudesDiferimiento OFF;

-- ClientesIncluidosDiferimiento
-- Schema: IncluidoId, ClienteId, SolicitudDifId, FechaInclusion, Motivo
INSERT INTO ClientesIncluidosDiferimiento (ClienteId, SolicitudDifId, Motivo)
SELECT TOP 200 
    ClienteId,
    1 AS SolicitudDifId,
    'Inclusión automática campaña' AS Motivo
FROM ClasificacionesRiesgo;

-- ClientesExcluidosDiferimiento
-- Schema: ExcluidoId, ClienteId, SolicitudDifId, FechaExclusion, MotivoExclusion
INSERT INTO ClientesExcluidosDiferimiento (ClienteId, SolicitudDifId, MotivoExclusion)
SELECT TOP 50 
    ClienteId,
    1 AS SolicitudDifId,
    'Mora mayor a 90 días previa a la campaña' AS MotivoExclusion
FROM ClasificacionesRiesgo 
WHERE Categoria IN ('D', 'E')
  AND ClienteId NOT IN (SELECT TOP 200 ClienteId FROM ClasificacionesRiesgo);

-- OperacionesDiferidas
-- Schema: OperacionDifId, OperacionId, SolicitudDifId, CuotasOriginales, CuotasDiferidas, NuevoPlazoMeses, FechaDiferimiento, Estado
-- Check: Estado IN ('VIGENTE','FINALIZADO','REVERTIDO')
INSERT INTO OperacionesDiferidas (OperacionId, SolicitudDifId, CuotasOriginales, CuotasDiferidas, NuevoPlazoMeses, FechaDiferimiento, Estado)
SELECT TOP 150 
    O.OperacionId,
    1 AS SolicitudDifId,
    O.NumeroCuotas AS CuotasOriginales,
    6 AS CuotasDiferidas,
    O.PlazoMeses + 6 AS NuevoPlazoMeses,
    CAST(GETDATE() AS DATE) AS FechaDiferimiento,
    'VIGENTE' AS Estado
FROM OperacionesCredito O
WHERE O.Estado = 'VIGENTE';

PRINT 'Riesgo y cobros poblados exitosamente.';
GO

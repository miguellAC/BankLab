-- =========================================
-- File: 02-schema/06-tables-integracion.sql
-- Description: Creates tables for system integration
-- =========================================

USE BankLabDB;
GO

IF OBJECT_ID('LotesImportacion', 'U') IS NULL
CREATE TABLE LotesImportacion (
    LoteId INT IDENTITY(1,1) PRIMARY KEY,
    NombreArchivo VARCHAR(200) NOT NULL,
    TipoImportacion VARCHAR(30) NOT NULL CHECK (TipoImportacion IN ('PAGOS','CLIENTES','OPERACIONES','SALDOS')),
    TotalRegistros INT NOT NULL DEFAULT 0,
    RegistrosProcesados INT NOT NULL DEFAULT 0,
    RegistrosExitosos INT NOT NULL DEFAULT 0,
    RegistrosConError INT NOT NULL DEFAULT 0,
    Estado VARCHAR(30) NOT NULL CHECK (Estado IN ('RECIBIDO','EN_PROCESO','COMPLETADO','COMPLETADO_CON_ERRORES','ERROR')),
    FechaRecepcion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaProcesamiento DATETIME,
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    Observaciones VARCHAR(500)
);
GO

IF OBJECT_ID('DetallesImportacion', 'U') IS NULL
CREATE TABLE DetallesImportacion (
    DetalleImportId BIGINT IDENTITY(1,1) PRIMARY KEY,
    LoteId INT NOT NULL REFERENCES LotesImportacion(LoteId),
    NumeroLinea INT NOT NULL,
    ContenidoLinea VARCHAR(MAX) NOT NULL,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('PENDIENTE','PROCESADO','ERROR','DUPLICADO')),
    MensajeError VARCHAR(500),
    ReferenciaEntidad VARCHAR(50),
    FechaProcesamiento DATETIME
);
GO

IF OBJECT_ID('MensajesIntegracion', 'U') IS NULL
CREATE TABLE MensajesIntegracion (
    MensajeId BIGINT IDENTITY(1,1) PRIMARY KEY,
    CodigoMensaje VARCHAR(50) NOT NULL UNIQUE,
    SistemaOrigen VARCHAR(50) NOT NULL,
    SistemaDestino VARCHAR(50) NOT NULL,
    TipoMensaje VARCHAR(30) NOT NULL CHECK (TipoMensaje IN ('PAGO','CONSULTA','NOTIFICACION','ACTUALIZACION')),
    Contenido VARCHAR(MAX) NOT NULL,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('RECIBIDO','EN_PROCESO','PROCESADO','ERROR','RECHAZADO')),
    Prioridad INT NOT NULL DEFAULT 5 CHECK (Prioridad BETWEEN 1 AND 10),
    IntentosProc INT NOT NULL DEFAULT 0,
    MaxIntentos INT NOT NULL DEFAULT 3,
    FechaRecepcion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaProcesamiento DATETIME,
    MensajeError VARCHAR(500),
    UsuarioProcesamiento VARCHAR(50)
);
GO

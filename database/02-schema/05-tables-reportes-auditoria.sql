-- =========================================
-- File: 02-schema/05-tables-reportes-auditoria.sql
-- Description: Creates tables for reports and auditing
-- =========================================

USE BankLabDB;
GO

IF OBJECT_ID('CatalogosReporte', 'U') IS NULL
CREATE TABLE CatalogosReporte (
    ReporteId INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    RutaArchivo VARCHAR(500),
    TipoReporte VARCHAR(20) NOT NULL CHECK (TipoReporte IN ('CARTERA','CLIENTES','OPERACIONES','RIESGO','AUDITORIA','GENERAL')),
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('ParametrosReporte', 'U') IS NULL
CREATE TABLE ParametrosReporte (
    ParametroReporteId INT IDENTITY(1,1) PRIMARY KEY,
    ReporteId INT NOT NULL REFERENCES CatalogosReporte(ReporteId),
    NombreParametro VARCHAR(50) NOT NULL,
    TipoDato VARCHAR(20) NOT NULL CHECK (TipoDato IN ('INT','VARCHAR','DATE','DECIMAL','BIT')),
    Obligatorio BIT NOT NULL DEFAULT 1,
    ValorPredeterminado VARCHAR(100),
    Orden INT NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('HistorialEjecuciones', 'U') IS NULL
CREATE TABLE HistorialEjecuciones (
    EjecucionId INT IDENTITY(1,1) PRIMARY KEY,
    ReporteId INT NOT NULL REFERENCES CatalogosReporte(ReporteId),
    EmpleadoId INT NOT NULL REFERENCES Empleados(EmpleadoId),
    Parametros VARCHAR(MAX),
    FechaEjecucion DATETIME NOT NULL DEFAULT GETDATE(),
    DuracionSegundos INT,
    Registros INT,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('EXITOSA','ERROR','CANCELADA')),
    MensajeError VARCHAR(500)
);
GO

IF OBJECT_ID('RegistrosAuditoria', 'U') IS NULL
CREATE TABLE RegistrosAuditoria (
    AuditoriaId BIGINT IDENTITY(1,1) PRIMARY KEY,
    TipoEntidad VARCHAR(50) NOT NULL,
    EntidadId INT NOT NULL,
    Accion VARCHAR(20) NOT NULL CHECK (Accion IN ('CREAR','MODIFICAR','ELIMINAR','CONSULTAR','EXPORTAR','LOGIN','LOGOUT')),
    ValorAnterior VARCHAR(MAX),
    ValorNuevo VARCHAR(MAX),
    DireccionIP VARCHAR(50),
    NombreEquipo VARCHAR(100),
    UsuarioAuditoria VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    FechaAuditoria DATETIME NOT NULL DEFAULT GETDATE()
);
GO

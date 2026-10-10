-- =========================================
-- File: 02-schema/04-tables-cobros-riesgo.sql
-- Description: Creates tables for collections and risk management
-- =========================================

USE BankLabDB;
GO

IF OBJECT_ID('Cobros', 'U') IS NULL
CREATE TABLE Cobros (
    CobroId BIGINT IDENTITY(1,1) PRIMARY KEY,
    OperacionId INT NOT NULL REFERENCES OperacionesCredito(OperacionId),
    EmpleadoId INT NOT NULL REFERENCES Empleados(EmpleadoId),
    TipoGestion VARCHAR(20) NOT NULL CHECK (TipoGestion IN ('LLAMADA','VISITA','CARTA','EMAIL','SMS')),
    Resultado VARCHAR(20) NOT NULL CHECK (Resultado IN ('CONTACTADO','NO_CONTACTADO','PROMESA_PAGO','RECHAZO','ILOCALIZABLE')),
    Observaciones VARCHAR(500),
    FechaGestion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaCompromisoPago DATE NULL,
    MontoCompromiso DECIMAL(18,2) NULL,
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER
);
GO

IF OBJECT_ID('SaldosVencidos', 'U') IS NULL
CREATE TABLE SaldosVencidos (
    SaldoVencidoId BIGINT IDENTITY(1,1) PRIMARY KEY,
    OperacionId INT NOT NULL REFERENCES OperacionesCredito(OperacionId),
    FechaCorte DATE NOT NULL,
    CapitalVencido DECIMAL(18,2) NOT NULL,
    InteresVencido DECIMAL(18,2) NOT NULL,
    DiasVencido INT NOT NULL,
    Rango VARCHAR(20) NOT NULL CHECK (Rango IN ('1-30','31-60','61-90','91-180','181-360','MAS_360')),
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('ClasificacionesRiesgo', 'U') IS NULL
CREATE TABLE ClasificacionesRiesgo (
    ClasificacionId INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL REFERENCES Clientes(ClienteId),
    Categoria CHAR(1) NOT NULL CHECK (Categoria IN ('A','B','C','D','E')),
    Puntaje INT NOT NULL,
    FechaClasificacion DATE NOT NULL,
    Vigente BIT NOT NULL DEFAULT 1,
    Observaciones VARCHAR(500),
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('HistorialRiesgo', 'U') IS NULL
CREATE TABLE HistorialRiesgo (
    HistorialRiesgoId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL REFERENCES Clientes(ClienteId),
    CategoriaAnterior CHAR(1) NOT NULL,
    CategoriaNueva CHAR(1) NOT NULL,
    PuntajeAnterior INT NOT NULL,
    PuntajeNuevo INT NOT NULL,
    MotivoTransicion VARCHAR(200),
    FechaCambio DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioCambio VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER
);
GO

IF OBJECT_ID('TiposDiferimiento', 'U') IS NULL
CREATE TABLE TiposDiferimiento (
    TipoDiferimientoId INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    PlazoMaxMeses INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('SolicitudesDiferimiento', 'U') IS NULL
CREATE TABLE SolicitudesDiferimiento (
    SolicitudDifId INT IDENTITY(1,1) PRIMARY KEY,
    TipoDiferimientoId INT NOT NULL REFERENCES TiposDiferimiento(TipoDiferimientoId),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('ACTIVA','CERRADA','CANCELADA')),
    CuotasDiferidas INT NOT NULL,
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('ClientesIncluidosDiferimiento', 'U') IS NULL
CREATE TABLE ClientesIncluidosDiferimiento (
    IncluidoId INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL REFERENCES Clientes(ClienteId),
    SolicitudDifId INT NOT NULL REFERENCES SolicitudesDiferimiento(SolicitudDifId),
    FechaInclusion DATETIME NOT NULL DEFAULT GETDATE(),
    Motivo VARCHAR(200),
    UsuarioInclusion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    CONSTRAINT UQ_Incluido UNIQUE(ClienteId, SolicitudDifId)
);
GO

IF OBJECT_ID('ClientesExcluidosDiferimiento', 'U') IS NULL
CREATE TABLE ClientesExcluidosDiferimiento (
    ExcluidoId INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL REFERENCES Clientes(ClienteId),
    SolicitudDifId INT NOT NULL REFERENCES SolicitudesDiferimiento(SolicitudDifId),
    FechaExclusion DATETIME NOT NULL DEFAULT GETDATE(),
    MotivoExclusion VARCHAR(200),
    UsuarioExclusion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    CONSTRAINT UQ_Excluido UNIQUE(ClienteId, SolicitudDifId)
);
GO

IF OBJECT_ID('OperacionesDiferidas', 'U') IS NULL
CREATE TABLE OperacionesDiferidas (
    OperacionDifId INT IDENTITY(1,1) PRIMARY KEY,
    OperacionId INT NOT NULL REFERENCES OperacionesCredito(OperacionId),
    SolicitudDifId INT NOT NULL REFERENCES SolicitudesDiferimiento(SolicitudDifId),
    CuotasOriginales INT NOT NULL,
    CuotasDiferidas INT NOT NULL,
    NuevoPlazoMeses INT NOT NULL,
    FechaDiferimiento DATE NOT NULL,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('VIGENTE','FINALIZADO','REVERTIDO')),
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UQ_OpDiferida UNIQUE(OperacionId, SolicitudDifId)
);
GO

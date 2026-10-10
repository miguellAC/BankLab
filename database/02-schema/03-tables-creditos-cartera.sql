-- =========================================
-- File: 02-schema/03-tables-creditos-cartera.sql
-- Description: Creates tables for credits and portfolio
-- =========================================

USE BankLabDB;
GO

IF OBJECT_ID('ProductosCredito', 'U') IS NULL
CREATE TABLE ProductosCredito (
    ProductoCreditoId INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    TasaBaseAnual DECIMAL(8,4) NOT NULL,
    PlazoMinMeses INT NOT NULL,
    PlazoMaxMeses INT NOT NULL,
    MontoMinimo DECIMAL(18,2) NOT NULL,
    MontoMaximo DECIMAL(18,2) NOT NULL,
    RequiereGarantia BIT NOT NULL DEFAULT 0,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('SolicitudesCredito', 'U') IS NULL
CREATE TABLE SolicitudesCredito (
    SolicitudId INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL REFERENCES Clientes(ClienteId),
    ProductoCreditoId INT NOT NULL REFERENCES ProductosCredito(ProductoCreditoId),
    EmpleadoId INT NOT NULL REFERENCES Empleados(EmpleadoId),
    MontoSolicitado DECIMAL(18,2) NOT NULL,
    PlazoMeses INT NOT NULL,
    IngresosMensuales DECIMAL(18,2) NOT NULL,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('RADICADA','EN_ESTUDIO','APROBADA','RECHAZADA','DESISTIDA','VENCIDA')),
    FechaRadicacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaResolucion DATETIME,
    ObservacionesResolucion VARCHAR(500),
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('OperacionesCredito', 'U') IS NULL
CREATE TABLE OperacionesCredito (
    OperacionId INT IDENTITY(1,1) PRIMARY KEY,
    NumeroOperacion VARCHAR(20) NOT NULL UNIQUE,
    SolicitudId INT NULL REFERENCES SolicitudesCredito(SolicitudId),
    ClienteId INT NOT NULL REFERENCES Clientes(ClienteId),
    ProductoCreditoId INT NOT NULL REFERENCES ProductosCredito(ProductoCreditoId),
    MontoDesembolsado DECIMAL(18,2) NOT NULL,
    TasaAnual DECIMAL(8,4) NOT NULL,
    PlazoMeses INT NOT NULL,
    NumeroCuotas INT NOT NULL,
    FechaDesembolso DATE NOT NULL,
    FechaVencimiento DATE NOT NULL,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('VIGENTE','MORA','CANCELADO','CASTIGADO','REESTRUCTURADO','DIFERIDO')),
    SucursalId INT NOT NULL REFERENCES Sucursales(SucursalId),
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME,
    UsuarioModificacion VARCHAR(50)
);
GO

IF OBJECT_ID('CondicionesCredito', 'U') IS NULL
CREATE TABLE CondicionesCredito (
    CondicionId INT IDENTITY(1,1) PRIMARY KEY,
    OperacionId INT NOT NULL UNIQUE REFERENCES OperacionesCredito(OperacionId),
    PeriodoGraciaMeses INT NOT NULL DEFAULT 0,
    TasaMoratoria DECIMAL(8,4) NOT NULL,
    DiaCorte INT NOT NULL CHECK (DiaCorte BETWEEN 1 AND 31),
    FormaPago VARCHAR(20) NOT NULL CHECK (FormaPago IN ('DEBITO_AUTOMATICO','CAJA','NOMINA')),
    CuentaDebito INT NULL REFERENCES Cuentas(CuentaId),
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('Cuotas', 'U') IS NULL
CREATE TABLE Cuotas (
    CuotaId BIGINT IDENTITY(1,1) PRIMARY KEY,
    OperacionId INT NOT NULL REFERENCES OperacionesCredito(OperacionId),
    NumeroCuota INT NOT NULL,
    FechaVencimiento DATE NOT NULL,
    CapitalCuota DECIMAL(18,2) NOT NULL,
    InteresCuota DECIMAL(18,2) NOT NULL,
    SeguroCuota DECIMAL(18,2) NOT NULL DEFAULT 0,
    ValorCuota DECIMAL(18,2) NOT NULL,
    SaldoCapital DECIMAL(18,2) NOT NULL,
    CapitalPagado DECIMAL(18,2) NOT NULL DEFAULT 0,
    InteresPagado DECIMAL(18,2) NOT NULL DEFAULT 0,
    SeguroPagado DECIMAL(18,2) NOT NULL DEFAULT 0,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('PENDIENTE','PARCIAL','PAGADA','VENCIDA','DIFERIDA','CONDONADA')),
    FechaPago DATE NULL,
    CONSTRAINT UQ_Cuota_Operacion UNIQUE(OperacionId, NumeroCuota)
);
GO

IF OBJECT_ID('SaldosOperacion', 'U') IS NULL
CREATE TABLE SaldosOperacion (
    SaldoOperacionId INT IDENTITY(1,1) PRIMARY KEY,
    OperacionId INT NOT NULL UNIQUE REFERENCES OperacionesCredito(OperacionId),
    SaldoCapitalVigente DECIMAL(18,2) NOT NULL,
    SaldoCapitalVencido DECIMAL(18,2) NOT NULL,
    SaldoInteresVigente DECIMAL(18,2) NOT NULL,
    SaldoInteresVencido DECIMAL(18,2) NOT NULL,
    SaldoSeguro DECIMAL(18,2) NOT NULL DEFAULT 0,
    SaldoTotal DECIMAL(18,2) NOT NULL,
    DiasEnMora INT NOT NULL DEFAULT 0,
    FechaUltimoPago DATE,
    FechaActualizacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('HistorialSaldos', 'U') IS NULL
CREATE TABLE HistorialSaldos (
    HistorialSaldoId BIGINT IDENTITY(1,1) PRIMARY KEY,
    OperacionId INT NOT NULL REFERENCES OperacionesCredito(OperacionId),
    FechaCorte DATE NOT NULL,
    SaldoCapitalVigente DECIMAL(18,2) NOT NULL,
    SaldoCapitalVencido DECIMAL(18,2) NOT NULL,
    SaldoInteresVigente DECIMAL(18,2) NOT NULL,
    SaldoInteresVencido DECIMAL(18,2) NOT NULL,
    SaldoTotal DECIMAL(18,2) NOT NULL,
    DiasEnMora INT NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('Pagos', 'U') IS NULL
CREATE TABLE Pagos (
    PagoId BIGINT IDENTITY(1,1) PRIMARY KEY,
    OperacionId INT NOT NULL REFERENCES OperacionesCredito(OperacionId),
    CuentaOrigenId INT NULL REFERENCES Cuentas(CuentaId),
    MontoPago DECIMAL(18,2) NOT NULL CHECK (MontoPago > 0),
    FechaPago DATETIME NOT NULL DEFAULT GETDATE(),
    FechaAplicacion DATE NOT NULL,
    TipoPago VARCHAR(20) NOT NULL CHECK (TipoPago IN ('ORDINARIO','EXTRAORDINARIO','ANTICIPADO','TOTAL')),
    CanalPago VARCHAR(20) NOT NULL CHECK (CanalPago IN ('CAJA','DEBITO_AUTOMATICO','WEB','INTERBANCARIO','BATCH')),
    Referencia VARCHAR(50),
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('APLICADO','REVERSADO','PENDIENTE')),
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('AplicacionesPagos', 'U') IS NULL
CREATE TABLE AplicacionesPagos (
    AplicacionPagoId BIGINT IDENTITY(1,1) PRIMARY KEY,
    PagoId BIGINT NOT NULL REFERENCES Pagos(PagoId),
    CuotaId BIGINT NOT NULL REFERENCES Cuotas(CuotaId),
    MontoCapital DECIMAL(18,2) NOT NULL DEFAULT 0,
    MontoInteres DECIMAL(18,2) NOT NULL DEFAULT 0,
    MontoSeguro DECIMAL(18,2) NOT NULL DEFAULT 0,
    MontoTotal DECIMAL(18,2) NOT NULL,
    FechaAplicacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('TiposGarantia', 'U') IS NULL
CREATE TABLE TiposGarantia (
    TipoGarantiaId INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('Garantias', 'U') IS NULL
CREATE TABLE Garantias (
    GarantiaId INT IDENTITY(1,1) PRIMARY KEY,
    OperacionId INT NOT NULL REFERENCES OperacionesCredito(OperacionId),
    TipoGarantiaId INT NOT NULL REFERENCES TiposGarantia(TipoGarantiaId),
    Descripcion VARCHAR(500) NOT NULL,
    ValorGarantia DECIMAL(18,2) NOT NULL,
    FechaConstitucion DATE NOT NULL,
    FechaVencimiento DATE NOT NULL,
    Estado VARCHAR(20) NOT NULL CHECK (Estado IN ('VIGENTE','LIBERADA','EJECUTADA','VENCIDA')),
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

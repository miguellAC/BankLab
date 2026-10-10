-- =========================================
-- File: 02-schema/02-tables-productos-cuentas.sql
-- Description: Creates tables for bank accounts and products
-- =========================================

USE BankLabDB;
GO

IF OBJECT_ID('ProductosBancarios', 'U') IS NULL
CREATE TABLE ProductosBancarios (
    ProductoId INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    TipoProducto VARCHAR(20) NOT NULL CHECK (TipoProducto IN ('CUENTA','CREDITO','INVERSION','SEGURO')),
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('TiposCuenta', 'U') IS NULL
CREATE TABLE TiposCuenta (
    TipoCuentaId INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,
    Nombre VARCHAR(50) NOT NULL,     -- Ahorros, Corriente, CDT, etc.
    RequiereSaldoMinimo BIT NOT NULL DEFAULT 0,
    SaldoMinimo DECIMAL(18,2) DEFAULT 0,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('Cuentas', 'U') IS NULL
CREATE TABLE Cuentas (
    CuentaId INT IDENTITY(1,1) PRIMARY KEY,
    NumeroCuenta VARCHAR(20) NOT NULL UNIQUE,
    ClienteId INT NOT NULL REFERENCES Clientes(ClienteId),
    ProductoId INT NOT NULL REFERENCES ProductosBancarios(ProductoId),
    TipoCuentaId INT NOT NULL REFERENCES TiposCuenta(TipoCuentaId),
    SucursalId INT NOT NULL REFERENCES Sucursales(SucursalId),
    Saldo DECIMAL(18,2) NOT NULL DEFAULT 0,
    SaldoDisponible DECIMAL(18,2) NOT NULL DEFAULT 0,
    Estado VARCHAR(20) NOT NULL DEFAULT 'ACTIVA' CHECK (Estado IN ('ACTIVA','INACTIVA','BLOQUEADA','CANCELADA','EMBARGADA')),
    FechaApertura DATE NOT NULL DEFAULT GETDATE(),
    FechaCancelacion DATE,
    Moneda VARCHAR(3) NOT NULL DEFAULT 'COP',
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME,
    UsuarioModificacion VARCHAR(50)
);
GO

IF OBJECT_ID('TitularesCuenta', 'U') IS NULL
CREATE TABLE TitularesCuenta (
    TitularId INT IDENTITY(1,1) PRIMARY KEY,
    CuentaId INT NOT NULL REFERENCES Cuentas(CuentaId),
    ClienteId INT NOT NULL REFERENCES Clientes(ClienteId),
    TipoTitular VARCHAR(20) NOT NULL DEFAULT 'TITULAR' CHECK (TipoTitular IN ('TITULAR','COTITULAR','AUTORIZADO')),
    FechaAsignacion DATE NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT UQ_Titular_Cuenta UNIQUE (CuentaId, ClienteId)
);
GO

IF OBJECT_ID('TiposTransaccion', 'U') IS NULL
CREATE TABLE TiposTransaccion (
    TipoTransaccionId INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,
    Nombre VARCHAR(50) NOT NULL,       -- Depósito, Retiro, Transferencia, etc.
    Naturaleza CHAR(1) NOT NULL CHECK (Naturaleza IN ('D','C')),  -- D=Debito, C=Credito
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('Movimientos', 'U') IS NULL
CREATE TABLE Movimientos (
    MovimientoId BIGINT IDENTITY(1,1) PRIMARY KEY,
    CuentaId INT NOT NULL REFERENCES Cuentas(CuentaId),
    TipoTransaccionId INT NOT NULL REFERENCES TiposTransaccion(TipoTransaccionId),
    Monto DECIMAL(18,2) NOT NULL CHECK (Monto > 0),
    SaldoAnterior DECIMAL(18,2) NOT NULL,
    SaldoPosterior DECIMAL(18,2) NOT NULL,
    Descripcion VARCHAR(200),
    Referencia VARCHAR(50),
    CanalOrigen VARCHAR(20) DEFAULT 'OFICINA' CHECK (CanalOrigen IN ('OFICINA','WEB','MOVIL','ATM','INTERBANCARIO','BATCH')),
    FechaMovimiento DATETIME NOT NULL DEFAULT GETDATE(),
    FechaValor DATE NOT NULL DEFAULT GETDATE(),
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER
);
GO

IF OBJECT_ID('EstadosCuenta', 'U') IS NULL
CREATE TABLE EstadosCuenta (
    EstadoCuentaHistId INT IDENTITY(1,1) PRIMARY KEY,
    CuentaId INT NOT NULL REFERENCES Cuentas(CuentaId),
    EstadoAnterior VARCHAR(20) NOT NULL,
    EstadoNuevo VARCHAR(20) NOT NULL,
    MotivoTransicion VARCHAR(200),
    FechaCambio DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioCambio VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER
);
GO

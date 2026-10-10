-- =========================================
-- File: 02-schema/01-tables-clientes.sql
-- Description: Creates tables for clients domain
-- =========================================

USE BankLabDB;
GO

IF OBJECT_ID('TiposIdentificacion', 'U') IS NULL
CREATE TABLE TiposIdentificacion (
    TipoIdentificacionId INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,          -- CC, NIT, CE, TI, PA
    Nombre VARCHAR(50) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('Sucursales', 'U') IS NULL
CREATE TABLE Sucursales (
    SucursalId INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Ciudad VARCHAR(50) NOT NULL,
    Direccion VARCHAR(200),
    Telefono VARCHAR(20),
    Activa BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('Empleados', 'U') IS NULL
CREATE TABLE Empleados (
    EmpleadoId INT IDENTITY(1,1) PRIMARY KEY,
    SucursalId INT NOT NULL REFERENCES Sucursales(SucursalId),
    NumeroEmpleado VARCHAR(20) NOT NULL UNIQUE,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Cargo VARCHAR(50),
    Email VARCHAR(100),
    Activo BIT NOT NULL DEFAULT 1,
    FechaIngreso DATE NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('Clientes', 'U') IS NULL
CREATE TABLE Clientes (
    ClienteId INT IDENTITY(1,1) PRIMARY KEY,
    TipoIdentificacionId INT NOT NULL REFERENCES TiposIdentificacion(TipoIdentificacionId),
    NumeroIdentificacion VARCHAR(20) NOT NULL,
    PrimerNombre VARCHAR(50) NOT NULL,
    SegundoNombre VARCHAR(50),
    PrimerApellido VARCHAR(50) NOT NULL,
    SegundoApellido VARCHAR(50),
    FechaNacimiento DATE,
    Genero CHAR(1) CHECK (Genero IN ('M','F','O')),
    Email VARCHAR(100),
    Telefono VARCHAR(20),
    SucursalId INT NOT NULL REFERENCES Sucursales(SucursalId),
    Estado VARCHAR(20) NOT NULL DEFAULT 'ACTIVO' CHECK (Estado IN ('ACTIVO','INACTIVO','BLOQUEADO','FALLECIDO')),
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioCreacion VARCHAR(50) NOT NULL DEFAULT SYSTEM_USER,
    FechaModificacion DATETIME,
    UsuarioModificacion VARCHAR(50),
    CONSTRAINT UQ_Cliente_Identificacion UNIQUE (TipoIdentificacionId, NumeroIdentificacion)
);
GO

IF OBJECT_ID('DireccionesCliente', 'U') IS NULL
CREATE TABLE DireccionesCliente (
    DireccionId INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL REFERENCES Clientes(ClienteId),
    TipoDireccion VARCHAR(20) NOT NULL CHECK (TipoDireccion IN ('RESIDENCIA','TRABAJO','CORRESPONDENCIA','OTRA')),
    Direccion VARCHAR(200) NOT NULL,
    Ciudad VARCHAR(50) NOT NULL,
    Departamento VARCHAR(50),
    CodigoPostal VARCHAR(10),
    Pais VARCHAR(50) NOT NULL DEFAULT 'Colombia',
    EsPrincipal BIT NOT NULL DEFAULT 0,
    Activa BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('ContactosCliente', 'U') IS NULL
CREATE TABLE ContactosCliente (
    ContactoId INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL REFERENCES Clientes(ClienteId),
    TipoContacto VARCHAR(20) NOT NULL CHECK (TipoContacto IN ('TELEFONO','CELULAR','EMAIL','FAX')),
    Valor VARCHAR(100) NOT NULL,
    EsPrincipal BIT NOT NULL DEFAULT 0,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

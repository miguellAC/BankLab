-- 01-catalogos.sql
-- Generates catalog and reference data for the BankLab database
-- Includes: TiposIdentificacion, Sucursales, Empleados, ProductosBancarios, TiposCuenta, TiposTransaccion, ProductosCredito, TiposGarantia, TiposDiferimiento, CatalogosReporte, ParametrosReporte

SET NOCOUNT ON;
GO

USE BankLabDB;
GO

PRINT 'Poblando tablas de catálogo...';

-- TiposIdentificacion
-- Schema: TipoIdentificacionId, Codigo, Nombre, Activo, FechaCreacion
SET IDENTITY_INSERT TiposIdentificacion ON;
INSERT INTO TiposIdentificacion (TipoIdentificacionId, Codigo, Nombre, Activo) VALUES
(1, 'CC', 'Cédula de Ciudadanía', 1),
(2, 'NIT', 'NIT', 1),
(3, 'CE', 'Cédula de Extranjería', 1),
(4, 'TI', 'Tarjeta de Identidad', 1),
(5, 'PA', 'Pasaporte', 1);
SET IDENTITY_INSERT TiposIdentificacion OFF;

-- Sucursales
-- Schema: SucursalId, Codigo, Nombre, Ciudad, Direccion, Telefono, Activa, FechaCreacion
SET IDENTITY_INSERT Sucursales ON;
INSERT INTO Sucursales (SucursalId, Codigo, Nombre, Ciudad, Direccion, Telefono, Activa) VALUES
(1, 'SUC001', 'Bogotá Principal', 'Bogotá', 'Cra 7 # 72-01', '6015550001', 1),
(2, 'SUC002', 'Medellín Centro', 'Medellín', 'Cl 50 # 51-20', '6045550002', 1),
(3, 'SUC003', 'Cali Norte', 'Cali', 'Av 6N # 21-50', '6025550003', 1),
(4, 'SUC004', 'Barranquilla Plaza', 'Barranquilla', 'Cl 72 # 46-10', '6055550004', 1),
(5, 'SUC005', 'Cartagena Centro', 'Cartagena', 'Centro Histórico Calle 33', '6055550005', 1),
(6, 'SUC006', 'Bucaramanga Cabecera', 'Bucaramanga', 'Cra 33 # 48-12', '6075550006', 1),
(7, 'SUC007', 'Pereira Centro', 'Pereira', 'Cl 19 # 7-30', '6065550007', 1),
(8, 'SUC008', 'Manizales Centro', 'Manizales', 'Cra 22 # 21-15', '6065550008', 1),
(9, 'SUC009', 'Ibagué Centro', 'Ibagué', 'Cra 3 # 12-45', '6085550009', 1),
(10, 'SUC010', 'Santa Marta Rodadero', 'Santa Marta', 'Cra 2 # 7-20', '6055550010', 1);
SET IDENTITY_INSERT Sucursales OFF;

-- Empleados (must be inserted before references in Solicitudes, Cobros, HistorialEjecuciones)
-- Schema: EmpleadoId, SucursalId, NumeroEmpleado, Nombres, Apellidos, Cargo, Email, Activo, FechaIngreso
SET IDENTITY_INSERT Empleados ON;
WITH Nums AS (
    SELECT 1 AS N UNION ALL SELECT N+1 FROM Nums WHERE N < 30
)
INSERT INTO Empleados (EmpleadoId, SucursalId, NumeroEmpleado, Nombres, Apellidos, Cargo, Email, Activo, FechaIngreso)
SELECT 
    N AS EmpleadoId,
    ((N - 1) / 3) + 1 AS SucursalId,
    'EMP' + RIGHT('00000' + CAST(N AS VARCHAR), 5) AS NumeroEmpleado,
    CHOOSE((N % 10) + 1, 'Carlos', 'Maria', 'Juan', 'Ana', 'Luis', 'Diana', 'Jorge', 'Laura', 'Andres', 'Carolina') AS Nombres,
    CHOOSE((N % 10) + 1, 'Gomez', 'Rodriguez', 'Martinez', 'Garcia', 'Lopez', 'Hernandez', 'Gonzalez', 'Perez', 'Sanchez', 'Ramirez') AS Apellidos,
    CHOOSE((N % 3) + 1, 'Director', 'Asesor', 'Cajero') AS Cargo,
    'emp' + CAST(N AS VARCHAR) + '@bancoejemplo.com.co' AS Email,
    1 AS Activo,
    DATEADD(YEAR, -(N % 5) - 1, GETDATE()) AS FechaIngreso
FROM Nums;
SET IDENTITY_INSERT Empleados OFF;

-- ProductosBancarios
-- Schema: ProductoId, Codigo, Nombre, Descripcion, TipoProducto, Activo
SET IDENTITY_INSERT ProductosBancarios ON;
INSERT INTO ProductosBancarios (ProductoId, Codigo, Nombre, TipoProducto, Activo) VALUES
(1, 'PROD01', 'Cuenta Ahorros', 'CUENTA', 1),
(2, 'PROD02', 'Cuenta Corriente', 'CUENTA', 1),
(3, 'PROD03', 'CDT', 'INVERSION', 1),
(4, 'PROD04', 'Crédito Libre Inversión', 'CREDITO', 1),
(5, 'PROD05', 'Crédito Hipotecario', 'CREDITO', 1),
(6, 'PROD06', 'Crédito Vehículo', 'CREDITO', 1),
(7, 'PROD07', 'Crédito Rotativo', 'CREDITO', 1),
(8, 'PROD08', 'Seguro Vida', 'SEGURO', 1);
SET IDENTITY_INSERT ProductosBancarios OFF;

-- TiposCuenta
-- Schema: TipoCuentaId, Codigo, Nombre, RequiereSaldoMinimo, SaldoMinimo, Activo
SET IDENTITY_INSERT TiposCuenta ON;
INSERT INTO TiposCuenta (TipoCuentaId, Codigo, Nombre, RequiereSaldoMinimo, SaldoMinimo, Activo) VALUES
(1, 'AHO', 'Ahorros', 0, 0, 1),
(2, 'CTE', 'Corriente', 1, 500000, 1),
(3, 'CDT', 'CDT', 0, 0, 1);
SET IDENTITY_INSERT TiposCuenta OFF;

-- TiposTransaccion
-- Schema: TipoTransaccionId, Codigo, Nombre, Naturaleza, Activo
SET IDENTITY_INSERT TiposTransaccion ON;
INSERT INTO TiposTransaccion (TipoTransaccionId, Codigo, Nombre, Naturaleza, Activo) VALUES
(1, 'DEP', 'Depósito', 'C', 1),
(2, 'RET', 'Retiro', 'D', 1),
(3, 'TRE', 'Transferencia Enviada', 'D', 1),
(4, 'TRR', 'Transferencia Recibida', 'C', 1),
(5, 'PAG', 'Pago Crédito', 'D', 1),
(6, 'INT', 'Intereses', 'C', 1),
(7, 'COM', 'Comisión', 'D', 1),
(8, 'NOM', 'Nómina', 'C', 1),
(9, 'REV', 'Reverso', 'C', 1);
SET IDENTITY_INSERT TiposTransaccion OFF;

-- ProductosCredito
-- Schema: ProductoCreditoId, Codigo, Nombre, TasaBaseAnual, PlazoMinMeses, PlazoMaxMeses, MontoMinimo, MontoMaximo, RequiereGarantia, Activo
SET IDENTITY_INSERT ProductosCredito ON;
INSERT INTO ProductosCredito (ProductoCreditoId, Codigo, Nombre, TasaBaseAnual, PlazoMinMeses, PlazoMaxMeses, MontoMinimo, MontoMaximo, RequiereGarantia, Activo) VALUES
(1, 'CRED-LI', 'Libre Inversión', 18.00, 6, 60, 1000000, 50000000, 0, 1),
(2, 'CRED-HIP', 'Hipotecario', 12.00, 60, 360, 50000000, 500000000, 1, 1),
(3, 'CRED-VEH', 'Vehículo', 15.00, 12, 84, 10000000, 200000000, 1, 1),
(4, 'CRED-ROT', 'Rotativo', 24.00, 1, 36, 500000, 20000000, 0, 1);
SET IDENTITY_INSERT ProductosCredito OFF;

-- TiposGarantia
-- Schema: TipoGarantiaId, Codigo, Nombre, Descripcion, Activo
SET IDENTITY_INSERT TiposGarantia ON;
INSERT INTO TiposGarantia (TipoGarantiaId, Codigo, Nombre, Descripcion, Activo) VALUES
(1, 'HIP', 'Hipoteca', 'Garantía sobre bien inmueble', 1),
(2, 'PRE', 'Prenda Vehicular', 'Garantía sobre vehículo', 1),
(3, 'COD', 'Codeudor', 'Garantía personal de un tercero', 1),
(4, 'DEP', 'Depósito', 'Dinero en depósito pignorado', 1),
(5, 'POL', 'Póliza', 'Póliza de seguro', 1);
SET IDENTITY_INSERT TiposGarantia OFF;

-- TiposDiferimiento
-- Schema: TipoDiferimientoId, Codigo, Nombre, Descripcion, PlazoMaxMeses, Activo
SET IDENTITY_INSERT TiposDiferimiento ON;
INSERT INTO TiposDiferimiento (TipoDiferimientoId, Codigo, Nombre, Descripcion, PlazoMaxMeses, Activo) VALUES
(1, 'EMERGENCIA', 'Emergencia Económica', 'Diferimiento por emergencia', 6, 1),
(2, 'CALAMIDAD', 'Calamidad', 'Diferimiento por calamidad doméstica', 3, 1),
(3, 'ESPECIAL', 'Programa Especial', 'Programa especial de alivio', 12, 1);
SET IDENTITY_INSERT TiposDiferimiento OFF;

-- CatalogosReporte
-- Schema: ReporteId, Codigo, Nombre, Descripcion, RutaArchivo, TipoReporte, Activo
SET IDENTITY_INSERT CatalogosReporte ON;
INSERT INTO CatalogosReporte (ReporteId, Codigo, Nombre, Descripcion, TipoReporte, Activo) VALUES
(1, 'REP-01', 'Cartera General', 'Reporte de toda la cartera', 'CARTERA', 1),
(2, 'REP-02', 'Cartera Vencida', 'Reporte de cartera en mora', 'CARTERA', 1),
(3, 'REP-03', 'Clientes por Sucursal', 'Distribución de clientes', 'CLIENTES', 1),
(4, 'REP-04', 'Riesgo por Categoría', 'Clasificación de riesgo', 'RIESGO', 1),
(5, 'REP-05', 'Auditoría de Operaciones', 'Operaciones relevantes', 'AUDITORIA', 1);
SET IDENTITY_INSERT CatalogosReporte OFF;

-- ParametrosReporte
-- Schema: ParametroReporteId, ReporteId, NombreParametro, TipoDato, Obligatorio, ValorPredeterminado, Orden
SET IDENTITY_INSERT ParametrosReporte ON;
INSERT INTO ParametrosReporte (ParametroReporteId, ReporteId, NombreParametro, TipoDato, Obligatorio, Orden) VALUES
(1, 1, '@FechaCorte', 'DATE', 1, 1),
(2, 2, '@DiasMoraMinimo', 'INT', 1, 1),
(3, 3, '@SucursalId', 'INT', 0, 1),
(4, 4, '@Clasificacion', 'VARCHAR', 0, 1),
(5, 5, '@FechaInicio', 'DATE', 1, 1),
(6, 5, '@FechaFin', 'DATE', 1, 2);
SET IDENTITY_INSERT ParametrosReporte OFF;

PRINT 'Catálogos poblados exitosamente.';
GO

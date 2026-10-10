-- =========================================
-- File: 03-constraints/01-foreign-keys.sql
-- Description: Supplementary constraints and cross-domain relationships
-- =========================================

USE BankLabDB;
GO

-- NOTE: All primary Foreign Keys have been created inline within the CREATE TABLE 
-- statements for clarity and domain grouping. This file contains any supplementary
-- constraints or comments regarding cross-domain checks.

/*
Cross-Domain Foreign Key References already included inline:
- Empleados.SucursalId -> Sucursales.SucursalId
- Clientes.SucursalId -> Sucursales.SucursalId
- Cuentas.ClienteId -> Clientes.ClienteId
- Cuentas.SucursalId -> Sucursales.SucursalId
- TitularesCuenta.ClienteId -> Clientes.ClienteId
- SolicitudesCredito.ClienteId -> Clientes.ClienteId
- SolicitudesCredito.EmpleadoId -> Empleados.EmpleadoId
- OperacionesCredito.ClienteId -> Clientes.ClienteId
- OperacionesCredito.SucursalId -> Sucursales.SucursalId
- CondicionesCredito.CuentaDebito -> Cuentas.CuentaId
- Pagos.CuentaOrigenId -> Cuentas.CuentaId
- Cobros.EmpleadoId -> Empleados.EmpleadoId
- ClasificacionesRiesgo.ClienteId -> Clientes.ClienteId
- HistorialRiesgo.ClienteId -> Clientes.ClienteId
- ClientesIncluidosDiferimiento.ClienteId -> Clientes.ClienteId
- ClientesExcluidosDiferimiento.ClienteId -> Clientes.ClienteId
- HistorialEjecuciones.EmpleadoId -> Empleados.EmpleadoId
*/

-- Add any extra CHECK constraints if missing

-- Ensure MontoMaximo > MontoMinimo in ProductosCredito
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_ProductosCredito_Montos')
BEGIN
    ALTER TABLE ProductosCredito
    ADD CONSTRAINT CK_ProductosCredito_Montos CHECK (MontoMaximo >= MontoMinimo);
END
GO

-- Ensure PlazoMaxMeses > PlazoMinMeses in ProductosCredito
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_ProductosCredito_Plazos')
BEGIN
    ALTER TABLE ProductosCredito
    ADD CONSTRAINT CK_ProductosCredito_Plazos CHECK (PlazoMaxMeses >= PlazoMinMeses);
END
GO

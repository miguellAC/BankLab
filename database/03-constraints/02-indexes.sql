-- =========================================
-- File: 03-constraints/02-indexes.sql
-- Description: Create indexes justified by query patterns
-- =========================================

USE BankLabDB;
GO

-- 1. IX_Clientes_NumeroIdentificacion (TipoIdentificacionId, NumeroIdentificacion)
-- Supports exact matching for client lookup by ID
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Clientes_NumeroIdentificacion')
CREATE NONCLUSTERED INDEX IX_Clientes_NumeroIdentificacion ON Clientes (TipoIdentificacionId, NumeroIdentificacion);
GO

-- 2. IX_Clientes_SucursalId
-- Supports filtering clients by branch
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Clientes_SucursalId')
CREATE NONCLUSTERED INDEX IX_Clientes_SucursalId ON Clientes (SucursalId);
GO

-- 3. IX_Clientes_Estado
-- Supports filtering active/inactive clients
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Clientes_Estado')
CREATE NONCLUSTERED INDEX IX_Clientes_Estado ON Clientes (Estado);
GO

-- 4. IX_Cuentas_ClienteId
-- Supports fetching all accounts for a specific client
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Cuentas_ClienteId')
CREATE NONCLUSTERED INDEX IX_Cuentas_ClienteId ON Cuentas (ClienteId);
GO

-- 5. IX_Cuentas_Estado
-- Supports filtering active/inactive accounts
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Cuentas_Estado')
CREATE NONCLUSTERED INDEX IX_Cuentas_Estado ON Cuentas (Estado);
GO

-- 6. IX_Movimientos_CuentaId_Fecha (CuentaId, FechaMovimiento DESC)
-- Supports fetching statement or latest movements for an account
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Movimientos_CuentaId_Fecha')
CREATE NONCLUSTERED INDEX IX_Movimientos_CuentaId_Fecha ON Movimientos (CuentaId, FechaMovimiento DESC);
GO

-- 7. IX_Movimientos_FechaMovimiento
-- Supports reports by movement date globally
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Movimientos_FechaMovimiento')
CREATE NONCLUSTERED INDEX IX_Movimientos_FechaMovimiento ON Movimientos (FechaMovimiento);
GO

-- 8. IX_Movimientos_Referencia
-- Supports locating specific transactions by reference number
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Movimientos_Referencia')
CREATE NONCLUSTERED INDEX IX_Movimientos_Referencia ON Movimientos (Referencia);
GO

-- 9. IX_OperacionesCredito_ClienteId
-- Supports listing all credit operations for a client
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_OperacionesCredito_ClienteId')
CREATE NONCLUSTERED INDEX IX_OperacionesCredito_ClienteId ON OperacionesCredito (ClienteId);
GO

-- 10. IX_OperacionesCredito_Estado
-- Supports portfolio and risk reporting by operation state
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_OperacionesCredito_Estado')
CREATE NONCLUSTERED INDEX IX_OperacionesCredito_Estado ON OperacionesCredito (Estado);
GO

-- 11. IX_OperacionesCredito_FechaVencimiento
-- Supports identifying upcoming maturing operations
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_OperacionesCredito_FechaVencimiento')
CREATE NONCLUSTERED INDEX IX_OperacionesCredito_FechaVencimiento ON OperacionesCredito (FechaVencimiento);
GO

-- 12. IX_Cuotas_OperacionId
-- Supports fetching all installments for a credit operation
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Cuotas_OperacionId')
CREATE NONCLUSTERED INDEX IX_Cuotas_OperacionId ON Cuotas (OperacionId);
GO

-- 13. IX_Cuotas_Estado_FechaVencimiento (Estado, FechaVencimiento)
-- Supports finding overdue installments across the portfolio
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Cuotas_Estado_FechaVencimiento')
CREATE NONCLUSTERED INDEX IX_Cuotas_Estado_FechaVencimiento ON Cuotas (Estado, FechaVencimiento);
GO

-- 14. IX_Pagos_OperacionId_FechaPago
-- Supports fetching payment history for a credit operation
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pagos_OperacionId_FechaPago')
CREATE NONCLUSTERED INDEX IX_Pagos_OperacionId_FechaPago ON Pagos (OperacionId, FechaPago DESC);
GO

-- 15. IX_Pagos_Referencia
-- Supports looking up payments by reference number
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pagos_Referencia')
CREATE NONCLUSTERED INDEX IX_Pagos_Referencia ON Pagos (Referencia);
GO

-- 16. IX_Pagos_Estado
-- Supports filtering pending or reversed payments
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pagos_Estado')
CREATE NONCLUSTERED INDEX IX_Pagos_Estado ON Pagos (Estado);
GO

-- 17. IX_SaldosVencidos_OperacionId_FechaCorte
-- Supports reporting on historical overdue balances
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SaldosVencidos_OperacionId_FechaCorte')
CREATE NONCLUSTERED INDEX IX_SaldosVencidos_OperacionId_FechaCorte ON SaldosVencidos (OperacionId, FechaCorte);
GO

-- 18. IX_ClasificacionesRiesgo_ClienteId_Vigente
-- Supports quick lookup of current risk classification for a client
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ClasificacionesRiesgo_ClienteId_Vigente')
CREATE NONCLUSTERED INDEX IX_ClasificacionesRiesgo_ClienteId_Vigente ON ClasificacionesRiesgo (ClienteId, Vigente);
GO

-- 19. IX_RegistrosAuditoria_TipoEntidad_EntidadId
-- Supports filtering audit trail by specific entity
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RegistrosAuditoria_TipoEntidad_EntidadId')
CREATE NONCLUSTERED INDEX IX_RegistrosAuditoria_TipoEntidad_EntidadId ON RegistrosAuditoria (TipoEntidad, EntidadId);
GO

-- 20. IX_RegistrosAuditoria_FechaAuditoria
-- Supports date range queries on audit trail
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RegistrosAuditoria_FechaAuditoria')
CREATE NONCLUSTERED INDEX IX_RegistrosAuditoria_FechaAuditoria ON RegistrosAuditoria (FechaAuditoria);
GO

-- 21. IX_LotesImportacion_Estado
-- Supports finding pending integration batches
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_LotesImportacion_Estado')
CREATE NONCLUSTERED INDEX IX_LotesImportacion_Estado ON LotesImportacion (Estado);
GO

-- 22. IX_MensajesIntegracion_Estado
-- Supports queue processing for integration messages
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_MensajesIntegracion_Estado')
CREATE NONCLUSTERED INDEX IX_MensajesIntegracion_Estado ON MensajesIntegracion (Estado);
GO

-- 23. IX_HistorialSaldos_OperacionId_FechaCorte
-- Supports balance history lookup for an operation
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_HistorialSaldos_OperacionId_FechaCorte')
CREATE NONCLUSTERED INDEX IX_HistorialSaldos_OperacionId_FechaCorte ON HistorialSaldos (OperacionId, FechaCorte);
GO

TRUNCATE TABLE [BankLabDB].[dbo].[Pagos];
TRUNCATE TABLE [BankLabDB].[dbo].[Cuotas];
TRUNCATE TABLE [BankLabDB].[dbo].[Movimientos];
-- Needs DELETE for tables with FKs if TRUNCATE fails, but for reset:
DELETE FROM [BankLabDB].[dbo].[Pagos];
DELETE FROM [BankLabDB].[dbo].[Cuotas];
DELETE FROM [BankLabDB].[dbo].[Movimientos];
DELETE FROM [BankLabDB].[dbo].[Cuentas];
DELETE FROM [BankLabDB].[dbo].[Operaciones];
DELETE FROM [BankLabDB].[dbo].[Clientes];
DELETE FROM [BankLabDB].[dbo].[Sucursales];

DBCC CHECKIDENT ('[BankLabDB].[dbo].[Pagos]', RESEED, 0);
DBCC CHECKIDENT ('[BankLabDB].[dbo].[Cuotas]', RESEED, 0);
DBCC CHECKIDENT ('[BankLabDB].[dbo].[Movimientos]', RESEED, 0);
DBCC CHECKIDENT ('[BankLabDB].[dbo].[Cuentas]', RESEED, 0);
DBCC CHECKIDENT ('[BankLabDB].[dbo].[Operaciones]', RESEED, 0);
DBCC CHECKIDENT ('[BankLabDB].[dbo].[Clientes]', RESEED, 0);
DBCC CHECKIDENT ('[BankLabDB].[dbo].[Sucursales]', RESEED, 0);

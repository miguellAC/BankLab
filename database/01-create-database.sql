-- =========================================
-- File: 01-create-database.sql
-- Description: Creates the BankLabDB database
-- =========================================

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BankLabDB')
BEGIN
    CREATE DATABASE BankLabDB;
END
GO

USE BankLabDB;
GO

-- Set basic database configurations
ALTER DATABASE BankLabDB SET RECOVERY SIMPLE;
GO

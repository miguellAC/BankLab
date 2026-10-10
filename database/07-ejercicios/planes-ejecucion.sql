-- Enable statistics
SET STATISTICS IO ON;
SET STATISTICS TIME ON;

-- Exercise: Compare index scan vs seek
-- Query 1 (Scan typically if no index on NombreCompleto)
SELECT * FROM Clientes WHERE NombreCompleto LIKE '%Perez%';

-- Query 2 (Seek if index exists)
SELECT * FROM Clientes WHERE ClienteId = 100;

-- Exercise: Show impact of missing index
-- Run a query on a column without an index and observe the execution plan
SELECT * FROM Operaciones WHERE Saldo > 10000;

-- Exercise: Compare with and without index hints
-- Assuming an index IX_Operaciones_Saldo exists
SELECT * FROM Operaciones WITH (INDEX(IX_Operaciones_Saldo)) WHERE Saldo > 10000;
SELECT * FROM Operaciones WHERE Saldo > 10000;

-- Instructions: Press Ctrl+M in SSMS to enable Actual Execution Plan before running queries.

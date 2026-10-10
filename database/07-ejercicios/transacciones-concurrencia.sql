-- Demonstrate READ COMMITTED vs REPEATABLE READ

-- Session 1
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
BEGIN TRAN;
SELECT Saldo FROM Cuentas WHERE CuentaId = 1;
-- Wait for Session 2 to update and commit
-- Run SELECT again, value changed (Non-repeatable read)
SELECT Saldo FROM Cuentas WHERE CuentaId = 1;
COMMIT;

-- Session 2
BEGIN TRAN;
UPDATE Cuentas SET Saldo = Saldo - 100 WHERE CuentaId = 1;
COMMIT;

-- Demonstrate deadlock scenario
-- Session 1
BEGIN TRAN;
UPDATE Cuentas SET Saldo = Saldo - 100 WHERE CuentaId = 1;
-- Wait
UPDATE Cuentas SET Saldo = Saldo + 100 WHERE CuentaId = 2;
COMMIT;

-- Session 2
BEGIN TRAN;
UPDATE Cuentas SET Saldo = Saldo - 100 WHERE CuentaId = 2;
-- Wait
UPDATE Cuentas SET Saldo = Saldo + 100 WHERE CuentaId = 1;
COMMIT;

-- Demonstrate dirty read prevention (READ COMMITTED is default and prevents it)
-- Session 1
BEGIN TRAN;
UPDATE Cuentas SET Saldo = 500 WHERE CuentaId = 1;
-- Do not commit yet

-- Session 2
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT Saldo FROM Cuentas WHERE CuentaId = 1; -- Dirty read

-- Proper transaction isolation for payment processing
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRAN;
-- Payment processing logic ensuring complete consistency
COMMIT;

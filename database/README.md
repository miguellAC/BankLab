# BankLab Database

This repository contains the database schema for the BankLab project (Banco Ejemplo). 

## Execution Order
To deploy the database from scratch, execute the scripts in the following order:

1. `01-create-database.sql`
2. `02-schema/01-tables-clientes.sql`
3. `02-schema/02-tables-productos-cuentas.sql`
4. `02-schema/03-tables-creditos-cartera.sql`
5. `02-schema/04-tables-cobros-riesgo.sql`
6. `02-schema/05-tables-reportes-auditoria.sql`
7. `02-schema/06-tables-integracion.sql`
8. `03-constraints/01-foreign-keys.sql`
9. `03-constraints/02-indexes.sql`

## Database Overview
The database is structured into several business domains:
- **Clientes (Clients):** Manage clients, branches, and employees.
- **Productos & Cuentas (Products & Accounts):** Manage bank products, current/savings accounts, movements, and states.
- **Créditos & Cartera (Credits & Portfolio):** Manage credit products, applications, operations, installments, and payments.
- **Cobros & Riesgo (Collections & Risk):** Track overdue balances, collections efforts, and client risk classifications.
- **Reportes & Auditoria (Reports & Auditing):** Centralized system auditing and report configuration.
- **Integración (Integration):** Data ingestion batches and message queues.

Total number of tables: 40.

## How to run the scripts
The SQL Server instance runs in Docker on port `1433`.
You can run the scripts using SQL Server Management Studio (SSMS), Azure Data Studio, or the `sqlcmd` tool:

```bash
# Example using sqlcmd (ensure you provide the correct SA password)
sqlcmd -S localhost,1433 -U sa -P "BankLab2024!" -i 01-create-database.sql
sqlcmd -S localhost,1433 -U sa -P "BankLab2024!" -i 02-schema/01-tables-clientes.sql
# ... and so on
```

Ensure you have enabled `TrustServerCertificate=True` if connecting from an application.

# Catálogo de Stored Procedures

Todo el acceso a datos en BankLab (tanto legacy como moderno) se realiza de forma exclusiva a través de Procedimientos Almacenados.

## Módulo: Clientes

| Nombre | Parámetros | Retorno | Transacción | Descripción |
|--------|------------|---------|-------------|-------------|
| `sp_Clientes_Buscar` | `@Criterio`, `@Offset`, `@Fetch` | Resultset | No | Búsqueda paginada de clientes. |
| `sp_Clientes_Insertar` | `@Identificacion`, `@Nombres`... | `@ClienteId` OUT | Sí | Registra un nuevo cliente. |
| `sp_Clientes_Actualizar`| `@ClienteId`, `@Nombres`... | N/A | Sí | Actualiza información demográfica. |

## Módulo: Cuentas

| Nombre | Parámetros | Retorno | Transacción | Descripción |
|--------|------------|---------|-------------|-------------|
| `sp_Cuentas_ObtenerSaldo` | `@CuentaId` | `@Saldo` OUT | No | Consulta saldo actual. |
| `sp_Cuentas_Debitar` | `@CuentaId`, `@Monto` | N/A | Sí | Resta saldo y registra movimiento. |
| `sp_Cuentas_Acreditar` | `@CuentaId`, `@Monto` | N/A | Sí | Suma saldo y registra movimiento. |

## Módulo: Créditos

| Nombre | Parámetros | Retorno | Transacción | Descripción |
|--------|------------|---------|-------------|-------------|
| `sp_Creditos_GenerarAmortizacion`| `@CreditoId`, `@Tasa`, `@Plazo` | N/A | Sí | Genera el plan de cuotas (Francesa). |
| `sp_Pagos_AplicarCuota` | `@CreditoId`, `@MontoPago` | N/A | Sí | Aplica un monto de pago a las cuotas vigentes. |

## Ejemplo de Uso

```sql
-- Obtener saldo de cuenta
DECLARE @SaldoActual DECIMAL(18,2)
EXEC sp_Cuentas_ObtenerSaldo 
    @CuentaId = 105, 
    @Saldo = @SaldoActual OUTPUT
    
PRINT 'El saldo es: ' + CAST(@SaldoActual AS VARCHAR)
```

## Consideraciones de Rendimiento
- Los procedimientos de búsqueda (`_Buscar`) utilizan `OFFSET` y `FETCH NEXT` en lugar de `TOP` para permitir una paginación eficiente en la base de datos, evitando transferir miles de registros a la capa intermedia.
- Los procedimientos transaccionales verifican `@@TRANCOUNT` para comportarse correctamente tanto si se llaman dentro de una transacción desde DAS, como si se llaman independientemente.

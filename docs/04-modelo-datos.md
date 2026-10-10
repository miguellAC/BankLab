# Modelo de Datos

La base de datos `BankLabDB` consta de aproximadamente 40 tablas distribuidas en varios dominios funcionales.

## Diagrama Entidad-Relación (Simplificado)

```mermaid
erDiagram
    CLIENTE ||--o{ CUENTA : "tiene"
    CLIENTE ||--o{ CREDITO : "solicita"
    CUENTA ||--o{ TRANSACCION : "registra"
    CREDITO ||--o{ CUOTA : "se divide en"
    CUOTA ||--o{ PAGO : "es cancelada por"
    
    CLIENTE {
        int ClienteId PK
        string Identificacion
        string Nombres
    }
    
    CUENTA {
        int CuentaId PK
        int ClienteId FK
        string NumeroCuenta
        decimal Saldo
    }
    
    CREDITO {
        int CreditoId PK
        int ClienteId FK
        decimal MontoAprobado
        string Estado
    }
```

## Dominios de Tablas

### Clientes
- `Clientes`: Información personal y demográfica.
- `Direcciones`: Múltiples direcciones por cliente.

### Cuentas y Ahorros
- `Cuentas`: Cuentas corrientes y de ahorro.
- `TransaccionesCuenta`: Movimientos (débitos/créditos) sobre las cuentas.

### Créditos y Cartera
- `Creditos`: Cabecera del préstamo.
- `Cuotas`: Tabla de amortización generada al desembolsar.
- `Pagos`: Registro de recaudación aplicada a cuotas.
- `Diferimientos`: Registro de arreglos de pago/refinanciamiento.

### Auditoría
- `LogTransacciones`: Traza de operaciones sensibles.
- `AuditoriaDatos`: Tabla genérica para cambios (Trigger-based).

## Patrón de Campos de Auditoría
Todas las tablas maestras y transaccionales implementan estos campos:
- `FechaCreacion` (DATETIME)
- `UsuarioCreacion` (VARCHAR)
- `FechaModificacion` (DATETIME, null)
- `UsuarioModificacion` (VARCHAR, null)
- `EstadoRegistro` (CHAR(1), 'A'=Activo, 'I'=Inactivo)

## Máquina de Estados: Crédito
```mermaid
stateDiagram-v2
    [*] --> Solicitado
    Solicitado --> Aprobado
    Solicitado --> Rechazado
    Aprobado --> Desembolsado
    Desembolsado --> EnMora
    Desembolsado --> Cancelado
    EnMora --> Cancelado
```

# Backlog de Ejercicios: Plan de 4 Semanas

Este plan estructura el aprendizaje del ecosistema BankLab.

## Semana 1: Fundamentos
- **Día 1: Explorar estructura del proyecto, compilar Ejemplo.Comun**
  - **Meta:** Entender la solución base.
  - **Ref:** `02-diagrama-soluciones-dependencias.md`
- **Día 2: Crear la base de datos, ejecutar scripts, explorar tablas en SSMS**
  - **Meta:** Tener el ambiente SQL operativo.
  - **Ref:** `tools\setup-database.ps1`, `04-modelo-datos.md`
- **Día 3: Compilar Ejemplo.Componentes, trazar el flujo BLL→DAS→DAL→DAT**
  - **Meta:** Entender separación de capas.
  - **Ref:** `03-responsabilidades-capas.md`
- **Día 4: Ejecutar stored procedures manualmente, analizar planes de ejecución**
  - **Meta:** Familiaridad con T-SQL de la app.
  - **Ref:** `05-catalogo-stored-procedures.md`
- **Día 5: Compilar y ejecutar WCF Host, probar servicios con cliente de consola**
  - **Meta:** Conexión C# -> WCF local operativa.
  - **Ref:** `tools\run-wcf-host.ps1`, `07-flujo-wcf-nettcp.md`

## Semana 2: Interfaces y Flujos
- **Día 1: Ejecutar App.exe, buscar clientes, trazar la llamada WCF**
  - **Meta:** Flujo Completo UI a BD.
  - **Ref:** `06-flujo-gui-a-sql.md`
- **Día 2: Consultar créditos y cuotas, entender el flujo de datos**
  - **Meta:** Entender consultas maestras/detalle complejas.
- **Día 3: Registrar un pago ficticio, verificar la transacción en BD**
  - **Meta:** Entender el control transaccional desde DAS.
- **Día 4: Ejecutar portales web, comparar con GUI**
  - **Meta:** Ver reuso de BLL en Web MVC 5.
- **Día 5: Ejecutar la API moderna, comparar con WCF**
  - **Meta:** Explorar la modernización.
  - **Ref:** `tools\run-api.ps1`, `08-flujo-api-net8.md`

## Semana 3: Mantenimiento
- **Día 1: Agregar un campo nuevo a ClienteDTO y propagarlo por las capas**
  - **Meta:** Ejercicio práctico de cascada: Tabla -> SP -> DAT -> DAL -> DTO -> WCF -> App.
- **Día 2: Crear un nuevo stored procedure y exponerlo en WCF**
  - **Meta:** Exponer una nueva función de negocio simple.
- **Día 3: Agregar un endpoint a la API moderna**
  - **Meta:** Contrastar la simplicidad de Controllers .NET 8 frente a WCF.
- **Día 4: Cambiar configuración sin modificar código**
  - **Meta:** Dominar los config files.
  - **Ref:** `09-configuracion-ambientes.md`
- **Día 5: Diagnosticar y corregir errores simulados**
  - **Meta:** Resiliencia (Provocar una caída en base de datos y rastrear el mensaje).
  - **Ref:** `11-diagnostico-errores.md`

## Semana 4: Avanzado
- **Día 1: Analizar y optimizar una consulta lenta**
  - **Meta:** Revisar índices y OFFSET/FETCH.
- **Día 2: Implementar un nuevo caso de uso end-to-end**
  - **Meta:** Implementar "Diferimiento de Cuota" desde cero.
- **Día 3: Crear y ejecutar pruebas automatizadas**
  - **Meta:** Ejecutar los Unit Tests de la solución.
  - **Ref:** `tools\run-all-tests.ps1`
- **Día 4: Documentar un flujo con diagramas**
  - **Meta:** Uso de Mermaid para diagramas de secuencia propios.
- **Día 5: Revisión general y plan de profundización**
  - **Meta:** Retrospectiva arquitectónica (ver ADRs).
  - **Ref:** `15-decisiones-arquitectonicas.md`

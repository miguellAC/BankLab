# Responsabilidades de las Capas

El sistema legacy de BankLab sigue un estricto patrón de arquitectura de n-capas.

## Capas de Presentación y Servicios

### GUI (Graphical User Interface)
- **Responsabilidad:** Manejar la interacción del usuario y mostrar datos.
- **Regla estricta:** NUNCA debe acceder directamente a la base de datos o contener lógica de negocio compleja. Sólo realiza validaciones visuales.

### USC (User Controls)
- **Responsabilidad:** Agrupar lógica y UI reutilizable (ej. un control de búsqueda de cliente).
- **Relación:** Usados en los formularios GUI.

### Agentes
- **Responsabilidad:** Encapsular la complejidad de la llamada a WCF. Construye canales (ChannelFactory), serializa parámetros, maneja excepciones de red.

### WCF Services
- **Responsabilidad:** Exponer la lógica de negocio a través de la red (NetTcp).
- **Acción:** Recibe la petición y delega inmediatamente a la capa BLL.

## Capas de Negocio y Datos (Componentes)

### BLL (Business Logic Layer)
- **Responsabilidad:** Punto de entrada de las reglas de negocio. Valida si la operación puede realizarse conceptualmente.
- **Ejemplo:** Validar que el cliente sea mayor de edad antes de solicitar un crédito.

### DAS (Data Access Services)
- **Responsabilidad:** Control de transacciones (límites transaccionales).
- **Acción:** Abre la conexión a la base de datos, inicia `SqlTransaction`, llama a DAL y decide si hace `Commit` o `Rollback`. 

### DAL (Data Access Logic)
- **Responsabilidad:** Preparar los datos antes de persistir o después de leer. Convierte conceptos de negocio en conceptos de datos.
- **Acción:** Puede realizar múltiples llamadas a DAT dentro de una misma transacción proporcionada por DAS.

### DAT (Data Access Technology)
- **Responsabilidad:** Única capa que ejecuta comandos SQL (ADO.NET).
- **Acción:** Configura `SqlCommand`, asigna parámetros, llama a `Stored Procedures` y mapea `SqlDataReader` a DTOs.

## Ejemplo de Llamada en Cascada
```csharp
// 1. Agente llama al WCF
public List<ClienteDTO> BuscarClientes(string criterio) { return _canal.BuscarClientes(criterio); }

// 2. WCF llama a BLL
public List<ClienteDTO> BuscarClientes(string criterio) { return ClientesBLL.BuscarClientes(criterio); }

// 3. BLL valida y llama a DAS
public static List<ClienteDTO> BuscarClientes(string criterio) { 
    if(string.IsNullOrEmpty(criterio)) throw new Exception("...");
    return ClientesDAS.BuscarClientes(criterio); 
}

// 4. DAS llama a DAL (sin transaccion por ser lectura)
public static List<ClienteDTO> BuscarClientes(string criterio) { return ClientesDAL.BuscarClientes(criterio); }

// 5. DAL delega a DAT
public static List<ClienteDTO> BuscarClientes(string criterio) { return ClientesDAT.BuscarClientes(criterio); }

// 6. DAT ejecuta y mapea
public static List<ClienteDTO> BuscarClientes(string criterio) {
    // using SqlConnection... SqlCommand("sp_BuscarClientes")...
    // return list;
}
```

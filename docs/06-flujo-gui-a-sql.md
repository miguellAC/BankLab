# Flujo: Desde la GUI hasta SQL Server

Este documento detalla el paso a paso de una operación típica (Buscar Cliente) atravesando todas las capas del sistema legacy.

## Paso a Paso

1. **Interacción del Usuario:** 
   El usuario teclea "Juan" en `FrmBuscarCliente` y presiona el botón Buscar.
   ```csharp
   // FrmBuscarCliente.cs
   private void btnBuscar_Click(object sender, EventArgs e) {
       var clientes = _agente.BuscarClientes(txtCriterio.Text);
       dgvResultados.DataSource = clientes;
   }
   ```

2. **Llamada del Agente:**
   El Agente instancia el proxy de WCF y realiza la llamada a través de NetTcp.
   ```csharp
   // ClientesAgente.cs
   public List<ClienteDTO> BuscarClientes(string criterio) {
       using (var proxy = new ClienteProxy()) {
           return proxy.Channel.BuscarClientes(criterio);
       }
   }
   ```

3. **Recepción en el Servicio WCF:**
   El host de WCF recibe el mensaje binario, lo deserializa y lo pasa a la implementación del servicio.
   ```csharp
   // ClientesService.cs
   public List<ClienteDTO> BuscarClientes(string criterio) {
       return ClientesBLL.BuscarClientes(criterio);
   }
   ```

4. **Reglas de Negocio (BLL):**
   ```csharp
   // ClientesBLL.cs
   public static List<ClienteDTO> BuscarClientes(string criterio) {
       if (string.IsNullOrWhiteSpace(criterio)) throw new ExcepcionNegocio("Criterio inválido");
       return ClientesDAS.BuscarClientes(criterio);
   }
   ```

5. **Límites Transaccionales (DAS):**
   Como es una consulta (solo lectura), DAS no abre transacción, simplemente delega.
   ```csharp
   // ClientesDAS.cs
   public static List<ClienteDTO> BuscarClientes(string criterio) {
       return ClientesDAL.BuscarClientes(criterio);
   }
   ```

6. **Lógica de Acceso a Datos (DAL):**
   ```csharp
   // ClientesDAL.cs
   public static List<ClienteDTO> BuscarClientes(string criterio) {
       return ClientesDAT.BuscarClientes(criterio);
   }
   ```

7. **Ejecución en ADO.NET (DAT):**
   ```csharp
   // ClientesDAT.cs
   public static List<ClienteDTO> BuscarClientes(string criterio) {
       var lista = new List<ClienteDTO>();
       using (var conn = new SqlConnection(Configuracion.CadenaConexion)) {
           using (var cmd = new SqlCommand("sp_Clientes_Buscar", conn)) {
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Criterio", criterio);
               conn.Open();
               using (var reader = cmd.ExecuteReader()) {
                   while(reader.Read()) {
                       lista.Add(MapearDesdeLector(reader));
                   }
               }
           }
       }
       return lista;
   }
   ```

8. **Retorno:**
   La lista de `ClienteDTO` viaja de regreso por DAT → DAL → DAS → BLL → WCF. El agente la recibe, retorna a la UI, y el DataGridView la muestra.

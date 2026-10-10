# Arquitectura Moderna: API en .NET 8

BankLab incluye un proyecto de Web API en .NET 8 para demostrar la modernización de los sistemas bancarios.

## Diferencias Clave (Moderno vs Legacy)

| Característica | Legacy WCF | Moderno .NET 8 |
|----------------|------------|----------------|
| Protocolo | NetTcp (Binario) | HTTP (REST / JSON) |
| Alojamiento | Consola / IIS + WAS | Kestrel / IIS |
| Dependencias | Acoplamiento fuerte / estático | Inyección de Dependencias nativa |
| Configuración | App.config (XML) | appsettings.json |
| Arquitectura | 5 capas (GUI, BLL, DAS, DAL, DAT) | 3 capas (Controller, Service, Repository) |

## Componentes del Flujo

1. **Pipeline de ASP.NET Core:** Middlewares para el manejo global de errores, CORS, Autenticación y Swagger.
2. **Controladores (Controllers):** Reciben las peticiones HTTP (GET, POST), validan el modelo (Data Annotations) y retornan `IActionResult`.
3. **Servicios (Application Services):** Equivalente a la antigua capa BLL. Contienen la lógica de negocio y son inyectados vía constructores (Scoped).
4. **Repositorios:** Equivalente a las capas DAS+DAL+DAT agrupadas. Siguen usando ADO.NET (por directiva arquitectónica para control preciso de los Stored Procedures) en lugar de Entity Framework.

## Configuración y Opciones
En lugar del `ConfigurationManager` estático, se utiliza el patrón Options.
```json
// appsettings.json
{
  "ConnectionStrings": {
    "BankLabDb": "Server=localhost;Database=BankLabDB;..."
  },
  "ApiSettings": {
    "PageSize": 50
  }
}
```

## Ejemplo de Inyección de Dependencias
```csharp
// Program.cs
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
```

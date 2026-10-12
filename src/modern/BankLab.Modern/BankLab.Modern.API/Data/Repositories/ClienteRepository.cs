using BankLab.Modern.API.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BankLab.Modern.API.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly string _connectionString;

    public ClienteRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("BankLabDB") ?? throw new InvalidOperationException("Connection string not found.");
    }

    public async Task<ClienteResponse?> GetByIdAsync(int clienteId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(@"
            SELECT ClienteId, 
                   PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido,
                   TipoIdentificacionId, NumeroIdentificacion, Email, SucursalId, Estado, FechaRegistro 
            FROM Clientes 
            WHERE ClienteId = @Id", connection);
        command.Parameters.AddWithValue("@Id", clienteId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapToResponse(reader);
        }
        return null;
    }

    public async Task<ClienteResponse?> GetByIdentificacionAsync(int tipoId, string numero)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(@"
            SELECT ClienteId, 
                   PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido,
                   TipoIdentificacionId, NumeroIdentificacion, Email, SucursalId, Estado, FechaRegistro 
            FROM Clientes 
            WHERE TipoIdentificacionId = @Tipo AND NumeroIdentificacion = @Numero", connection);
        command.Parameters.AddWithValue("@Tipo", tipoId);
        command.Parameters.AddWithValue("@Numero", numero);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapToResponse(reader);
        }
        return null;
    }

    public async Task<PaginatedResponse<ClienteResponse>> SearchAsync(string? nombres, string? apellidos, string? identificacion, string? estado, int? sucursalId, int page, int pageSize)
    {
        var items = new List<ClienteResponse>();
        var totalRecords = 0;

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(@"
            SELECT COUNT(*) OVER() as TotalRecords, 
                   ClienteId, 
                   PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido,
                   TipoIdentificacionId, NumeroIdentificacion, Email, SucursalId, Estado, FechaRegistro
            FROM Clientes
            WHERE (@Nombres IS NULL OR (PrimerNombre + ' ' + ISNULL(SegundoNombre, '')) LIKE '%' + @Nombres + '%')
              AND (@Apellidos IS NULL OR (PrimerApellido + ' ' + ISNULL(SegundoApellido, '')) LIKE '%' + @Apellidos + '%')
              AND (@Identificacion IS NULL OR NumeroIdentificacion = @Identificacion)
              AND (@Estado IS NULL OR Estado = @Estado)
              AND (@SucursalId IS NULL OR SucursalId = @SucursalId)
            ORDER BY ClienteId
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", connection);

        command.Parameters.AddWithValue("@Nombres", (object?)nombres ?? DBNull.Value);
        command.Parameters.AddWithValue("@Apellidos", (object?)apellidos ?? DBNull.Value);
        command.Parameters.AddWithValue("@Identificacion", (object?)identificacion ?? DBNull.Value);
        command.Parameters.AddWithValue("@Estado", (object?)estado ?? DBNull.Value);
        command.Parameters.AddWithValue("@SucursalId", (object?)sucursalId ?? DBNull.Value);
        command.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);
        command.Parameters.AddWithValue("@PageSize", pageSize);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            if (totalRecords == 0)
            {
                totalRecords = reader.GetInt32(reader.GetOrdinal("TotalRecords"));
            }
            items.Add(MapToResponse(reader));
        }

        return new PaginatedResponse<ClienteResponse>
        {
            Items = items,
            PageNumber = page,
            PageSize = pageSize,
            TotalRecords = totalRecords
        };
    }

    private static ClienteResponse MapToResponse(SqlDataReader reader)
    {
        var pNombre = reader["PrimerNombre"].ToString() ?? "";
        var sNombre = reader["SegundoNombre"] != DBNull.Value ? reader["SegundoNombre"].ToString() : null;
        var pApellido = reader["PrimerApellido"].ToString() ?? "";
        var sApellido = reader["SegundoApellido"] != DBNull.Value ? reader["SegundoApellido"].ToString() : null;

        var nombreCompleto = string.Join(" ", new[] { pNombre, sNombre, pApellido, sApellido }.Where(s => !string.IsNullOrWhiteSpace(s))).Trim();

        return new ClienteResponse
        {
            ClienteId = Convert.ToInt32(reader["ClienteId"]),
            NombreCompleto = nombreCompleto,
            TipoIdentificacion = Convert.ToInt32(reader["TipoIdentificacionId"]),
            NumeroIdentificacion = reader["NumeroIdentificacion"].ToString() ?? "",
            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
            Sucursal = Convert.ToInt32(reader["SucursalId"]),
            Estado = reader["Estado"].ToString() ?? "",
            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
        };
    }
}

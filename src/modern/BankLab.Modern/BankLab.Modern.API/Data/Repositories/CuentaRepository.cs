using BankLab.Modern.API.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BankLab.Modern.API.Data.Repositories;

public class CuentaRepository : ICuentaRepository
{
    private readonly string _connectionString;

    public CuentaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("BankLabDB") ?? throw new InvalidOperationException("Connection string not found.");
    }

    public async Task<IEnumerable<CuentaResponse>> GetCuentasClienteAsync(int clienteId)
    {
        var result = new List<CuentaResponse>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(@"
            SELECT c.CuentaId, c.NumeroCuenta, c.TipoCuenta, c.Saldo, c.SaldoDisponible, c.Estado, c.FechaApertura,
                   cl.Nombres + ' ' + cl.Apellidos as NombreCliente
            FROM Cuentas c
            JOIN Clientes cl ON c.ClienteId = cl.ClienteId
            WHERE c.ClienteId = @ClienteId", connection);
            
        command.Parameters.AddWithValue("@ClienteId", clienteId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(new CuentaResponse
            {
                CuentaId = Convert.ToInt32(reader["CuentaId"]),
                NumeroCuenta = reader["NumeroCuenta"].ToString() ?? "",
                TipoCuenta = reader["TipoCuenta"].ToString() ?? "",
                Saldo = Convert.ToDecimal(reader["Saldo"]),
                SaldoDisponible = Convert.ToDecimal(reader["SaldoDisponible"]),
                Estado = reader["Estado"].ToString() ?? "",
                FechaApertura = Convert.ToDateTime(reader["FechaApertura"]),
                NombreCliente = reader["NombreCliente"].ToString() ?? ""
            });
        }
        return result;
    }

    public async Task<PaginatedResponse<object>> GetMovimientosAsync(int cuentaId, DateTime? desde, DateTime? hasta, int page, int pageSize)
    {
        var items = new List<object>();
        var totalRecords = 0;

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(@"
            SELECT COUNT(*) OVER() as TotalRecords, MovimientoId, FechaOperacion, Tipo, Valor, SaldoResultante, Descripcion
            FROM Movimientos
            WHERE CuentaId = @CuentaId
              AND (@Desde IS NULL OR FechaOperacion >= @Desde)
              AND (@Hasta IS NULL OR FechaOperacion <= @Hasta)
            ORDER BY MovimientoId DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", connection);

        command.Parameters.AddWithValue("@CuentaId", cuentaId);
        command.Parameters.AddWithValue("@Desde", (object?)desde ?? DBNull.Value);
        command.Parameters.AddWithValue("@Hasta", (object?)hasta ?? DBNull.Value);
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
            items.Add(new
            {
                MovimientoId = Convert.ToInt32(reader["MovimientoId"]),
                FechaOperacion = Convert.ToDateTime(reader["FechaOperacion"]),
                Tipo = reader["Tipo"].ToString(),
                Valor = Convert.ToDecimal(reader["Valor"]),
                SaldoResultante = Convert.ToDecimal(reader["SaldoResultante"]),
                Descripcion = reader["Descripcion"].ToString()
            });
        }

        return new PaginatedResponse<object>
        {
            Items = items,
            PageNumber = page,
            PageSize = pageSize,
            TotalRecords = totalRecords
        };
    }

    public async Task<decimal> GetSaldoAsync(int cuentaId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("SELECT Saldo FROM Cuentas WHERE CuentaId = @CuentaId", connection);
        command.Parameters.AddWithValue("@CuentaId", cuentaId);

        await connection.OpenAsync();
        var result = await command.ExecuteScalarAsync();
        return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
    }
}

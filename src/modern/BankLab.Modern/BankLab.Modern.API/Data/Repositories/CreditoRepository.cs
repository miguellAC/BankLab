using BankLab.Modern.API.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BankLab.Modern.API.Data.Repositories;

public class CreditoRepository : ICreditoRepository
{
    private readonly string _connectionString;

    public CreditoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("BankLabDB") ?? throw new InvalidOperationException("Connection string not found.");
    }

    public async Task<CreditoResponse?> GetOperacionAsync(int? opId, string? numOp)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(@"
            SELECT o.OperacionId, o.NumeroOperacion, o.Producto, o.MontoDesembolsado, o.TasaAnual, o.PlazoMeses, o.Estado, 
                   c.Nombres + ' ' + c.Apellidos as NombreCliente,
                   0 as SaldoVigente, 0 as SaldoVencido, 0 as SaldoTotal, 0 as DiasEnMora
            FROM OperacionesCredito o
            JOIN Clientes c ON o.ClienteId = c.ClienteId
            WHERE (@OpId IS NULL OR o.OperacionId = @OpId)
              AND (@NumOp IS NULL OR o.NumeroOperacion = @NumOp)", connection);

        command.Parameters.AddWithValue("@OpId", (object?)opId ?? DBNull.Value);
        command.Parameters.AddWithValue("@NumOp", (object?)numOp ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new CreditoResponse
            {
                OperacionId = Convert.ToInt32(reader["OperacionId"]),
                NumeroOperacion = reader["NumeroOperacion"].ToString() ?? "",
                NombreCliente = reader["NombreCliente"].ToString() ?? "",
                Producto = reader["Producto"].ToString() ?? "",
                MontoDesembolsado = Convert.ToDecimal(reader["MontoDesembolsado"]),
                TasaAnual = Convert.ToDecimal(reader["TasaAnual"]),
                PlazoMeses = Convert.ToInt32(reader["PlazoMeses"]),
                Estado = reader["Estado"].ToString() ?? "",
                SaldoVigente = 0, // En un sistema real se calcularía o consultaría de otra tabla
                SaldoVencido = 0,
                SaldoTotal = 0,
                DiasEnMora = 0
            };
        }
        return null;
    }

    public async Task<object?> GetSaldosAsync(int operacionId)
    {
        // En un caso real llamaría a un SP. Retornamos datos de prueba.
        await Task.CompletedTask;
        return new { OperacionId = operacionId, SaldoCapital = 1000m, SaldoInteres = 50m, SaldoMora = 0m, TotalPagar = 1050m };
    }

    public record CuotaItem(int NumeroCuota, decimal Valor, DateTime FechaVencimiento, string Estado);

    public async Task<IEnumerable<object>> GetCuotasAsync(int operacionId, bool soloVencidas, bool soloPendientes)
    {
        await Task.CompletedTask;
        var cuotas = new List<CuotaItem>
        {
            new CuotaItem(1, 500m, DateTime.Now.AddDays(-10), "VENCIDA"),
            new CuotaItem(2, 500m, DateTime.Now.AddDays(20), "PENDIENTE")
        };

        return cuotas
            .Where(c => (!soloVencidas || c.Estado == "VENCIDA") && (!soloPendientes || c.Estado == "PENDIENTE" || c.Estado == "VENCIDA"))
            .Cast<object>();
    }
}

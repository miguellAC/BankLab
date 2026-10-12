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
            SELECT o.OperacionId, o.NumeroOperacion, p.Nombre as Producto, o.MontoDesembolsado, o.TasaAnual, o.PlazoMeses, o.Estado, 
                   LTRIM(RTRIM(c.PrimerNombre + ' ' + ISNULL(c.SegundoNombre + ' ', '') + c.PrimerApellido + ' ' + ISNULL(c.SegundoApellido, ''))) as NombreCliente,
                   ISNULL(s.SaldoCapitalVigente, 0) as SaldoVigente, 
                   ISNULL(s.SaldoCapitalVencido, 0) as SaldoVencido, 
                   ISNULL(s.SaldoTotal, 0) as SaldoTotal, 
                   ISNULL(s.DiasEnMora, 0) as DiasEnMora
            FROM OperacionesCredito o
            JOIN Clientes c ON o.ClienteId = c.ClienteId
            JOIN ProductosCredito p ON o.ProductoCreditoId = p.ProductoCreditoId
            LEFT JOIN SaldosOperacion s ON o.OperacionId = s.OperacionId
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
                SaldoVigente = Convert.ToDecimal(reader["SaldoVigente"]),
                SaldoVencido = Convert.ToDecimal(reader["SaldoVencido"]),
                SaldoTotal = Convert.ToDecimal(reader["SaldoTotal"]),
                DiasEnMora = Convert.ToInt32(reader["DiasEnMora"])
            };
        }
        return null;
    }

    public async Task<object?> GetSaldosAsync(int operacionId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(@"
            SELECT OperacionId, SaldoCapitalVigente, SaldoCapitalVencido, SaldoInteresVigente, SaldoInteresVencido, SaldoTotal, DiasEnMora
            FROM SaldosOperacion
            WHERE OperacionId = @OperacionId", connection);
        command.Parameters.AddWithValue("@OperacionId", operacionId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                OperacionId = Convert.ToInt32(reader["OperacionId"]),
                SaldoCapital = Convert.ToDecimal(reader["SaldoCapitalVigente"]),
                SaldoVencido = Convert.ToDecimal(reader["SaldoCapitalVencido"]),
                SaldoInteres = Convert.ToDecimal(reader["SaldoInteresVigente"]),
                SaldoInteresVencido = Convert.ToDecimal(reader["SaldoInteresVencido"]),
                TotalPagar = Convert.ToDecimal(reader["SaldoTotal"]),
                DiasMora = Convert.ToInt32(reader["DiasEnMora"])
            };
        }
        return null;
    }

    public record CuotaItem(int NumeroCuota, decimal Valor, DateTime FechaVencimiento, string Estado);

    public async Task<IEnumerable<object>> GetCuotasAsync(int operacionId, bool soloVencidas, bool soloPendientes)
    {
        var cuotas = new List<CuotaItem>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(@"
            SELECT NumeroCuota, ValorCuota, FechaVencimiento, Estado
            FROM Cuotas
            WHERE OperacionId = @OperacionId
              AND (@SoloVencidas = 0 OR Estado = 'VENCIDA')
              AND (@SoloPendientes = 0 OR Estado IN ('PENDIENTE', 'VENCIDA'))
            ORDER BY NumeroCuota", connection);

        command.Parameters.AddWithValue("@OperacionId", operacionId);
        command.Parameters.AddWithValue("@SoloVencidas", soloVencidas ? 1 : 0);
        command.Parameters.AddWithValue("@SoloPendientes", soloPendientes ? 1 : 0);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            cuotas.Add(new CuotaItem(
                Convert.ToInt32(reader["NumeroCuota"]),
                Convert.ToDecimal(reader["ValorCuota"]),
                Convert.ToDateTime(reader["FechaVencimiento"]),
                reader["Estado"].ToString() ?? ""
            ));
        }

        return cuotas.Cast<object>();
    }
}

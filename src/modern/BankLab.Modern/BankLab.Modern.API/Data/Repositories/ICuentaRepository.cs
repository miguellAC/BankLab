using BankLab.Modern.API.DTOs;
namespace BankLab.Modern.API.Data.Repositories;

public interface ICuentaRepository
{
    Task<IEnumerable<CuentaResponse>> GetCuentasClienteAsync(int clienteId);
    Task<PaginatedResponse<object>> GetMovimientosAsync(int cuentaId, DateTime? desde, DateTime? hasta, int page, int pageSize);
    Task<decimal> GetSaldoAsync(int cuentaId);
}

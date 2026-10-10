using BankLab.Modern.API.DTOs;
namespace BankLab.Modern.API.Data.Repositories;

public interface ICreditoRepository
{
    Task<CreditoResponse?> GetOperacionAsync(int? opId, string? numOp);
    Task<object?> GetSaldosAsync(int operacionId);
    Task<IEnumerable<object>> GetCuotasAsync(int operacionId, bool soloVencidas, bool soloPendientes);
}

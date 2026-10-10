using BankLab.Modern.API.DTOs;
namespace BankLab.Modern.API.Services;

public interface ICreditosService
{
    Task<CreditoResponse?> GetOperacionAsync(int? opId, string? numOp);
    Task<object?> GetSaldosAsync(int operacionId);
    Task<IEnumerable<object>> GetCuotasAsync(int operacionId, bool soloVencidas, bool soloPendientes);
}

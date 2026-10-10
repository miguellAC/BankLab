using BankLab.Modern.API.Data.Repositories;
using BankLab.Modern.API.DTOs;

namespace BankLab.Modern.API.Services;

public class CreditosService : ICreditosService
{
    private readonly ICreditoRepository _repository;

    public CreditosService(ICreditoRepository repository)
    {
        _repository = repository;
    }

    public Task<CreditoResponse?> GetOperacionAsync(int? opId, string? numOp)
    {
        return _repository.GetOperacionAsync(opId, numOp);
    }

    public Task<object?> GetSaldosAsync(int operacionId)
    {
        return _repository.GetSaldosAsync(operacionId);
    }

    public Task<IEnumerable<object>> GetCuotasAsync(int operacionId, bool soloVencidas, bool soloPendientes)
    {
        return _repository.GetCuotasAsync(operacionId, soloVencidas, soloPendientes);
    }
}

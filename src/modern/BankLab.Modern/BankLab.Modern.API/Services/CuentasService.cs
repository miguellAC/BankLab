using BankLab.Modern.API.Data.Repositories;
using BankLab.Modern.API.DTOs;
using BankLab.Modern.API.Configuration;
using Microsoft.Extensions.Options;

namespace BankLab.Modern.API.Services;

public class CuentasService : ICuentasService
{
    private readonly ICuentaRepository _repository;
    private readonly BankLabOptions _options;

    public CuentasService(ICuentaRepository repository, IOptions<BankLabOptions> options)
    {
        _repository = repository;
        _options = options.Value;
    }

    public Task<IEnumerable<CuentaResponse>> GetCuentasClienteAsync(int clienteId)
    {
        return _repository.GetCuentasClienteAsync(clienteId);
    }

    public Task<PaginatedResponse<object>> GetMovimientosAsync(int cuentaId, DateTime? desde, DateTime? hasta, int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize <= 0) pageSize = _options.DefaultPageSize;
        if (pageSize > _options.MaxPageSize) pageSize = _options.MaxPageSize;

        return _repository.GetMovimientosAsync(cuentaId, desde, hasta, page, pageSize);
    }

    public Task<decimal> GetSaldoAsync(int cuentaId)
    {
        return _repository.GetSaldoAsync(cuentaId);
    }
}

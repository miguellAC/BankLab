using BankLab.Modern.API.Data.Repositories;
using BankLab.Modern.API.DTOs;
using BankLab.Modern.API.Configuration;
using Microsoft.Extensions.Options;

namespace BankLab.Modern.API.Services;

public class ClientesService : IClientesService
{
    private readonly IClienteRepository _repository;
    private readonly BankLabOptions _options;

    public ClientesService(IClienteRepository repository, IOptions<BankLabOptions> options)
    {
        _repository = repository;
        _options = options.Value;
    }

    public Task<ClienteResponse?> GetByIdAsync(int clienteId)
    {
        return _repository.GetByIdAsync(clienteId);
    }

    public Task<ClienteResponse?> GetByIdentificacionAsync(int tipoId, string numero)
    {
        return _repository.GetByIdentificacionAsync(tipoId, numero);
    }

    public Task<PaginatedResponse<ClienteResponse>> SearchAsync(string? nombres, string? apellidos, string? identificacion, string? estado, int? sucursalId, int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize <= 0) pageSize = _options.DefaultPageSize;
        if (pageSize > _options.MaxPageSize) pageSize = _options.MaxPageSize;

        return _repository.SearchAsync(nombres, apellidos, identificacion, estado, sucursalId, page, pageSize);
    }
}

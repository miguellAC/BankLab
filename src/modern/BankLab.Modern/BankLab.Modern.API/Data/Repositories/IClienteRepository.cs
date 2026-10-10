namespace BankLab.Modern.API.Data.Repositories;
using BankLab.Modern.API.DTOs;

public interface IClienteRepository
{
    Task<ClienteResponse?> GetByIdAsync(int clienteId);
    Task<ClienteResponse?> GetByIdentificacionAsync(int tipoId, string numero);
    Task<PaginatedResponse<ClienteResponse>> SearchAsync(string? nombres, string? apellidos, string? identificacion, string? estado, int? sucursalId, int page, int pageSize);
}

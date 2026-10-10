using BankLab.Modern.API.DTOs;
namespace BankLab.Modern.API.Services;

public interface IClientesService
{
    Task<ClienteResponse?> GetByIdAsync(int clienteId);
    Task<ClienteResponse?> GetByIdentificacionAsync(int tipoId, string numero);
    Task<PaginatedResponse<ClienteResponse>> SearchAsync(string? nombres, string? apellidos, string? identificacion, string? estado, int? sucursalId, int page, int pageSize);
}

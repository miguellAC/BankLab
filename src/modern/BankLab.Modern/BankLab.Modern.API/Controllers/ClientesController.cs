using Microsoft.AspNetCore.Mvc;
using BankLab.Modern.API.DTOs;
using BankLab.Modern.API.Services;

namespace BankLab.Modern.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ClientesController : ControllerBase
{
    private readonly IClientesService _service;

    public ClientesController(IClientesService service) => _service = service;

    /// <summary>Buscar clientes con filtros y paginación</summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<ClienteResponse>), 200)]
    public async Task<IActionResult> Search(
        [FromQuery] string? nombres, [FromQuery] string? apellidos,
        [FromQuery] string? identificacion, [FromQuery] string? estado,
        [FromQuery] int? sucursalId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _service.SearchAsync(nombres, apellidos, identificacion, estado, sucursalId, page, pageSize);
        return Ok(result);
    }

    /// <summary>Obtener cliente por ID</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClienteResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>Buscar cliente por identificación</summary>
    [HttpGet("identificacion/{tipoId:int}/{numero}")]
    [ProducesResponseType(typeof(ClienteResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByIdentificacion(int tipoId, string numero)
    {
        var result = await _service.GetByIdentificacionAsync(tipoId, numero);
        return result is null ? NotFound() : Ok(result);
    }
}

using Microsoft.AspNetCore.Mvc;
using BankLab.Modern.API.DTOs;
using BankLab.Modern.API.Services;

namespace BankLab.Modern.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CuentasController : ControllerBase
{
    private readonly ICuentasService _service;

    public CuentasController(ICuentasService service) => _service = service;

    /// <summary>Obtener cuentas de un cliente</summary>
    [HttpGet("cliente/{clienteId:int}")]
    [ProducesResponseType(typeof(IEnumerable<CuentaResponse>), 200)]
    public async Task<IActionResult> GetCuentasCliente(int clienteId)
    {
        var result = await _service.GetCuentasClienteAsync(clienteId);
        return Ok(result);
    }

    /// <summary>Obtener movimientos de una cuenta</summary>
    [HttpGet("{id:int}/movimientos")]
    [ProducesResponseType(typeof(PaginatedResponse<object>), 200)]
    public async Task<IActionResult> GetMovimientos(
        int id, [FromQuery] DateTime? desde, [FromQuery] DateTime? hasta,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _service.GetMovimientosAsync(id, desde, hasta, page, pageSize);
        return Ok(result);
    }

    /// <summary>Obtener saldo de una cuenta</summary>
    [HttpGet("{id:int}/saldo")]
    [ProducesResponseType(typeof(decimal), 200)]
    public async Task<IActionResult> GetSaldo(int id)
    {
        var result = await _service.GetSaldoAsync(id);
        return Ok(result);
    }
}

using Microsoft.AspNetCore.Mvc;
using BankLab.Modern.API.DTOs;
using BankLab.Modern.API.Services;

namespace BankLab.Modern.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CreditosController : ControllerBase
{
    private readonly ICreditosService _service;

    public CreditosController(ICreditosService service) => _service = service;

    /// <summary>Detalle de operación por ID</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CreditoResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetOperacionAsync(id, null);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>Detalle de operación por número</summary>
    [HttpGet("operacion/{numero}")]
    [ProducesResponseType(typeof(CreditoResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByNumero(string numero)
    {
        var result = await _service.GetOperacionAsync(null, numero);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>Obtener saldos de una operación</summary>
    [HttpGet("{id:int}/saldos")]
    [ProducesResponseType(typeof(object), 200)]
    public async Task<IActionResult> GetSaldos(int id)
    {
        var result = await _service.GetSaldosAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>Obtener cuotas de una operación</summary>
    [HttpGet("{id:int}/cuotas")]
    [ProducesResponseType(typeof(IEnumerable<object>), 200)]
    public async Task<IActionResult> GetCuotas(int id, [FromQuery] bool soloVencidas = false, [FromQuery] bool soloPendientes = false)
    {
        var result = await _service.GetCuotasAsync(id, soloVencidas, soloPendientes);
        return Ok(result);
    }
}

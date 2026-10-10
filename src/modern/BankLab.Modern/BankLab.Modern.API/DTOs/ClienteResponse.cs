namespace BankLab.Modern.API.DTOs;

public class ClienteResponse
{
    public int ClienteId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public int TipoIdentificacion { get; set; }
    public string NumeroIdentificacion { get; set; } = string.Empty;
    public string? Email { get; set; }
    public int Sucursal { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
}

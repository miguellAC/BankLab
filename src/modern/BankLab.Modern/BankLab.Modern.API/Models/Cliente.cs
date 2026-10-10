namespace BankLab.Modern.API.Models;

public class Cliente
{
    public int ClienteId { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public int TipoIdentificacion { get; set; }
    public string NumeroIdentificacion { get; set; } = string.Empty;
    public string? Email { get; set; }
    public int SucursalId { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
}

namespace BankLab.Modern.API.DTOs;

public class CreditoResponse
{
    public int OperacionId { get; set; }
    public string NumeroOperacion { get; set; } = string.Empty;
    public string NombreCliente { get; set; } = string.Empty;
    public string Producto { get; set; } = string.Empty;
    public decimal MontoDesembolsado { get; set; }
    public decimal TasaAnual { get; set; }
    public int PlazoMeses { get; set; }
    public string Estado { get; set; } = string.Empty;
    public decimal SaldoVigente { get; set; }
    public decimal SaldoVencido { get; set; }
    public decimal SaldoTotal { get; set; }
    public int DiasEnMora { get; set; }
}

namespace BankLab.Modern.API.Models;

public class OperacionCredito
{
    public int OperacionId { get; set; }
    public string NumeroOperacion { get; set; } = string.Empty;
    public int ClienteId { get; set; }
    public string Producto { get; set; } = string.Empty;
    public decimal MontoDesembolsado { get; set; }
    public decimal TasaAnual { get; set; }
    public int PlazoMeses { get; set; }
    public string Estado { get; set; } = string.Empty;
}

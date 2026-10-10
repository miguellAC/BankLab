namespace BankLab.Modern.API.DTOs;

public class CuentaResponse
{
    public int CuentaId { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public string NombreCliente { get; set; } = string.Empty;
    public string TipoCuenta { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public decimal SaldoDisponible { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaApertura { get; set; }
}

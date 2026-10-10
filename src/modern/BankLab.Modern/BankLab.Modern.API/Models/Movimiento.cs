namespace BankLab.Modern.API.Models;

public class Movimiento
{
    public int MovimientoId { get; set; }
    public int CuentaId { get; set; }
    public DateTime FechaOperacion { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public decimal SaldoResultante { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}

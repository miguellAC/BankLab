namespace BankLab.Modern.API.Configuration;

public class BankLabOptions
{
    public int DefaultPageSize { get; set; } = 20;
    public int MaxPageSize { get; set; } = 100;
    public int TimeoutSeconds { get; set; } = 30;
    public int ImportBatchSize { get; set; } = 500;
    public string DefaultCurrency { get; set; } = "COP";
}

namespace BankLab.Modern.API.Validators;

public class PagoValidator
{
    public bool Validate(decimal amount, out string errorMessage)
    {
        if (amount < 0)
        {
            errorMessage = "El monto no puede ser negativo.";
            return false;
        }
        if (amount == 0)
        {
            errorMessage = "El monto no puede ser cero.";
            return false;
        }
        
        errorMessage = string.Empty;
        return true;
    }
}

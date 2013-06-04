namespace MyWalletz.Models
{
    using System.ComponentModel.DataAnnotations;

    public static class Validations
    {
        public static ValidationResult ValidateCurrency(string value)
        {
            return new CurrencyList().ContainsKey(value) ?
                       ValidationResult.Success :
                       new ValidationResult("Unknown currency.");
        }

        public static ValidationResult ValidateMoney(decimal value)
        {
            return value >= 0 ?
                ValidationResult.Success :
                new ValidationResult("Must be non-negative decimal value.");
        }
    }
}
namespace MyWalletz.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateAccount
    {
        [Required]
        public string Title { get; set; }

        public AccountType Type { get; set; }

        public string Notes { get; set; }

        [CustomValidation(typeof(Validations), "ValidateMoney")]
        public decimal Balance { get; set; }

        [Required]
        [CustomValidation(typeof(Validations), "ValidateCurrency")]
        public string Currency { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
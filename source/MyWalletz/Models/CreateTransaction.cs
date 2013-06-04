namespace MyWalletz.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateTransaction
    {
        public string Payee { get; set; }

        public string Notes { get; set; }

        public int AccountId { get; set; }

        public int CategoryId { get; set; }

        [CustomValidation(typeof(Validations), "ValidateMoney")]
        public decimal Amount { get; set; }

        public DateTime PostedAt { get; set; }
    }
}
namespace MyWalletz.DataTransferObjects
{
    using System;

    [Serializable]
    public class Transaction
    {
        public int Id { get; set; }

        public string Payee { get; set; }

        public string Notes { get; set; }

        public int AccountId { get; set; }

        public int? CategoryId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PostedAt { get; set; }
    }
}
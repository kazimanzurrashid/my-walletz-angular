namespace MyWalletz.DataTransferObjects
{
    using System;

    [Serializable]
    public class Account
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        public AccountType Type { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }
    }
}
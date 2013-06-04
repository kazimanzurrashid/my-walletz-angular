namespace MyWalletz.DomainObjects
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("mw_Accounts")]
    public class Account
    {
        private ICollection<Transaction> transactions;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        public AccountType Type { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Transaction> Transactions
        {
            get
            {
                return transactions ??
                    (transactions = new HashSet<Transaction>());
            }
        }

        public virtual Transaction Deposit(decimal amount)
        {
            return CreateTransaction(amount);
        }

        public virtual Transaction Withdraw(decimal amount)
        {
            return CreateTransaction(-amount);
        }

        private Transaction CreateTransaction(decimal amount)
        {
            Balance += amount;

            var transaction = new Transaction
            {
                Account = this,
                UserId = UserId,
                Amount = amount
            };

            Transactions.Add(transaction);

            return transaction;
        }
    }
}
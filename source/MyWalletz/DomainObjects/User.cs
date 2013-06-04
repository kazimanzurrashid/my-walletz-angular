namespace MyWalletz.DomainObjects
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("mw_Users")]
    public class User
    {
        private ICollection<Category> categories;
        private ICollection<Account> accounts;
        private ICollection<Transaction> transactions;

        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Category> Categories
        {
            get { return categories ?? (categories = new HashSet<Category>()); }
        }

        public virtual ICollection<Account> Accounts
        {
            get { return accounts ?? (accounts = new HashSet<Account>()); }
        }

        public virtual ICollection<Transaction> Transactions
        {
            get
            {
                return transactions ??
                    (transactions = new HashSet<Transaction>());
            }
        }
    }
}
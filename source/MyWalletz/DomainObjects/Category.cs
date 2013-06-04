namespace MyWalletz.DomainObjects
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("mw_Categories")]
    public class Category
    {
        private ICollection<Transaction> transactions;

        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public CategoryType Type { get; set; }

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
    }
}
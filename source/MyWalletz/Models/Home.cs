namespace MyWalletz.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using DataTransferObjects;

    public class Home
    {
        public Home()
        {
            Categories = Enumerable.Empty<Category>();
            Accounts = Enumerable.Empty<Account>();
        }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Account> Accounts { get; set; }
    }
}
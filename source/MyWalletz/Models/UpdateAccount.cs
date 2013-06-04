namespace MyWalletz.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateAccount
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public AccountType Type { get; set; }

        public string Notes { get; set; }
    }
}
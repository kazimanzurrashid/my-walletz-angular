namespace MyWalletz.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCategory
    {
        [Required]
        public string Title { get; set; }

        public CategoryType Type { get; set; }
    }
}
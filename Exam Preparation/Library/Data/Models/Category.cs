namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Library.Utilities.Constants;

    public class Category
    {
        public Category()
        {
            Books = new List<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryConstants.MaxCategoryName)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; }
    }
}

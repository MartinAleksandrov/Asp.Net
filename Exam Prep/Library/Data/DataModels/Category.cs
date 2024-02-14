namespace Library.Data.DataModels
{
    using System.ComponentModel.DataAnnotations;
    using static Library.Utilities.Constants.CategoryConstants;

    public class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCategoryName)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    } 
}
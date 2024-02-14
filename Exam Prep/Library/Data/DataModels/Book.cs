namespace Library.Data.DataModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Library.Utilities.Constants.BookConstants;

    public class Book
    {
        public Book()
        {
            UsersBooks = new HashSet<IdentityUserBook>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTitleLenght)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(MaxAuthorLenght)]
        public string Author { get; set; } = string.Empty;

        [Required]
        [MaxLength(MaxDescriptionLenght)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(MaxRatingLenght)]
        public decimal Rating { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        [Required]
        public ICollection<IdentityUserBook> UsersBooks { get; set; }

    }
}
namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Library.Utilities.Constants;

    public class Book
    {
        public Book()
        {
            UsersBooks = new List<IdentityUserBook>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BookConstants.MaxTitleLenght)]
        public string Title { get; set; } = null!;


        [Required]
        [MaxLength(BookConstants.MaxAuthorLenght)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(BookConstants.MaxDescriptionLenght)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(BookConstants.MaxRatingLenght)]
        public decimal Rating { get; set; } 

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public ICollection<IdentityUserBook> UsersBooks { get; set; }
    }
}
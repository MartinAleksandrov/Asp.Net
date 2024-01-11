namespace Library.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Library.Utilities.Constants;

    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(BookConstants.MaxTitleLenght, MinimumLength = BookConstants.MinTitleLenght)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(BookConstants.MaxAuthorLenght, MinimumLength = BookConstants.MinAuthorLenght)]
        public string Author { get; set; } = null!;

        [Required]
        [MinLength(5)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(BookConstants.MinRatingLenght,BookConstants.MaxRatingLenght)]
        public decimal Rating { get; set; }

        [Required]
        [StringLength(BookConstants.MaxDescriptionLenght, MinimumLength = BookConstants.MinDescriptionLenght)]
        public string Description { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}

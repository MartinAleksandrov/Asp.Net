namespace Library.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Library.Utilities.Constants.BookConstants;
    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            Categories = new HashSet<CategoryViewModel>();
        }

        [Required]
        [StringLength(MaxTitleLenght,MinimumLength = MinTitleLenght)]
        public string Title { get; set; } = string.Empty;


        [Required]
        [StringLength(MaxDescriptionLenght, MinimumLength = MinDescriptionLenght)]
        public string Description { get; set; } = string.Empty;


        [Required]
        [StringLength(MaxAuthorLenght, MinimumLength = MinAuthorLenght)]
        public string Author { get; set; } = string.Empty;


        [Required]
        public string Url { get; set; } = string.Empty;


        [Range(MinRatingLenght,MaxRatingLenght)]
        public string Rating { get; set; } = string.Empty;

        public int CategoryId { get; set; } 

        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}

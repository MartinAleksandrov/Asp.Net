namespace Forum.App.ViewModels.Post
{
    using System.ComponentModel.DataAnnotations;
    using Forum.Common.Validations;

    public class PostFormModel
    {
        [Required]
        [StringLength(ValidationEntity.TitleMaxLenght, MinimumLength = ValidationEntity.TitleMinLenght)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ValidationEntity.ContentMaxLenght, MinimumLength = ValidationEntity.ContentMinLenght)]
        public string Content { get; set; } = null!;
    }
}

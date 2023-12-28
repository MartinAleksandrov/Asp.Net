namespace Forum.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Forum.Common.Validations;
    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(ValidationEntity.TitleMaxLenght)]
        public string Title { get; set; } = null!;


        [Required]
        [MaxLength(ValidationEntity.ContentMaxLenght)]
        public string Content { get; set; } = null!;

    }
}

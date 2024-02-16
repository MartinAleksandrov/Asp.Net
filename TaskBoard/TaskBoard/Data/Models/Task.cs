namespace TaskBoard.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static TaskBoard.Utilities.DataConstants.TaskConstants;

    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTaskTitleLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Board))]
        public int BoardId { get; set;}

        public Board? Board { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;


        public IdentityUser Owner { get; set; } = null!;

    }
}
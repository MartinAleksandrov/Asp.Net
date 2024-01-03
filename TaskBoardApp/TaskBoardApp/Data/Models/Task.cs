namespace TaskBoardApp.Data.ViewModels
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static TaskBoardApp.DataConstants.Constants;

    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TaskConstants.TaskMaxTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(TaskConstants.TaskMaxTitle)]
        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Board))]
        public int BoardId { get; set; }

        public Board? Board { get; set; }

        [Required]
        public string OwnerId { get; set; } = null!;

        public IdentityUser Owner { get; set; } = null!;
    }
}

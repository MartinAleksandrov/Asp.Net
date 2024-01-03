namespace TaskBoardApp.ViewModels.Task
{
    using System.ComponentModel.DataAnnotations;
    using static TaskBoardApp.DataConstants.Constants;

    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TaskConstants.TaskMaxTitle, MinimumLength = TaskConstants.TaskMinTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(TaskConstants.TaskMaxDescription, MinimumLength = TaskConstants.TaskMinDescription)]
        public string Description { get; set; } = null!;

        [Required]
        public string Owner { get; set; } = null!;
    }
}

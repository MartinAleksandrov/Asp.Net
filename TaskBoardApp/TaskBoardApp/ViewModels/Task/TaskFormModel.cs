namespace TaskBoardApp.ViewModels.Task
{
    using System.ComponentModel.DataAnnotations;
    using TaskBoardApp.ViewModels.Board;
    using static TaskBoardApp.DataConstants.Constants;

    public class TaskFormModel
    {
        [Required]
        [StringLength(TaskConstants.TaskMaxTitle, MinimumLength = TaskConstants.TaskMinTitle,
            ErrorMessage = "Title should be atleast {2} characters long.")]
        public string Title { get; set; } = null!;



        [Required]
        [StringLength(TaskConstants.TaskMaxTitle, MinimumLength = TaskConstants.TaskMinTitle,
            ErrorMessage = "Description should be atleast {2} characters long.")]
        public string Description { get; set; } = null!;


        [Display(Name= "Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; } = null!;
    }
}

namespace TaskBoard.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static TaskBoard.Utilities.DataConstants.TaskConstants;
    public class TaskFormViewModel
    {
        public TaskFormViewModel()
        {
            Boards = new List<TaskBoardViewModel>();
        }

        [Required]
        [StringLength(MaxTaskTitleLength, MinimumLength =MinTaskTitleLength,
            ErrorMessage = "Title should be at least {2} characters long")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength,
           ErrorMessage = "Description should be at least {2} characters long")]
        public string Description { get; set; } = string.Empty;

        [Display(Name ="Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardViewModel> Boards { get; set;}
    }
}

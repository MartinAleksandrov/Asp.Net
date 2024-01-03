namespace TaskBoardApp.ViewModels.Board
{
    using System.ComponentModel.DataAnnotations;
    using TaskBoardApp.ViewModels.Task;
    using static TaskBoardApp.DataConstants.Constants;

    public class BoardViewModel
    {
        public BoardViewModel()
        {
            Tasks = new List<TaskViewModel>();
        }

        public int Id { get; init; }

        [Required]
        [StringLength(BoardConstants.BoardNameMaxlenght, MinimumLength = BoardConstants.BoardNameMinLenght)]
        public string Name { get; init; } = null!;


        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}

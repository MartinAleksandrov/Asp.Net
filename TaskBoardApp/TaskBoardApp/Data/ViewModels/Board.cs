namespace TaskBoardApp.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static TaskBoardApp.DataConstants.Constants;

    public class Board
    {
        public Board()
        {
            Tasks = new List<Task>();
        }

        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(BoardConstants.BoardNameMaxlenght)]
        public string Name { get; set; } = null!;

        public IEnumerable<Task> Tasks { get; set; } = null!;


    }
}

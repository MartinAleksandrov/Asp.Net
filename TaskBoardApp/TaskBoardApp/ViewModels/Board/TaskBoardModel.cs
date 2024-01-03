namespace TaskBoardApp.ViewModels.Board
{
    using System.ComponentModel.DataAnnotations;

    public class TaskBoardModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

    }
}

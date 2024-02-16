namespace TaskBoard.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using static TaskBoard.Utilities.DataConstants.BoardConstants;

    public class Board
    {
        public Board()
        {
            Tasks = new List<Task>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(BoardMaxName)]
        public string Name { get; set; } = null!;

        public IEnumerable<Task> Tasks { get; set; }
    }
}
namespace TaskBoard.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? OwnerId { get; set; } 

        public string Owner { get; set; } = string.Empty;
    }
}

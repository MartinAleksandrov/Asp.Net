namespace TaskBoard.ViewModels
{
    public class BoardViewModel
    {
        public BoardViewModel()
        {
            Tasks = new List<TaskViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}

namespace TaskBoard.ViewModels
{
    public class HomeViewModel
    {


        public HomeViewModel()
        {
            BoardsWithTasksCount = new List<HomeBoardViewModel>();
        }
        public int AllTasksCount { get; set; }

        public ICollection<HomeBoardViewModel> BoardsWithTasksCount { get; set; }

        public int UserTasksCount { get; set; }

    }
}
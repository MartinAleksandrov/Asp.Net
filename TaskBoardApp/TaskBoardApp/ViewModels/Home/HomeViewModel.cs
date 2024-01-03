namespace TaskBoardApp.ViewModels.Home
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            BoardsWithTasksCount = new List<HomdeBoardModel>();
        }

        public int AllTasksCount { get; set; }

        public IEnumerable<HomdeBoardModel> BoardsWithTasksCount { get; set;}

        public int UserTasksCount { get; set; }
    }
}

namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using TaskBoardApp.Data;
    using TaskBoardApp.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly TaskBoardAppDbContext dbContext;

        public HomeController(TaskBoardAppDbContext context)
        {
            this.dbContext = context;
        }

        public IActionResult Index()
        {
            var taskBoards = dbContext
                .Boards
                .Select(b => b.Name)
                .Distinct();

            var taskCounts = new List<HomdeBoardModel>();

            foreach (var boardName in taskBoards)
            {
                var taskInBoard = dbContext.Tasks.Where(t => t.Board.Name == boardName).Count();
                taskCounts.Add(new HomdeBoardModel()
                {
                    BoardName = boardName,
                    TasksCount = taskInBoard
                });
            }

            var userTasksCount = -1;

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                userTasksCount = dbContext.Tasks.Where(t => t.OwnerId == currentUserId).Count();
            }

            var homeModel = new HomeViewModel()
            {
                AllTasksCount = dbContext.Tasks.Count(),
                BoardsWithTasksCount = taskCounts,
                UserTasksCount = userTasksCount
            };

            return View(homeModel);
        }
    }
}
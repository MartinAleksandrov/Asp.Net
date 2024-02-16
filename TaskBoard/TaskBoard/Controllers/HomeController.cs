namespace TaskBoard.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TaskBoard.Data;
    using TaskBoard.Extensions;
    using TaskBoard.ViewModels;

    [Authorize]
    public class HomeController : Controller
    {

        //Im sorry but here i'll brek good practice because it is too late right 2:00 now and im tired so yes :D

        private readonly TaskBoardAppDbContext context;

        public HomeController(TaskBoardAppDbContext dbContext)
        {
            context = dbContext;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var tasksBoards =  context.Boards
                .Select(t => t.Name)
                .Distinct();

            var tasksCounts = new List<HomeBoardViewModel>();

            foreach (var boardName in tasksBoards)
            {
                var taskInBoard = await context.Tasks.Where(b => b.Board!.Name == boardName).CountAsync();

                tasksCounts.Add(new HomeBoardViewModel
                {
                    BoardName = boardName,
                    TasksCount = taskInBoard
                });
            }

            var userTasksCount = -1;

            if (User.Identity!.IsAuthenticated)
            {
                var userId = User.GetId();
                userTasksCount = await context.Tasks.Where(t => t.OwnerId == userId).CountAsync();
            }

            var homeViewModel = new HomeViewModel()
            {
                AllTasksCount = userTasksCount,
                BoardsWithTasksCount = tasksCounts,
                UserTasksCount = userTasksCount
            };

            return View(homeViewModel);
        }
    }
}
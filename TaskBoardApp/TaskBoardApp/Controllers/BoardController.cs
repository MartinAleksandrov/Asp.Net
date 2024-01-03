namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TaskBoardApp.Data;
    using TaskBoardApp.ViewModels.Board;
    using TaskBoardApp.ViewModels.Task;

    public class BoardController : Controller
    {
        private readonly TaskBoardAppDbContext dbContext;

        public BoardController(TaskBoardAppDbContext context)
        {
            this.dbContext = context;
        }

        public async  Task<IActionResult> All()
        {
            var boards = await dbContext.Boards
                .Select(b => new BoardViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks
                                .Select(t => new TaskViewModel
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    Description = t.Description,
                                    Owner = t.Owner.UserName

                                })
                })
                .ToListAsync();

            return View(boards);
        }
    }
}

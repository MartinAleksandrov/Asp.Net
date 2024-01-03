namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using TaskBoardApp.Data;
    using TaskBoardApp.Data.ViewModels;
    using TaskBoardApp.ViewModels.Board;
    using TaskBoardApp.ViewModels.Task;

    [Authorize]
    public class TaskController : Controller
    {
        private readonly TaskBoardAppDbContext dbContext;

        public TaskController(TaskBoardAppDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<IActionResult> Create()
        {
            var task = new TaskFormModel()
            {
                Boards = GetBoards()
            };

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel taskModel)
        {


            if (!GetBoards().Any(b => b.Id == taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Boards does not exist.");
            }

            var currentUserId = GetUserId();

            Task task = new Task
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                CreatedOn = DateTime.Now,
                BoardId = taskModel.BoardId,
                OwnerId = currentUserId
            };

            await dbContext.Tasks.AddAsync(task);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("All", "Board");

        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await dbContext.Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName

                })
                .FirstOrDefaultAsync();


            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await dbContext.Tasks.FindAsync(id);


            if (task == null)
            {
                return RedirectToAction("All", "Board");
            }

            string currentUserId = GetUserId();

            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskFormModel model = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = GetBoards()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id , TaskFormModel model)
        {
            var task = await dbContext.Tasks.FindAsync(id);

            if (task == null)
            {
                return RedirectToAction("All", "Board");
            }

            string currentUserId = GetUserId();

            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            if (!GetBoards().Any(b => b.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist.");
            }

            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;

            await dbContext.SaveChangesAsync();

            return RedirectToAction("All", "Board");

        }

        public async Task<IActionResult> Delete(int Id)
        {
            var task = await dbContext.Tasks.FindAsync(Id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();

            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel taskView = new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description
            };

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id, TaskViewModel model)
        {
            var task = await dbContext.Tasks.FindAsync(Id);

            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = GetUserId();

            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            dbContext.Remove(task);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("All", "Board");
        }

        private string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public IEnumerable<TaskBoardModel> GetBoards()
        {
            return dbContext.Boards.Select(b => new TaskBoardModel
            {
                Id = b.Id,
                Name = b.Name
            });
        }
    }
}

namespace TaskBoard.Services
{
    using Microsoft.EntityFrameworkCore;
    using TaskBoard.Data;
    using TaskBoard.Data.Models;
    using TaskBoard.Services.Contracts;
    using TaskBoard.ViewModels;

    public class BoardService : IBoardService
    {
        private readonly TaskBoardAppDbContext context;

        public BoardService(TaskBoardAppDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<bool> CreateTask(TaskFormViewModel model, string userId)
        {
            var allBoards = await GetBoards();


            if (!allBoards.Any(b => b.Id == model.BoardId))
            {
                return false;
            }

            var task = new Task
            {
                Title = model.Title,
                Description = model.Description,
                BoardId = model.BoardId,
                CreatedOn = DateTime.UtcNow,
                OwnerId = userId
            };

            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTask(int id,string userId)
        {
            var task = await GetTask(id);

            if (task == null)
            {
                return false;
            }

            if (task.OwnerId != userId)
            {
                return false;
            }

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<TaskFormViewModel> EditTask(int id)
        {
            var task = await GetTask(id);

            if (task == null)
            {
                return null!;
            }

            var viewModel = new TaskFormViewModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = await GetBoards(),
                OwnerId = task.OwnerId

            };

            return viewModel;
        }
        public async Task<bool> IsEdited(int id,string userId, TaskFormViewModel viewModel)
        {
            var task = await GetTask(id);

            if (task == null)
            {
                return false;
            }

            if (task.OwnerId != userId)
            {
                return false;
            }

            task.Title = viewModel.Title;
            task.Description = viewModel.Description;
            task.BoardId = viewModel.BoardId;

            await context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<BoardViewModel>> GetAllTasks()
        {
            var model = await context
                .Boards
                .AsNoTracking()
                .Select(b => new BoardViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b
                            .Tasks
                            .Select(t => new TaskViewModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Owner = t.Owner.UserName
                    })

                })
                .ToListAsync();

            return model;
        }


        public async Task<IEnumerable<TaskBoardViewModel>> GetBoards()
        {
            return await context.Boards
                .AsNoTracking()
                .Select(b => new TaskBoardViewModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToListAsync();
        }

        public async Task<TaskViewModel> GetCurrentTask(int id)
        {
            var task = await GetTask(id);

            if (task == null)
            {
                return null!;
            }
            var model = new TaskViewModel()
            {
                Id = task!.Id,
                Title = task.Title,
                Description = task.Description,
                OwnerId = task!.OwnerId
            };

            return model;
        }

       

        public async Task<TaskDetailsViewModel> TaskDetails(int id)
        {
            var model = await context
                .Tasks
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Owner = t.Owner.UserName,
                    CreatedOn = t.CreatedOn.ToString("yyyy-MM-dd HH-mm"),
                    Board = t.Board!.Name

                }).FirstOrDefaultAsync();


            return model!;
        }

        private async Task<Task> GetTask(int id)
        {
            var task = await context.Tasks.FindAsync(id);

            return task!;
        }
    }
}

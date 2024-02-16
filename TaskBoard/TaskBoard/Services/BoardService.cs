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
    }
}

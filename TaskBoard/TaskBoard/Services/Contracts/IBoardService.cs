namespace TaskBoard.Services.Contracts
{
    using TaskBoard.ViewModels;
    public interface IBoardService
    {
        Task<IEnumerable<BoardViewModel>> GetAllTasks();

        Task<IEnumerable<TaskBoardViewModel>> GetBoards();

        Task<bool> CreateTask(TaskFormViewModel model,string userId);
    }
}

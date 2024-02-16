namespace TaskBoard.Services.Contracts
{
    using TaskBoard.ViewModels;
    public interface IBoardService
    {
        Task<IEnumerable<BoardViewModel>> GetAllTasks();

        Task<IEnumerable<TaskBoardViewModel>> GetBoards();

        Task<bool> CreateTask(TaskFormViewModel model,string userId);

        Task<TaskDetailsViewModel> TaskDetails(int id);

        Task<bool> DeleteTask(int id,string userId);

        Task<TaskViewModel> GetCurrentTask(int id);

        Task<TaskFormViewModel> EditTask(int id);

        Task<bool> IsEdited(int id, string userId, TaskFormViewModel viewModel);
    }
}

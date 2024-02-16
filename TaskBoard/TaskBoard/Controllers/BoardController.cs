namespace TaskBoard.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TaskBoard.Extensions;
    using TaskBoard.Services.Contracts;
    using TaskBoard.ViewModels;

    [Authorize]
    public class BoardController : Controller
    {
        private readonly IBoardService service;

        public BoardController(IBoardService boardService)
        {
            service = boardService;
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allTasks = await service.GetAllTasks();

            return View(allTasks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TaskFormViewModel()
            {
                Boards = await service.GetBoards()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormViewModel model)
        {
            var result = await service.CreateTask(model, User.GetId());
            var allBoards = await service.GetBoards();

            if (!result)
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Boards = allBoards;
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }

    }
}

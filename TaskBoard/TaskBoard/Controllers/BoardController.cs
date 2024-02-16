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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await service.TaskDetails(id);

            if (model == null)
            {
                ModelState.AddModelError(nameof(model), "Unexpected error occured!");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await service.GetCurrentTask(id);

            if (model == null)
            {
                return BadRequest();
            }

            if (model.OwnerId != User.GetId())
            {
                return Unauthorized();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, TaskViewModel viewModel)
        {
            var isDeleted = await service.DeleteTask(id, User.GetId());

            if (!isDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await service.EditTask(id);

            if (viewModel == null)
            {
                return RedirectToAction(nameof(All));
            }

            if (viewModel.OwnerId != User.GetId())
            {
                return RedirectToAction(nameof(All));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,TaskFormViewModel viewModel)
        {

            var isEdited = await service.IsEdited(id,User.GetId(),viewModel);

            if (!isEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}

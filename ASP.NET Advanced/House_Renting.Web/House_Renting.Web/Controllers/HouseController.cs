namespace House_Renting.Web.Controllers
{
    using House_Renting.Services.Interfaces;
    using House_Renting.Web.Infrastructure.Extensions;
    using House_Renting.Web.ViewModels.House;
    using Microsoft.AspNetCore.Authorization;
    using static HouseRenting.Common.NotificationMessagesConstants;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.EntityFrameworkCore.Query;

    [Authorize]
    public class HouseController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;
        private readonly IHouseService houseService;


        public HouseController(ICategoryService service, IAgentService agentService, IHouseService houseService)
        {
            categoryService = service;
            this.agentService = agentService;
            this.houseService = houseService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel queryModel)
        {
            var serviceModel = await houseService.AllAync(queryModel);
            queryModel.Houses = serviceModel.Houses;
            queryModel.TotalHouses = serviceModel.TotalHousesCount;
            queryModel.Categories = await categoryService.AllCategoryNamesAsync();

            return View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent = await agentService.AgentExistByUserIdAsync(User.GetId()!);

            if (isAgent)
            {
                TempData[ErrorMessage] = "You must become an agent in order to add new houses";

                return RedirectToAction("Become", "Agent");
            }

            HouseFormModel houseFormModel = new HouseFormModel()
            {
                Categories = await categoryService.AllCategoriesAsync()
            };

            return View(houseFormModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            bool isAgent = await agentService.AgentExistByUserIdAsync(User.GetId()!);

            if (isAgent)
            {
                TempData[ErrorMessage] = "You must become an agent in order to add new houses";

                return RedirectToAction("Become", "Agent");
            }

            var categoryExist = await categoryService.ExistById(model.CategoryId);

            if (!categoryExist)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.AllCategoriesAsync();

                return View(model);
            }

            try
            {
                var agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
                await houseService.CreateAsync(model, agentId!);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to add new hosue!");
                model.Categories = await categoryService.AllCategoriesAsync();

                return View(model);
            }

            return RedirectToAction("All", "House");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string Id)
        {
            var viewModel = await houseService.GetDetailsByIdAsync(Id);

            if (viewModel == null)
            {
                TempData[ErrorMessage] = "House with this id does not exist";

                return RedirectToAction("All","House");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<HouseAllViewModel> myHouses =
                new List<HouseAllViewModel>();

            string userId = User.GetId()!;
            bool isUserAgent = await agentService
                .AgentExistByUserIdAsync(userId);

            try
            {
                //if (User.IsAdmin())
                //{
                //    string? agentId =
                //        await agentService.GetAgentIdByUserIdAsync(userId);

                //    // Added houses as an Agent
                //    myHouses.AddRange(await houseService.AllByAgentIdAsync(agentId!));

                //    // Rented houses as user
                //    myHouses.AddRange(await houseService.AllByUserIdAsync(userId));

                //    myHouses = myHouses
                //        .DistinctBy(h => h.Id)
                //        .ToList();
                //}
                if (isUserAgent)
                {
                    string? agentId =
                        await agentService.GetAgentIdByUserIdAsync(userId);

                    myHouses.AddRange(await houseService.AllByAgentIdAsync(agentId!));
                }
                else
                {
                    myHouses.AddRange(await houseService.AllByUserIdAsync(userId));
                }

                return View(myHouses);
            }
            catch (Exception)
            {
                return Ok();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id) 
        {
            bool houseExists = await houseService
                .ExistsByIdAsync(id);
            if (!houseExists)
            {
                TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");

                //return this.NotFound(); -> If you want to return 404 page
            }

            bool isUserAgent = await agentService
                .AgentExistByUserIdAsync(User.GetId()!);
            if (!isUserAgent /*&& !User.IsAdmin()*/)
            {
                TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId =
                await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
            bool isAgentOwner = await houseService
                .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
            if (!isAgentOwner /*&& !User.IsAdmin()*/)
            {
                TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

                return RedirectToAction("Mine", "House");
            }

            try
            {
                HouseFormModel formModel = await houseService
                    .GetHouseForEditByIdAsync(id);
                formModel.Categories = await categoryService.AllCategoriesAsync();

                return View(formModel);
            }
            catch (Exception)
            {
                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, HouseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.AllCategoriesAsync();

                return View(model);
            }

            bool houseExists = await houseService
                .ExistsByIdAsync(id);
            if (!houseExists)
            {
                TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");
            }

            bool isUserAgent = await agentService
                .AgentExistByUserIdAsync(User.GetId()!);
            if (!isUserAgent /*&& !User.IsAdmin()*/)
            {
                TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId =
                await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
            bool isAgentOwner = await houseService
                .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
            if (!isAgentOwner)
            {
                TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

                return RedirectToAction("Mine", "House");
            }

            try
            {
                await houseService.EditHouseByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to update the house. Please try again later or contact administrator!");
                model.Categories = await categoryService.AllCategoriesAsync();

                return View(model);
            }

            TempData[SuccessMessage] = "House was edited successfully!";
            return RedirectToAction("Details", "House", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool houseExists = await houseService
                .ExistsByIdAsync(id);
            if (!houseExists)
            {
                TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");
            }

            bool isUserAgent = await agentService
                .AgentExistByUserIdAsync(User.GetId()!);
            if (!isUserAgent)
            {
                TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId =
                await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
            bool isAgentOwner = await houseService
                .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
            if (!isAgentOwner)
            {
                TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

                return RedirectToAction("Mine", "House");
            }

            try
            {
                HousePreDeleteDetailsViewModel viewModel =
                    await houseService.GetHouseForDeleteByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, HousePreDeleteDetailsViewModel model)
        {
            bool houseExists = await houseService
                .ExistsByIdAsync(id);
            if (!houseExists)
            {
                TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");
            }

            bool isUserAgent = await agentService
                .AgentExistByUserIdAsync(User.GetId()!);
            if (!isUserAgent)
            {
                TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId =
                await agentService.GetAgentIdByUserIdAsync(User.GetId()!);
            bool isAgentOwner = await houseService
                .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
            if (!isAgentOwner)
            {
                TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

                return RedirectToAction("Mine", "House");
            }

            try
            {
                await houseService.DeleteHouseByIdAsync(id);

                TempData[WarningMessage] = "The house was successfully deleted!";
                return RedirectToAction("Mine", "House");
            }
            catch (Exception)
            {
                return Ok();
            }
        }
    }
}

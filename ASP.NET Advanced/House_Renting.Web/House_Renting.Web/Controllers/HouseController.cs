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

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllHousesQueryModel queryModel)
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
                await houseService.CreateAsync(model,agentId!);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,"Unexpected error occured while trying to add new hosue!");
                model.Categories = await categoryService.AllCategoriesAsync();

                return View(model);
            }

            return RedirectToAction("All","House");
        }


    }
}

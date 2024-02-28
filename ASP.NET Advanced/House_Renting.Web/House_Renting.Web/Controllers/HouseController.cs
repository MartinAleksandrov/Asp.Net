namespace House_Renting.Web.Controllers
{
    using House_Renting.Services.Interfaces;
    using House_Renting.Web.Infrastructure.Extensions;
    using House_Renting.Web.ViewModels.House;
    using Microsoft.AspNetCore.Authorization;
    using static HouseRenting.Common.NotificationMessagesConstants;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class HouseController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;

        public HouseController(ICategoryService service, IAgentService agentService)
        {
            categoryService = service;
            this.agentService = agentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
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
    }
}

namespace House_Renting.Web.Controllers
{
    using House_Renting.Services.Interfaces;
    using House_Renting.Web.Infrastructure.Extensions;
    using House_Renting.Web.ViewModels.House;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Components.Web;
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
                TempData
            }
            
            HouseFormModel houseFormModel = new HouseFormModel()
            {
                Categories = await categoryService.AllCategoriesAsync()
            };

            return View(houseFormModel);
        }
    }
}

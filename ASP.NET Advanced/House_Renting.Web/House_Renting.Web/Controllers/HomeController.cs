namespace House_Renting.Web.Controllers
{
    using House_Renting.Services.Interfaces;
    using House_Renting.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHouseService houseService;

        public HomeController(IHouseService service)
        {
            houseService = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var viewModel = await houseService.LastFreeHousesAsync();

            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
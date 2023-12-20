using Microsoft.AspNetCore.Mvc;
using MyWebSite.Models;
using System.Diagnostics;

namespace MyWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Hello there!!!";
            ViewData["Msg"] = "Ohhh hy!";

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {

            ViewBag.Message = "This is my first message using asp.net framework!";
            return View();
        }

        public IActionResult Nums1To50(int num)
        {
            ViewData["Message"] = "This page displays numbers from 1 to 50!";
            return View();
        }

        [HttpGet]
        public IActionResult NumbersToN()
        {
            ViewBag.Count = -1;
            return this.View();
        }

        [HttpPost]
        public IActionResult NumbersToN(int count = -1)
        {
            ViewBag.Count = count;
            return this.View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
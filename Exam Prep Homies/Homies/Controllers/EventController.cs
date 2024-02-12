namespace Homies.Controllers
{
    using Homies.Services;
    using Homies.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Authorize]
    public class EventController : Controller
    {

        public readonly IEventService eventService;


        public EventController(IEventService service)
        {
            eventService = service;
        }
        public async Task<IActionResult> All()
        {
            var allEvents = await eventService.AllEvents();

            return View(allEvents);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var userEvents = await eventService.JoinedEvents(GetUserId());

            return View(userEvents);
        }


        [HttpPost]
        public async Task<IActionResult> Joined(int id)
        {
            var isJoined = await eventService.JoinEvent(id);

            if (!isJoined)
            {
                return BadRequest();
            }
            else
            {
                return View();
            }
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

    }
}

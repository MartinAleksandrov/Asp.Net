namespace Homies.Controllers
{
    using Homies.Data;
    using Homies.Data.DataModels;
    using Homies.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Security.Claims;
    using static Homies.Utilities.GlobalConstants;

    [Authorize]
    public class EventController : Controller
    {

        public readonly HomiesDbContext context;


        public EventController(HomiesDbContext dbContext)
        {
            context = dbContext;
        }
        public async Task<IActionResult> All()
        {
            var events = await context.Events
               .AsNoTracking()
               .Select(e => new AllEventsViewModel
               {
                   Id = e.Id,
                   Start = e.Start,
                   Name = e.Name,
                   Type = e.Type.Name,
                   Organiser = e.Organiser.UserName
               })
               .ToListAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var userId = GetUserId();

            var userEvents = await context
                    .EventParticipants.Where(ep => ep.HelperId == userId)
                    .AsNoTracking()
                    .Select(ep => new AllEventsViewModel
                    {
                        Id = ep.EventId,
                        Name = ep.Event.Name,
                        Start = ep.Event.Start,
                        Type = ep.Event.Type.Name,
                        Organiser = ep.Event.Organiser.UserName
                    })
                    .ToListAsync();

            return View(userEvents);
        }


        [HttpPost]
        public async Task<IActionResult> Join(int Id)
        {
            var userId = GetUserId();

            var ev = await context.Events
                .Include(e => e.EventsParticipants)
                .Where(e => e.Id == Id)
                .FirstOrDefaultAsync();


            if (ev == null)
            {
                return BadRequest();
            }

            if (!ev.EventsParticipants.Any(e => e.HelperId == userId))
            {
                var ep = new EventsParticipants()
                {
                    EventId = Id,
                    HelperId = userId
                };

                context.EventParticipants.Add(ep);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new CreateEventViewModel();
            model.Types = await GetTypes();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(CreateEventViewModel model)
        {
            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {DateFormat}");
            }
            if (!DateTime.TryParseExact(model.End, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {DateFormat}");
            }

            var newEvent = new Event()
            {
                CreatedOn = DateTime.Now,
                Name = model.Name,
                Description = model.Description,
                Start = startDate,
                End = endDate,
                OrganiserId = GetUserId(),
                TypeId = model.TypeId,
            };

            if (context.Events.Any(e => e.Name == newEvent.Name))
            {
                ModelState.AddModelError(newEvent.Name, "Event with the same name already exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Types = await GetTypes();
                return View(model);
            }

            context.Events.Add(newEvent);
            context.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int Id)
        {
            var ev = await context
                .Events
                .FirstOrDefaultAsync(e => e.Id == Id);

            if (ev == null)
            {
                return BadRequest();
            }

            if (!context.EventParticipants.Any(ep => ep.HelperId == GetUserId()))
            {
                return BadRequest();
            }
            var user = context.EventParticipants.FirstOrDefault(ep => ep.HelperId == GetUserId());

            context.EventParticipants.Remove(user!);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var evenT = await context
                           .Events
                           .AsNoTracking()
                           .Where(e => e.Id == id)
                           .Select(e => new DetailsViewModel()
                           {
                               Id = e.Id,
                               Name = e.Name,
                               Description = e.Description,
                               Start = e.Start.ToString(DateFormat),
                               End = e.End.ToString(DateFormat),
                               Organiser = e.Organiser.UserName,
                               CreatedOn = e.CreatedOn.ToString(DateFormat),
                               Type = e.Type.Name
                           })
                           .FirstOrDefaultAsync();

            if (evenT == null)
            {
                return BadRequest();
            }


            return View(evenT);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ev = await context
                .Events.FindAsync(id);


            if (ev == null)
            {
                return BadRequest();
            }

            if (ev.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new CreateEventViewModel()
            {
                Name = ev.Name,
                Description = ev.Description,
                Start = ev.Start.ToString(DateFormat),
                End = ev.End.ToString(DateFormat),
                TypeId = ev.TypeId
            };

            model.Types = await GetTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateEventViewModel model, int id)
        {
            var ev = await context
                .Events.FindAsync(id);


            if (ev == null)
            {
                return BadRequest();
            }

            if (ev.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                ModelState.AddModelError(nameof(model.Start), $"Invalid date! Format must be: {DateFormat}");
            }
            if (!DateTime.TryParseExact(model.End, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                ModelState.AddModelError(nameof(model.End), $"Invalid date! Format must be: {DateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Types = await GetTypes();

                return View(model);
            }

            ev.Start = startDate;
            ev.End = endDate;
            ev.Name = model.Name;
            ev.Description = model.Description;
            ev.TypeId = model.TypeId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<List<TypeViewModel>> GetTypes()
        {
            var types = await context.Types.
                AsNoTracking()
                .Select(t => new TypeViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToListAsync();

            return types;
        }
    }
}

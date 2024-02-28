namespace SeminarHub.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SeminarHub.Data;
    using SeminarHub.Data.Models;
    using SeminarHub.Extensions;
    using SeminarHub.Models;
    using System.Globalization;
    using static SeminarHub.Utilities.GlobalConstants.SeminarConstants;

    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext context;

        public SeminarController(SeminarHubDbContext dbContext)
        {
            context = dbContext;
        }

        //Shows all available seminars; 
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var viewModels = await context
                .Seminars
                .AsNoTracking()
                .Select(s => new AllSeminarsViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Category = s.Category.Name,
                    Lecturer = s.Lecturer,
                    DateAndTime = s.DateAndTime.ToString("dd/MM/yyyy HH:mm"),
                    Organizer = s.Organizer.UserName
                }).ToListAsync();

            return View(viewModels);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddSeminarViewModel();

            model.Categories = await GetAllCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSeminarViewModel model)
        {
            var date = DateTime.Now;
            var format = "dd/MM/yyyy HH:mm";

            //Try to parse date 
            if (!DateTime.TryParseExact(model.DateAndTime, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {format}");
            }

            if (context.Seminars.Any(s => s.Topic == model.Topic))
            {
                ModelState.AddModelError(model.Topic, "Seminar about the same topic name already exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetAllCategories();
                return View(model);
            }

            var newSeminar = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = date,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
                OrganizerId = User.GetId()
            };

            await context.Seminars.AddAsync(newSeminar);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await context.Seminars
                .Where(s => s.Id == id)
                .AsNoTracking()
                .Select(s => new DetailsViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime.ToString(dateFormat),
                    Duration = s.Duration,
                    Category = s.Category.Name,
                    Lecturer = s.Lecturer,
                    Organizer = s.Organizer.UserName,
                    Details = s.Details

                }).FirstOrDefaultAsync();

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var userSeminars = await context
                .SeminarsParticipants
                .Where(sp => sp.ParticipantId == User.GetId())
                .AsNoTracking()
                .Select(sp => new JoinedViewModel()
                {
                    Id = sp.Seminar.Id,
                    Topic = sp.Seminar.Topic,
                    Lecturer = sp.Seminar.Lecturer,
                    DateAndTime = sp.Seminar.DateAndTime.ToString(dateFormat),
                    Organizer = sp.Seminar.Organizer.UserName
                })
                .ToListAsync();

            return View(userSeminars);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var model = await context.Seminars.FindAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            if (context.SeminarsParticipants.Any(sp => sp.Seminar.Id == id && sp.ParticipantId == User.GetId()))
            {
                return RedirectToAction(nameof(All));
            }

            var sp = new SeminarParticipant()
            {
                SeminarId = id,
                ParticipantId = User.GetId()
            };

            context.SeminarsParticipants.Add(sp);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var model = await context.Seminars.FindAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            if (!context.SeminarsParticipants.Any(sp => sp.ParticipantId == User.GetId()))
            {
                return BadRequest();
            }

            var sp = await context.SeminarsParticipants.FirstOrDefaultAsync(sp => sp.ParticipantId == User.GetId());


            context.SeminarsParticipants.Remove(sp!);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await context
                .Seminars
                .Where(sp => sp.Id == id)
                .AsNoTracking()
                .Select(s => new DeleteViewModel()
                {
                    Id = s.Id,
                    DateAndTime = s.DateAndTime,
                    Topic = s.Topic
                }).FirstOrDefaultAsync();

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            var currentModel = await context.Seminars.FindAsync(id);

            if (currentModel!.OrganizerId != User.GetId())
            {
                return Unauthorized();
            }

            return View(model);
        }

        //Final delete from database 
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await context.Seminars.FindAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            if (model.OrganizerId != User.GetId())
            {
                return RedirectToAction(nameof(All));
            }

            //checks if there are other users who have subscribed to the seminar
            //and if so removes this seminar from their seminar collection
            if (context.SeminarsParticipants.Any(sp => sp.ParticipantId != User.GetId() && sp.SeminarId == id))
            {
                var sp = await context.SeminarsParticipants.Where(sp => sp.ParticipantId != User.GetId() && sp.SeminarId == id).ToListAsync();

                foreach (var s in sp)
                {
                    context.SeminarsParticipants.Remove(s!);
                    await context.SaveChangesAsync();
                }
                
            }

            context.Seminars.Remove(model);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var s = await context
                .Seminars.FindAsync(id);


            if (s == null)
            {
                return BadRequest();
            }

            if (s.OrganizerId != User.GetId())
            {
                return Unauthorized();
            }

            var model = new EditSeminarViewModel()
            {
                Id = s.Id,
                Topic = s.Topic,
                Lecturer = s.Lecturer,
                Details = s.Details,
                DateAndTime = s.DateAndTime.ToString(dateFormat,CultureInfo.InvariantCulture),
                Duration = s.Duration,
                CategoryId = s.CategoryId
            };

            model.Categories = await GetAllCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditSeminarViewModel model)
        {
            var s = await context
               .Seminars.FindAsync(id);


            if (s == null)
            {
                return BadRequest();
            }

            if (s.OrganizerId != User.GetId())
            {
                return Unauthorized();
            }

            var date = DateTime.Now;
            var format = "dd/MM/yyyy HH:mm";


            //Check if date is in correvt format if not add error to modelstate.
            if (!DateTime.TryParseExact(model.DateAndTime, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {format}");
            }

            if (context.Seminars.Any(s => s.Topic == model.Topic))
            {
                ModelState.AddModelError(model.Topic, "Seminar about the same topic name already exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetAllCategories();
                return View(model);
            }

            //Make the changes
            s.Topic = model.Topic;
            s.Lecturer = model.Lecturer;
            s.Details = model.Details;
            s.DateAndTime = date;
            s.Duration = model.Duration;
            s.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        //Gets all categories from database;
        private async Task<IEnumerable<CategotyViewModel>> GetAllCategories()
        {
            return await context
                .Categories
                .Select(c => new CategotyViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();
        }
    }
}

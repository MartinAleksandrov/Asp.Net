namespace Homies.Services
{

    using Homies.Data;
    using Homies.Data.DataModels;
    using Homies.Models;
    using Homies.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;

    public class EventService : IEventService
    {
        private readonly HomiesDbContext context;


        public EventService(HomiesDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<AllEventsViewModel>> AllEvents()
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

            return events;
        }

        public async Task<List<AllEventsViewModel>> JoinedEvents()
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

            return userEvents;
        }

        //Join current user to the event he choose
        public async Task<bool> JoinEvent(int Id)
        {
            var ev = await context.Events
                .Include(e => e.EventsParticipants)
                .Where(e => e.Id == Id)
                .FirstOrDefaultAsync();

            string userId = GetUserId();

            if (ev != null)
            {
                if (ev.EventsParticipants.Any(e => e.HelperId == userId))
                {
                    var ep = new EventsParticipants()
                    {
                        EventId = Id,
                        HelperId = userId
                    };

                    context.EventParticipants.Add(ep);
                    await context.SaveChangesAsync();

                    return true;
                }
            }

            return false;
        }

        
    }
}

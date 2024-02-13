namespace Homies.Services.Contracts
{
    using Homies.Models;
    public interface IEventService
    {
        public Task<List<AllEventsViewModel>> AllEvents();

        public Task<bool> JoinEvent(int Id,string userId);

        public Task<List<AllEventsViewModel>> JoinedEvents(string userId);
    }
}

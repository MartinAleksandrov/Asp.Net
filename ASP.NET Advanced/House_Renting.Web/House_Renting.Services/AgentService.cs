namespace House_Renting.Services
{
    using House_Renting.Data;
    using House_Renting.Data.Models;
    using House_Renting.Services.Interfaces;
    using House_Renting.Web.ViewModels.Agent;
    using Microsoft.EntityFrameworkCore;

    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext dbContext;

        public AgentService(HouseRentingDbContext context)
        {
            dbContext = context;
        }

        public async Task<bool> AgentExistByPhoneNumberAsync(string number)
        {
            var result = await dbContext
                           .Agents
                           .AnyAsync(a => a.PhoneNumber == number);

            return result;
        }

        public async Task<bool> AgentExistByUserIdAsync(string userId)
        {
            var result = await dbContext
                .Agents
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }

        public async Task Create(string userId, BecomeAgentViewModel model)
        {
            Agent newAgent = new Agent()
            {
                PhoneNumber = model.PhoneNumber,
                UserId = Guid.Parse(userId)
            };

            await dbContext.Agents.AddAsync(newAgent);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> UserHasRentsAsync(string userId)
        {
            ApplicationUser? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return false;
            }

            return user.RentedHouses.Any();
        }
    }
}

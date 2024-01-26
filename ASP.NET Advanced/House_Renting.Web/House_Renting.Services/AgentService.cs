namespace House_Renting.Services
{
    using House_Renting.Data;
    using House_Renting.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext dbContext;

        public AgentService(HouseRentingDbContext context)
        {
            dbContext = context;
        }

        public async Task<bool> AgentExistByUserId(string userId)
        {
            var result = await dbContext
                .Agents
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }
    }
}

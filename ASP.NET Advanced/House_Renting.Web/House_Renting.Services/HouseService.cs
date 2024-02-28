namespace House_Renting.Services
{
    using House_Renting.Data;
    using House_Renting.Data.Models;
    using House_Renting.Services.Interfaces;
    using House_Renting.Web.ViewModels.Home;
    using House_Renting.Web.ViewModels.House;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext dbContext;

        public HouseService(HouseRentingDbContext context)
        {
            dbContext = context;
        }

        public async Task CreateAsync(HouseFormModel formModel,string agentId)
        {
            var newHouse = new House()
            {
                Title = formModel.Title,
                Address = formModel.Address,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                PricePerMonth = formModel.PricePerMonth,
                CategoryId = formModel.CategoryId,
                AgentId = Guid.Parse(agentId),
            };

            await dbContext.Houses.AddAsync(newHouse);
            await dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<IndexViewModel>> LastFreeHousesAsync()
        {
            var lastThreeHouses = await dbContext
                .Houses.
                OrderByDescending(h => h.CreatedOn)
                .Take(3)
                .Select(h => new IndexViewModel()
                {
                    Id = h.Id.ToString(),
                    Title = h.Title,
                    ImageUrl = h.ImageUrl,
                })
                .ToListAsync();


            return lastThreeHouses;
        }
    }
}
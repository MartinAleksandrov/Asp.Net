namespace House_Renting.Services
{
    using House_Renting.Data;
    using House_Renting.Data.Models;
    using House_Renting.Services.DataModels.House;
    using House_Renting.Services.Interfaces;
    using House_Renting.Web.ViewModels.Home;
    using House_Renting.Web.ViewModels.House;
    using House_Renting.Web.ViewModels.House.Enums;
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

        public async Task<AllHousesfilteredAndPagedServiceModel> AllAync(AllHousesQueryModel queryModel)
        {
            var housesQuery = dbContext.Houses.AsQueryable();

            if (string.IsNullOrWhiteSpace(queryModel.Category))
            {
                housesQuery = housesQuery.Where(h => h.Category.Name == queryModel.Category);
            }
            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                housesQuery = housesQuery
                    .Where(h => EF.Functions.Like(h.Title, wildCard) ||
                                EF.Functions.Like(h.Address, wildCard) ||
                                EF.Functions.Like(h.Description, wildCard));
            }

            housesQuery = queryModel.HouseSorting switch
            {
                HouseSorting.Newest => housesQuery
                    .OrderByDescending(h => h.CreatedOn),

                HouseSorting.Oldest => housesQuery
                    .OrderBy(h => h.CreatedOn),

                HouseSorting.PriceAscending => housesQuery
                    .OrderBy(h => h.PricePerMonth),

                HouseSorting.PriceDescending => housesQuery
                    .OrderByDescending(h => h.PricePerMonth),

                _ => housesQuery
                    .OrderBy(h => h.RenterId != null)
                    .ThenByDescending(h => h.CreatedOn)
            };

            var allHouses = await housesQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.HousesPerPage)
                .Take(queryModel.HousesPerPage)
                .Select(h => new HouseAllViewModel
                {
                    Id = h.Id.ToString(),
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId.HasValue
                })
                .ToArrayAsync();

            int totalHouses = housesQuery.Count();

            return new AllHousesfilteredAndPagedServiceModel()
            {
                TotalHousesCount = totalHouses,
                Houses = allHouses
            };
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
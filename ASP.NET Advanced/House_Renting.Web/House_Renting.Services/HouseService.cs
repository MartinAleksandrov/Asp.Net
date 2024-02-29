namespace House_Renting.Services
{
    using House_Renting.Data;
    using Data.Models;
    using House_Renting.Services.DataModels.House;
    using House_Renting.Services.Interfaces;
    using House_Renting.Web.ViewModels.Agent;
    using Web.ViewModels.Home;
    using Web.ViewModels.House;
    using Web.ViewModels.House.Enums;
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
                .Where(h => h.IsActive)
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

        public async Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<HouseAllViewModel> allAgentHouses = await dbContext
                .Houses
                .Where(h => h.IsActive &&
                            h.AgentId.ToString() == agentId)
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

            return allAgentHouses;
        }

        public async Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<HouseAllViewModel> allUserHouses = await dbContext
                .Houses
                .Where(h => h.IsActive &&
                            h.RenterId.HasValue &&
                            h.RenterId.ToString() == userId)
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

            return allUserHouses;
        }

        public async Task<string> CreateAsync(HouseFormModel formModel,string agentId)
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

            return newHouse.Id.ToString();
        }

        public async Task DeleteHouseByIdAsync(string houseId)
        {
            var housesToDelete = await dbContext.Houses.Where(h => h.IsActive).FirstAsync(h => h.Id.ToString() == houseId);

            housesToDelete.IsActive = false;

            await dbContext.SaveChangesAsync();

        }

        public async Task EditHouseByIdAndFormModelAsync(string houseId, HouseFormModel formModel)
        {
            House house = await dbContext
                           .Houses
                           .Where(h => h.IsActive)
                           .FirstAsync(h => h.Id.ToString() == houseId);

            house.Title = formModel.Title;
            house.Address = formModel.Address;
            house.Description = formModel.Description;
            house.ImageUrl = formModel.ImageUrl;
            house.PricePerMonth = formModel.PricePerMonth;
            house.CategoryId = formModel.CategoryId;

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string houseId)
        {
            bool result = await dbContext
                            .Houses
                            .Where(h => h.IsActive)
                            .AnyAsync(h => h.Id.ToString() == houseId);

            return result;
        }

        public async Task<HouseDetailsViewModel?> GetDetailsByIdAsync(string houseId)
        {
            House? house = await dbContext.Houses
                .Include(h => h.Category)
                .Include(h => h.Agent)
                .ThenInclude(a => a.User)
                .Where(h => h.IsActive)
                .FirstOrDefaultAsync(h => h.Id.ToString() == houseId);

            if (house == null)
            {
                return null;
            }

            return new HouseDetailsViewModel()
            {
                Id = house.Id.ToString(),
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                IsRented = house.RenterId.HasValue,
                Category = house.Category.Name,
                Agent = new AgentInfoOnHouseViewModel()
                {
                    Email = house.Agent.User.Email,
                    PhoneNumber = house.Agent.PhoneNumber
                }
            };
        }

        public async Task<HousePreDeleteDetailsViewModel> GetHouseForDeleteByIdAsync(string houseId)
        {
            House house = await dbContext
                            .Houses
                            .Where(h => h.IsActive)
                            .FirstAsync(h => h.Id.ToString() == houseId);

            return new HousePreDeleteDetailsViewModel
            {
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl
            };
        }

        public async Task<HouseFormModel> GetHouseForEditByIdAsync(string houseId)
        {
            House house = await dbContext
                          .Houses
                          .Include(h => h.Category)
                          .Where(h => h.IsActive)
                          .FirstAsync(h => h.Id.ToString() == houseId);

            return new HouseFormModel
            {
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                CategoryId = house.CategoryId,
            };
        }

        public async Task<bool> IsAgentWithIdOwnerOfHouseWithIdAsync(string houseId, string agentId)
        {
            var house = await dbContext.Houses.Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == houseId);

            return house.AgentId.ToString() == agentId;
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
namespace House_Renting.Services
{
    using House_Renting.Data;
    using House_Renting.Services.Interfaces;
    using House_Renting.Web.ViewModels.Category;
    using House_Renting.Web.ViewModels.House;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly HouseRentingDbContext dbContext;


        public CategoryService(HouseRentingDbContext context)
        {
            dbContext = context;
        }

        public async Task<bool> ExistById(int id)
        {
            var result = await dbContext.Categories.AnyAsync(c => c.Id == id);

            return result;
        }
        public async Task<IEnumerable<CategoryViewModel>> AllCategoriesAsync()
        {
            var allCategories = await dbContext.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();

            return allCategories;
        }
    }
}

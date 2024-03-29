﻿namespace House_Renting.Services.Interfaces
{
    using House_Renting.Web.ViewModels.Category;
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> AllCategoriesAsync();
        Task<bool> ExistById(int id);
        Task<IEnumerable<string>> AllCategoryNamesAsync();
    }
} 
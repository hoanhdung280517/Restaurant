using RSSolution.ViewModels.Catalog.MealCategories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.MealCategories
{
    public interface IMealCategoryService
    {
        Task<List<MealCategoryVm>> GetAll(string languageId);

        Task<MealCategoryVm> GetById(string languageId, int id);
        Task<int> CreateMealCategory(MealCategoryCreateRequest request);
        Task<int> DeleteMealCategory(int mealCotegoryId);
        Task<int> UpdateMealCategory(MealCategoryUpdateRequest request);
    }
}
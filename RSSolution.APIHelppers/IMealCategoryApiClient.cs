using RSSolution.ViewModels.Catalog.MealCategories;
using RSSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSSolution.APIHelpers
{
    public interface IMealCategoryApiClient
    {
        Task<List<MealCategoryVm>> GetAll(string languageId);
        Task<MealCategoryVm> GetById(string languageId, int id);
        Task<bool> CreateMealCategory(MealCategoryCreateRequest request);
        Task<bool> UpdateMealCategory(MealCategoryUpdateRequest request);
        Task<bool> DeleteMealCategory(int id);
    }
}
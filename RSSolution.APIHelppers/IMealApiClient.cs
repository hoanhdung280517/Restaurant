using RSSolution.ViewModels.Catalog.Meals;
using RSSolution.ViewModels.Common;
using RSSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSSolution.APIHelpers
{
    public interface IMealApiClient
    {
        Task<PagedResult<MealVm>> GetPagings(GetManageMealPagingRequest request);

        Task<bool> CreateMeal(MealCreateRequest request);

        Task<bool> UpdateMeal(MealUpdateRequest request);
        Task<bool> UpdatePrice(MealUpdatePriceRequest request);

        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

        Task<MealVm> GetById(int id, string languageId);

        Task<List<MealVm>> GetFeaturedMeals(string languageId, int take);

        Task<List<MealVm>> GetLatestMeals(string languageId, int take);

        Task<bool> DeleteMeal(int id);
    }
}
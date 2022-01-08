using RSSolution.ViewModels.Catalog.MealImages;
using RSSolution.ViewModels.Catalog.Meals;
using RSSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Meals
{
    public interface IMealService
    {
        Task<int> Create(MealCreateRequest request);

        Task<int> Update(MealUpdateRequest request);

        Task<int> Delete(int mealId);

        Task<MealVm> GetById(int mealId, string languageId);

        Task<bool> UpdatePrice(MealUpdatePriceRequest request);

        Task AddViewcount(int mealId);

        Task<PagedResult<MealVm>> GetAllPaging(GetManageMealPagingRequest request);

        Task<int> AddImage(int mealId, MealImageCreateRequest request);

        Task<int> RemoveImage(int imageId);

        Task<int> UpdateImage(int imageId, MealImageUpdateRequest request);

        Task<MealImageViewModel> GetImageById(int imageId);

        Task<List<MealImageViewModel>> GetListImages(int productId);

        Task<PagedResult<MealVm>> GetAllByCategoryId(string languageId, GetPublicMealPagingRequest request);

        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

        Task<List<MealVm>> GetFeaturedMeals(string languageId, int take);

        Task<List<MealVm>> GetLatestMeals(string languageId, int take);
    }
}
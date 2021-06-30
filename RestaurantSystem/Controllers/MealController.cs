using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Models;
using RSSolution.APIHelpers;
using RSSolution.ViewModels.Catalog.Meals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealApiClient _mealApiClient;
        private readonly IMealCategoryApiClient _mealCategoryApiClient;

        public MealController(IMealApiClient mealApiClient, IMealCategoryApiClient mealCategoryApiClient)
        {
            _mealApiClient = mealApiClient;
            _mealCategoryApiClient = mealCategoryApiClient;
        }

        public async Task<IActionResult> Detail(int id, string culture)
        {
            var meal = await _mealApiClient.GetById(id, culture);
            return View(new MealDetailViewModel()
            {
                Meal = meal
            });
        }

        public async Task<IActionResult> MealCategory(int id, string culture, int page = 1)
        {
            var meals = await _mealApiClient.GetPagings(new GetManageMealPagingRequest()
            {
                MealCategoryId = id,
                PageIndex = page,
                LanguageId = culture,
                PageSize = 10
                
            });
            return View(new MealCategoryViewModel()
            {
                MealCategory = await _mealCategoryApiClient.GetById(culture, id),
                Meals = meals
            }); 
        }
    }
}

using RSSolution.APIHelpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using RSSolution.ViewModels.Catalog.MealCategories;
using RestaurantSystem.Models;
using RSSolution.ViewModels.Catalog.Table;

namespace RestaurantSystem.Controllers.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IMealCategoryApiClient _mealCategoryApiClient;

        public SideBarViewComponent(IMealCategoryApiClient mealCategoryApiClient)
        {
            _mealCategoryApiClient = mealCategoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(int tableId)
        {
            var items = await _mealCategoryApiClient.GetAll(CultureInfo.CurrentCulture.Name);
            var request = new ViewModel()
            {
                tableId = tableId,
                mealcategory = items,
            };
            return View(request);
        }
    }
}
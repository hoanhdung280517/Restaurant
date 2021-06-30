using RSSolution.APIHelpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Controllers.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IMealCategoryApiClient _mealCategoryApiClient;

        public SideBarViewComponent(IMealCategoryApiClient mealCategoryApiClient)
        {
            _mealCategoryApiClient = mealCategoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _mealCategoryApiClient.GetAll(CultureInfo.CurrentCulture.Name);
            return View(items);
        }
    }
}
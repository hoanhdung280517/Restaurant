using RSSolution.ViewModels.Catalog.MealCategories;
using RSSolution.ViewModels.Catalog.Meals;
using RSSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
{
    public class MealCategoryViewModel
    {
        public MealCategoryVm MealCategory { get; set; }

        public PagedResult<MealVm> Meals { get; set; }
    }
}
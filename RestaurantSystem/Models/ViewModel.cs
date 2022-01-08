using RSSolution.ViewModels.Catalog.MealCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
{
    public class ViewModel
    {
        public int tableId { get; set; }
        public List<MealCategoryVm> mealcategory { get; set; }
    }
}

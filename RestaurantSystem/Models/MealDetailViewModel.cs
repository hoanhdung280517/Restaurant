using RSSolution.ViewModels.Catalog.MealCategories;
using RSSolution.ViewModels.Catalog.MealImages;
using RSSolution.ViewModels.Catalog.Meals;
using RSSolution.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
{
    public class MealDetailViewModel
    {
        public int TableId { get; set; }
        public MealCategoryVm MealCategory { get; set; }

        public MealVm Meal { get; set; }

        public List<MealVm> RelatedMeals { get; set; }

        public List<MealImageViewModel> MealImages { get; set; }
    }
}
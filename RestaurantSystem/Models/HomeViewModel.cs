using RSSolution.ViewModels.Catalog.Meals;
using RSSolution.ViewModels.Catalog.Table;
using RSSolution.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
{
    public class HomeViewModel
    {
        public int TableId { get; set; }
        public List<SlideVm> Slides { get; set; }

        public List<MealVm> FeaturedMeals { get; set; }

        public List<MealVm> LatestMeals { get; set; }
    }
}
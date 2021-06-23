using RSSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.ViewModels.Catalog.Meals
{
    public class CategoryAssignRequest
    {
        public int Id { get; set; }
        public List<SelectItem> MealCategories { get; set; } = new List<SelectItem>();
    }
}
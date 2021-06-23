using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.ViewModels.Catalog.MealCategories
{
    public class MealCategoryVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }
    }
}
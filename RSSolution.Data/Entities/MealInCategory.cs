using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class MealInCategory
    {
        public int MealId { get; set; }

        public Meal Meal { get; set; }

        public int MealCategoryId { get; set; }

        public MealCategory MealCategory { get; set; }
    }
}

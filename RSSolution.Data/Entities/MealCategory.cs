using RSSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class MealCategory
    {
        public int Id { set; get; }
        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public int? ParentId { set; get; }
        public Status Status { set; get; }

        public List<MealInCategory> MealInCategories { get; set; }

        public List<MealCategoryTranslation> MealCategoryTranslations { get; set; }

    }
}

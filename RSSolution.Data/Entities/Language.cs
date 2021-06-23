using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class Language
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public List<MealTranslation> MealTranslations { get; set; }

        public List<MealCategoryTranslation> MealCategoryTranslations   { get; set; }
    }
}

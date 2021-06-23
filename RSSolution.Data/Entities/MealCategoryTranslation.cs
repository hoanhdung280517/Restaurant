using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class MealCategoryTranslation
    {
        public int Id { set; get; }
        public int MealCategoryId { set; get; }
        public string Name { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string LanguageId { set; get; }
        public string SeoAlias { set; get; }

        public MealCategory MealCategory { get; set; }

        public Language Language { get; set; }

    }
}

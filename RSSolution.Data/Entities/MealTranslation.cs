using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class MealTranslation
    {
        public int Id { set; get; }
        public int MealId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }

        public Meal Meal { get; set; }

        public Language Language { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Catalog.MealCategories
{
    public class MealCategoryCreateRequest
    {
        public string Name { set; get; }
        public bool IsShowOnHome { set; get; }
        public int SortOrder { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string LanguageId { set; get; }
        public string SeoAlias { set; get; }
    }
}

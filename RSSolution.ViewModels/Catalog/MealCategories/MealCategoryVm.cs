using RSSolution.ViewModels.Catalog.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.ViewModels.Catalog.MealCategories
{
    public class MealCategoryVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { set; get; }
        public int? ParentId { get; set; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public bool IsShowOnHome { set; get; }
    }
}
using RSSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.ViewModels.Catalog.Meals
{
    public class GetManageMealPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public string LanguageId { get; set; }

        public int? MealCategoryId { get; set; }
    }
}
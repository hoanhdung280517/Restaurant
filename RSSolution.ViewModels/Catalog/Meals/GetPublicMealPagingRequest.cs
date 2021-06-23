using RSSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.ViewModels.Catalog.Meals
{
    public class GetPublicMealPagingRequest : PagingRequestBase
    {
        public int? MealCategoryId { get; set; }
    }
}
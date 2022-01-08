using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.ViewModels.Catalog.Meals
{
    public class MealUpdatePriceRequest
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
    }
}
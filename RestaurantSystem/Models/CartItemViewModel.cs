using RSSolution.ViewModels.Catalog.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
{
    public class CartItemViewModel
    {
        public int TableId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int MealId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }
    }
}
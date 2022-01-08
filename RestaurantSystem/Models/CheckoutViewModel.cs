using RSSolution.ViewModels.Catalog.Promotions;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
{
    public class CheckoutViewModel
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int DiscountPercent { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
        public CheckoutRequest CheckoutModel { get; set; }
    }
}
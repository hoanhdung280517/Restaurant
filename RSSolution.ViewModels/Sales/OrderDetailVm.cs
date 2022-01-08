using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.ViewModels.Sales
{
    public class OrderDetailVm
    {

        public int MealId { get; set; }
        public string MealName { get; set; }
        public int PromotionId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public string Description { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class OrderDetail
    {

        public int OrderId { set; get; }
        public int MealId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Order Order { get; set; }

        public Meal Meal { get; set; }

    }
}

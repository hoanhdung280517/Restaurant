using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class Cart
    {
        public int Id { set; get; }
        public int MealId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public int TableId { get; set; }
        public Table Table { get; set; }
        public Guid UserId { get; set; }

        public Meal Meal { get; set; }

        public DateTime DateCreated { get; set; }

        public AppUser AppUser { get; set; }
    }
}

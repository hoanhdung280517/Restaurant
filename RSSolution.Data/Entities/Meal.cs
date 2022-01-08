using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class Meal
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreated { set; get; }
        public bool? IsFeatured { get; set; }

        public List<MealInCategory> MealInCategories { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public List<Cart> Carts { get; set; }

        public List<MealTranslation> MealTranslations { get; set; }

        public List<MealImage> MealImages { get; set; }
        public List<Comment> comments { get; set; }
    }
}
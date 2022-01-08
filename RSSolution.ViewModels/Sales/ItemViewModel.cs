using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Sales
{
    public class ItemViewModel
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

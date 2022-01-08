using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Sales
{
    public class OrderIdetailtemvm
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public int MealId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

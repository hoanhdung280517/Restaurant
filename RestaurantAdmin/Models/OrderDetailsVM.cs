using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAdmin.Models
{
    public class OrderDetailsVM
    {
        public List<OrderDetailVm> orderdetails { get; set; }
        public int DiscountPercent { get; set; }
    }
}

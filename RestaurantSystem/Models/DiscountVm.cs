using RSSolution.Data.Enums;
using RSSolution.ViewModels.Catalog.Promotions;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
{
    public class DiscountVM
    {
        public int Id { get; set; }
        public string Code { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public int DiscountPercent { set; get; }
        public string Status { set; get; }
    }
}
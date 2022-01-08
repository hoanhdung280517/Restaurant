using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Catalog.Promotions
{
    public class PromotionCreateRequest
    {
        public int Id { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public int DiscountPercent { set; get; }
        public string Code { set; get; }
    }
}

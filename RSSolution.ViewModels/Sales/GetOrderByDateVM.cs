using RSSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Sales
{
    public class GetOrderByDateVM
    {
        public int Id { set; get; }
        public int PromotionId { get; set; }
        public int? DiscountPercent { get; set; }
        public string UserName { get; set; }
        public string Table { get; set; }
        public DateTime OrderDate { set; get; }
        public int TableId { get; set; }
        public OrderStatus Status { set; get; }
        public Guid UserId { get; set; }
    }
}

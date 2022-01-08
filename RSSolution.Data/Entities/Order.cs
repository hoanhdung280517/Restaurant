using RSSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
   public class Order
    {

        public int Id { set; get; }
        public int PromotionId { get;set; }
        public DateTime OrderDate { set; get; }
        public int TableId { get; set; }
        public OrderStatus Status { set; get; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}

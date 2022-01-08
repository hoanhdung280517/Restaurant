using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RSSolution.ViewModels.Sales
{
    public class CheckoutRequest
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int PromotionId { get; set; }
        public List<OrderDetailVm> OrderDetail { set; get; } = new List<OrderDetailVm>();
    }
}
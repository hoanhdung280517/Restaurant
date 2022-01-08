using RSSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class Promotions
    {
        public int Id { set; get; }
        public string Code { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public int DiscountPercent { set; get; }
        public Status Status { set; get; }
 
    }
}

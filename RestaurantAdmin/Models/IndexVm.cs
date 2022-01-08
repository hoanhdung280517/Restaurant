using RSSolution.ViewModels.Catalog.Contacts;
using RSSolution.ViewModels.Sales;
using RSSolution.ViewModels.System.Users;
using System.Collections.Generic;

namespace RestaurantAdmin.Models
{
    public class IndexVm
    {
        public List<ContactVm> contactViews { get; set; }
        public List<OrderDetailVm> orderDetailVms { get; set; }
        public List<UserVm> userVms { get; set; }
    }
}

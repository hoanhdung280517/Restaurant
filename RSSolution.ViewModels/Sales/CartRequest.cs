using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Sales
{
    public class CartRequest
    {
        public int Id { get; set; }      
        public int TableId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<ItemViewModel> CartItems { get; set; } = new List<ItemViewModel>();
    }
}

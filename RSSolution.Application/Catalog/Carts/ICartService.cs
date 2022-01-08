using RSSolution.Data.Entities;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Carts
{
    public interface ICartService
    {
        Task<int> Add(CartRequest request);
        Task<int> ChangeTable(ChangeTableRequest request);
        Task<int> MergeTable(MergeTableRequest request);
        Task<List<CartViewModel>> GetCartById(int tableId);
        Task<int> DeleteCart(int tableId);
    }
}

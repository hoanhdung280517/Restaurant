using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.APIHelppers
{
    public interface ICartApiClient
    {
        Task<bool> Add(CartRequest request);
        Task<bool> ChangeTable(ChangeTableRequest request);
        Task<bool> MergeTable(MergeTableRequest request);
        Task<List<CartViewModel>> GetCartById(int tableId);
        Task<bool> DeleteCart(int tableId);
    }
}

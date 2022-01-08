using RSSolution.ViewModels.Common;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.APIHelpers
{
    public interface IOrderApiClient
    {
        Task<bool> Create(CheckoutRequest request);
        Task<List<OrderViewModel>> GetAll();
        Task<List<OrderDetailVm>> GetAllOrderDetails();
        Task<List<OrderDetailVm>> GetById(int orderId);
        Task<List<GetOrderByDateVM>> GetByDate(TimeRequest time);
    }
}
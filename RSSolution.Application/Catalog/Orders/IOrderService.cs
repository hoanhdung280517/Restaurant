using RSSolution.Data.Entities;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Orders
{
    public interface IOrderService
    {
        Task<int> Create(CheckoutRequest order);
        Task<List<OrderViewModel>> GetAll();
        Task<List<OrderDetailVm>> GetOrderById(int orderId);
        Task<List<OrderDetailVm>> GetAllOrderDetails();
        Task<List<GetOrderByDateVM>> GetByDate(TimeRequest time);
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RSSolution.Data.EF;
using RSSolution.Data.Entities;
using RSSolution.Data.Enums;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Orders
{   
    public class OrderService: IOrderService
    {
        private readonly RSDbContext _context;
        public OrderService( RSDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderViewModel>> GetAll()
        {
            var order = await _context.Orders
                 .Select(x => new OrderViewModel()
                 {
                     Id = x.Id,
                     OrderDate = x.OrderDate,
                     Status = x.Status,
                     TableId = x.TableId,
                     UserId = x.UserId,
                     PromotionId = x.PromotionId                   
                 }).ToListAsync();
            return order;
        }
        public async Task<int> Create(CheckoutRequest request)
            {
                if (request == null)
                    throw new Exception("Order cannot be null");
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            var userid = user.Id;
            var order = new Order();
            {
                order.OrderDate = DateTime.Now;
                order.Status = OrderStatus.Success;
                order.TableId = request.TableId;
                order.UserId = userid;
                order.PromotionId = request.PromotionId;
                order.OrderDetails = new List<OrderDetail>();            
                foreach (var item in request.OrderDetail)
                {
                    order.OrderDetails.Add(new OrderDetail()
                    {
                        TableId= request.TableId,
                        MealId = item.MealId,
                        Price = item.Price,
                        Quantity = item.Quantity,                          
                    });
                }
            };
            _context.Orders.Add(order);                              
            await _context.SaveChangesAsync();
            return order.Id;
        }
        public async Task<List<OrderDetailVm>> GetOrderById(int orderId)
        {
            var orderdetail = await _context.OrderDetails
                 .Select(x => new OrderIdetailtemvm()
                 {                     
                     OrderId = x.OrderId,
                     TableId = x.TableId,
                     MealId = x.MealId,
                     Price = x.Price,
                     Quantity = x.Quantity
                 }).ToListAsync();
            var itemorderdetail = new List<OrderDetailVm>();
            foreach (var item in orderdetail)
            {
                if (item.OrderId == orderId)
                {
                    var meal = _context.MealTranslations.FirstOrDefault(u => u.MealId == item.MealId);
                    var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
                    var vm = new OrderDetailVm()
                    {
                        PromotionId = order.PromotionId,
                        MealName = meal.Name,
                        MealId = item.MealId,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Description = meal.Description,
                    };
                     itemorderdetail.Add(vm);                  
                }
            }
            return itemorderdetail;
            
        }
        public async Task<List<GetOrderByDateVM>> GetByDate(TimeRequest time)
        {
            var order = await _context.Orders
                 .Select(x => new  OrderViewModel()
                 {
                     Id = x.Id,
                     OrderDate = x.OrderDate,
                     Status = x.Status,
                     TableId = x.TableId,
                     UserId = x.UserId,
                     PromotionId = x.PromotionId
                 }).ToListAsync();
            var orderdate = new List<GetOrderByDateVM>();
            foreach (var item in order)
            {
                if (time.Fromdate <= item.OrderDate && time.Todate >= item.OrderDate)
                {
                    var vm = new GetOrderByDateVM()
                    {
                        Id = item.Id,
                        OrderDate = item.OrderDate,
                        PromotionId = item.PromotionId,
                        Status = item.Status,
                        TableId = item.TableId,
                        UserId = item.UserId
                    };
                    orderdate.Add(vm);
                }
            }
            return orderdate;

        }
        public async Task<List<OrderDetailVm>> GetAllOrderDetails()
        {
            var orderdetail = await _context.OrderDetails
                 .Select(x => new OrderDetailVm()
                 {
                     OrderId = x.OrderId,
                     MealId = x.MealId,
                     Price = x.Price,
                     Quantity = x.Quantity
                 }).ToListAsync();
            return orderdetail;

        }
    }
}

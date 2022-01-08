using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAdmin.Models;
using RSSolution.APIHelpers;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAdmin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderApiClient _orderApiClient;
        private readonly IPromotionApiClient _promotionApiClient;
        private readonly ITableApiClient _tableApiClient;
        private readonly IUserApiClient _userApiClient;
        public OrderController(
            IUserApiClient userApiClient,
           IOrderApiClient orderApiClient,
           IPromotionApiClient promotionApiClient,
           ITableApiClient tableApiClient)
        {
            _orderApiClient = orderApiClient;
            _promotionApiClient = promotionApiClient;
            _tableApiClient = tableApiClient;
            _userApiClient = userApiClient;
        }
        public async Task<IActionResult> Index()
        {
            var order = await _orderApiClient.GetAll();
            var ordervm = new List<OrderViewModel>();
            {
                foreach(var item in order)
                {
                    if (item.PromotionId == 0)
                    {
                        var users = await _userApiClient.GetById(item.UserId);
                        var tables = await _tableApiClient.GetById(item.TableId);
                        var vms = new OrderViewModel()
                        {
                            Id = item.Id,
                            OrderDate = item.OrderDate,
                            DiscountPercent = 0,
                            PromotionId = item.PromotionId,
                            Status = item.Status,
                            Table = tables.Name,
                            UserName = users.ResultObj.UserName,
                            TableId = item.TableId,
                            UserId = item.UserId
                        };
                        ordervm.Add(vms);

                    }
                    else
                    {
                        var percent = await _promotionApiClient.GetById(item.PromotionId);
                        var user = await _userApiClient.GetById(item.UserId);
                        var table = await _tableApiClient.GetById(item.TableId);
                        var vm = new OrderViewModel()
                        {
                            Id = item.Id,
                            OrderDate = item.OrderDate,
                            DiscountPercent = percent.DiscountPercent,
                            PromotionId = item.PromotionId,
                            Status = item.Status,
                            Table = table.Name,
                            UserName = user.ResultObj.UserName,
                            TableId = item.TableId,
                            UserId = item.UserId
                        };
                        ordervm.Add(vm);
                    }          
                }              
            }
            if (TempData["Order"] != null)
            {
                ViewBag.Success = TempData["Order"];
            }
            return View(ordervm);
        }
        [HttpPost]
        public async Task<IActionResult> Index(DateTime fromdate, DateTime todate)
        {
            var Fromdate = fromdate.ToShortDateString();
            var Todate = todate.ToShortDateString();
            if (Fromdate == "01/01/0001" && Todate == "01/01/0001")
            {
                TempData["Order"] = "Can't find order";
                return RedirectToAction("Index");
            }
            var date = new TimeRequest()
            {
                Fromdate = fromdate,
                Todate = todate,
            };
            var orderdate = await _orderApiClient.GetByDate(date);
            var ordervm = new List<OrderViewModel>();
            {
                foreach (var item in orderdate)
                {
                    if (item.PromotionId == 0)
                    {
                        var users = await _userApiClient.GetById(item.UserId);
                        var tables = await _tableApiClient.GetById(item.TableId);
                        var vms = new OrderViewModel()
                        {
                            Id = item.Id,
                            OrderDate = item.OrderDate,
                            DiscountPercent = 0,
                            PromotionId = item.PromotionId,
                            Status = item.Status,
                            Table = tables.Name,
                            UserName = users.ResultObj.UserName,
                            TableId = item.TableId,
                            UserId = item.UserId
                        };
                        ordervm.Add(vms);

                    }
                    else
                    {
                        var percent = await _promotionApiClient.GetById(item.PromotionId);
                        var user = await _userApiClient.GetById(item.UserId);
                        var table = await _tableApiClient.GetById(item.TableId);
                        var vm = new OrderViewModel()
                        {
                            Id = item.Id,
                            OrderDate = item.OrderDate,
                            DiscountPercent = percent.DiscountPercent,
                            PromotionId = item.PromotionId,
                            Status = item.Status,
                            Table = table.Name,
                            UserName = user.ResultObj.UserName,
                            TableId = item.TableId,
                            UserId = item.UserId
                        };
                        ordervm.Add(vm);
                    }
                }
            }
            return View(ordervm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int orderId)
        {
            var result = await _orderApiClient.GetById(orderId);
            var orderdetailsvm = new OrderDetailsVM();
            {
                foreach(var item in result)
                {
                    if(item.PromotionId != 0)
                    {
                        var promotion = await _promotionApiClient.GetById(item.PromotionId);
                        orderdetailsvm.DiscountPercent = promotion.DiscountPercent;
                    }
                    else
                    {
                        orderdetailsvm.DiscountPercent = 0;
                    }
                    
                }
                orderdetailsvm.orderdetails = result;
            };
            return View(orderdetailsvm);
        }
    }
}

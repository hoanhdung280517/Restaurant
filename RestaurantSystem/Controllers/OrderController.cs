using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestaurantSystem.Models;
using RSSolution.APIHelpers;
using RSSolution.APIHelppers;
using RSSolution.Data.Enums;
using RSSolution.Utilities.Constants;
using RSSolution.ViewModels.Catalog.Promotions;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ITableApiClient _tableApiClient;
        private readonly IOrderApiClient _orderApiClient;
        private readonly ICartApiClient _cartApiClient;
        private readonly IPromotionApiClient _promotionApiClient;


        public OrderController( 
            ITableApiClient tableApiClient,
            IOrderApiClient orderApiClient,
            ICartApiClient cartApiClient,
            IPromotionApiClient promotionApiClient)
        {
            _promotionApiClient = promotionApiClient;
            _cartApiClient = cartApiClient;
            _orderApiClient = orderApiClient;
            _tableApiClient = tableApiClient;
        }
        public async Task<IActionResult> Table()
        {
            var table = await _tableApiClient.GetAll();
            return View(table.ToList());
        }
        public IActionResult Error()
        {
            var messege = TempData["ErrorMsg"] = "Table isn't order";
            return View();
        }
        public IActionResult Error1()
        {
            var messege = TempData["ErrorMsg"] = "Table have order , You can't change table";
            return View();
        }
        public async Task<IActionResult> Checkout(int tableId)
        {
            var promotion = await _promotionApiClient.GetAll();
            var cart = await _cartApiClient.GetCartById(tableId);
            var cartitem = new List<CartItemViewModel>();
            {
                foreach (var item in cart)
                {
                   var list = new CartItemViewModel()
                   {
                        Email = item.Email,
                        UserName = item.UserName,
                        Name = item.UserName,
                        TableId = tableId,
                        MealId = item.MealId,
                        Image = item.Image,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Description = item.Description,
                   };
                    cartitem.Add(list);  
                };
            }
            var checkout = new CheckoutViewModel();
            {
                foreach (var item in cartitem)
                {
                    var tablename = await _tableApiClient.GetById(item.TableId);
                    checkout.Name = item.UserName;
                    checkout.Email = item.Email;
                    checkout.TableId = item.TableId;
                    checkout.TableName = tablename.Name;
                }
                checkout.CartItems = cartitem;
            }
            if (cart.Count == 0)
            {
                return RedirectToAction("Error");
            }
            ViewBag.Promotion = promotion.Select(x => new SelectListItem()
            {
                Text = x.DiscountPercent.ToString(),
                Value = x.Id.ToString(),              
            });
            return View(checkout);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel request,int PromotionId)
        {
            var cart = await _cartApiClient.GetCartById(request.TableId);            
            var orderDetails = new List<OrderDetailVm>();
            foreach (var item in cart)
            {
                orderDetails.Add(new OrderDetailVm()
                {
                    MealId = item.MealId,
                    MealName = item.Name,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    
                });
            }
            var checkoutRequest = new CheckoutRequest()
            {
                TableId = request.TableId,
                UserName = request.Name,
                Email = request.Email,
                PromotionId = PromotionId,
                OrderDetail = orderDetails,
            };
            var result = await _orderApiClient.Create(checkoutRequest); 
            if (result == true)
            {
                await _cartApiClient.DeleteCart(checkoutRequest.TableId);
                return RedirectToAction("Successes");
            }
            ModelState.AddModelError("", "Checkout failed");
            return RedirectToAction("Checkout");
        }
        public IActionResult Successes()
        {
            TempData["SuccessMsg"] = "Checkout successful";
            return View();
        }
        public async Task<IActionResult> ChangeTable(int tableId)
        {
            var table = await _tableApiClient.GetAll();
            var cart = await _cartApiClient.GetCartById(tableId);
            var cartitem = new List<CartItemViewModel>();
            {
                foreach (var item in cart)
                {
                    var list = new CartItemViewModel()
                    {
                        Email = item.Email,
                        UserName = item.UserName,
                        Name = item.UserName,
                        TableId = tableId,
                        MealId = item.MealId,
                        Image = item.Image,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Description = item.Description,
                    };
                    cartitem.Add(list);
                };
            }
            var checkout = new CheckoutViewModel();
            {
                foreach (var item in cartitem)
                {
                    var tablename = await _tableApiClient.GetById(item.TableId);
                    checkout.Name = item.UserName;
                    checkout.Email = item.Email;
                    checkout.TableId = item.TableId;
                    checkout.TableName = tablename.Name;
                }
                checkout.CartItems = cartitem;
            }
            if (cart.Count == 0)
            {
                return RedirectToAction("Error");
            }
            ViewBag.Table = table.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            return View(checkout);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeTable(CheckoutViewModel request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var changetable = new ChangeTableRequest()
            {
                Email = request.Email,
                TableId = request.TableId,            
            };
            var result = await _cartApiClient.ChangeTable(changetable);
            if (result)
            {
                return RedirectToAction("Table", "Order");
            }
            return RedirectToAction("Error1");
        }
        public async Task<IActionResult> MergeTable(int tableId)
        {
            var table = await _tableApiClient.GetAll();
            var cart = await _cartApiClient.GetCartById(tableId);
            var cartitem = new List<CartItemViewModel>();
            {
                foreach (var item in cart)
                {
                    var list = new CartItemViewModel()
                    {
                        Email = item.Email,
                        UserName = item.UserName,
                        Name = item.UserName,
                        TableId = tableId,
                        MealId = item.MealId,
                        Image = item.Image,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Description = item.Description,
                    };
                    cartitem.Add(list);
                };
            }
            var checkout = new CheckoutViewModel();
            {
                foreach (var item in cartitem)
                {
                    var tablename = await _tableApiClient.GetById(item.TableId);
                    checkout.Name = item.UserName;
                    checkout.Email = item.Email;
                    checkout.TableName = tablename.Name;
                    checkout.TableId = item.TableId;
                }
                checkout.CartItems = cartitem;
            }
            if (cart.Count == 0)
            {
                return RedirectToAction("Error");
            }
            ViewBag.Table = table.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            return View(checkout);
        }
        [HttpPost]
        public async Task<IActionResult> MergeTable(int TableId, string UserName)
        {
            var mergeTableRequest = new MergeTableRequest()
            {
                TableId = TableId,
                UserName = UserName,
            };
            var result = await _cartApiClient.MergeTable(mergeTableRequest);
            if (result)
            {
                return RedirectToAction("Table", "Order");
            }
            return RedirectToAction("MergeTable");
        } 
        [HttpPost]
        public async Task<IActionResult> FindPromotion(int Id)
        {
            if(Id != 0) { 
            var promotion = await _promotionApiClient.GetById(Id);
            var discount = new List<CheckoutViewModel>();
            {
                discount.Add(new CheckoutViewModel() 
                {
                    DiscountPercent = promotion.DiscountPercent,
                });
                
            };
                return Ok(discount);
            }
            var discounts = new List<CheckoutViewModel>();
            {
                discounts.Add(new CheckoutViewModel()
                {
                    DiscountPercent = 0,
                });

            };
            return Ok(discounts);
        }
        [HttpPost]
        public async Task<IActionResult> CheckCode (string code)
        {
            var promotion = await _promotionApiClient.GetAll();
            var discount = new DiscountVM();
            foreach(var item in promotion)
            {
                if(item.Code == code)
                {
                    discount.Id = item.Id;
                    discount.DiscountPercent = item.DiscountPercent;
                    discount.ToDate = item.ToDate;
                    discount.Status = "Still";
                }
            }
            if(discount.ToDate >= DateTime.Now)
            {
                return Ok(discount);
            }
            var discounts = new DiscountVM()
            {
                Status = "Over"
            };           
            return Ok(discounts);
            
        }
    }
}

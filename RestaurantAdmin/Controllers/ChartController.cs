using Microsoft.AspNetCore.Mvc;
using RSSolution.APIHelpers;
using RSSolution.APIHelppers;
using RSSolution.Data.Enums;
using RSSolution.ViewModels.Catalog.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAdmin.Controllers
{
    [Route("api/chart")]
    [ApiController]
    public class ChartController : Controller
    {
        private readonly IPDApiClient _pDApiClient;
        private readonly IUserApiClient _userApiClient;
        private readonly IBookTableApiClient _bookTableApiClient;
        private readonly IOrderApiClient _orderApiClient;
        private readonly IPromotionApiClient _promotionApiClient;
        public ChartController(IPDApiClient pDApiClient,
            IUserApiClient userApiClient,
            IBookTableApiClient bookTableApiClient,
            IOrderApiClient orderApiClient,
            IPromotionApiClient promotionApiClient)
        {
            _orderApiClient = orderApiClient;
            _promotionApiClient = promotionApiClient;
            _pDApiClient = pDApiClient;
            _userApiClient = userApiClient;
             _bookTableApiClient = bookTableApiClient;
        }
        [HttpGet("checkout_discountpercent")]
        [Produces("application/json")]
        public async Task<IActionResult> Checkout_DiscountPercent()
        {
            try
            {
                int total = 0;
                var checkout = await _orderApiClient.GetAll();
                var promotion = await _promotionApiClient.GetAll();
                List<Chart_Checkout_Discount> statistics = new List<Chart_Checkout_Discount>();
                foreach (var item in promotion)
                {
                    var checkoutpm = checkout.Where(x => x.PromotionId == item.Id).Select(x => x.Id).ToList();
                    var totalcheckoutpercent = checkoutpm.Count();
                    var temp = new Chart_Checkout_Discount()
                    {
                        DiscounPercent = item.DiscountPercent.ToString(),
                        TotalDiscountPercent = totalcheckoutpercent
                    };
                    statistics.Add(temp);
                }
                foreach(var item in checkout)
                {
                    
                    if (item.PromotionId == 0)
                    {
                        total++;
                    }                    
                }
                var temps = new Chart_Checkout_Discount()
                {
                    DiscounPercent = "0",
                    TotalDiscountPercent = total
                };
                statistics.Add(temps);
                return Ok(statistics);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("user_province")]
        [Produces("application/json")]
        public async Task<IActionResult> User_Province()
        {
            try
            {
                var province = await _pDApiClient.GetAllProvince();
                var user = await _userApiClient.GetAll();
                List<Chart_User_Province> statistics = new List<Chart_User_Province>();
                foreach(var item in province)
                {
                    var userprovince = user.Where(u => u.ProvinceId == item.Id).Select(u => u.Id).ToList();
                    var totaluser = userprovince.Count();
                    var temp = new Chart_User_Province()
                    {
                        Province = item.Name,
                        TotalUser = totaluser                  
                    };
                    statistics.Add(temp);
                }
                return Ok(statistics);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("status_booktable")]
        [Produces("application/json")]
        public async Task<IActionResult> Status_Booktable()
        {
            try
            {
                var book = await _bookTableApiClient.GetAll();
                var user = await _userApiClient.GetAll();               
                List<Chart_Status_BookTable> statistics = new List<Chart_Status_BookTable>();               
                foreach(var item in user)
                {
                    if (item.UserName != "Admin")
                    { 
                        if(item.UserName != "manager")
                        {
                            int totalInprogress = 0;
                            int totalConfirmed = 0;
                            int totalSuccess = 0;
                            int totalCanceled = 0;
                            foreach (var items in book)
                            {
                                if (item.UserName == items.UserName)
                                {
                                    if (items.Status == BookTableStatus.InProgress)
                                    {
                                        totalInprogress++;
                                    }
                                    if (items.Status == BookTableStatus.Confirmed)
                                    {
                                        totalConfirmed++;
                                    }
                                    if (items.Status == BookTableStatus.Success)
                                    {
                                        totalSuccess++;
                                    }
                                    if (items.Status == BookTableStatus.Canceled)
                                    {
                                        totalCanceled++;
                                    }
                                }
                            };
                            var temp = new Chart_Status_BookTable()
                            {
                                UserName = item.UserName,
                                TotalInProgress = totalInprogress,
                                TotalConfirmed = totalConfirmed,
                                TotalCanceled = totalCanceled,
                                TotalSuccess = totalSuccess,
                            };
                            statistics.Add(temp);
                        }
                        
                    }
                };
                return Ok(statistics);

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("total_order")]
        [Produces("application/json")]
        public async Task<IActionResult> Total_Income()
        {
            try
            {
                var order = await _orderApiClient.GetAll();
                var time = new List<Time>();
                string Time = null;
                foreach (var item in order)
                {                  
                    if(item.OrderDate.Date.ToString()!= Time)
                    {
                        Time = item.OrderDate.Date.ToString();
                        time.Add(new Time()
                        {
                            time = item.OrderDate.Date.ToString()
                        });
                    }
                    
                }
                List<Chart_Total_Order> statistics = new List<Chart_Total_Order>();
                foreach(var item in time)
                {
                    var countorder = order.Where(o => o.OrderDate.Date.ToString() == item.time).Select(o => o.Id).ToList();
                    var totalorder = countorder.Count();
                    var temp = new Chart_Total_Order()
                    {
                        Date = item.time,
                        TotalOrder = totalorder
                    };
                    statistics.Add(temp);
                }
                return Ok(statistics);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSSolution.APIHelpers;
using RSSolution.Utilities.Constants;
using RestaurantSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RSSolution.Data.EF;
using RSSolution.ViewModels.Catalog.Table;
using RSSolution.ViewModels.System.Users;
using RSSolution.ViewModels.Sales;
using System.Security.Claims;
using RSSolution.APIHelppers;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantSystem.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IMealApiClient _mealApiClient;
        private readonly ITableApiClient _tableApiClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserApiClient _userApiClient;
        private readonly ICartApiClient _cartApiClient;
        public CartController(
            IUserApiClient userApiClient,
            IMealApiClient mealApiClient, 
            ITableApiClient tableApiClient,
            IHttpContextAccessor httpContextAccessor,
            ICartApiClient cartApiClient)
        {
            _cartApiClient = cartApiClient;
            _userApiClient = userApiClient;
            _mealApiClient = mealApiClient;
            _tableApiClient = tableApiClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index(TableRequest table)
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var model = new CheckoutViewModel()
            {
                TableId = table.Id,
                Name = userName,
                Email = email,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var model = GetCheckoutViewModel();
            var itemVm = new List<ItemViewModel>();
            foreach (var item in model.CartItems)
            {
                itemVm.Add(new ItemViewModel()
                {
                    MealId = item.MealId,
                    MealName = item.Name,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }
            var cartRequest = new CartRequest()
            {
                TableId = model.TableId,
                UserName = model.Name,
                Email = model.Email,
                CartItems = itemVm,
            };
            var result = await _cartApiClient.Add(cartRequest);
            if (result == true)
            {
                return RedirectToAction("Successes");

            }
            TempData["result"] = "order isn't successful";
            return View(model);
        }
        public IActionResult Successes()
        {          
            TempData["SuccessMsg"] = "Order successful";
            return View();
        }
        public IActionResult Error()
        {
            var messege = TempData["SuccessMsg"] = "Table isn't order";
            return View();
        }
        [HttpGet]
        public IActionResult GetListItems()
        {
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            return Ok(currentCart);
        }
        public async Task<IActionResult> AddToCart(int id, string languageId, int tableId)
        {
            var user = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var meal = await _mealApiClient.GetById(id, languageId);
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);     
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);

            int quantity = 1;
            if (currentCart.Any(x => x.MealId == id))
            {
                quantity = currentCart.First(x => x.MealId == id).Quantity + 1;
            }          
            var cartItem = new CartItemViewModel()
            {
                Email = email,
                UserName = user,
                MealId = id,
                Description = meal.Description,
                Image = meal.ThumbnailImage,
                Name = meal.Name,
                Price = meal.Price,
                Quantity = quantity,
                TableId = tableId
            };
            currentCart.Add(cartItem);
            HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(currentCart));           
            return Ok(currentCart);
        }        
        public IActionResult UpdateCart(int id, int quantity)
        {
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);

            foreach (var item in currentCart)
            {
                if (item.MealId == id)
                {
                    if (quantity == 0)
                    {
                        currentCart.Remove(item);
                        break;
                    }
                    item.Quantity = quantity;
                }
            }

            HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }
        private CheckoutViewModel GetCheckoutViewModel()
        {
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            var checkoutVm = new CheckoutViewModel();
            {
                foreach (var item in currentCart)
                {
                    checkoutVm.Name = item.UserName;
                    checkoutVm.Email = item.Email;
                    checkoutVm.TableId = item.TableId;
                }
                checkoutVm.CartItems = currentCart;
                checkoutVm.CheckoutModel = new CheckoutRequest();
            };

            return checkoutVm;
        }
    }
}
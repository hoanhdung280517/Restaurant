using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantSystem.Models;
using RSSolution.APIHelpers;
using RSSolution.APIHelppers;
using RSSolution.Utilities.Constants;
using RSSolution.ViewModels.Catalog.Contacts;
using RSSolution.ViewModels.Catalog.MealCategories;
using RSSolution.ViewModels.Catalog.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISharedCultureLocalizer _loc;
        private readonly ISlideApiClient _slideApiClient;
        private readonly IMealApiClient _mealApiClient;
        private readonly ITableApiClient _tableApiClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContactApiClient _contactApiClient;

        public HomeController(ILogger<HomeController> logger, 
            ITableApiClient tableApiClient,
            ISlideApiClient slideApiClient,
            IMealApiClient mealApiClient,
            IHttpContextAccessor httpContextAccessor,
            IContactApiClient contactApiClient)
        {
            _logger = logger;
            _slideApiClient = slideApiClient;
            _mealApiClient = mealApiClient;
            _tableApiClient = tableApiClient;
            _httpContextAccessor = httpContextAccessor;
            _contactApiClient = contactApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
           
            return View();
        }
        public IActionResult Contact()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var contact = new ContactViewModel()
            {
                Email = email,
                UserName = userName,
            };
            if (TempData["Notification"] != null)
            {
                ViewBag.Success = TempData["notification"];
            }
            return View(contact);
        }
        [HttpPost]
        public async Task<IActionResult> Contact(string UserName, string Email, int PhoneNumber, string Message)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if(email == null)
            {
                TempData["Notification"] = "You must be login to comments";
                return RedirectToAction("Contact");
            }
            var contact = new ContactCreateRequest()
            {
                UserName = UserName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Message = Message,
            };
            var result = await _contactApiClient.Create(contact);
            if(result == true)
            {
                TempData["Notification"] = "Thanks for your comments";
                return RedirectToAction("Contact");
            }
            if (result == false)
            {
                TempData["Notification"] = "Your comments isn't successfully";
                return RedirectToAction("Contact");
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Table()
        {
            var table = await _tableApiClient.GetAll();
            return View(table.ToList());
        }
        [Authorize]
        public async Task<IActionResult> Menu(int tableId )
        {
            var culture = CultureInfo.CurrentCulture.Name;
            var viewModel = new HomeViewModel
            {
                TableId = tableId,
                Slides = await _slideApiClient.GetAll(),
                FeaturedMeals = await _mealApiClient.GetFeaturedMeals(culture, SystemConstants.MealSettings.NumberOfFeaturedMeals),
                LatestMeals = await _mealApiClient.GetLatestMeals(culture, SystemConstants.MealSettings.NumberOfLatestMeals),
            };
            TableRequest table = new TableRequest();
            {
                table.Id = tableId;
            };
            RequestTable request = new RequestTable();
            {
                request.TableId = tableId;
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}

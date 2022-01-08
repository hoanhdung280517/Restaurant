using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantAdmin.Models;
using RSSolution.APIHelpers;
using RSSolution.APIHelppers;
using RSSolution.Utilities.Constants;
using RSSolution.ViewModels.Catalog.Contacts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactApiClient _contactApiClient;
        private readonly IUserApiClient _userApiClient;
        private readonly IOrderApiClient _orderApiClient;

        public HomeController(ILogger<HomeController> logger,
            IContactApiClient contactApiClient,
            IUserApiClient userApiClient,
            IOrderApiClient orderApiClient)
        {
            _logger = logger;
            _contactApiClient = contactApiClient;
            _userApiClient = userApiClient;
            _orderApiClient = orderApiClient;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            if(role == "Admin")
            {
                var contact = await _contactApiClient.GetAll();
                var user = await _userApiClient.GetAll();
                var orderdetail = await _orderApiClient.GetAllOrderDetails();
                var index = new IndexVm()
                {
                    contactViews = contact,
                    userVms = user,
                    orderDetailVms = orderdetail,
                };
                return View(index);
            }
            TempData["result"] = "You are not Admin";
            return RedirectToAction("Index", "Login");
            
        }
        public async Task<IActionResult> Contact()
        {
            var contact = await _contactApiClient.GetAll();
            if (TempData["Notification"] != null)
            {
                ViewBag.Success = TempData["notification"];
            }
            return View(contact);
        }
        [HttpPost]
        public async Task<IActionResult> Contact(DateTime fromdate, DateTime todate)
        {
            var Fromdate = fromdate.ToShortDateString();
            var Todate = todate.ToShortDateString();
            if (Fromdate == "01/01/0001" && Todate == "01/01/0001")
            {
                TempData["Notification"] = "Can't find contact";
                return RedirectToAction("Contact");
            }
            var date = new Time()
            {
                Fromdate = fromdate,
                Todate = todate,
            };
            var contacts = await _contactApiClient.GetByDate(date);
            return View(contacts);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _contactApiClient.GetById(id);
            return View(result);
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
        [HttpPost]
        public IActionResult Language(NavigationViewModel viewModel)
        {
            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId,
                viewModel.CurrentLanguageId);

            return Redirect(viewModel.ReturnUrl);
        }
    }
}

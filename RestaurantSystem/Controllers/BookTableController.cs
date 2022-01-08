using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Models;
using RSSolution.APIHelppers;
using RSSolution.Data.Enums;
using RSSolution.ViewModels.Catalog.BookTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantSystem.Controllers
{
    [Authorize]
    public class BookTableController : Controller
    {
        private readonly IBookTableApiClient _bookTableApiClient;
        public BookTableController(
            IBookTableApiClient bookTableApiClient)
        {
            _bookTableApiClient = bookTableApiClient;
        }
        public async Task<IActionResult> Index()
        {
            var booking = await _bookTableApiClient.GetAll();
            if (TempData["BookTable"] != null)
            {
                ViewBag.Success = TempData["BookTable"];
            }
            return View(booking);
        }
        [HttpPost]
        public async Task<IActionResult> Index(DateTime time)
        {
            var Date = time.ToShortDateString();
            if (Date == "01/01/0001")
            {
                TempData["BookTable"] = "Can't find BookTable";
                return RedirectToAction("Index");
            }
            var date = new RequestTime()
            {
                Time = time,
            };
            var booking = await _bookTableApiClient.GetByDate(date);
            return View(booking);
        }
        public async Task<IActionResult> Details(int id)
        {
            var details = await _bookTableApiClient.GetById(id);
            return View(details);
        }
        [HttpPost]
        public async Task<IActionResult> Details(EditBookTableRequest request)
        {
            var result = await _bookTableApiClient.Edit(request);
            if (result)
            {
                TempData["BookTable"] = "Edit Successfully";
                return RedirectToAction("Index");
            }

            TempData["BookTable"] = "Edit fails";
            return View(request);
        }
        public IActionResult Create()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var booktable = new BookTableVm()
            {
                Email = email,
                UserName = userName,
            };
            if (TempData["Booking"] != null)
            {
                ViewBag.Success = TempData["Booking"];
            }
            return View(booktable);
        }
        [HttpPost]
        public async Task<IActionResult> Create(string UserName, string Email, string PhoneNumber, int Adults, int Child, DateTime dateTime, string Note)
        {
            var booktable = new BookTableRequest()
            {
                UserName = UserName,
                Email = Email,
                Note = Note,
                BookDate = dateTime,
                CountAdults = Adults,
                CountChilds =Child

            };
            var result = await _bookTableApiClient.Create(booktable);
            if (result == true)
            {
                TempData["Booking"] = "Thanks for Booking Table";
                return RedirectToAction("Create");
            }
            if (result == false)
            {
                TempData["Booking"] = "Book a Table isn't successfully";
                return RedirectToAction("Create");
            }
            return View();
        }
    }
}

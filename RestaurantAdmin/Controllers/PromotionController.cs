using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RSSolution.APIHelpers;
using RSSolution.ViewModels.Catalog.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAdmin.Controllers
{
    [Authorize]
    public class PromotionController : Controller
    {        
        private readonly IConfiguration _configuration;

        private readonly IPromotionApiClient _promotionApiClient;

        public PromotionController(
            IConfiguration configuration,
            IPromotionApiClient promotionApiClient)
        {
            _configuration = configuration;
            _promotionApiClient = promotionApiClient;
        }   
        public async Task<IActionResult> Index()
        {
            var promotion = await _promotionApiClient.GetAll();
            if (TempData["Promotion"] != null)
            {
                ViewBag.SuccessMsg = TempData["Promotion"];
            }
            return View(promotion.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] PromotionCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _promotionApiClient.Create(request);
            if (result)
            {
                TempData["Promotion"] = "Add new promotion is successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Add new promotion failed");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var promotion = await _promotionApiClient.GetById(id);
            var editVm = new PromotionUpdateRequest()
            {
                Id = promotion.Id,
                DiscountPercent = promotion.DiscountPercent,
                FromDate = promotion.FromDate,
                ToDate = promotion.ToDate,
                Code = promotion.Code,
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] PromotionUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _promotionApiClient.Update(request);
            if (result)
            {
                TempData["Promotion"] = "Update promotion successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Update promotion failed");
            return View(request);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new PromotionDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PromotionDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _promotionApiClient.Delete(request.Id);
            if (result)
            {
                TempData["Promotion"] = "Delete successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Delete failed");
            return View(request);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RSSolution.APIHelpers;
using RSSolution.Utilities.Constants;
using RSSolution.ViewModels.Catalog.MealCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAdmin.Controllers
{
    public class MealCategoryController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IMealCategoryApiClient _mealCategoryApiClient;

        public MealCategoryController(
            IConfiguration configuration,
            IMealCategoryApiClient meaCategoryApiClient)
        {
            _configuration = configuration;
            _mealCategoryApiClient = meaCategoryApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string languageId)
        {
            var mealCategories = await _mealCategoryApiClient.GetAll(languageId);
            return View(mealCategories.ToList());
        }
        [HttpGet]
        public IActionResult CreateMealCategory()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateMealCategory([FromForm] MealCategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _mealCategoryApiClient.CreateMealCategory(request);
            if (result)
            {
                TempData["result"] = "Add new meal category is successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Add new meal category failed");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var mealCategory = await _mealCategoryApiClient.GetById(languageId,id);
            var editVm = new MealCategoryUpdateRequest()
            {
                Id = mealCategory.Id,
                Name = mealCategory.Name,
                SeoAlias = mealCategory.SeoAlias,
                SeoDescription = mealCategory.SeoDescription,
                SeoTitle = mealCategory.SeoTitle,
                SortOrder = mealCategory.SortOrder,
                ParentId = mealCategory.ParentId               
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditCategory([FromForm] MealCategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _mealCategoryApiClient.UpdateMealCategory(request);
            if (result)
            {
                TempData["result"] = "Update meal category successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Update meal category failed");
            return View(request);
        }
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            return View(new MealCategoryDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(MealCategoryDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _mealCategoryApiClient.DeleteMealCategory(request.Id);
            if (result)
            {
                TempData["result"] = "Delete successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Delete failed");
            return View(request);
        }
    }
}

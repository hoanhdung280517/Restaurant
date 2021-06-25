using System.Linq;
using System.Threading.Tasks;
using RSSolution.APIHelpers;
using RSSolution.Utilities.Constants;
using RSSolution.ViewModels.Catalog.Meals;
using RSSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace RestaurantAdmin.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealApiClient _mealApiClient;
        private readonly IConfiguration _configuration;

        private readonly IMealCategoryApiClient _mealCategoryApiClient;

        public MealController(IMealApiClient mealApiClient,
            IConfiguration configuration,
            IMealCategoryApiClient meaCategoryApiClient)
        {
            _configuration = configuration;
            _mealApiClient = mealApiClient;
            _mealCategoryApiClient = meaCategoryApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int? mealCategoryId, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageMealPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
                MealCategoryId = mealCategoryId
            };
            var data = await _mealApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;

            var mealCategories = await _mealCategoryApiClient.GetAll(languageId);
            ViewBag.MealCategories = mealCategories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = mealCategoryId.HasValue && mealCategoryId.Value == x.Id
            });

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] MealCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _mealApiClient.CreateMeal(request);
            if (result)
            {
                TempData["result"] = "Add new meal is successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Add new meal failed");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryAssign(int id)
        {
            var roleAssignRequest = await GetCategoryAssignRequest(id);
            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAssign(CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _mealApiClient.CategoryAssign(request.Id, request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Add Category Successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetCategoryAssignRequest(request.Id);

            return View(roleAssignRequest);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var meal = await _mealApiClient.GetById(id, languageId);
            var editVm = new MealUpdateRequest()
            {
                Id = meal.Id,
                Description = meal.Description,
                Details = meal.Details,
                Name = meal.Name,
                SeoAlias = meal.SeoAlias,
                SeoDescription = meal.SeoDescription,
                SeoTitle = meal.SeoTitle
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] MealUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _mealApiClient.UpdateMeal(request);
            if (result)
            {
                TempData["result"] = "Update meal successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Update meal failed");
            return View(request);
        }

        private async Task<CategoryAssignRequest> GetCategoryAssignRequest(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var mealObj = await _mealApiClient.GetById(id, languageId);
            var mealCategories = await _mealCategoryApiClient.GetAll(languageId);
            var categoryAssignRequest = new CategoryAssignRequest();
            foreach (var role in mealCategories)
            {
                categoryAssignRequest.MealCategories.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = mealObj.MealCategories.Contains(role.Name)
                });
            }
            return categoryAssignRequest;
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new MealDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MealDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _mealApiClient.DeleteMeal(request.Id);
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
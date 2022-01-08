using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Models;
using RSSolution.APIHelpers;
using RSSolution.APIHelppers;
using RSSolution.ViewModels.Catalog.Meals;
using RSSolution.ViewModels.Catalog.Table;
using RSSolution.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantSystem.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly IMealApiClient _mealApiClient;
        private readonly IMealCategoryApiClient _mealCategoryApiClient;
        private readonly ICommentApiClient _commentApiClient;

        public MealController(IMealApiClient mealApiClient, IMealCategoryApiClient mealCategoryApiClient , ICommentApiClient commentApiClient)
        {
            _mealApiClient = mealApiClient;
            _mealCategoryApiClient = mealCategoryApiClient;
            _commentApiClient = commentApiClient;
        }

        public async Task<IActionResult> Detail(int id, string culture, RequestTable table)
        {
            var meal = await _mealApiClient.GetById(id, culture);
            return View(new MealDetailViewModel()
            {
                TableId = table.TableId,
                Meal = meal
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int id , string comment)
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var comments = new CommentCreateRequest()
            {
                Comments = comment,
                UserName = userName,
                mealId = id
            };
            var result = await _commentApiClient.Create(comments);
            return Ok();
        }

        public async Task<IActionResult> MealCategory( int id, string culture, int tableId, int page = 1 )
        {
            var meals = await _mealApiClient.GetPagings(new GetManageMealPagingRequest()
            {                
                MealCategoryId = id,
                PageIndex = page,
                LanguageId = culture,
                PageSize = 10
                
            });
            return View(new MealCategoryViewModel()
            {
                TableId = tableId,
                MealCategory = await _mealCategoryApiClient.GetById(culture, id),
                Meals = meals
            }); 
        }
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _commentApiClient.GetByMealId(id);
            return Ok(comment);
        }
    }
}

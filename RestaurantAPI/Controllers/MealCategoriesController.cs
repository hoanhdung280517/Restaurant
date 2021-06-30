using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSSolution.Application.Catalog.MealCategories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSolution.ViewModels.Catalog.MealCategories;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MealCategoriesController : ControllerBase
    {
        private readonly IMealCategoryService _mealCategoryService;

        public MealCategoriesController(
            IMealCategoryService mealCategoryService)
        {
            _mealCategoryService = mealCategoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var products = await _mealCategoryService.GetAll(languageId);
            return Ok(products);
        }

        [HttpGet("{id}/{languageId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string languageId, int id)
        {
            var category = await _mealCategoryService.GetById(languageId, id);
            return Ok(category);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> CreateMealCategory([FromForm] MealCategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mealCategoryId = await _mealCategoryService.CreateMealCategory(request);
            if (mealCategoryId == 0)
                return BadRequest();

            var mealCategory = await _mealCategoryService.GetById (request.LanguageId,mealCategoryId);

            return CreatedAtAction(nameof(GetById), new { id = mealCategoryId }, mealCategory);
        }
        [HttpPut("{mealCategoryId}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> UpdateMealCategory([FromRoute] int mealCategoryId, [FromForm] MealCategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = mealCategoryId;
            var affectedResult = await _mealCategoryService.UpdateMealCategory(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        [HttpDelete("{mealCategoryId}")]
        [Authorize]
        public async Task<IActionResult> DeleteMealCategory(int mealCategoryId)
        {
            var affectedResult = await _mealCategoryService.DeleteMealCategory(mealCategoryId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
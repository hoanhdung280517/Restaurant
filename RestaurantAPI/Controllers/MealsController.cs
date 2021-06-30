using RSSolution.Application.Catalog.Meals;
using RSSolution.ViewModels.Catalog.MealImages;
using RSSolution.ViewModels.Catalog.Meals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    //api/products
    [Route("api/[controller]")]
    [ApiController]
    
    public class MealsController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealsController(
            IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageMealPagingRequest request)
        {
            var meal = await _mealService.GetAllPaging(request);
            return Ok(meal);
        }

        [HttpGet("{mealId}/{languageId}")]
        public async Task<IActionResult> GetById(int mealId, string languageId)
        {
            var meal = await _mealService.GetById(mealId, languageId);
            if (meal == null)
                return BadRequest("Cannot find Meal");
            return Ok(meal);
        }

        [HttpGet("featured/{languageId}/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeaturedMeals(int take, string languageId)
        {
            var meal = await _mealService.GetFeaturedMeals(languageId, take);
            return Ok(meal);
        }

        [HttpGet("latest/{languageId}/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestProducts(int take, string languageId)
        {
            var meal = await _mealService.GetLatestMeals(languageId, take);
            return Ok(meal);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] MealCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mealId = await _mealService.Create(request);
            if (mealId == 0)
                return BadRequest();

            var meal = await _mealService.GetById(mealId, request.LanguageId);

            return CreatedAtAction(nameof(GetById), new { id = mealId }, meal);
        }

        [HttpPut("{mealId}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int mealId, [FromForm] MealUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = mealId;
            var affectedResult = await _mealService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{mealId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int mealId)
        {
            var affectedResult = await _mealService.Delete(mealId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPatch("{mealId}/{newPrice}")]
        [Authorize]
        public async Task<IActionResult> UpdatePrice(int mealId, decimal newPrice)
        {
            var isSuccessful = await _mealService.UpdatePrice(mealId, newPrice);
            if (isSuccessful)
                return Ok();

            return BadRequest();
        }

        //Images
        [HttpPost("{mealId}/images")]

        public async Task<IActionResult> CreateImage(int mealId, [FromForm] MealImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _mealService.AddImage(mealId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _mealService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{mealId}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] MealImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mealService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{mealId}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mealService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{mealId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int mealId, int imageId)
        {
            var image = await _mealService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find meal");
            return Ok(image);
        }

        [HttpPut("{id}/mealCategories")]
        [Authorize]
        public async Task<IActionResult> CategoryAssign(int id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mealService.CategoryAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
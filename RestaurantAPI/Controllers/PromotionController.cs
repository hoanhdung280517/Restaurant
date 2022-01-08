using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSolution.Application.Catalog.Promotions;
using RSSolution.ViewModels.Catalog.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
       private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var promotions = await _promotionService.GetAll();
            return Ok(promotions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var promotions = await _promotionService.GetById(id);
            if (promotions == null)
                return BadRequest("Cannot find promotions");
            return Ok(promotions);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreatePromotion([FromForm] PromotionCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var promotionId = await _promotionService.Create(request);
            if (promotionId == 0)
                return BadRequest();

            return Ok(request);
        }
        [HttpPut("{promotionId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePromotion([FromRoute] int promotionId, [FromForm] PromotionUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = promotionId;
            var result = await _promotionService.Update(request);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }
        [HttpDelete("{promotionid}")]
        public async Task<IActionResult> DeleteMealCategory(int promotionid)
        {
            var affectedResult = await _promotionService.Delete(promotionid);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}

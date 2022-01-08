using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSolution.Application.Catalog.Carts;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CartRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cartid = await _cartService.Add(request);
            if (cartid == 0)
                return BadRequest();

            return Ok(request);
        }
        [HttpGet("{tableId}")]
        public async Task<IActionResult> GetById(int tableId)
        {
            var cart = await _cartService.GetCartById(tableId);
            if (cart == null)
                return BadRequest("Cannot find Meal");
            return Ok(cart);
        }
        [HttpPut]
        public async Task<IActionResult> ChangeTable([FromBody] ChangeTableRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _cartService.ChangeTable(request);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }
        [HttpPut("mergeTable")]
        public async Task<IActionResult> MergeTable([FromBody] MergeTableRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _cartService.MergeTable(request);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }
        [HttpDelete("{tableId}")]
        public async Task<IActionResult> Delete(int tableId)
        {
            var Result = await _cartService.DeleteCart(tableId);
            if (Result == 0)
                return BadRequest();
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSolution.Application.Catalog.Orders;
using RSSolution.Data.Entities;
using RSSolution.ViewModels.Sales;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CheckoutRequest request)
        {
                if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderId = await _orderService.Create(request);
            if (orderId == 0)
                return BadRequest();

            return Ok(request);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var order = await _orderService.GetAll();
            return Ok(order);
        }
        [HttpGet("orderDetail")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var order = await _orderService.GetAllOrderDetails();
            return Ok(order);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetById(int orderId)
        {
            var orderdetail = await _orderService.GetOrderById(orderId);
            if (orderdetail == null)
                return BadRequest("Cannot find Meal");
            return Ok(orderdetail);
        }
        [HttpPost("getbydate")]
        public async Task<IActionResult> GetByDate(TimeRequest time)
        {
            var orderdate = await _orderService.GetByDate(time);
            if (orderdate == null)
                return BadRequest("Cannot find Order");
            return Ok(orderdate);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RSSolution.Application.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDController : ControllerBase
    { 
        IPDService _pDService;
        public PDController(IPDService pDService)
        {
            _pDService = pDService;
        }
        [HttpGet("district")]
        public async Task<IActionResult> GetAllDistrict()
        {
            var district = await _pDService.GetAllDistrict();
            return Ok(district);
        }
        [HttpGet("province")]
        public async Task<IActionResult> GetAllProvince()
        {
            var provinces = await _pDService.GetAllProvince();
            return Ok(provinces);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSolution.Application.Catalog.Tables;
using RSSolution.ViewModels.Catalog.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class TableController : ControllerBase
    {
       private readonly ITableService _tableService;
        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var table = await _tableService.GetAll();
            return Ok(table);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var table = await _tableService.GetById(id);
            if (table == null)
                return BadRequest("Cannot find table");
            return Ok(table);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateTabel([FromForm] TableCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tableId = await _tableService.Create(request);
            if (tableId == 0)
                return BadRequest();

            return Ok(request);
        }
        [HttpPut("{tableId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateTable([FromRoute] int tableId,[FromForm] TableUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = tableId;
            var result = await _tableService.Update(request);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }
        [HttpDelete("{tableid}")]
        public async Task<IActionResult> DeleteMealCategory(int tableid)
        {
            var affectedResult = await _tableService.Delete(tableid);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSolution.Application.Catalog.BookTables;
using RSSolution.ViewModels.Catalog.BookTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTableController : ControllerBase
    {
        private readonly IBookTableService _bookTableService;
        public BookTableController(IBookTableService bookTableService)
        {
            _bookTableService = bookTableService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] BookTableRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var booktableId = await _bookTableService.Create(request);
            if (booktableId == 0)
                return BadRequest();

            return Ok(request);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var booktable = await _bookTableService.GetAll();
            return Ok(booktable);
        }

        [HttpPost("getbydate")]
        public async Task<IActionResult> GetByDate(RequestTime time)
        {
            var booktable = await _bookTableService.GetByDate(time);
            if (booktable == null)
                return BadRequest("Cannot find booktable");
            return Ok(booktable);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update( [FromBody] EditBookTableRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookTableService.UpdateStatus(request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _bookTableService.GetById(id);
            if (contact == null)
                return BadRequest("Cannot find BookTable");
            return Ok(contact);
        }
    }
}

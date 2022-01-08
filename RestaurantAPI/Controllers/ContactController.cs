using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSolution.Application.Catalog.Contacts;
using RSSolution.ViewModels.Catalog.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contactId = await _contactService.AddContact(request);
            if (contactId == 0)
                return BadRequest();

            return Ok(request);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contact = await _contactService.GetAll();
            return Ok(contact);
        }
        [HttpPost("getbydate")]
        public async Task<IActionResult> GetByDate(Time time)
        {
            var contact = await _contactService.GetByDate(time);
            if (contact == null)
                return BadRequest("Cannot find contact");
            return Ok(contact);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _contactService.GetById(id);
            if (contact == null)
                return BadRequest("Cannot find contact");
            return Ok(contact);
        }
    }
}

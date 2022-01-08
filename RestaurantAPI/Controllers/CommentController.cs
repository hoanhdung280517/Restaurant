using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSolution.Application.System.Comments;
using RSSolution.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService tableService)
        {
            _commentService = tableService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByMealId(int id)
        {
            var comment = await _commentService.GetByMealId(id);
            return Ok(comment);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentId = await _commentService.Create(request);
            if (commentId == 0)
                return BadRequest();

            return Ok(request);
        }
    }
}


using BlogApi.Application.Commands;
using BlogApi.Application.Queries;
using BlogApi.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(string id)
        {
            var query = new GetBlogByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var query = new GetAllBlogsQuery();
            var result = await _mediator.Send(query);
            return Ok(result.OrderByDescending(b => b.CreatedOn));
        }

        [HttpPost]
        public async Task<IActionResult> CreatedBlog([FromBody] CreateBlogCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBlogById), new { id = result }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(string id, [FromBody] UpdateBlogCommand command)
        {
            if (id != command.Id) return BadRequest();
            var updatedBlog = await _mediator.Send(command);
            return Ok(updatedBlog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(string id)
        {
            var command = new DeleteBlogCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{id}/relatedBlogs")]
        public async Task<IActionResult> AddRelatedBlogs(string id, [FromBody] List<string> relatedBlogIds)
        {
            var command = new AddRelatedBlogsCommand { BlogId = id, RelatedBlogIds = relatedBlogIds };
            var result = await _mediator.Send(command);
            return Ok(result);
        }



        [HttpPut("{id}/relatedBlogs")]
        public async Task<ActionResult<Blog>> UpdateRelatedBlogs(string id, [FromBody] List<string> relatedBlogIds)
        {
            var command = new UpdateRelatedBlogsCommand { BlogId = id, RelatedBlogIds = relatedBlogIds };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}/relatedBlogs")]
        public async Task<ActionResult<Blog>> RemoveRelatedBlogs(string id, [FromBody] List<string> relatedBlogIds)
        {
            var command = new RemoveRelatedBlogsCommand { BlogId = id, RelatedBlogIds = relatedBlogIds };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}/relatedBlogs")]
        public async Task<ActionResult<List<Blog>>> GetRelatedBlogs(string id)
        {
            var query = new GetRelatedBlogsQuery { BlogId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
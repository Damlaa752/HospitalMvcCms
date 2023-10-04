using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCommentsController : ControllerBase
    {
        private readonly IPostCommentService _service;

        public PostCommentsController(IPostCommentService service)
        {
            _service = service;
        }



        // GET: api/<PostCommentsController>
        [HttpGet]
        public async Task<IEnumerable<PostComment>> Get()
        {
            return await _service.GetAllPostCommentsByIncludeAsync();

        }

        // GET api/<PostCommentsController>/5
        [HttpGet("{id}")]
        public async Task<PostComment> Get(int id)
        {
            return await _service.GetPostCommentByIncludeAsync(id);
        }

        // POST api/<PostCommentsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostComment value)
        {
            await _service.AddAsync(value);
            var response = await _service.SaveAsync();
            if (response > 0)
            {
                return Ok();
            }
            return Problem();
        }

        // PUT api/<PostCommentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PostComment value)
        {
            var postComment = await _service.GetPostCommentByIncludeAsync(id);

            if (postComment != null)
            {
                postComment.UserId = value.UserId;
                postComment.Comment = value.Comment;
                postComment.IsActive = value.IsActive;
                postComment.PostId = value.PostId;
                postComment.UpdatedAt = DateTime.UtcNow;
                _service.Update(postComment);
                await _service.SaveAsync();
                return Ok(postComment);
            }
            return NotFound();
        }

        // DELETE api/<PostCommentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var postComment = await _service.GetPostCommentByIncludeAsync(id);
            if (postComment != null)
            {
                _service.Delete(postComment);
                var response = await _service.SaveAsync();

                if (response > 0)
                {
                    return Ok();

                }
                return Problem();
            }
            return NotFound();
        }
    }
}

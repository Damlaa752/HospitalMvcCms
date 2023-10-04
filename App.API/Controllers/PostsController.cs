using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;

        public PostsController(IPostService service)
        {
            _service = service;
        }



        // GET: api/<PostsController>
        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            return await _service.GetAllPostsByIncludeAsync();
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public async Task<Post> GetAsync(int id)
        {
            Post model = await _service.GetPostByIncludeAsync(id);

            return model;
        }

        // POST api/<PostsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Post value)
        {
            await _service.AddAsync(value);
            var response = await _service.SaveAsync();
            if (response > 0)
            {
                return Ok();

            }
            return Problem();
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Post value)
        {
            var mainModel = await _service.GetPostByIncludeAsync(id);
            if (mainModel != null)
            {
                mainModel.Title = value.Title;
                mainModel.Content = value.Content;
                mainModel.Image = value.Image;
                mainModel.UpdatedAt = DateTime.UtcNow;

                _service.Update(mainModel);
                var response = await _service.SaveAsync();

                if (response > 0)
                    return Ok();
            }
            return Problem();
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var mainModel = await _service.GetPostByIncludeAsync(id);
            if (mainModel != null)
            {
                _service.Delete(mainModel);
                var response = await _service.SaveAsync();

                if (response > 0)
                    return Ok();

            }
            return Problem();
        }
    }
}

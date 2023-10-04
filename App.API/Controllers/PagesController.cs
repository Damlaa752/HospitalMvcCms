using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IService<Page> _service;

        public PagesController(IService<Page> service)
        {
            _service = service;
        }

        // GET: api/<PagesController>
        [HttpGet]
        public async Task<IEnumerable<Page>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<PagesController>/5
        [HttpGet("{id}")]
        public async Task<Page> Get(int id)
        {
            return await _service.FindAsync(id);
        }

        // POST api/<PagesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Page value)
        {
            await _service.AddAsync(value);
            var response = await _service.SaveAsync();

            if (response > 0)
            {
                return Ok();
            }

            return Problem();
        }

        // PUT api/<PagesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Page value)
        {
            var mainModel = await _service.FindAsync(id);

            if (mainModel != null)
            {
                mainModel.Title = value.Title;
                mainModel.Content = value.Content;
                mainModel.IsActive = value.IsActive;
                mainModel.UpdatedAt = DateTime.UtcNow;

                _service.Update(mainModel);

                var response = await _service.SaveAsync();

                if (response > 0)
                {
                    return Ok();
                }
            }
            return Problem();
        }

        // DELETE api/<PagesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Page mainModel = await _service.FindAsync(id);

            _service.Delete(mainModel);
            var response = await _service.SaveAsync();

            if (response > 0)
            {
                return Ok();
            }
            return Problem();
        }
    }
}

using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IService<Role> _service;

        public RolesController(IService<Role> service)
        {
            _service = service;
        }

        // GET: api/<RolesController>
        [HttpGet]
        public async Task<IEnumerable<Role>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public async Task<Role> Get(int id)
        {
            return await _service.FindAsync(id);
        }

        // POST api/<RolesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Role value)
        {
            await _service.AddAsync(value);
            var response = await _service.SaveAsync();

            if (response > 0)
            {
                return Ok();
            }

            return Problem();
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Role value)
        {
            Role mainModel = await _service.FindAsync(id);

            if (mainModel != null)
            {
                mainModel.RoleName = value.RoleName;

                _service.Update(mainModel);

                var response = await _service.SaveAsync();

                if (response > 0)
                {
                    return Ok();
                }
            }

            return Problem();

        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Role mainModel = await _service.FindAsync(id);

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

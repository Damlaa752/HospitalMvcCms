using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _service.GetAllUserByIncludeAsync();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _service.GetUserByIncludeAsync(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] User value)
        {
			await _service.AddAsync(value);
            await _service.SaveAsync();
            return Ok();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User value)
        {
            var mainUser = await _service.GetUserByIncludeAsync(id);

            if (mainUser != null)
            {
                mainUser.Image = value.Image;
                mainUser.FullName = value.FullName;
                mainUser.Email = value.Email;
                mainUser.Password = value.Password;
                mainUser.City = value.City;
                mainUser.Phone = value.Phone;
                mainUser.UpdatedAt = DateTime.UtcNow;
                if (value.RoleId is not null)
                {
                mainUser.RoleId = value.RoleId;
                }
				_service.Update(mainUser);
                var response = await _service.SaveAsync();
                if (response > 0)
                {
                    return Ok();
                }
            }
            return Problem();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            User mainUser = await _service.FindAsync(id);

            if (mainUser is not null)
            {
                _service.Delete(mainUser);
                var response = await _service.SaveAsync();

                if (response > 0)
                {
                    return Ok();

                }
            }
            return BadRequest();
        }
    }
}

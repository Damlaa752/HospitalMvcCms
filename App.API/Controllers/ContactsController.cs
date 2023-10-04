using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IService<Contact> _service;

        public ContactsController(IService<Contact> service)
        {
            _service = service;
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public async Task<Contact> Get(int id)
        {
            return await _service.FindAsync(id);
        }

        // POST api/<ContactsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Contact value)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(value);
               var response =  await _service.SaveAsync();
                if (response>0)
                {
                    return Ok();
                }
            }
            return Problem();
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Contact value)
        {
           Contact mainModel = await _service.FindAsync(id);

            if (mainModel!=null)
            {
                mainModel.Email = value.Email;
                mainModel.FullName = value.FullName;
                mainModel.Phone = value.Phone;
                mainModel.UpdatedAt = value.UpdatedAt;
                mainModel.Message = value.Message;
                mainModel.Title = value.Title;

                _service.Update(mainModel);
                var response = await _service.SaveAsync();
                if (response > 0)
                {
                    return Ok();
                }

            }
            return Problem();
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Contact mainModel = await _service.FindAsync(id);

            if (mainModel!= null)
            {
                _service.Delete(mainModel);
                var response = await _service.SaveAsync();
                if (response > 0)
                {
                    return Ok();
                }
            }
            return Problem();
        }
    }
}

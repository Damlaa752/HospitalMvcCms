using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IService<Setting> _service;

        public SettingsController(IService<Setting> service)
        {
            _service = service;
        }



        // GET: api/<SettingsController>
        [HttpGet]
        public async Task<IEnumerable<Setting>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<SettingsController>/5
        [HttpGet("{id}")]
        public async Task<Setting> GetAsync(int id)
        {
            return await _service.FindAsync(id);
        }

        // POST api/<SettingsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Setting value)
        {
            await _service.AddAsync(value);
            await _service.SaveAsync();
            return Ok();
        }

        // PUT api/<SettingsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Setting value)
        {
            Setting mainSetting = await _service.FindAsync(id);
            if (mainSetting != null)
            {
                mainSetting.Name = value.Name;
                mainSetting.Email = value.Email;
                mainSetting.Address = value.Address;
                mainSetting.Phone = value.Phone;
                mainSetting.AlpLinkedin = value.AlpLinkedin;
                mainSetting.KadirLinkedin = value.KadirLinkedin;
                mainSetting.UtkuLinkedin = value.UtkuLinkedin;
                mainSetting.Image = value.Image;
                mainSetting.IsActive = value.IsActive;

                _service.Update(mainSetting);
                await _service.SaveAsync();
                return Ok();
            }
            return Problem();
        }

        // DELETE api/<SettingsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var settings = await _service.GetAllAsync();
            if (settings.Count <= 1)
                return BadRequest("Cannot delete the only setting available.");

            Setting mainSetting = await _service.FindAsync(id);

            if (mainSetting != null)
            {
                _service.Delete(mainSetting);
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

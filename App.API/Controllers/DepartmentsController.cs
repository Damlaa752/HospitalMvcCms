using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }



        // GET: api/<DepartmentsController>
        [HttpGet]
        public async Task<IEnumerable<Department>> Get()
        {
            return await _service.GetAllDepartmentsByIncludeAsync();
        }

        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        public async Task<Department> Get(int id)
        {
            return await _service.GetDepartmentByIncludeAsync(id);
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Department value)
        {
            await _service.AddAsync(value);
            await _service.SaveAsync();
            return Ok(value);
        }

        // PUT api/<DepartmentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Department value)
        {
            Department mainDepartment = await _service.FindAsync(id);

            if (mainDepartment != null)
            {
                mainDepartment.Name = value.Name;
                mainDepartment.Description = value.Description;
                mainDepartment.Image = value.Image;
               

                _service.Update(mainDepartment);
                await _service.SaveAsync();
                return Ok(mainDepartment);
            }
            return NotFound();
        }

        // DELETE api/<DepartmentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var mainDepartment = await _service.FindAsync(id);
            if (mainDepartment != null)
            {
                _service.Delete(mainDepartment);
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

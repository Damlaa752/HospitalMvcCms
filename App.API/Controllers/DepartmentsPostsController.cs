using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;


namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsPostsController : ControllerBase
    {
        private readonly IDepartmentsPostsService _service;

        public DepartmentsPostsController(IDepartmentsPostsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<DepartmentPost>> Get()
        {
            return await _service.GetAllDepartmentPostByIncludeAsync();
        }

        // GET api/<DepartmentsPostsController>/5
        [HttpGet("{id}")]
        public async Task<DepartmentPost> Get(int id)
        {
            return await _service.GetDepartmentPostByIncludeAsync(id);
        }

        // POST api/<DepartmentsPostsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DepartmentPost value)
        {
            await _service.AddAsync(value);
            var response = await _service.SaveAsync();

            if (response > 0)
            {
                return Ok();
            }

            return Problem();
        }

        // PUT api/<DepartmentsPostsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DepartmentPost value)
        {
            DepartmentPost mainModel = await _service.GetDepartmentPostByIncludeAsync(id);

            if (mainModel != null)
            {
                mainModel.Departments = value.Departments;
                mainModel.DepartmentId = value.DepartmentId;
                mainModel.PostId = value.PostId;
                mainModel.Post = value.Post;


                _service.Update(mainModel);

                var response = await _service.SaveAsync();

                if (response > 0)
                {
                    return Ok();
                }
            }
            return Problem();
        }

        // DELETE api/<DepartmentsPostsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            DepartmentPost mainModel = await _service.GetDepartmentPostByIncludeAsync(id);

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

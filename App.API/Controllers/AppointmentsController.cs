using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentsController(IAppointmentService service)
        {
            _service = service;
        }

        // GET: api/<AppointmentsController>
        [HttpGet]
        public async Task<IEnumerable<Appointment>> Get()
        {
            var model = await _service.GetAllAppointmentsByIncludeAsync();

            return model;
        }

        // GET api/<AppointmentsController>/5
        [HttpGet("{id}")]
        public async Task<Appointment> Get(int id)
        {
            var model = await _service.GetAppointmentByIncludeAsync(id);
            return model;
        }

        // POST api/<AppointmentsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Appointment value)
        {
            await _service.AddAsync(value);
            var response = await _service.SaveAsync();

            if (response > 0)
            {
                return Ok();
            }

            return Problem();

        }

        // PUT api/<AppointmentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Appointment value)
        {
            Appointment mainModel = await _service.GetAppointmentByIncludeAsync(id);

            if (mainModel!=null)
            {
                mainModel.FullName = value.FullName;
                mainModel.AppointmentDate = value.AppointmentDate;
                mainModel.Email = value.Email;
                mainModel.DepartmentId = value.DepartmentId;
                mainModel.DoctorId = value.DoctorId;
                mainModel.Message = value.Message;
                mainModel.Phone = value.Phone;
                mainModel.UpdatedAt = DateTime.UtcNow;

                _service.Update(mainModel);
                var response = await _service.SaveAsync();
                if (response>0)
                {
                    return Ok();
                }
            }

            return Problem();
        }

        // DELETE api/<AppointmentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            Appointment mainModel = await _service.GetAppointmentByIncludeAsync(id);

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

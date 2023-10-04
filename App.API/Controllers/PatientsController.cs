using App.Data.Entity;
using App.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
        {
            _service = service;
        }

        // GET: api/<PatientsController>
        [HttpGet]
        public async Task<IEnumerable<Patient>> Get()
        {
            return await _service.GetAllPatientsByIncludeAsync();
        }

        // GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public async Task<Patient> Get(int id)
        {
            return await _service.GetPatientByIncludeAsync(id);
        }

        // POST api/<PatientsController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Patient value)
        {
            value.RoleId = 3;
            await _service.AddAsync(value);
            var response = await _service.SaveAsync();
            if (response > 0)
            {
                return Ok();
            }
            return Problem();

        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Patient value)
        {
            var mainPatient = await _service.GetPatientByIncludeAsync(id);

            if (mainPatient != null)
            {
                mainPatient.FullName = value.FullName;
                mainPatient.Password = value.Password;
                mainPatient.Phone = value.Phone;
                mainPatient.City = value.City;
                mainPatient.Email = value.Email;
                mainPatient.Diagnosis = value.Diagnosis;
                mainPatient.DoctorId = value.DoctorId;
                mainPatient.UpdatedAt = DateTime.UtcNow;
                //mainPatient.Doctor = value.Doctor;
                //mainPatient.Role = value.Role;
                //mainPatient.ImageId = value.ImageId;
                //mainPatient.Image = value.Image;

                _service.Update(mainPatient);
                var response = await _service.SaveAsync();
                if (response > 0)
                {
                    return Ok();
                }
            }
            return Problem();
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var mainPatient = await _service.GetPatientByIncludeAsync(id);

            if (mainPatient != null)
            {


                _service.Delete(mainPatient);
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

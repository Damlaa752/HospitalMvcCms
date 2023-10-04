using App.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Doctors = App.Data.Entity.Doctors;

namespace App.Web.Mvc.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAddress = "http://localhost:5005/api/Appointments";
        private readonly string _apiAddressDepartments = "http://localhost:5005/api/Departments";
        private readonly string _apiAddressDoctors = "http://localhost:5005/api/Doctors";
		

		public AppointmentsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //kaaWasHere

        // GET: AppointmentsController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiAddressDepartments), "Id", "Name");
			
			
			return View();
		}


        // POST: AppointmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Appointment collection)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiAddress, collection);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "<div class='alert alert-success'>Your Appointment has been created... Thank you for choosing us</div>";
                    return RedirectToAction("Index", "Home");

                }
                TempData["Message"] = "<div class='alert alert-success'>Error, Please Try Again! </div>";




                return View(collection);
            }
            catch
            {


                ModelState.AddModelError("", "Your Appointment cannot sended. Please Try Again!");
                return View(collection);
            }
        }


        public async Task<JsonResult> LoadDoctor(int departmentId)
        {
            var doctorlist = await _httpClient.GetFromJsonAsync<List<Doctors>>(_apiAddressDoctors);

            var newDoctors = doctorlist?.Where(d => d.DepartmentId == departmentId).ToList();


            return Json(new SelectList(newDoctors, "Id", "FullName"));
        }



    }
}

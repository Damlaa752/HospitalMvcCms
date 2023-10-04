using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppointmentsController : Controller
    {

        private readonly HttpClient _httpClient;

        private readonly string _apiAddress = "http://localhost:5005/api/Appointments";
        private readonly string _apiRoles = "http://localhost:5005/api/Roles";
        private readonly string _apiDoctors = "http://localhost:5005/api/Doctors";
        private readonly string _apiDepartments = "http://localhost:5005/api/Departments";

        public AppointmentsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: AppointmentsController
        public async Task<ActionResult> Index()
        {
            ViewBag.RoleId = new SelectList(await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoles), "Id", "RoleName");
            ViewBag.DoctorId = new SelectList(await _httpClient.GetFromJsonAsync<List<Doctors>>(_apiDoctors), "Id", "FullName");
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            var model = await _httpClient.GetFromJsonAsync<List<Appointment>>(_apiAddress);
            return View(model);
        }

        // GET: AppointmentsController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.RoleId = new SelectList(await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoles), "Id", "RoleName");
            ViewBag.DoctorId = new SelectList(await _httpClient.GetFromJsonAsync<List<Doctors>>(_apiDoctors), "Id", "FullName");
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
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
                    TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Hata oluştu : " + e.Message);
            }
            ViewBag.RoleId = new SelectList(await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoles), "Id", "RoleName");
            ViewBag.DoctorId = new SelectList(await _httpClient.GetFromJsonAsync<List<Doctors>>(_apiDoctors), "Id", "FullName");
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            return View(collection);
        }

        // GET: AppointmentsController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.RoleId = new SelectList(await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoles), "Id", "RoleName");
            ViewBag.DoctorId = new SelectList(await _httpClient.GetFromJsonAsync<List<Doctors>>(_apiDoctors), "Id", "FullName");
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            var model = await _httpClient.GetFromJsonAsync<Appointment>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: AppointmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Appointment collection)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiAddress + "/" + id, collection);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
            }
            ViewBag.RoleId = new SelectList(await _httpClient.GetFromJsonAsync<List<Role>>(_apiRoles), "Id", "RoleName");
            ViewBag.DoctorId = new SelectList(await _httpClient.GetFromJsonAsync<List<Doctors>>(_apiDoctors), "Id", "FullName");
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            return RedirectToAction(nameof(Index));
        }

        // GET: AppointmentsController/Delete/5
        public async Task<ActionResult> Remove(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Appointment>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: AppointmentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(int id, Appointment collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAddress + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

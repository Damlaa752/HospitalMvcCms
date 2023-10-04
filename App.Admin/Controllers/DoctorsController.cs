using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace App.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAddress = "http://localhost:5005/api/Doctors";
        private readonly string _apiDepartments = "http://localhost:5005/api/Departments";
        public DoctorsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: DoctorController
        public async Task<ActionResult> Index()
        {
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            var model = await _httpClient.GetFromJsonAsync<List<Doctors>>(_apiAddress);
            return View(model);
        }

        // GET: DoctorController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: DoctorController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            return View();
        }

        // POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Doctors collection)
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
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            return View(collection);
        }

        // GET: DoctorController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            var model = await _httpClient.GetFromJsonAsync<Doctors>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Doctors collection)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiAddress + "/" + id, collection);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
            }
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            return RedirectToAction(nameof(Index));
        }

        // GET: DoctorController/Delete/5
        //public async Task<ActionResult> Remove(int? id)
        //{
        //    var model = await _httpClient.GetFromJsonAsync<Doctors>(_apiAddress + "/" + id);
        //    return View(model);
        //}

        // POST: DoctorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(int id)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAddress + "/" + id);
                TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}

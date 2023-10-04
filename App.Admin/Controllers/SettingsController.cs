using Microsoft.AspNetCore.Mvc;
using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace App.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAddress = "http://localhost:5005/api/Settings";
        public SettingsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: HomeController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Setting>>(_apiAddress);
            return View(model);
        }

        // GET: HomeController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Setting collection)
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
            return View(collection);
        }

        // GET: HomeController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var model = await _httpClient.GetFromJsonAsync<Setting>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Setting collection)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiAddress + "/" + id, collection);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController/Delete/5
        public async Task<ActionResult> Remove(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Setting>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(int id, Setting collection)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_apiAddress + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>Cannot delete the only setting available.</div>";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}


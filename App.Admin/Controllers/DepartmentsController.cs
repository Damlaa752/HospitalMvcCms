using App.Admin.Utils;
using Microsoft.AspNetCore.Mvc;
using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace App.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : Controller
    {
        private readonly HttpClient _httpClient;

        public DepartmentsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAddress = "http://localhost:5005/api/Departments";



        // GET: DepartmentsController
        public async Task<ActionResult> Index()
        {
            List<Department> model = await _httpClient.GetFromJsonAsync<List<Department>>(_apiAddress);
            return View(model);
        }

        // GET: DepartmentsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: DepartmentsController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: DepartmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Department collection, IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    collection.Image = await FileHelper.FileLoaderAsync(Image);

                }
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
            return View();
        }

        // GET: DepartmentsController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var model = await _httpClient.GetFromJsonAsync<Department>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: DepartmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Department collection, IFormFile? Image)
        {
            //try
            //{


            //}
            //catch(Exception e)
            //{
            //    ModelState.AddModelError("", "Hata Oluştu : " + e.Message);
            //}

            //    return View();

            if (Image != null)
            {
                collection.Image = await FileHelper.FileLoaderAsync(Image);
            }

            var response = await _httpClient.PutAsJsonAsync(_apiAddress + "/" + id, collection);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: DepartmentsController/Delete/5
        public async Task<ActionResult> Remove(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Department>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: DepartmentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(int id, Department collection)
        {
            try
            {
                //FileHelper.FileRemover(collection.);
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

using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DepartmentPostsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAddress = "http://localhost:5005/api/DepartmentsPosts";
        private readonly string _apiDepartments = "http://localhost:5005/api/Departments";
        private readonly string _apiPosts = "http://localhost:5005/api/Posts";
        public DepartmentPostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: DepartmentPostsController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<DepartmentPost>>(_apiAddress);
            return View(model);
        }

        //GET: DepartmentPostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentPostsController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            ViewBag.PostId = new SelectList(await _httpClient.GetFromJsonAsync<List<Post>>(_apiPosts), "Id", "Title");
            return View();
        }

        // POST: DepartmentPostsController/Createz
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(DepartmentPost collection)
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
            ViewBag.PostId = new SelectList(await _httpClient.GetFromJsonAsync<List<Post>>(_apiPosts), "Id", "Title");
            return View(collection); //selectlist
        }


        // GET: DepartmentPostsController/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            ViewBag.PostId = new SelectList(await _httpClient.GetFromJsonAsync<List<Post>>(_apiPosts), "Id", "Title");
            DepartmentPost model = await _httpClient.GetFromJsonAsync<DepartmentPost>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: DepartmentPostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DepartmentPost collection)
        {
            //try
            //{


            //}
            //catch(Exception e)
            //{
            //    ModelState.AddModelError("", "Hata Oluştu : " + e.Message);
            //}

            //    return View();

            var response = await _httpClient.PutAsJsonAsync(_apiAddress + "/" + id, collection);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DepartmentId = new SelectList(await _httpClient.GetFromJsonAsync<List<Department>>(_apiDepartments), "Id", "Name");
            ViewBag.PostId = new SelectList(await _httpClient.GetFromJsonAsync<List<Post>>(_apiPosts), "Id", "Title");
            return RedirectToAction(nameof(Index));
        }

        // GET: DepartmentPostsController/Delete/5
        public async Task<ActionResult> Remove(int id)
        {
            DepartmentPost model = await _httpClient.GetFromJsonAsync<DepartmentPost>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: DepartmentPostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(int id, DepartmentPost collection)
        {
            try
            {
                //FileHelper.FileRemover(collection.);
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Azure;

namespace App.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAddress = "http://localhost:5005/api/Posts";
        private readonly string _apiUsers = "http://localhost:5005/api/Users";
        public PostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: PostsController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAddress);
            return View(model);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostsController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiUsers), "Id", "Email");
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post collection, IFormFile? Image)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiAddress, collection);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiUsers), "Id", "Email");
            return View(collection);
        }

        // GET: PostsController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var model = await _httpClient.GetFromJsonAsync<Post>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Post collection)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiAddress + "/" + id, collection);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PostsController/Delete/5
        public async Task<ActionResult> Remove(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Post>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(int id, Post collection)
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
                    TempData["Message"] = "<div class='alert alert-danger'>Please delete the department post first.</div>";
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

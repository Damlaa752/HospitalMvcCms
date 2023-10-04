using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace App.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostCommentsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAddress = "http://localhost:5005/api/PostComments";
        private readonly string _apiUsers = "http://localhost:5005/api/Users";
        private readonly string _apiPosts = "http://localhost:5005/api/Posts";
        public PostCommentsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: PostCommentsController
        public async Task<ActionResult> Index()
        {
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiUsers), "Id", "FullName");
            ViewBag.PostId = new SelectList(await _httpClient.GetFromJsonAsync<List<Post>>(_apiPosts), "Id", "Title");
            var model = await _httpClient.GetFromJsonAsync<List<PostComment>>(_apiAddress);
            return View(model);
        }

        // GET: PostCommentsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: PostCommentsController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiUsers), "Id", "FullName");
            ViewBag.PostId = new SelectList(await _httpClient.GetFromJsonAsync<List<Post>>(_apiPosts), "Id", "Title");
            return View();
        }

        // POST: PostCommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostComment collection)
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
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiUsers), "Id", "FullName");
            ViewBag.PostId = new SelectList(await _httpClient.GetFromJsonAsync<List<Post>>(_apiPosts), "Id", "Title");
            return View(collection);
        }

        // GET: PostCommentsController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiUsers), "Id", "FullName");
            ViewBag.PostId = new SelectList(await _httpClient.GetFromJsonAsync<List<Post>>(_apiPosts), "Id", "Title");
            var model = await _httpClient.GetFromJsonAsync<PostComment>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: PostCommentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PostComment collection)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiAddress + "/" + id, collection);
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "<div class='alert alert-success'>The Job is Done Sir!</div>";
            }
            ViewBag.UserId = new SelectList(await _httpClient.GetFromJsonAsync<List<User>>(_apiUsers), "Id", "FullName");
            ViewBag.PostId = new SelectList(await _httpClient.GetFromJsonAsync<List<Post>>(_apiPosts), "Id", "Title");
            return RedirectToAction(nameof(Index));
        }

        // GET: PostCommentsController/Delete/5
        public async Task<ActionResult> Remove(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<PostComment>(_apiAddress + "/" + id);
            return View(model);
        }

        // POST: PostCommentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(int id, PostComment collection)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_apiAddress + "/" + id);
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

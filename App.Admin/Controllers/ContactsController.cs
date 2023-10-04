using App.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace App.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactsController : Controller
    {

        private readonly HttpClient _httpClient;

        private readonly string _apiAddress = "http://localhost:5005/api/Contacts";

        public ContactsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: ContactsController
        public async Task<ActionResult> Index()
        {
            List<Contact>? model = await _httpClient.GetFromJsonAsync<List<Contact>>(_apiAddress);
            return View(model);
        }



        // GET: ContactsController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ContactsController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: ContactsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContactsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

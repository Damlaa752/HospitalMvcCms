using App.Data.Entity;
using App.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly string _apiSettingAddress = "http://localhost:5005/api/Settings";
        private readonly string _apiAddress = "http://localhost:5005/api/Contacts";
        private readonly HttpClient _httpClient;

        public ContactController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _httpClient.GetFromJsonAsync<List<Setting>>(_apiSettingAddress);
            var settingDefault = settings?.FirstOrDefault(s => s.IsActive);

            ContactViewModel pageView = new()
            {
                Setting = settingDefault
                
            };

            return View(pageView);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Contact collection)
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
    }
}

using App.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Controllers
{
    public class AboutController : Controller
    {
		private readonly string _apiSettingAddress = "http://localhost:5005/api/Settings";
		private readonly HttpClient _httpClient;

		public AboutController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IActionResult> Index()
		{
			var settings = await _httpClient.GetFromJsonAsync<List<Setting>>(_apiSettingAddress);
			var model = settings?.FirstOrDefault(s => s.IsActive);
			return View(model);
		}
	}
}

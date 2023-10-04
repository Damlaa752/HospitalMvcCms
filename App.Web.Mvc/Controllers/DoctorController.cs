using App.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Controllers
{
	public class DoctorController : Controller
	{
		private readonly string _apiSettingAddress = "http://localhost:5005/api/Settings";
		private readonly HttpClient _httpClient;

		public DoctorController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IActionResult> Index()
		{
			var settings = await _httpClient.GetFromJsonAsync<List<Setting>>(_apiSettingAddress);
			var model = settings?.FirstOrDefault(s => s.IsActive);

			return View(model);
		}

		public async Task<IActionResult> Detail(int id)
		{
			var settings = await _httpClient.GetFromJsonAsync<List<Setting>>(_apiSettingAddress);
			var model = settings?.FirstOrDefault(s => s.IsActive);

			return View(model);
		}
	}
}

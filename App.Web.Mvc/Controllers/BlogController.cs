using App.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace App.Web.Mvc.Controllers
{
	public class BlogController : Controller
	{
		private readonly string _apiSettingAddress = "http://localhost:5005/api/Settings";
		private readonly HttpClient _httpClient;

		public BlogController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IActionResult> Index()
		{
			var settings = await _httpClient.GetFromJsonAsync<List<Setting>>(_apiSettingAddress);
			var model = settings?.FirstOrDefault(s => s.IsActive);

			return View(model);
		}
		public async Task<IActionResult> Post(int id)
		{
			var settings = await _httpClient.GetFromJsonAsync<List<Setting>>(_apiSettingAddress);
			var model = settings?.FirstOrDefault(s => s.IsActive);

			return View(model);
		}
	}
}

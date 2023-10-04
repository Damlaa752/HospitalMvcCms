using App.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAddress = "http://localhost:5005/api/Departments";
		private readonly string _apiSettingAddress = "http://localhost:5005/api/Settings";

		public DepartmentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(int id, int page)
        {
            //var model = await _httpClient.GetFromJsonAsync<List<Department>>(_apiAddress);

			var settings = await _httpClient.GetFromJsonAsync<List<Setting>>(_apiSettingAddress);
			var model = settings?.FirstOrDefault(s => s.IsActive);

			return View(model);
        }


        public async Task<IActionResult> Detail(int id, int page)
        {
            //var model = await _httpClient.GetFromJsonAsync<List<Department>>(_apiAddress + "/" + id);

			var settings = await _httpClient.GetFromJsonAsync<List<Setting>>(_apiSettingAddress);
			var model = settings?.FirstOrDefault(s => s.IsActive);
			return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            //var categories = await _dbContext.Categories.ToListAsync(); 

            return View(null);
        }


        //[HttpGet]
        //public async Task<IActionResult> ListWithService([FromQuery] string? searchKey = null)
        //{
        //          var sonuc = await _categoryService.GetCategories(searchKey);

        //	return View(sonuc);
        //}
    }
}

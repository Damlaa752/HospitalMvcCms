using App.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.ViewComponents
{
    public class Doctors : ViewComponent
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiAddress = "http://localhost:5005/api/Departments";
        public Doctors(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Department>>(_apiAddress);
            model.OrderBy(d => d.Name);
            return View(model);
        }

    }
}

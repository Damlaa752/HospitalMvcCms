using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Detail(int id)
        {
            return View();
        }
    }
}

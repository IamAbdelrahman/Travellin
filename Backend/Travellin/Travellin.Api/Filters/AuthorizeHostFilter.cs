using Microsoft.AspNetCore.Mvc;

namespace Travellin.Travellin.Api.Filters
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

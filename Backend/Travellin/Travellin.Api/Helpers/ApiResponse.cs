using Microsoft.AspNetCore.Mvc;

namespace Travellin.Travellin.Api.Helpers
{
    public class ApiResponse : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

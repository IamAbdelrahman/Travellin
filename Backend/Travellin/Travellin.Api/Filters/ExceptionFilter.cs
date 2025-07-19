using Microsoft.AspNetCore.Mvc;

namespace Travellin.Travellin.Api.Filters
{
    public class ExceptionFilter : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

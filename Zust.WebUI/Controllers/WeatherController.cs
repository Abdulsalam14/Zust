using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

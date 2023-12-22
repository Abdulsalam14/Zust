using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

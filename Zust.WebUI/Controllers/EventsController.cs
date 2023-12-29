using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

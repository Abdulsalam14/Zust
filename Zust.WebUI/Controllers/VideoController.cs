using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

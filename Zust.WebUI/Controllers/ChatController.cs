using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

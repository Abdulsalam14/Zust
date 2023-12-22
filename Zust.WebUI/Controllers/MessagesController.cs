using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

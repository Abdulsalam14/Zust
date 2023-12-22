using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    public class FriendsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

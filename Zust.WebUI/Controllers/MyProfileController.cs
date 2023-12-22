using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    public class MyProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

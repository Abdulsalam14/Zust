using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HelpAndSupport()
        {
            return View();
        }
    }
}

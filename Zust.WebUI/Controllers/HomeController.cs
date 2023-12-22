using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
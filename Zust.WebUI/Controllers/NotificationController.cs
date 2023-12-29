using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Controllers
{
    [Authorize]

    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

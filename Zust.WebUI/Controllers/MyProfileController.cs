using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities;
using Zust.WebUI.Models;

namespace Zust.WebUI.Controllers
{
    [Authorize]
    public class MyProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFriendService _friendService;
        private UserManager<AppUser> _userManager;


        public MyProfileController(IUserService userService, IFriendService friendService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _friendService = friendService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string id=null!)
        {
            AppUser user;
            if (id == null)
                 user = await _userManager.GetUserAsync(HttpContext.User);
            else 
                user = await _userService.GetById(id);
            var friends = await _friendService.GetFriendsByUserId(user.Id);
            if (friends != null)
            {
                user.Friends = friends;
            }
            var model = new ProfileViewModel
            {
                CurrentUser = user
            };
            return View(model);
        }

    }
}

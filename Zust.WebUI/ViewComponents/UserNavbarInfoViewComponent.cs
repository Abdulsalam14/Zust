using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Zust.Business.Abstract;
using Zust.Entities;
using Zust.WebUI.Models;

namespace Zust.WebUI.ViewComponents
{
    public class UserNavbarInfoViewComponent:ViewComponent
    {
        private UserManager<AppUser> _userManager;

        private readonly IUserService _userService;

        public UserNavbarInfoViewComponent(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new UserNavbarInfoViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserName= user.UserName
                
            };
            //var cu
            //var model = new CartSummaryViewModel
            //{
            //    Cart = _cartSessionService.GetCart()
            //};
            return View(model);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.Entities;
using Zust.WebUI.Models;

namespace Zust.WebUI.Controllers
{
    [Authorize]
    public class MyProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFriendService _friendService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private UserManager<AppUser> _userManager;
        private readonly IPostService _postService;


        public MyProfileController(IUserService userService, IFriendService friendService, UserManager<AppUser> userManager, IWebHostEnvironment hostingEnvironment = null, IPostService postService = null)
        {
            _userService = userService;
            _friendService = friendService;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            _postService = postService;
        }

        public async Task<IActionResult> Index(string id = null!)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (id == null)
                user = await _userManager.GetUserAsync(HttpContext.User);
            else
                user = await _userService.GetById(id);
            var friends = await _friendService.GetFriendsByUserId(user.Id);
            if (friends != null)
            {
                user.Friends = friends;
            }
            var posts=await _postService.GetPostsByUserId(user.Id);


            ViewBag.User = user;
            var model = new ProfileViewModel
            {
                CurrentUser = user,
                Posts = posts.OrderByDescending(p=>p.Time).ToList(),
            };
            return View(model);

        }

        

        [HttpPost]
        public async Task<IActionResult> ImagePost(IFormFile profileImage)
        {
            try
            {
                if (profileImage != null && profileImage.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "assets/images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + profileImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await profileImage.CopyToAsync(fileStream);
                    }

                    var user = await _userManager.GetUserAsync(User);

                    user.CoverImageUrl = "/assets/images/" + uniqueFileName;

                    await _userService.Update(user);

                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "File not provided." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> ProfileImagePost(IFormFile profileImage)
        {
            try
            {
                if (profileImage != null && profileImage.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "assets/images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + profileImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await profileImage.CopyToAsync(fileStream);
                    }

                    var user = await _userManager.GetUserAsync(User);

                    user.ImageUrl = "/assets/images/" + uniqueFileName;

                    await _userService.Update(user);

                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "File not provided." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}

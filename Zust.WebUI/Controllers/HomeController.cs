using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities;
using Zust.WebUI.Models;

namespace Zust.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IFriendService _friendService;

        public HomeController(UserManager<AppUser> userManager, IWebHostEnvironment hostingEnvironment = null, IPostService postService = null, IUserService userService = null, IFriendService friendService = null)
        {
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            _postService = postService;
            _userService = userService;
            _friendService = friendService;
        }

        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var friends = await _friendService.GetFriendsByUserId(user.Id);

            var friendIds = friends.Select(f => f.YourFriendId).ToList();
            friendIds.Add(user.Id);

            var posts = await _postService.GetPosts(friendIds);


            ViewBag.User = user;
            var model = new ProfileViewModel
            {
                CurrentUser = user,
                Posts = posts.OrderByDescending(p => p.Time).ToList()
            };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Post(string message, IFormFile photo, IFormFile video)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var post = new Post();
            post.User = user;
            post.UserId = user.Id;
            post.Time = DateTime.Now;

            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    post.Content = message;
                }

                if (photo != null)
                {
                    var photoFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);
                    var photoFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "assets", "photos", photoFileName);

                    using (var stream = new FileStream(photoFilePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }

                    post.ImageUrl = Path.Combine("assets", "photos", photoFileName);
                }

                if (video != null)
                {
                    var videoFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(video.FileName);
                    var videoFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "\'assets", "videos", videoFileName);

                    using (var stream = new FileStream(videoFilePath, FileMode.Create))
                    {
                        video.CopyTo(stream);
                    }

                    post.VideoUrl = Path.Combine("assets", "videos", videoFileName);
                }

                await _postService.Add(post);
                return Ok("TAMAMLANDI");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Like(int postId)
        {
            
            var post = await _postService.Get(postId);
            var postOwner = await _userService.GetById(post.UserId);
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (post != null)
            {
                post.LikeCount++;

                await _postService.Update(post);

            }
            return Ok(new {postOwner=postOwner,user=user});

        }


    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zust.Business.Abstract;
using Zust.Entities;
using Zust.WebUI.Models;

namespace Zust.WebUI.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private readonly IUserService _userService;
        private UserManager<AppUser> _userManager;
        private readonly IFriendRequestService _friendRequestService;
        private readonly IFriendService _friendService;


        public FriendsController(IUserService userService, UserManager<AppUser> userManager, IFriendRequestService friendRequestService, IFriendService friendService)
        {
            _userService = userService;
            _userManager = userManager;
            _friendRequestService = friendRequestService;
            _friendService = friendService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> GetRequests()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var requestsIncludeUser = await _friendRequestService.GetRequestsWithSenderUser(currentUser.Id);
            return Ok(requestsIncludeUser);
        }


        public async Task<IActionResult> GetYouKnowUsers()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var requests = await _friendRequestService.GetRequestsByUserId(currentUser.Id);
            var users = await _userService.GetUsersYouKnow(currentUser.Id);
            var model = new List<UserRequestsViewModel>();
            foreach (var user in users)
            {
                var friend = await _friendService.GetMyFriend(currentUser.Id, user.Id);
                if (friend != null) continue;
                var request = requests.FirstOrDefault(rr => rr.ReceiverId == user.Id);
                int rid=0;
                if (request != null)
                {
                    user.HasRequestPending = true;
                    rid = request.Id;
                }
                var srequset = await _friendRequestService.GetRequestsByUserId(user.Id);
                var ss = srequset.FirstOrDefault(r => r.ReceiverId == currentUser.Id);
                if (ss != null)
                {
                    user.HasReceivedRequest = true;
                }
                var vm = new UserRequestsViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    HasRequestPending = user.HasRequestPending,
                    HasReceivedRequest = user.HasReceivedRequest,
                    RequestId = rid,
                    ReceiverId = currentUser.Id,
                    ImageUrl=user.ImageUrl

                };
                model.Add(vm);
            }

            return Ok(model);
        }



        public async Task<IActionResult> AddFriend(string id)
        {
            var sender = await _userManager.GetUserAsync(HttpContext.User);
            var receiverUser = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (receiverUser != null)
            {
                var request = new FriendRequest
                {
                    Content = $"{sender.UserName} send friend request at {DateTime.Now.ToLongDateString()}",
                    SenderId = sender.Id,
                    Sender = sender,
                    ReceiverId = id
                };
                await _friendRequestService.Add(request);
                await _userManager.UpdateAsync(receiverUser);
                return Ok();
            }
            return BadRequest();
        }


        public async Task<IActionResult> AcceptRequest(string userId, string senderId, int requestId)
        {
            var receiverUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var sender = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == senderId);

            if (receiverUser != null)
            {
                var receiverFriend = new Friend
                {
                    OwnId = receiverUser.Id,
                    YourFriendId = sender?.Id,
                };

                var senderFriend = new Friend
                {
                    OwnId = sender.Id,
                    YourFriendId = receiverUser.Id,
                };

                await _friendService.Add(receiverFriend);
                await _friendService.Add(senderFriend);

                await _friendRequestService.Delete(requestId);

                await _userManager.UpdateAsync(receiverUser);
                await _userManager.UpdateAsync(sender);
            }
            return Ok();
        }


        public async Task<IActionResult> RemoveFriend(string id)
        {
            try
            {
                var current = await _userManager.GetUserAsync(HttpContext.User);
                var friend1 = await _friendService.GetMyFriend(current.Id, id);
                var friend2 = await _friendService.GetMyFriend(id, current.Id);
                await _friendService.Delete(friend1.Id);
                await _friendService.Delete(friend2.Id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteRequest(int id,string userid)
        {
            try
            {
                var request = await _friendRequestService.GetFriendRequest(id);
                await _friendRequestService.Delete(id);
                return Ok(request);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetMyFriends()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = await _userService.GetById(currentUser.Id);
            var friends = await _friendService.GetFriendsByUserId(user.Id);
            if (friends != null)
            {
                user.Friends = friends;
            }
            return Ok(user);
        }
    }
}

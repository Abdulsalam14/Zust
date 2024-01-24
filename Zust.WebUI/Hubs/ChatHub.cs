using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Zust.Business.Abstract;
using Zust.Entities;

namespace Zust.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        private UserManager<AppUser> _userManager;
        private IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;

        private static List<string> usersandchat = new List<string>();
        public ChatHub(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {

            await Clients.Others.SendAsync("Connect");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            await Clients.Others.SendAsync("Disconnect");

        }

        public async Task GetMessages(string receiverId, string senderId, int chatId)
        {

            await Task.Delay(500);
            await Clients.User(receiverId).SendAsync("ReceiveMessages", receiverId, senderId);

        }
        private bool IsUserConnected(string userId, int chatId, string currentPageName)
        {
            return usersandchat.Contains(userId + chatId + currentPageName);
        }

    }
}

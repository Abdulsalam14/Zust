using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Zust.Business.Abstract;
using Zust.Entities;

namespace Zust.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        private UserManager<AppUser> _userManager;
        private IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;


        //private BackgroundQueueService _queueService;
        private static List<string> usersandchat = new List<string>();
        public ChatHub(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            usersandchat.Add(user.Id + user.LastChatId);


            await Clients.Others.SendAsync("Connect");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            usersandchat.Remove(user.Id + user.LastChatId);
            await Clients.Others.SendAsync("Disconnect");

        }


        public async Task GetMessages(string receiverId, string senderId, int chatId)
        {
            var user = await _userService.GetById(receiverId);

            //await Clients.User(senderId).SendAsync("ReceiveMessages", receiverId, senderId);
            await Task.Delay(500);
            if (user.LastChatId == chatId)
            {
                if (IsUserConnected(receiverId, chatId))
                {
                    await Clients.User(receiverId).SendAsync("ReceiveMessages", receiverId, senderId);
                }
                else
                {
                    await Clients.User(receiverId).SendAsync("ReceiveMessagesNull", receiverId, senderId);
                }
            }


        }
        private bool IsUserConnected(string userId, int chatId)
        {
            return usersandchat.Contains(userId + chatId);
        }

    }
}

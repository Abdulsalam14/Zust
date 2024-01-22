using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.Entities;

namespace Zust.WebUI.Hubs
{
    public class FriendRequestHub : Hub
    {
        private UserManager<AppUser> _userManager;
        private IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        //private BackgroundQueueService _queueService;


        public FriendRequestHub(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IUserService userService, IMessageService messageService)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _userService = userService;
            _messageService = messageService;
        }

        public async override Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            var userItem = await _userService.GetById(user.Id);
            userItem.IsOnline = true;

            await _userService.Update(user);
            await Task.Delay(1000);
            await Clients.Others.SendAsync("Connect");
        }


        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            var userItem = await _userService.GetById(user.Id);
            userItem.IsOnline = false;
            userItem.DisconnectTime = DateTime.Now;
            await _userService.Update(user);
            await Task.Delay(1000);
            await Clients.Others.SendAsync("Disconnect");
        }

        public async Task AddRequest(string id)
        {
            await Clients.Users(new String[] { id }).SendAsync("ReceiveRequest");
        }
        //public async Task GetFriends()
        //{
        //    await Clients.Users(new String[] {  }).SendAsync("Friends");
        //}

        public async Task AddNotification(string id)
        {
            await Task.Delay(500);
            await Clients.Users(new String[] { id }).SendAsync("ReceiveNotification");
        }

        public async Task GetHasntSeenMessages(string id)
        {
            if (id == "Current")
            {
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                await Clients.Users(new String[] { user.Id }).SendAsync("ReceiveHasntSeenMessages");

            }
            else
            {
                var user = await _userService.GetById(id);
                await Clients.Users(new String[] { user.Id }).SendAsync("ReceiveHasntSeenMessages");

            }
        }

        //public async Task GetMessages(string receiverId, string senderId, int chatId)
        //{

        //    var user = await _userService.GetById(receiverId);
        //    await Task.Delay(500);
        //    if (user.LastChatId == chatId)
        //    {
        //        await Clients.Users(new String[] { receiverId, senderId }).SendAsync("ReceiveMessages", receiverId, senderId);

        //    }
        //    else
        //    {
        //        await Clients.Users(new String[] { senderId }).SendAsync("ReceiveMessages", receiverId, senderId);
        //    }
        //}

        //public async Task GetMessages(string receiverId, string senderId, int chatId)
        //{
        //    //var controllerName = _contextAccessor.HttpContext.Request.RouteValues["controller"] as string;
        //    //var path = _contextAccessor.HttpContext.Request.Path.ToString();

        //    //Debug.WriteLine(path);
        //    var user = await _userService.GetById(receiverId);
        //    //var user2 = await _userService.GetById(senderId);
        //    await Task.Delay(500);
        //    await Clients.Users(new String[] { senderId }).SendAsync("ReceiveMessages", receiverId, senderId);
        //    if (user.LastChatId == chatId)
        //    {
        //        await Clients.Users(new String[] { receiverId }).SendAsync("ReceiveMessages", receiverId, senderId);
        //    }
        //}
    }
}

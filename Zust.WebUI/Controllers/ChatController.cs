using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities;

namespace Zust.WebUI.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private UserManager<AppUser> _userManager;
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IFriendService  _friendService;

        public ChatController(UserManager<AppUser> userManager, IMessageService messageService, IChatService chatService, IUserService userService, IFriendService friendService)
        {
            _userManager = userManager;
            _messageService = messageService;
            _chatService = chatService;
            _userService = userService;
            _friendService = friendService;
        }


        public async Task<IActionResult> Index(string id = null)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var onlines = await _friendService.GetOnlineFriends(user.Id);

            return View(onlines);
        }


        public async Task<IActionResult> SelectChat (string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var chat = await _chatService.GetByUsersId(user.Id, id);

            var receiver = await _userService.GetById(id);
            if (chat == null)
            {
                chat = new Chat
                {
                    Messages = new List<Message>(),
                    ReceiverId = id,
                    SenderId = user.Id,
                    //Receiver = receiver
                };

                await _chatService.Add(chat);
            }
            else
            {
                if (chat.ReceiverId == user.Id)
                {
                    chat.Receiver = receiver;
                    chat.ReceiverId = receiver.Id;
                    chat.SenderId = user.Id;
                }
            }

            return Ok(chat);
        }

    }
}
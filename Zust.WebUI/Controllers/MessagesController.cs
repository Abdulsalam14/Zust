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
    public class MessagesController : Controller
    {
        private UserManager<AppUser> _userManager;
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public MessagesController(UserManager<AppUser> userManager, IMessageService messageService, IChatService chatService, IUserService userService)
        {
            _userManager = userManager;
            _messageService = messageService;
            _chatService = chatService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(string id = null)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (id == null)
            {
                var lastMessage = await _messageService.GetUserLastMessage(user.Id);
                if (lastMessage == null)
                {
                    return View();
                }
                var _lastChat = await _chatService.Get(lastMessage.ChatId);
                var receiverr = await _userService.GetById(_lastChat.SenderId);

                if (_lastChat.ReceiverId == user.Id)
                {
                    _lastChat.Receiver = receiverr;
                    _lastChat.ReceiverId = receiverr.Id;
                    _lastChat.SenderId = user.Id;
                };
                var messagess = _lastChat.Messages;
                    var hasnotmessagess = messagess.Where(m => m.HasSeen == false && m.SenderId == receiverr.Id && m.ReceiverId == user.Id).ToList();
                    foreach (var message in hasnotmessagess)
                    {
                        message.HasSeen = true;
                        await _messageService.Update(message);
                    }

                
                return View(_lastChat);
            }



            var chat = await _chatService.GetByUsersId(user.Id, id);
            if (chat == null)
            {
                chat = new Chat
                {
                    Messages = new List<Message>(),
                    ReceiverId = id,
                    SenderId = user.Id,
                };

                await _chatService.Add(chat);
            }
            user.LastChatId = chat.Id;
            await _userService.Update(user);
            var receiver = await _userService.GetById(id);
            if (chat.ReceiverId == user.Id)
            {
                chat.Receiver = receiver;
                chat.ReceiverId = receiver.Id;
                chat.SenderId = user.Id;
            }
            var messages = chat.Messages;
            var hasnotmessages = messages.Where(m => m.HasSeen == false && m.SenderId == receiver.Id && m.ReceiverId == user.Id).ToList();

            foreach (var message in hasnotmessages)
            {
                message.HasSeen = true;
                await _messageService.Update(message);
            }

            return View(chat);
        }

        public async Task<IActionResult> GetHasntSeenMessages()
        {
            var viewchatlist = new List<ChatViewModel>();
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var chats = await _chatService.GetUserChats(user.Id);
                var count = 0;
                if (chats.Count == 0)
                {
                    return Ok(new { chats = viewchatlist.OrderByDescending(c => c.Time), count = count });
                }
                foreach (var chat in chats)
                {
                    Message message = new Message();
                    int itemhasntCount = 0;
                    if (chat.Receiver.Id == user.Id)
                    {
                        chat.Receiver = await _userService.GetById(chat.SenderId);
                    }
                    if (chat.Messages.Count > 0)
                    {
                        message = chat.Messages[chat.Messages.Count - 1];
                        if (message.SenderId != user.Id)
                        {
                            itemhasntCount = chat.Messages.Count(m => !m.HasSeen);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    count += itemhasntCount;
                    if (message != null)
                    {
                        var model = new ChatViewModel
                        {
                            Sender = chat.Receiver,
                            LastMessage = message.Content,
                            Time = message.DateTime,
                            HasntSeenCount = itemhasntCount
                        };
                        viewchatlist.Add(model);
                    }
                }


                return Ok(new { chats = viewchatlist.OrderByDescending(c => c.Time), count = count });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        public async Task<IActionResult> GetChat(string receiverId, string senderId)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var chat = await _chatService.GetByUsersId(senderId, receiverId);
            if (chat == null)
            {
                return BadRequest();
            }
            var messages = chat.Messages;
            var hasnotmessages = messages.Where(m => m.HasSeen == false && m.ReceiverId == user.Id).ToList();
            var receiver = await _userService.GetById(receiverId);
            if (receiver.LastChatId == chat.Id)
            {
                foreach (var message in hasnotmessages)
                {
                    message.HasSeen = true;
                    await _messageService.Update(message);
                }
            }

            bool islast = user.LastChatId == chat.Id;
            return Ok(new { Messages = messages, CurrentUserId = user.Id, islast = islast });

        }


        [HttpPost(Name = "AddMessage")]
        public async Task<IActionResult> AddMessage(MessageModel model)
        {
            try
            {
                var chat = await _chatService.GetByUsersId(model.SenderId, model.ReceiverId);
                if (chat != null)
                {
                    var sender = await _userService.GetById(model.SenderId);
                    sender.LastChatId = chat.Id;

                    await _userService.Update(sender);
                    var message = new Message
                    {
                        ChatId = chat.Id,
                        Content = model.Content,
                        DateTime = DateTime.Now,
                        HasSeen = false,
                        IsImage = false,
                        ReceiverId = model.ReceiverId,
                        SenderId = model.SenderId,
                    };
                    await _messageService.Add(message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}

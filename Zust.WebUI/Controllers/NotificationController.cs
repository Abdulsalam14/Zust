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

    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private UserManager<AppUser> _userManager;

        public NotificationController(INotificationService notificationService, UserManager<AppUser> userManager)
        {
            _notificationService = notificationService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNotifications()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var notifications = await _notificationService.GetNotificationsByUserId(currentUser.Id);
            return Ok(notifications);
        }



        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody]AddNotificationViewModel vm)
        {
            var sender = await _userManager.GetUserAsync(HttpContext.User);
            var receiverUser = _userManager.Users.FirstOrDefault(u => u.Id == vm.Id);
            if (receiverUser != null)
            {
                var notification = new Notification
                {
                    Description = vm.Content,
                    SenderId = sender.Id,
                    Sender = sender,
                    ReceiverId = vm.Id,
                    Time= DateTime.Now
                };
                await _notificationService.Add(notification);
                await _userManager.UpdateAsync(receiverUser);
                return Ok();
            }
            return Ok(vm);
        }



        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                await _notificationService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteAllNotifications()
        {
            var user= await _userManager.GetUserAsync(HttpContext.User);
            await _notificationService.DeleteAllNotificationByUserId(user.Id);
            return Ok();
        }

    }
}

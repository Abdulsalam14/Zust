using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.DataAccess.Concrete.EFEntityFramework;
using Zust.Entities;

namespace Zust.Business.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationDal _notificationDal;

        public NotificationService(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public async Task Add(Notification notification)
        {
            await _notificationDal.Add(notification);
        }

        public async Task Delete(int id)
        {
            var notification = await _notificationDal.Get(n => n.Id == id);
            await _notificationDal.Delete(notification);
        }

        public async Task DeleteAllNotificationByUserId(string userId)
        {
            await _notificationDal.DeleteAllNotifications(n => n.ReceiverId == userId);
        }

        public async Task<List<Notification>> GetAll()
        {
            return await _notificationDal.GetList();
        }

        public async Task<Notification> GetNotificationById(int id)
        {
            return await _notificationDal.Get(n => n.Id == id);
        }

        public async Task<List<Notification>> GetNotificationsByUserId(string userid)
        {
            var notifications = await _notificationDal.GetList(r => r.ReceiverId == userid, r => r.Include(fr => fr.Sender));
            return notifications.OrderByDescending(n => n.Time).ToList();
        }

        public async Task Update(Notification notification)
        {
            await _notificationDal.Update(notification);
        }
    }
}

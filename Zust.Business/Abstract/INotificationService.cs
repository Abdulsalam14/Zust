using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities;

namespace Zust.Business.Abstract
{
    public interface INotificationService
    {
        Task<List<Notification>> GetAll();
        Task Add(Notification notification);
        Task Update(Notification notification);
        Task Delete(int id);
        Task<List<Notification>> GetNotificationsByUserId(string userid);
        Task<Notification> GetNotificationById(int id);

        Task DeleteAllNotificationByUserId(string userId);
        //public Task<Friend> GetMyFriend(string userId, string friendId);
    }
}

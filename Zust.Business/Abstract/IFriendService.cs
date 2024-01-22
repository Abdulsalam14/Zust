using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities;

namespace Zust.Business.Abstract
{
    public interface IFriendService
    {
        Task<List<Friend>> GetAll();
        Task Add(Friend friend);
        Task Update(Friend friend);
        Task Delete(int id);
        Task<List<Friend>> GetFriendsByUserId(string userid);
        Task<Friend> GetMyFriend(string userId, string friendId);
        Task<List<Friend>> GetOnlineFriends(string userid);
    }
}

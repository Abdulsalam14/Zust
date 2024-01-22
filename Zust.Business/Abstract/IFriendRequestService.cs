using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities;

namespace Zust.Business.Abstract
{
    public interface IFriendRequestService
    {
        Task<List<FriendRequest>> GetAll();
        Task Add(FriendRequest friendRequest);
        Task Update(FriendRequest friendRequest);
        Task Delete(int id);
        Task<List<FriendRequest>> GetRequestsWithSenderUser(string userId);
        Task<List<FriendRequest>> GetRequestsByUserId(string userid);
        Task<FriendRequest> GetFriendRequest(int id);
    }
}

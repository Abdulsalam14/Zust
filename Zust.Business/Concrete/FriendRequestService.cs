using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities;

namespace Zust.Business.Concrete
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IFriendRequestDal _friendRequestDal;

        public FriendRequestService(IFriendRequestDal friendRequestDal)
        {
            _friendRequestDal = friendRequestDal;
        }

        public async Task Add(FriendRequest friendRequest)
        {
            await _friendRequestDal.Add(friendRequest);
        }

        public async Task Delete(int id)
        {
            var request= await _friendRequestDal.Get(r=>r.Id==id);
            await _friendRequestDal.Delete(request);
        }

        public async Task<List<FriendRequest>> GetAll()
        {
            return await _friendRequestDal.GetList();
        }

        public async Task<List<FriendRequest>> GetRequestsWithSenderUser(string userid)
        {
            return await _friendRequestDal.GetList(r => r.ReceiverId == userid, r=>r.Include(fr=>fr.Sender));
        }

        public async Task<List<FriendRequest>> GetRequestsByUserId(string userid)
        {
            return await _friendRequestDal.GetList(r => r.SenderId==userid);
        }


        public async Task Update(FriendRequest friendRequest)
        {
            await _friendRequestDal.Update(friendRequest);
        }

        public async Task<FriendRequest> GetFriendRequest(int id)
        {
            return await _friendRequestDal.Get(r => r.Id == id);
        }
    }
}

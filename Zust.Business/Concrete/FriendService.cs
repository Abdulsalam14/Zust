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
    public class FriendService : IFriendService
    {
        private readonly IFriendDal _friendDal;

        public FriendService(IFriendDal friendDal)
        {
            _friendDal = friendDal;
        }

        public async Task Add(Friend friend)
        {
           await _friendDal.Add(friend);
        }

        public async Task Delete(int id)
        {
            var deletedFriend =await _friendDal.Get(f => f.Id == id);
            await _friendDal.Delete(deletedFriend);
        }

        public async Task<Friend> GetMyFriend(string userId,string friendId)
        {

            return await _friendDal.Get(f=>f.OwnId==userId&&f.YourFriendId==friendId);
        }

        public async Task<List<Friend>> GetAll()
        {
            return await _friendDal.GetList();
        }

        public async Task<List<Friend>> GetMyFriends(string id)
        {
            return await _friendDal.GetList(f=>f.OwnId==id);
        }

        public async Task<List<Friend>> GetFriendsByUserId(string userid)
        {
            var friends=await _friendDal.GetList(f=>f.OwnId== userid,fr=>fr.Include(friend=>friend.YourFriend));
            return friends;
        }

        public async Task<List<Friend>> GetOnlineFriends(string userid)
        {
            var friends = await _friendDal.GetList(f => f.OwnId == userid, fr => fr.Include(friend => friend.YourFriend));
            var onlineFriends = friends.Where(f => f.YourFriend.IsOnline==true).ToList();
            return onlineFriends;
        }

        public async Task Update(Friend friend)
        {
            await _friendDal.Update(friend);
        }

        
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.Core.Abstraction;
using Zust.DataAccess.Abstract;
using Zust.Entities;

namespace Zust.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task Add(AppUser user)
        {
            await _userDal.Add(user);
        }

        public async Task Delete(string id)
        {
            var user = await _userDal.Get(u => u.Id == id);
            await _userDal.Delete(user);
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await _userDal.GetList();
        }

        public async Task<AppUser> GetById(string id)
        {
            return await _userDal.Get(u => u.Id == id);
        }

        public async Task<AppUser> GetByIdIncludeFriends(string id)
        {
            return await _userDal.GetUserIncludeFriends(u => u.Id == id);
        }

        public async Task<AppUser> GetByUserName(string username)
        {
            return await _userDal.Get(u => u.UserName == username);
        }

        //public async Task<List<AppUser>> GetOnlineUsers(string id)
        //{
        //    return await _userDal.GetList(u=>u.Id==id.);
        //}

        public async Task<List<AppUser>> GetUsersYouKnow(string id)
        {

            var users = await _userDal.GetList(u => u.Id != id);
            return users;
        }

        public async Task Update(AppUser user)
        {
            await _userDal.Update(user);
        }

    }
}

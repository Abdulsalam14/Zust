using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities;

namespace Zust.Business.Abstract
{
    public interface IChatService
    {
        Task<List<Chat>> GetAll();
        Task Add(Chat chat);
        Task Update(Chat chat);
        Task Delete(int id);
        Task<Chat> GetByUsersId(string currentUserId, string userId);
        Task<Chat> Get(int id);
        Task<List<Chat>> GetUserChats(string userId);
    }
}

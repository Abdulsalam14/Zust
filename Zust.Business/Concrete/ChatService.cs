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
    public class ChatService : IChatService
    {
        private readonly IChatDal _chatDal;

        public ChatService(IChatDal chatDal)
        {
            _chatDal = chatDal;
        }

        public async Task Add(Chat chat)
        {
            await _chatDal.Add(chat);
        }

        public async Task Delete(int id)
        {
            var chat = await _chatDal.Get(c => c.Id == id);
            await _chatDal.Delete(chat);
        }


        public async Task<List<Chat>> GetAll()
        {
            return await _chatDal.GetList();
        }

        public async Task<Chat> GetByUsersId(string currentUserId, string userId)
        {
            return await _chatDal.Get(c => c.SenderId == currentUserId && c.ReceiverId == userId
             || c.SenderId == userId && c.ReceiverId == currentUserId, c => c.Include(ch => ch.Receiver).
             Include(c => c.Messages.OrderBy(m => m.DateTime)));
        }

        public async Task<List<Chat>> GetUserChats(string userId)
        {

            return await _chatDal.GetList(c => c.SenderId == userId || c.ReceiverId == userId
            , c => c.Include(ch => ch.Receiver).Include(c => c.Messages.OrderBy(m => m.DateTime)));
        }
        public async Task<Chat> Get(int id)
        {
            return await _chatDal.Get(c => c.Id == id, c => c.Include(ch => ch.Messages.OrderBy(m => m.DateTime)));
        }

        public async Task Update(Chat chat)
        {
            await _chatDal.Update(chat);
        }

    }

}

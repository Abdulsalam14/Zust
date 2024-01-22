using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities;

namespace Zust.Business.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageService(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public async Task Add(Message message)
        {
           await _messageDal.Add(message);
        }

        public async Task Delete(int id)
        {
            var message = await _messageDal.Get(m=>m.Id==id);
            await _messageDal.Delete(message);
        }

        public async Task<List<Message>> GetAll()
        {
            return await _messageDal.GetList();
        }

        public async Task Update(Message message)
        {
            await _messageDal.Update(message);
        }
        public async Task<Message> GetUserLastMessage(string userId)
        {
           return await _messageDal.GetLastMessage(userId);
        }

        public async Task<List<Message>> GetHasntSeenMessages(string userId)
        {
            return await _messageDal.GetList(m=>m.ReceiverId==userId,m=>m.Include(msg=>msg.Chat).Include(c=>c.SenderId));
        }

        //public Task<List<Message>> GetMessagesByChatId(int chatid)
        //{
        //    _messageDal.ge
        //}
    }
}

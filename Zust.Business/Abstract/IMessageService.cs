using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities;

namespace Zust.Business.Abstract
{
    public interface IMessageService
    {
        Task<List<Message>> GetAll();
        Task Add(Message message);
        Task Update(Message message);
        Task Delete(int id);
        Task<Message> GetUserLastMessage(string userId);
        Task<List<Message>> GetHasntSeenMessages(string userId);
        //Task<List<Message>> GetMessagesByChatId(int chatid);
    }
}

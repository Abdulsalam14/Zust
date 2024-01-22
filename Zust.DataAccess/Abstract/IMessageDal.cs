using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.DataAccess;
using Zust.Entities;

namespace Zust.DataAccess.Abstract
{
    public interface IMessageDal:IEntityRepository<Message>
    {
        public Task<Message> GetLastMessage(string userid);
    }
}

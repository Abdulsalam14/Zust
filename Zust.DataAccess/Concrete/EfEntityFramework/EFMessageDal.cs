using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.DataAccess.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities;

namespace Zust.DataAccess.Concrete.EfEntityFramework
{
    public class EFMessageDal:EFEntityFrameworkRepositoryBase<Message,AppDBContext>,IMessageDal
    {
        private readonly AppDBContext _context;
        public EFMessageDal(AppDBContext context):base(context)
        {
            _context = context;
        }
        public async Task<Message> GetLastMessage( string userid)

        {
            var msg=await _context.Messages.Where(m => m.SenderId == userid || m.ReceiverId == userid).OrderByDescending(m => m.DateTime).FirstOrDefaultAsync();
            return msg;
        }
    }
}

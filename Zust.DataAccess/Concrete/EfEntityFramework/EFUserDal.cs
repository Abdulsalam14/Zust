using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;
using Zust.Core.DataAccess.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities;

namespace Zust.DataAccess.Concrete.EFEntityFramework
{

    public class EFUserDal:EFEntityFrameworkRepositoryBase<AppUser,AppDBContext>,IUserDal
    {
        private readonly AppDBContext _context;
        public EFUserDal(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserIncludeFriends(Expression<Func<AppUser, bool>> filter = null)
        {
            IQueryable<AppUser> query = _context.Set<AppUser>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Include(u => u.Friends!);

            return await query.SingleOrDefaultAsync();
        }
    }
}

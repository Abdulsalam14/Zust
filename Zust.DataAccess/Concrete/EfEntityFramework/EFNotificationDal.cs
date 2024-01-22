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

namespace Zust.DataAccess.Concrete.EfEntityFramework
{
    public class EFNotificationDal : EFEntityFrameworkRepositoryBase<Notification, AppDBContext>, INotificationDal
    {
        private readonly AppDBContext _context;

        public EFNotificationDal(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteAllNotifications(Expression<Func<Notification, bool>> filter = null)
        {
            IQueryable<Notification> query = _context.Set<Notification>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            var notificationsToDelete = await query.ToListAsync();

            _context.Notifications.RemoveRange(notificationsToDelete);

            await _context.SaveChangesAsync();
        }
    }
}

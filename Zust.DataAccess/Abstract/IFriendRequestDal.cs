using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.DataAccess;
using Zust.Entities;

namespace Zust.DataAccess.Abstract
{
    public interface IFriendRequestDal : IEntityRepository<FriendRequest>
    {
    }
}

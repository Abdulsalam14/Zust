using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;
using Zust.Core.DataAccess.EnttyFramework;
using Zust.DataAccess.Abstract;
using Zust.Entities;

namespace Zust.DataAccess.Concrete.EfEnttyFramework
{

    public class EFUserDal:EFEntityFrameworkRepositoryBase<AppUser,AppDBContext>,IUserDal
    {
        public EFUserDal(AppDBContext context) : base(context)
        {
        }
    }
}

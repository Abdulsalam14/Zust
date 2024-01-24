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
    public class EFPostDal:EFEntityFrameworkRepositoryBase<Post,AppDBContext>,IPostDal
    {
        public EFPostDal(AppDBContext context):base(context) { }
    }
}

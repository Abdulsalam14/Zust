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
    public class EFCommentDal:EFEntityFrameworkRepositoryBase<Comment,AppDBContext>,ICommentDal
    {
        public EFCommentDal(AppDBContext context):base(context)
        {
            
        }
    }
}

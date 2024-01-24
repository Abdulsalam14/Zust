using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities;

namespace Zust.Business.Abstract
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAll();
        Task<List<Comment>> GetList(Expression<Func<Comment, bool>> filter);
        Task Add(Comment comment);
        Task Update(Comment comment);
        Task Delete(int id);
        Task<Comment> Get(int id);

    }
}

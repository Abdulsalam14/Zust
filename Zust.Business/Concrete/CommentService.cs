using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.DataAccess.Concrete.EfEntityFramework;
using Zust.Entities;

namespace Zust.Business.Concrete
{
    public class CommentService:ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentService(ICommentDal commentDal)
        {
            _commentDal = _commentDal;
        }

        public async Task Add(Comment post)
        {
            await _commentDal.Add(post);
        }

        public async Task Delete(int id)
        {
            var post = await _commentDal.Get(p => p.Id == id);
            await _commentDal.Delete(post);
        }

        public async Task<Comment> Get(int id)
        {
            return await _commentDal.Get(p => p.Id == id);
        }

        public async Task<List<Comment>> GetAll()
        {
            return await _commentDal.GetList();
        }

        public async Task<List<Comment>> GetList(Expression<Func<Comment, bool>> filter)
        {
            return await _commentDal.GetList(filter);
        }

        public Task Update(Comment comment)
        {
            return _commentDal.Update(comment);
        }
    }
}

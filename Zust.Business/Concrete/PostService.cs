using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities;

namespace Zust.Business.Concrete
{
    public class PostService : IPostService
    {
        private readonly IPostDal _postDal;

        public PostService(IPostDal postDal)
        {
            _postDal = postDal;
        }



        public async Task Add(Post post)
        {
            await _postDal.Add(post);
        }

        public async Task Delete(int id)
        {
            var post=await _postDal.Get(p=>p.Id==id);
            await _postDal.Delete(post);
        }

        public async Task<Post> Get(int id)
        {
            return await _postDal.Get(p=>p.Id == id);
        }

        public async Task<List<Post>> GetAll()
        {
            return await _postDal.GetList();
        }

        public async Task<List<Post>> GetList(Expression<Func<Post, bool>> filter)
        {
            return await _postDal.GetList(filter);
        }

        public async  Task<List<Post>> GetPosts(List<string> friendsId)
        {
            var posts = await _postDal.GetList(p => friendsId.Contains(p.UserId),p=>p.Include(ps=>ps.User));
            return posts;
        }

        public async Task<List<Post>> GetPostsByUserId(string userid)
        {
            var posts = await _postDal.GetList(p => p.UserId==userid, p => p.Include(ps => ps.User));
            return posts;

        }

        public Task Update(Post post)
        {
            return _postDal.Update(post);
        }
    }
}

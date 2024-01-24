using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;
using Zust.Entities;

namespace Zust.Business.Abstract
{
    public interface IPostService
    {
        Task<List<Post>> GetAll();
        Task<List<Post>> GetList(Expression<Func<Post, bool>> filter);
        Task<List<Post>> GetPosts(List<string> friendsId);
        Task<List<Post>> GetPostsByUserId(string userid);
        Task Add(Post post);
        Task Update(Post post);
        Task Delete(int id);
        Task<Post> Get(int id);

    }
}

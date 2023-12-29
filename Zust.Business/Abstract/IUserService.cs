using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;
using Zust.Entities;

namespace Zust.Business.Abstract
{
    public interface IUserService
    {
        Task<List<AppUser>> GetAll();
        Task Add(AppUser user);
        Task Update(AppUser user);
        Task Delete(string id);
        Task<AppUser> GetById(string id);
        Task<AppUser> GetByUserName(string username);

    }
}

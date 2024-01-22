using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Zust.Core.DataAccess.EntityFramework
{
    public class EFEntityFrameworkRepositoryBase<TEntity, TContext>
           : IEntityRepository<TEntity>
           where TEntity : class, IEntity, new()
           where TContext : DbContext
    {
        private readonly TContext _context;

        public EFEntityFrameworkRepositoryBase(TContext context)
        {
            _context = context;
        }
        public async Task Add(TEntity entity)
        {
            //using (var context = _context)
            //{
            //}
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            //using (var context = _context)
            //{
            //}


            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {

            IQueryable<TEntity> query = _context.Set<TEntity>();


            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            //return await _context.Set<TEntity>().FirstOrDefaultAsync(query);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {


            IQueryable<TEntity> query =_context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            //using (var context = _context)
            //{
            //}
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

using Ecommerce.Constants;
using Ecommerce.Data;
using Ecommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.Services
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<TEntity>> GetAllAsync()
                        => await _context.Set<TEntity>().ToListAsync();


        public async Task<TEntity> GetByIdAsync(int id)
                        => await _context.Set<TEntity>().FindAsync(id);


        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> criteria)
                        => await _context.Set<TEntity>().SingleOrDefaultAsync(criteria);


        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> criteria,
                string[] includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);

            return await query.SingleOrDefaultAsync(criteria);
        }


        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria) =>
                            await _context.Set<TEntity>().Where(criteria).ToListAsync();


        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria,
                string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);
            return await query.Where(criteria).ToListAsync();
        }


        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria,
                int skip, int take) =>
                            await _context.Set<TEntity>().Where(criteria).Skip(skip).Take(take).ToListAsync();


        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria,
                int? skip, int? take, Expression<Func<TEntity, object>> orderBy = null,
                string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
                if (orderByDirection == OrderBy.Descending)
                    query = query.OrderByDescending(orderBy);
                else
                    query = query.OrderBy(orderBy);

            return await query.ToListAsync();

        }


        public async Task AddAsync(TEntity entity)
                => await _context.Set<TEntity>().AddAsync(entity);


        public async Task AddRangeAsync(List<TEntity> entities)
                => await _context.Set<TEntity>().AddRangeAsync(entities);


        public void Update(TEntity entity)
                => _context.Update(entity);

        public void UpdateRange(List<TEntity> entities)
                => _context.UpdateRange(entities);

        public void Delete(TEntity entity)
                => _context.Remove(entity);


        public void DeleteRange(List<TEntity> entities)
                => _context.RemoveRange(entities);

    }
}

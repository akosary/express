using Ecommerce.Constants;
using System.Linq.Expressions;

namespace Ecommerce.Services.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<List<TEntity>> GetAllAsync();
        public Task<TEntity> GetByIdAsync(int id);
        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> criteria);
        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> criteria, string[] includes = null);
        public Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria);
        public Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes);
        public Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, int skip, int take);
        public Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, int? skip, int? take,
                Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> entity);
    }
}

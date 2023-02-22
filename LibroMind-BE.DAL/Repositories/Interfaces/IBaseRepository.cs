using System.Linq.Expressions;

namespace LibroMind_BE.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity: class
    {
        Task<TEntity> FindByIdAsync(int id);

        Task<IEnumerable<TEntity>> FindAllAsync();

        Task<IEnumerable<TEntity>> FindAllPaginatedAsync(int pageSize, int pageNumber);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task<int> CountAsync();
    }
}
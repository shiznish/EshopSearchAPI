using Domain.Common;
using System.Linq.Expressions;

namespace Application.Core.Data.Repository;
public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    //Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, string? includeProperties = null);
    //Task<TEntity> GetAllAsync(string? includeProperties = null);
    //Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, string? includeProperties = null);
    //Task Add(TEntity entity);
    //Task AddRange(IEnumerable<TEntity> entities);
    //Task Remove(TEntity entity);

    //Task RemoveRange(IEnumerable<TEntity> entities);
    //Task Update(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    IEnumerable<TEntity?> Find(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    TEntity? Get(int id);
    IEnumerable<TEntity> GetAll();
    Task<IEnumerable<TEntity?>> GetAllAsync(CancellationToken cancellationToken = default);
    IQueryable<TEntity> GetQueryable();
    Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange<T>(IEnumerable<TEntity> entities);
}

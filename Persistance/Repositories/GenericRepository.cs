using Application.Core.Data.Repository;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistance.DatabaseContext;
using System.Linq.Expressions;

namespace Persistance.Repositories;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    public GenericRepository(ApplicationDbContext dbcontext)
    {
        _context = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        _dbSet = dbcontext.Set<TEntity>();
    }
    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking().ToList();
    }
    public async Task<IEnumerable<TEntity?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }
    public IQueryable<TEntity> GetQueryable()
    {
        return _dbSet.AsNoTracking();
    }
    public IEnumerable<TEntity?> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate);
    }
    public TEntity? Get(int id)
    {
        return _dbSet.Find(id);
    }
    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    public void UpdateRange<T>(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public void Remove(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

}
using Application.Core.Data.Repository;
using Application.Core.Data.UnitOfWork;
using Domain.Common;
using Persistance.DatabaseContext;
using Persistance.Repositories;

namespace Persistance.UnitOfWrok;
public class UnitOfWork : IUnitOfWork
{
    private bool _disposed = false;

    private readonly ApplicationDbContext _dbcontext;
    public ICustomerRepository CustomerRepository { get; private set; }
    public IProductRepository ProductRepository { get; private set; }
    public IOrderRepository OrderRepository { get; private set; }
    public UnitOfWork(ApplicationDbContext dbcontext)
    {
        this._dbcontext = dbcontext;
        CustomerRepository = new CustomerRepository(dbcontext);
        ProductRepository = new ProductRepository(dbcontext);
        OrderRepository = new OrderRepository(dbcontext);
    }
    public IGenericRepository<T> GetRepository<T>()
        where T : BaseEntity
    {
        return new GenericRepository<T>(_dbcontext);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _dbcontext.Dispose();
        _disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbcontext.SaveChangesAsync(cancellationToken);
    }

}
